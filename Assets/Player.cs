using UnityEngine;
using System.Collections;

public enum PlayerInputMethod{
	KeyboardMouse,
	Controller
};

public class Player : MonoBehaviour {
	public Camera lookCamera;
	public PlayerInputMethod inputMethod = PlayerInputMethod.KeyboardMouse;
	public float speed = 5f;
	public Vector2 sensitivity = new Vector2(-5f, 5f);

	Vector2 rot = new Vector2();

	void Start () {
	
	}
	
	void Update () {
		HandleMovement();
	}

	void HandleMovement() {
		Vector3 dir = Vector3.zero;
		if(inputMethod == PlayerInputMethod.KeyboardMouse){
			float dvz = Input.GetAxis("Vertical");
			float dvx = Input.GetAxis("Horizontal");

			float dry = Input.GetAxis("Mouse X") * sensitivity.y;
			float drx = Input.GetAxis("Mouse Y") * sensitivity.x;

			rot.x += drx;
			rot.y += dry;

			transform.rotation = Quaternion.Euler(rot.x, rot.y, 0);

			dir = transform.forward * dvz;
			dir += transform.right * dvx;
		}

		dir = dir*speed;
		dir.y = rigidbody.velocity.y;
		rigidbody.velocity = dir;
	}

	void HandleCamera(){

	}
}
