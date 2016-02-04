<nav class="large-3 medium-4 columns" id="actions-sidebar">
    <ul class="side-nav">
        <li class="heading"><?= __('Actions') ?></li>
        <li><?= $this->Html->link(__('Edit Machine'), ['action' => 'edit', $machine->id]) ?> </li>
        <li><?= $this->Form->postLink(__('Delete Machine'), ['action' => 'delete', $machine->id], ['confirm' => __('Are you sure you want to delete # {0}?', $machine->id)]) ?> </li>
        <li><?= $this->Html->link(__('List Machines'), ['action' => 'index']) ?> </li>
        <li><?= $this->Html->link(__('New Machine'), ['action' => 'add']) ?> </li>
    </ul>
</nav>
<div class="machines view large-9 medium-8 columns content">
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
            <th><?= __('Last Meta Received') ?></th>
            <td><?= h($machine->last_meta_received) ?></td>
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
        <tr>
            <th><?= __('Is Authorized') ?></th>
            <td><?= $machine->is_authorized ? __('Yes') : __('No'); ?></td>
         </tr>
    </table>
</div>
