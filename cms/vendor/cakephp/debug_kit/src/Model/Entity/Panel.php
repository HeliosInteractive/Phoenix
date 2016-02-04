<?php
/**
 * Copyright (c) Cake Software Foundation, Inc. (http://cakefoundation.org)
 *
 * Licensed under The MIT License
 * Redistributions of files must retain the above copyright notice.
 *
 * @copyright     Copyright (c) Cake Software Foundation, Inc. (http://cakefoundation.org)
 * @link          http://cakephp.org CakePHP(tm) Project
 * @license       http://www.opensource.org/licenses/mit-license.php MIT License
 */
namespace DebugKit\Model\Entity;

use Cake\ORM\Entity;

/**
 * Panel entity class.
 */
class Panel extends Entity
{

    /**
     * Some fields should not be in JSON/array exports.
     *
     * @var array
     */
    protected $_hidden = ['content'];

    /**
     * Read the stream contents.
     *
     * Over certain sizes PDO will return file handles.
     * For backwards compatibility and consistency we smooth over that difference here.
     *
     * @return string
     */
    protected function _getContent($content)
    {
        if (is_resource($content)) {
            return stream_get_contents($content);
        }
        return $content;
    }
}
