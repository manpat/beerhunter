using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {
	public string buttonName;

	public Sprite normal;
	public Sprite hover;

	SpriteRenderer spriteRenderer;

	void Start(){
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void OnMouseDown(){
		MenuManager.main.ButtonDown(buttonName);
	}
	void OnMouseOver(){
		spriteRenderer.sprite = hover;
	}
	void OnMouseExit(){
		spriteRenderer.sprite = normal;
	}
}
