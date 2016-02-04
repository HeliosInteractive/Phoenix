<?php
/**
 * CakePHP(tm) : Rapid Development Framework (http://cakephp.org)
 * Copyright (c) Cake Software Foundation, Inc. (http://cakefoundation.org)
 *
 * Licensed under The MIT License
 * For full copyright and license information, please see the LICENSE.txt
 * Redistributions of files must retain the above copyright notice.
 *
 * @copyright     Copyright (c) Cake Software Foundation, Inc. (http://cakefoundation.org)
 * @link          http://cakephp.org CakePHP(tm) Project
 * @since         0.1.0
 * @license       http://www.opensource.org/licenses/mit-license.php MIT License
 */
namespace Bake\View;

use Cake\Core\Configure;
use Cake\Core\ConventionsTrait;
use Cake\Core\InstanceConfigTrait;
use Cake\Event\EventManager;
use Cake\Network\Request;
use Cake\Network\Response;
use Cake\Utility\Inflector;
use Cake\View\View;

class BakeView extends View
{
    use ConventionsTrait;
    use InstanceConfigTrait;

    /**
     * Default class config
     *
     * This config is read when evaluating a template file.
     *
     * phpTagReplacements are applied to the contents of a bake template, to allow php tags
     * to be treated as plain text
     *
     * replacements are applied in order on the template contents before the template is evaluated
     * In order these:
     *     swallow leading whitespace for <%- tags
     *     swallow trailing whitespace for -%> tags
     *     Add an extra newline to <%=, to counteract php automatically removing a newline
     *     Replace remaining <=% with php short echo tags
     *     Replace <% with php open tags
     *     Replace %> with php close tags
     *
     * @var array
     */
    protected $_defaultConfig = [
        'phpTagReplacements' => [
            '<?' => "<CakePHPBakeOpenTag",
            '?>' => "CakePHPBakeCloseTag>"
        ],
        'replacements' => [
            '/\n[ \t]+<%- /' => "\n<% ",
            '/-%>[ \t]+\n/' => "%>\n",
            '/<%=(.*)\%>\n(.)/' => "<%=$1%>\n\n$2",
            '<%=' => '<?=',
            '<%' => '<?php',
            '%>' => '?>'
        ]
    ];

    /**
     * Path where bake's intermediary files are written.
     * Defaults to `TMP . 'bake' . DS`.
     *
     * @var string
     */
    protected $_tmpLocation;

    /**
     * Upon construction, append the plugin's template paths to the paths to check
     *
     * @param \Cake\Network\Request|null $request Request instance.
     * @param \Cake\Network\Response|null $response Response instance.
     * @param \Cake\Event\EventManager|null $eventManager Event manager instance.
     * @param array $viewOptions View options. See View::$_passedVars for list of
     *   options which get set as class properties.
     */
    public function __construct(
        Request $request = null,
        Response $response = null,
        EventManager $eventManager = null,
        array $viewOptions = []
    ) {
        parent::__construct($request, $response, $eventManager, $viewOptions);

        $bakeTemplates = dirname(dirname(__FILE__)) . DS . 'Template' . DS;
        $paths = (array)Configure::read('App.paths.templates');

        if (!in_array($bakeTemplates, $paths)) {
            $paths[] = $bakeTemplates;
            Configure::write('App.paths.templates', $paths);
        }

        $this->_tmpLocation = TMP . 'bake' . DS;
        if (!file_exists($this->_tmpLocation)) {
            mkdir($this->_tmpLocation);
        }
    }

