using UnityEngine;
using System.Collections;

public class buttonHelp : MonoBehaviour {
	public Texture mouseover, mouseout;
	
	void OnMouseDown(){
		Debug.Log ("Help button pressed");
	}
	void OnMouseOver(){
		Debug.Log ("Help on");
		GetComponent<GUITexture>().texture = mouseover;
	}
	void OnMouseExit(){
		Debug.Log ("Help off");
		GetComponent<GUITexture>().texture = mouseout;
	}
}