using UnityEngine;
using System.Collections;

public enum NPCBehaviour{
	None,
	Gather,
	Converse,
	HitPlayer
};

public class NPC : MonoBehaviour {
	public NPCBehaviour behaviour = NPCBehaviour.None;
	public float speed = 4f;

	public Vector3 initPos;
	public Vector3 toPos;
	public float behaviourLength;

	void Start () {
		initPos = transform.position;
		toPos = transform.position;
	}
	
	void Update () {
		if(behaviour == NPCBehaviour.None){
			MoveTowardsToPos(initPos);
			
			return;
		}

		behaviourLength -= Time.deltaTime;
		
		if(behaviourLength <= 0f){
			behaviour = NPCBehaviour.None;
		}

		MoveTowardsToPos(toPos);
	}

	void MoveTowardsToPos(Vector3 _toPos){
		Vector3 vel = rigidbody.velocity;
		Vector3 pos = transform.position;
		Vector3 diff = (_toPos - pos);

		if(diff.magnitude > 1f){
			Vector3 dir = diff.normalized;
			vel.x = dir.x*speed;
			vel.z = dir.z*speed;
		}else{
			vel.x = vel.z = 0f;
		}

		rigidbody.velocity = vel;
	}
}
