<nav class="large-3 medium-4 columns" id="actions-sidebar">
    <ul class="side-nav">
        <li class="heading"><?= __('Actions') ?></li>
        <li><?= $this->Html->link(__('New Machine'), ['action' => 'add']) ?></li>
    </ul>
</nav>
<div class="machines index large-9 medium-8 columns content">
    <h3><?= __('Machines') ?></h3>
    <table cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th><?= $this->Paginator->sort('id') ?></th>
                <th><?= $this->Paginator->sort('created') ?></th>
                <th><?= $this->Paginator->sort('modified') ?></th>
                <th><?= $this->Paginator->sort('name') ?></th>
                <th><?= $this->Paginator->sort('public_key') ?></th>
                <th><?= $this->Paginator->sort('is_authorized') ?></th>
                <th><?= $this->Paginator->sort('last_meta_received') ?></th>
                <th class="actions"><?= __('Actions') ?></th>
            </tr>
        </thead>
        <tbody>
            <?php foreach ($machines as $machine): ?>
            <tr>
                <td><?= $this->Number->format($machine->id) ?></td>
                <td><?= h($machine->created) ?></td>
                <td><?= h($machine->modified) ?></td>
                <td><?= h($machine->name) ?></td>
                <td><?= h($machine->public_key) ?></td>
                <td><?= h($machine->is_authorized) ?></td>
                <td><?= h($machine->last_meta_received) ?></td>
                <td class="actions">
                    <?= $this->Html->link(__('View'), ['action' => 'view', $machine->id]) ?>
                    <?= $this->Html->link(__('Edit'), ['action' => 'edit', $machine->id]) ?>
                    <?= $this->Form->postLink(__('Delete'), ['action' => 'delete', $machine->id], ['confirm' => __('Are you sure you want to delete # {0}?', $machine->id)]) ?>
                </td>
            </tr>
            <?php endforeach; ?>
        </tbody>
    </table>
    <div class="paginator">
        <ul class="pagination">
            <?= $this->Paginator->prev('< ' . __('previous')) ?>
            <?= $this->Paginator->numbers() ?>
            <?= $this->Paginator->next(__('next') . ' >') ?>
        </ul>
        <p><?= $this->Paginator->counter() ?></p>
    </div>
</div>
