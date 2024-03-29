﻿using UnityEngine;
using System.Collections;

public class buttonHelp : MonoBehaviour {
	public Texture mouseover, mouseout;
	public GameObject menu1, menu2;
	void OnMouseDown(){
		Debug.Log ("Help button pressed");
		if(menu1.activeSelf == true){
			menu2.gameObject.SetActive(true);
			menu1.gameObject.SetActive (false);
		}
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