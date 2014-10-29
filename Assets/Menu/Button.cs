using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {
	public string buttonName;

	public Sprite normal;
	public Sprite hover;

	private bool _selected;
	public bool Selected {
		get{
			return _selected;
		}

		set{
			if(value){
				OnMouseOver();
			}else{
				OnMouseExit();
			}
			_selected = value;
		}
	}

	SpriteRenderer spriteRenderer;

	void Awake(){
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
