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
					pgs.invertAxes[ 0 ] = 2;
					pgs.invertAxes[ 1 ] = 2;

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
				Vector2 axes;

				if ( pgs.invertAxes[ 0 ] == 2 ) { // Player 1
					pia[ 0 ].text = "Choose";

					axes = InputHelper.DetectAxes( pgs.inputMethods[ 0 ] );

					if ( axes.x == 1 ) {
						pgs.invertAxes[ 0 ] = 0;
					} else if ( axes.x == -1 ) {
						pgs.invertAxes[ 0 ] = 1; // Invert
					}
				} else if ( pgs.invertAxes[ 1 ] == 2 ) { // Player 2
					pia[ 0 ].text = pgs.invertAxes[ 0 ] == 0 ? "Standard" : "Inverted";
					pia[ 1 ].text = "Choose";

					axes = InputHelper.DetectAxes( pgs.inputMethods[ 1 ] );

					if ( axes.x == 1 ) {
						pgs.invertAxes[ 1 ] = 0;
					} else if ( axes.x == -1 ) {
						pgs.invertAxes[ 1 ] = 1; // Invert
					}
				} else {
					pia[ 1 ].text = pgs.invertAxes[ 1 ] == 0 ? "Standard" : "Inverted";
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
