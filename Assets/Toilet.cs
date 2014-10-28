using UnityEngine;
using System.Collections;

public class Toilet : MonoBehaviour {
	[SerializeField] public static float peeDrainPerSecond = 1f/5f;
	
	void OnTriggerEnter(Collider col){
		col.attachedRigidbody.gameObject.SendMessage("EnterToilet", SendMessageOptions.DontRequireReceiver);
	}
	
	void OnTriggerStay(Collider col){
		col.attachedRigidbody.gameObject.SendMessage("WhileInToilet", SendMessageOptions.DontRequireReceiver);
	}
	
	void OnTriggerExit(Collider col){
		col.attachedRigidbody.gameObject.SendMessage("ExitToilet", SendMessageOptions.DontRequireReceiver);
	}
}
