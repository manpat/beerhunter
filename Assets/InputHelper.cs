using UnityEngine;
using System.Collections;

public class InputHelper : MonoBehaviour {
	static int joystickButtonBegin = 350;

	static public bool AnyButton(){
		if(Input.anyKeyDown) return true;

		for(int i = 0; i < 40; ++i){
			if(Input.GetKeyDown( (KeyCode)(joystickButtonBegin + i) )) return true;
		}

		return false;
	}

	static public PlayerInputMethod DetectInput(){
		for(int i = 0; i < 20; ++i){
			if(Input.GetKeyDown( (KeyCode)(joystickButtonBegin + i) )) return PlayerInputMethod.Controller;
		}
		for(int i = 20; i < 40; ++i){
			if(Input.GetKeyDown( (KeyCode)(joystickButtonBegin + i) )) return PlayerInputMethod.Controller2;
		}

		if(Input.anyKeyDown) return PlayerInputMethod.KeyboardMouse;

		return PlayerInputMethod.None;
	}
}
