<div class="users columns content">
	<?= $this->Form->create() ?>
    <fieldset>
        <legend><?= __('Login') ?></legend>
		<?= $this->Form->input('email') ?>
		<?= $this->Form->input('password') ?>
		<?= $this->Form->button('Login') ?>
		<?= $this->Form->end() ?>
    </fieldset>
</div>
