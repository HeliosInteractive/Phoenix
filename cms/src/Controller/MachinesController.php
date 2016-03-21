<?php
namespace App\Controller;

use App\Controller\AppController;
use Cake\Core\Configure;

/**
 * Machines Controller
 *
 * @property \App\Model\Table\MachinesTable $Machines
 */
class MachinesController extends AppController
{

	private function add_key($key)
	{
		$success = false;
		
		if (is_writable(Configure::read('Phoenix.KeyFile')))
		{
			$handle = fopen(Configure::read('Phoenix.KeyFile'), "r");
			$keys = [];
			if ($handle)
			{
				while (($line = fgets($handle)) !== false)
				{
					$keys[] = trim($line);
				}
				fclose($handle);
				
				if (!in_array($key, $keys))
				{
					if (!file_put_contents(Configure::read('Phoenix.KeyFile'), (PHP_EOL . trim($key) . PHP_EOL), FILE_APPEND))
					{
						$this->Flash->error(__('Appending key failed.'));
					}
					else
					{
						$this->Flash->success(__('Public Key added.'));
						$success = true;
					}
				}
				else
				{
					$this->Flash->error(__('Nothing to add.'));
					$success = true;
				}
			}
			else
			{
				$this->Flash->error(__('Key File could not be opened for reading.'));
			} 
		}
		else
		{
			$this->Flash->error(__('Key File is not writable.'));
		}
		
		return $success;
	}
	
	private function remove_key($key)
	{
		$success = false;
		
		if (is_writable(Configure::read('Phoenix.KeyFile')))
		{
			$handle = fopen(Configure::read('Phoenix.KeyFile'), "r");
			$keys = [];
			if ($handle)
			{
				while (($line = fgets($handle)) !== false)
				{
					$line = trim($line);
					if (!empty($line))
						$keys[] = trim($line);
				}
				fclose($handle);
				
				if(($index = array_search($key, $keys)) !== false)
				{
					unset($keys[$index]);
					
					if (file_put_contents(Configure::read('Phoenix.KeyFile'), implode(PHP_EOL, $keys)) === FALSE)
					{
						$this->Flash->error(__('Updating key file failed.'));
					}
					else
					{
						$this->Flash->success(__('Public Key removed.'));
						$success = true;
					}
				}
				else
				{
					$this->Flash->error(__('Nothing to remove.'));
					$success = true;
				}
			}
			else
			{
				$this->Flash->error(__('Key File could not be opened for reading.'));
			} 
		}
		else
		{
			$this->Flash->error(__('Key File is not writable.'));
		}
		
		return $success;
	}

    /**
     * Index method
     *
     * @return void
     */
    public function index()
    {
        $this->set('machines', $this->Machines->find('all'));
        $this->set('_serialize', ['machines']);
    }

    /**
     * View method
     *
     * @param string|null $id Machine id.
     * @return void
     * @throws \Cake\Network\Exception\NotFoundException When record not found.
     */
    public function view($id = null)
    {
        $machine = $this->Machines->get($id, [
            'contain' => []
        ]);
        $this->set('machine', $machine);
        $this->set('_serialize', ['machine']);
    }

    /**
     * Add method
     *
     * @return void Redirects on successful add, renders view otherwise.
     */
    public function add()
    {
        $machine = $this->Machines->newEntity();
        if ($this->request->is('post')) {
            $machine = $this->Machines->patchEntity($machine, $this->request->data);
			if ($this->add_key($machine['public_key']))
			{
				if ($this->Machines->save($machine))
				{
					$this->Flash->success(__('The machine has been saved.'));
				}
				else
				{
					$this->Flash->error(__('The machine could not be saved. Please, try again.'));
					$this->remove_key($machine['public_key']);
				}
			}
			else
			{
				$this->Flash->error(__('The machine could not be saved. Please, try again.'));
			}
			return $this->redirect(['action' => 'index']);
        }
        $this->set(compact('machine'));
        $this->set('_serialize', ['machine']);
    }

    /**
     * Delete method
     *
     * @param string|null $id Machine id.
     * @return \Cake\Network\Response|null Redirects to index.
     * @throws \Cake\Network\Exception\NotFoundException When record not found.
     */
    public function delete($id = null)
    {
        $this->request->allowMethod(['post', 'delete']);
        $machine = $this->Machines->get($id);
		if ($this->remove_key($machine['public_key']))
		{
			if ($this->Machines->delete($machine))
			{
				$this->Flash->success(__('The machine has been deleted.'));
			}
			else
			{
				$this->Flash->error(__('The machine could not be deleted. Please, try again 1.'));
				$this->add_key($machine['public_key']);
			}
		}
		else
		{
			$this->Flash->error(__('The machine could not be deleted. Please, try again 2.'));
		}
        return $this->redirect(['action' => 'index']);
    }
}
