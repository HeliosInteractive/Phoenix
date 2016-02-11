<div class="machines form columns content">
    <?= $this->Form->create($machine) ?>
    <fieldset>
        <legend><?= __('Add Machine') ?></legend>
        <?php
            echo $this->Form->input('name');
            echo $this->Form->input('public_key');
        ?>
    </fieldset>
    <?= $this->Form->button(__('Submit')) ?>
    <?= $this->Form->end() ?>
</div>
