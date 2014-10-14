using UnityEngine;
using System.Collections;

public enum MainMenuState {
	Start,
	Instructions,
	PlayerJoin,
	CharacterSelect,
};

public class MenuManager : MonoBehaviour {
	const int joystickButtonBegin = 350;

	MainMenuState state = MainMenuState.PlayerJoin;

	PersistentGameState pgs;
	int numPlayersDetected = 0;
	bool[] inputMethodsUsed = new bool[4]; // PlayerInputMethod

	public TextMesh[] pim;

	// Use this for initialization
	void Start () {
		GameObject o = new GameObject("GameSettings", typeof(PersistentGameState));
		DontDestroyOnLoad(o);
		pgs = o.GetComponent<PersistentGameState>();
	}
	
	// Update is called once per frame
	void Update () {
		switch(state){
			case MainMenuState.Start:{
				if(AnyButton()){
					state = MainMenuState.PlayerJoin;
				}
				break;
			}
			case MainMenuState.Instructions:{
				break;
			}
			case MainMenuState.PlayerJoin:{
				if(numPlayersDetected == 2) {
					StartGame();
					return;
				}

				PlayerInputMethod im = DetectInput();
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
			case MainMenuState.CharacterSelect:{
				break;
			}
		}
	}

	static public bool AnyButton(){
		if(Input.anyKeyDown) return true;

		for(int i = 0; i < 40; ++i){
			if(Input.GetKeyDown( (KeyCode)(joystickButtonBegin + i) )) return true;
		}

		return false;
	}

	public PlayerInputMethod DetectInput(){
		for(int i = 0; i < 20; ++i){
			if(Input.GetKeyDown( (KeyCode)(joystickButtonBegin + i) )) return PlayerInputMethod.Controller;
		}
		for(int i = 20; i < 40; ++i){
			if(Input.GetKeyDown( (KeyCode)(joystickButtonBegin + i) )) return PlayerInputMethod.Controller2;
		}

		if(Input.anyKeyDown) return PlayerInputMethod.KeyboardMouse;

		return PlayerInputMethod.None;
	}

	public void StartGame(){
		Application.LoadLevel("game");
	}
}
