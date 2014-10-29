using UnityEngine;
using System.Collections;

public enum MainMenuState {
	Start,
	Instructions,
	PlayerJoin,
	InvertAxis,
	CharacterSelect,
};

public class MenuManager : MonoBehaviour {
	static public MenuManager main;

	[SerializeField] private MainMenuState state = MainMenuState.Start;
	public GameObject[] screens;

	PersistentGameState pgs;
	int numPlayersDetected = 0;
	bool[] inputMethodsUsed = new bool[4]; // PlayerInputMethod
	bool[] playerReady = new bool[2];
	float startgameCountdown = 1f;

	public TextMesh[] pim;
	public TextMesh[] pia;

	public Button[] screenButtons;
	private int buttonSelected = 0;

	float instructionTimeout = 0f;
	float inputTimeout = 0f;

	void Awake(){
		main = this;
	}

	// Use this for initialization
	void Start () {
		Screen.lockCursor = false;
		GameObject o = new GameObject("GameSettings", typeof(PersistentGameState));
		DontDestroyOnLoad(o);
		pgs = o.GetComponent<PersistentGameState>();

		foreach(Button b in screenButtons){
			b.Selected = false;
		}
	}

	// Update is called once per frame
	void Update () {
		switch(state){
			case MainMenuState.Start:{
				Vector2 j = InputHelper.AnyDir();
				inputTimeout -= Time.deltaTime;

				if(inputTimeout <= 0f && j.y != 0f){
					screenButtons[buttonSelected].Selected = false;
					buttonSelected = (buttonSelected + (int)j.y) % screenButtons.Length;
					if(buttonSelected < 0f) buttonSelected += screenButtons.Length;
					print(buttonSelected);
					screenButtons[buttonSelected].Selected = true;

					inputTimeout = 0.2f;
				}

				if(InputHelper.AnyButton()){
					ButtonDown(screenButtons[buttonSelected].buttonName);
				}

				break;
			}

			case MainMenuState.Instructions:
				instructionTimeout -= Time.deltaTime;

				if(instructionTimeout <= 0f && InputHelper.AnyButton()){
					state = MainMenuState.Start;
				}
				break;

			case MainMenuState.PlayerJoin:{
				if(numPlayersDetected == 2) {
					// state = MainMenuState.CharacterSelect;
					//StartGame();
					pgs.invertAxes[ 0 ] = ControlMode.None;
					pgs.invertAxes[ 1 ] = ControlMode.None;

					playerReady[0] = false;
					playerReady[1] = false;

					state = MainMenuState.InvertAxis;
					return;
				}

				PlayerInputMethod im = InputHelper.DetectInput();
				if(im == PlayerInputMethod.None) break;

				if(!inputMethodsUsed[(int)im]){
					inputMethodsUsed[(int)im] = true;
					pgs.inputMethods[numPlayersDetected] = im;
					pim[numPlayersDetected].text = im.ToString();
					numPlayersDetected++;
					print("PlayerJoin im: " + im.ToString());
				}

				break;
			}
			case MainMenuState.InvertAxis: {
				Vector2[] axes = new Vector2[2];
				axes[0] = InputHelper.DetectAxes(pgs.inputMethods[0]);
				axes[1] = InputHelper.DetectAxes(pgs.inputMethods[1]);

				for(int pid = 0; pid < 2; pid++){
					if(axes[pid].x != 0f){
						if(axes[pid].x > 0f){
							pgs.invertAxes[pid] = ControlMode.Inverted;
						}else{
							pgs.invertAxes[pid] = ControlMode.Standard;
						}
					}

					if(!playerReady[pid]){
						switch(pgs.invertAxes[pid]){
							case ControlMode.None:
								pia[pid].text = "Choose";
								break;
							case ControlMode.Standard:
								pia[pid].text = "Standard";
								break;
							case ControlMode.Inverted:
								pia[pid].text = "Inverted";
								break;
						}
					}else{
						pia[pid].text = "Ready";
					}

					if(InputHelper.DetectAny(pgs.inputMethods[pid]) && axes[pid].x == 0f && pgs.invertAxes[pid] != ControlMode.None){
						playerReady[pid] = true;
					}
				}

				if(playerReady[0] && playerReady[1]){
					startgameCountdown -= Time.deltaTime;

					if(startgameCountdown <= 0f)
						StartGame();
				}

				break;
			}
			case MainMenuState.CharacterSelect:
				StartGame();
				break;
		}

		Vector3 cpos = Camera.main.transform.position;
		Vector3 diff = screens[(int)state].transform.position - cpos;

		if(diff.magnitude > 0.01f)
			Camera.main.transform.position = cpos + diff * 0.1f;
	}

	public void StartGame(){
		Application.LoadLevel("game");
	}

	public void ButtonDown(string buttonName){
		print(buttonName);

		switch(buttonName){
			case "start":
				state = MainMenuState.PlayerJoin;
				break;
			case "instruct":
				state = MainMenuState.Instructions;
				instructionTimeout = 1f;
				break;
			case "back":
				state = MainMenuState.Start;
				break;
			case "quit":
				Application.Quit();
				break;
		}
	}
}
