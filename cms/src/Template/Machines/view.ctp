<div class="machines view columns content">
    <h3><?= h($machine->name) ?></h3>
    <table class="vertical-table">
        <tr>
            <th><?= __('Name') ?></th>
            <td><?= h($machine->name) ?></td>
        </tr>
        <tr>
            <th><?= __('Public Key') ?></th>
            <td><?= h($machine->public_key) ?></td>
        </tr>
        <tr>
            <th><?= __('Id') ?></th>
            <td><?= $this->Number->format($machine->id) ?></td>
        </tr>
        <tr>
            <th><?= __('Created') ?></th>
            <td><?= h($machine->created) ?></td>
        </tr>
        <tr>
            <th><?= __('Modified') ?></th>
            <td><?= h($machine->modified) ?></td>
        </tr>
    </table>
</div>
