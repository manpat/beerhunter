using UnityEngine;
using System.Collections;

public class buttonStart : MonoBehaviour {
	public Texture mouseover, mouseout;
	
	void OnMouseDown(){
		Debug.Log ("Start button pressed");
		Application.LoadLevel (1);
	}
	void OnMouseOver(){
		Debug.Log ("Start on");
		GetComponent<GUITexture>().texture = mouseover;
	}
	void OnMouseExit(){
		Debug.Log ("Start off");
		GetComponent<GUITexture>().texture = mouseout;
	}
}
