using UnityEngine;
using System.Collections;

public class buton_retryscript : MonoBehaviour {
	public Texture mouseover, mouseout;

	void OnMouseDown(){
		Debug.Log ("Retry button pressed");
		Application.LoadLevel (1);
	}
	void OnMouseOver(){
		Debug.Log ("Retry on");
		GetComponent<GUITexture>().texture = mouseover;
	}
	void OnMouseExit(){
		Debug.Log ("Retry off");
		GetComponent<GUITexture>().texture = mouseout;
	}
}
