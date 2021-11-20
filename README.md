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

- To begin training, call `mlagents-learn` from your terminal:
- NVIDIA:

`$ mlagents-learn ./Assets/config/PlayGame.yaml --run-id=Test1 [--force --time-scale 1]`

- AMD:

`$ mlagents-learn ./Assets/config/PlayGame.yaml --run-id=Test1 --force --time-scale 1`

(The time-scale flag sets the training speed.  Use the force flag if re-using a run-id.)

- In the Unity Editor, click play to start the game.  If starting from the StartUp screen, hit the 'start' button.

- While training is running, you can view your progress in TensorBoard by using the `tensorboard` command:

`$ tensorboard --logdir results`

Navigate to `localhost:6006` to view your TensorBoard.

- View your NN results in your local `results` directory.

 
