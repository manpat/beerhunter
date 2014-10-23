using UnityEngine;
using System.Collections;

public enum MainMenuState {
	Start,
	Instructions,
	PlayerJoin,
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

	void Awake(){
		main = this;
	}

	// Use this for initialization
	void Start () {
		GameObject o = new GameObject("GameSettings", typeof(PersistentGameState));
		DontDestroyOnLoad(o);
		pgs = o.GetComponent<PersistentGameState>();
	}
	
	// Update is called once per frame
	void Update () {
		switch(state){
			case MainMenuState.Start:
				break;
			
			case MainMenuState.Instructions:
				break;
			
			case MainMenuState.PlayerJoin:{
				if(numPlayersDetected == 2) {
					state = MainMenuState.CharacterSelect;
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
		print(buttonName + " pressed");

		switch(buttonName){
			case "start":
				state = MainMenuState.PlayerJoin;
				break;
			case "instruct":
				state = MainMenuState.Instructions;
				break;
			case "quit":
				Application.Quit();
				break;

		}
	}
}
