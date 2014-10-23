using UnityEngine;
using System.Collections;

public enum MainMenuState {
	Start,
	Instructions,
	PlayerJoin,
	CharacterSelect,
};

public class MenuManager : MonoBehaviour {
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
				if(InputHelper.AnyButton()){
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
			case MainMenuState.CharacterSelect:{
				break;
			}
		}
	}

	public void StartGame(){
		Application.LoadLevel("game");
	}
}
