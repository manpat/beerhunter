using UnityEngine;
using System.Collections;

public enum PlayerInputMethod{
	KeyboardMouse,
	Controller
};

public class PlayerMove : MonoBehaviour {
	public PlayerInputMethod inputMethod = PlayerInputMethod.KeyboardMouse;
	public float speed = 5f;
	public Vector2 sensitivity = new Vector2(-5f, 5f);

	Vector2 rot = new Vector2();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
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
}
