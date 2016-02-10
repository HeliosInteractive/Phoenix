<script type="text/javascript">
	var registered_systems = [];
	<?php foreach ($machines as $machine): ?>
	registered_systems.push({ "name":"<?php echo $machine->name; ?>", "public_key":"<?php echo $machine->public_key; ?>" });
	<?php endforeach; ?>
</script>
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
                <th><?= __('Name') ?></th>
				<th><?= __('Status') ?></th>
                <th><?= __('Public Key') ?></th>
                <th class="actions"><?= __('Actions') ?></th>
            </tr>
        </thead>
        <tbody>
            <?php foreach ($machines as $machine): ?>
            <tr>
                <td class="name"><?= h($machine->name) ?></td>
                <td class="status"></td>
                <td class="public_key"><?= h($machine->public_key) ?></td>
                <td class="actions">
                    <?= $this->Html->link(__('View'), ['action' => 'view', $machine->id]) ?>
                    <?= $this->Html->link(__('Edit'), ['action' => 'edit', $machine->id]) ?>
                    <?= $this->Form->postLink(__('Delete'), ['action' => 'delete', $machine->id], ['confirm' => __('Are you sure you want to delete # {0}?', $machine->id)]) ?>
                </td>
            </tr>
            <?php endforeach; ?>
        </tbody>
    </table>
</div>
