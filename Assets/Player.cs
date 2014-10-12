using UnityEngine;
using System.Collections;

public enum PlayerInputMethod{
	None,
	KeyboardMouse,
	Controller,
	Controller2,
};

public class Player : MonoBehaviour {
	public PlayerInputMethod inputMethod = PlayerInputMethod.KeyboardMouse;
	public Vector2 rotSensitivity = new Vector2(5f, 10f);
	public Vector2 rotClampX = new Vector2(0, 60f);
	public int playerNum = 0;
	public Camera lookCamera;
	public float cameraDist = 5f;
	
	public float speed = 5f;

	Vector2 rot = new Vector2();

	void Start () {
	
	}
	
	void Update () {
		HandleMovement();

		Debug.DrawLine(transform.position, transform.position + transform.forward * 10f);
	}

	void HandleMovement() {
		Vector3 dir = Vector3.zero;
		Vector3 pos = transform.position;

		Vector3 dv = Vector3.zero;
		Vector2 dr = Vector3.zero;

		if(inputMethod == PlayerInputMethod.KeyboardMouse){
			dv.x = Input.GetAxis("Horizontal");
			dv.z = Input.GetAxis("Vertical");

			dr.x = Input.GetAxis("Mouse Y") * -rotSensitivity.x;
			dr.y = Input.GetAxis("Mouse X") * rotSensitivity.y;
		}else if(inputMethod == PlayerInputMethod.Controller){
			dv.x = Input.GetAxis("J1X");
			dv.z = -Input.GetAxis("J1Y");

			dr.x = Input.GetAxis("J1Z") * rotSensitivity.x;
			dr.y = Input.GetAxis("J1W") * rotSensitivity.y;
		}else if(inputMethod == PlayerInputMethod.Controller2){
			dv.x = Input.GetAxis("J2X");
			dv.z = -Input.GetAxis("J2Y");

			dr.x = Input.GetAxis("J2Z") * rotSensitivity.x;
			dr.y = Input.GetAxis("J2W") * rotSensitivity.y;
		}

		rot.x = Mathf.Clamp(rot.x + dr.x, rotClampX.x, rotClampX.y);
		rot.y += dr.y;

		lookCamera.transform.rotation = Quaternion.Euler(rot.x, rot.y, 0);
		lookCamera.transform.position = pos - lookCamera.transform.forward * cameraDist;

		dir = transform.forward * dv.z;
		dir += transform.right * dv.x;

		dir = dir*speed;
		dir.y = rigidbody.velocity.y;
		rigidbody.velocity = dir;
	}
}
