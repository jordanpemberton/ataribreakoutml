## ML Breakout

<br/>

![Human-vs-AI WebGL MLBreakout game](Docs/Images/human-vs-ai-game.png)

<br/>

### To set-up your Python environment:

#### using pip

`$ pip install -r requirements.txt`

#### using Conda

`$ conda create --name <env_name> --file requirements.txt`

<br/>

### Training 

![Training Video, MLBreakout game](Docs/Images/training-video.gif)

<br/>

### Training environments:

- The Training scene includes multiple training environments.  More environments can easily be added by adding 'Environment' prefabs to the scene.

- The behavior parameters and reward values for all agents in the Training scene can be adjusted in the 'Environment' prefab properties.  
 
- Updating the 'PaddleAI' prefab's properties will not update the Environment objects.  

- Values in the 'PaddleAgent' script will be overridden by values in the the game object's properties.

- When using multiple training environments, the objects in each Environment must be correctly linked.  Resetting the scripts for either the Environment prefab or the PaddleAI prefab will unlink the necessecary local objects in the Environment prefab.

- If objects in the Environment prefab need to be re-linked, link the local objects by opening the Environment prefab properties and linking the required obejcts.  Updating the Environment prefab should re-link the correct local objects for all Environments. 

- Each training environment is managed by an 'EnvironmentManager'. 

<br/>

### Training the neural network:

- To begin training, call `mlagents-learn` from your terminal:

`$ mlagents-learn ./Assets/config/PlayGame.yaml --run-id=Test1 --force --time-scale 10`

- The time-scale flag sets the training speed, and the force flag is required if re-using /over-writing a run-id.

- In the Unity Editor, open the Training scene and click play to start training.

- You can view your progress in TensorBoard by using the `tensorboard` command:

`$ tensorboard --logdir results`

- Navigate to `localhost:6006` to view your TensorBoard.

- Your NN model results will appear in your local `results` directory.


