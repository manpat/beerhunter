using UnityEngine;
using System.Collections;

public class buttonQuit : MonoBehaviour {
	public Texture mouseover, mouseout;
	
	void OnMouseDown(){
		Debug.Log ("Quit button pressed");
		Application.Quit ();
	}
	void OnMouseOver(){
		Debug.Log ("Quit on");
		GetComponent<GUITexture>().texture = mouseover;
	}
	void OnMouseExit(){
		Debug.Log ("Quit off");
		GetComponent<GUITexture>().texture = mouseout;
	}
}