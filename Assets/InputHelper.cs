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

	static float Sign(float x){
		if(x == 0f) return 0f;

		if(x > 0f) return 1f;
		return -1f;
	}

	static public Vector2 AnyDir(){
		Vector2 v = new Vector2();
		v.x = Input.GetAxis("J1X") + Input.GetAxis("J2X");
		v.y = Input.GetAxis("J1Y") + Input.GetAxis("J2Y");

		v.x = Sign(v.x);
		v.y = Sign(v.y);

		return v;
	}
}
