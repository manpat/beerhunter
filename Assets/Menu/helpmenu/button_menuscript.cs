using UnityEngine;
using System.Collections;

public class button_menuscript : MonoBehaviour {
	public Texture mouseover, mouseout;
	public GameObject menu1, menu2;
	
	void OnMouseDown(){
		Debug.Log ("Menu button pressed");
		if(menu1.activeSelf == true){
			menu2.gameObject.SetActive(true);
			menu1.gameObject.SetActive (false);
		}
	}
	void OnMouseOver(){
		Debug.Log ("Menu on");
		GetComponent<GUITexture>().texture = mouseover;
	}
	void OnMouseExit(){
		Debug.Log ("Menu off");
		GetComponent<GUITexture>().texture = mouseout;
	}
}
