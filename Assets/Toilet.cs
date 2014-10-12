using UnityEngine;
using System.Collections;

public class Toilet : MonoBehaviour {
	[SerializeField] public static float peeDrainPerSecond = 1f/5f;

	void OnTriggerStay(Collider col){
		col.attachedRigidbody.gameObject.SendMessage("WhileInToilet", SendMessageOptions.DontRequireReceiver);
	}
}
