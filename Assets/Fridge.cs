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

	void SetVisible(bool v){
		gameObject.GetComponent<MeshRenderer>().enabled = v;
	}
}
