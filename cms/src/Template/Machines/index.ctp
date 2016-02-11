<script type="text/javascript">
	var registered_systems = [];
	<?php foreach ($machines as $machine): ?>
	registered_systems.push({ "name":"<?php echo $machine->name; ?>", "public_key":"<?php echo $machine->public_key; ?>" });
	<?php endforeach; ?>
</script>
<div class="machines index columns content">
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
                    <?= $this->Form->postLink(__('Delete'), ['action' => 'delete', $machine->id], ['confirm' => __('Are you sure you want to delete # {0}?', $machine->id)]) ?>
					<br />
					<a class="stop" onclick="Command('stop', '<?= $machine->name ?>')">Stop</a>
					<a class="start" onclick="Command('start', '<?= $machine->name ?>')">Start</a>
					<a class="report" onclick="Command('report', '<?= $machine->name ?>')">Report</a>
					<br />
					<a class="update" onclick="Command('update', '<?= $machine->name ?>')">Update</a>
					<a class="upgrade" onclick="Command('upgrade', '<?= $machine->name ?>')">Upgrade</a>
                </td>
            </tr>
            <?php endforeach; ?>
        </tbody>
    </table>
</div>
