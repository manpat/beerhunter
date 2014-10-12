using UnityEngine;
using System.Collections;

public class Fridge : MonoBehaviour {
	[SerializeField] private bool _hasBeer = false;

	public bool hasBeer {
		get { return _hasBeer; }
		set {
			SetVisible(value);
			_hasBeer = value;
		}
	}

	void OnCollisionEnter(Collision col){
		col.gameObject.SendMessage("OnFridgeCollide", this, SendMessageOptions.DontRequireReceiver);
		hasBeer = false;
	}

	void SetVisible(bool v){
		if(v){
			gameObject.layer = LayerMask.NameToLayer("Fridge");
		}else{
			gameObject.layer = LayerMask.NameToLayer("Default");
		}
	}
}