    /**
     * Renders view for given view file and layout.
     *
     * Render triggers helper callbacks, which are fired before and after the view are rendered,
     * as well as before and after the layout. The helper callbacks are called:
     *
     * - `beforeRender`
     * - `afterRender`
     *
     * View names can point to plugin views/layouts. Using the `Plugin.view` syntax
     * a plugin view/layout can be used instead of the app ones. If the chosen plugin is not found
     * the view will be located along the regular view path cascade.
     *
     * View can also be a template string, rather than the name of a view file
     *
     * @param string|null $view Name of view file to use, or a template string to render
     * @param string|null $layout Layout to use. Not used, for consistency with other views only
     * @return string|null Rendered content.
     * @throws \Cake\Core\Exception\Exception If there is an error in the view.
     */
    public function render($view = null, $layout = null)
    {
        $viewFileName = $this->_getViewFileName($view);
        $templateEventName = str_replace(
            ['.ctp', DS],
            ['', '.'],
            explode('Template' . DS . 'Bake' . DS, $viewFileName)[1]
        );

        $this->_currentType = static::TYPE_VIEW;
        $this->dispatchEvent('View.beforeRender', [$viewFileName]);
        $this->dispatchEvent('View.beforeRender.' . $templateEventName, [$viewFileName]);
        $this->Blocks->set('content', $this->_render($viewFileName));
        $this->dispatchEvent('View.afterRender', [$viewFileName]);
        $this->dispatchEvent('View.afterRender.' . $templateEventName, [$viewFileName]);

        if ($layout === null) {
            $layout = $this->layout;
        }
        if ($layout && $this->autoLayout) {
            $this->Blocks->set('content', $this->renderLayout('', $layout));
        }

        return $this->Blocks->get('content');
    }

    /**
     * Wrapper for creating and dispatching events.
     *
     * Use the Bake prefix for bake related view events
     *
     * @param string $name Name of the event.
     * @param array|null $data Any value you wish to be transported with this event to
     * it can be read by listeners.
     *
     * @param object|null $subject The object that this event applies to
     * ($this by default).
     *
     * @return \Cake\Event\Event
     */
    public function dispatchEvent($name, $data = null, $subject = null)
    {
        $name = preg_replace('/^View\./', 'Bake.', $name);
        return parent::dispatchEvent($name, $data, $subject);
    }

    /**
     * Sandbox method to evaluate a template / view script in.
     *
     * @param string $viewFile Filename of the view
     * @param array $dataForView Data to include in rendered view.
     *    If empty the current View::$viewVars will be used.
     * @return string Rendered output
     */
    protected function _evaluate($viewFile, $dataForView)
    {
        $viewString = $this->_getViewFileContents($viewFile);

        $replacements = array_merge($this->config('phpTagReplacements') + $this->config('replacements'));

        foreach ($replacements as $find => $replace) {
            if ($this->_isRegex($find)) {
                $viewString = preg_replace($find, $replace, $viewString);
            } else {
                $viewString = str_replace($find, $replace, $viewString);
            }
        }

        $this->__viewFile = $this->_tmpLocation . Inflector::slug(preg_replace('@.*Template[/\\\\]@', '', $viewFile)) . '.php';
        file_put_contents($this->__viewFile, $viewString);

        unset($viewFile, $viewString, $replacements, $find, $replace);
        extract($dataForView);
        ob_start();

        include $this->__viewFile;

        $content = ob_get_clean();

        $unPhp = $this->config('phpTagReplacements');
        return str_replace(array_values($unPhp), array_keys($unPhp), $content);
    }

    /**
     * Get the contents of the template file
     *
     * @param string $filename A template filename
     * @return string Bake template to evaluate
     */
    protected function _getViewFileContents($filename)
    {
        return file_get_contents($filename);
    }

    /**
     * Return all possible paths to find view files in order
     *
     * @param string $plugin Optional plugin name to scan for view files.
     * @param bool $cached Set to false to force a refresh of view paths. Default true.
     * @return array paths
     */
    protected function _paths($plugin = null, $cached = true)
    {
        $paths = parent::_paths($plugin, false);
        foreach ($paths as &$path) {
            $path .= 'Bake' . DS;
        }
        return $paths;
    }

    /**
     * Check if a replacement pattern is a regex
     *
     * Use preg_match to detect invalid regexes
     *
     * @param string $maybeRegex a fixed string or a regex
     * @return bool
     */
    protected function _isRegex($maybeRegex)
    {
        // @codingStandardsIgnoreStart
        $isRegex = @preg_match($maybeRegex, '');
        // @codingStandardsIgnoreEnd

        return $isRegex !== false;
    }
}
