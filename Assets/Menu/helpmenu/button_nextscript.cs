using UnityEngine;
using System.Collections;

public class button_nextscript : MonoBehaviour {
	public Texture mouseover, mouseout;
	public GameObject menu1, menu2;
	
	void OnMouseDown(){
		Debug.Log ("Next button pressed");
		if(menu1.activeSelf == true){
			menu2.gameObject.SetActive(true);
			menu1.gameObject.SetActive (false);
		}
	}
	void OnMouseOver(){
		Debug.Log ("Next on");
		GetComponent<GUITexture>().texture = mouseover;
	}
	void OnMouseExit(){
		Debug.Log ("Next off");
		GetComponent<GUITexture>().texture = mouseout;
	}
}

