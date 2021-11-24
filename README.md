## Atari Breakout ML

<br/>

![Live WebGL MLBreakout game](Docs/Images/live-game-3.png)

<br/>

### To set-up your Python environment:

#### using pip

`$ pip install -r requirements.txt`

#### using Conda

`$ conda create --name <env_name> --file requirements.txt`

<br/>

### To Train the NN:

- Toggle the PaddleHuman game object's active status off.

- Toggle the PaddleAI game object's active status on.

- Activate your Python virtual environment if using.

- The Training scene includes multiple training environments.  More environments can easily be added by adding 'Environment' prefabs to the scene.

- The behavior parameters for all agents in a scene can be adjusted in the 'PaddleAI' prefab properties.  

- Note that when using multiple training environments, the objects in each Environment must be correctly linked.  Resetting the scripts for either the Environment prefab or the PaddleAI prefab will unlink the necessecary local objects in the Environment prefab.

- If objects in the Environment prefab need to be re-linked, link the local objects by opening the Environment prefab properties and linking the required obejcts.  This should link the correct local objects for all Environments. 

- The training environments are managed by the EnvironmentManager, which is similar to the GameManager, but customized to allow training on multiple enviroments within one scene. 

- A number of games is run in each training episode, kept track of in the 'gameCount' variable. 

- To begin training, call `mlagents-learn` from your terminal:

`$ mlagents-learn ./Assets/config/PlayGame.yaml --run-id=Test1 --force --time-scale 10`

  - The time-scale flag sets the training speed.  Training speed can be adjusted to speed-up training, but might impact the game physics.  

  - The force flag is required if re-using /over-writing a run-id.

- In the Unity Editor, click play to start the game.  If starting from the StartUp screen, hit the 'start' button.

- While training is running, you can view your progress in TensorBoard by using the `tensorboard` command:

`$ tensorboard --logdir results`

- Navigate to `localhost:6006` to view your TensorBoard.

- View your NN results in your local `results` directory.

 
