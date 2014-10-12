using UnityEngine;
using System.Collections;

public enum PlayerInputMethod{
	None,
	KeyboardMouse,
	Controller,
	Controller2,
};

public class Player : MonoBehaviour {
	[SerializeField] private Vector2 rotSensitivity = new Vector2(5f, 10f);
	[SerializeField] private Vector2 rotClampX = new Vector2(0, 60f);
	//[SerializeField] private float cameraDist = 5f;
	[SerializeField] private Vector2 rot = new Vector2();
	[SerializeField] private float drunkWobbleAmt = 20f;
	public PlayerInputMethod inputMethod = PlayerInputMethod.KeyboardMouse;
	public int playerNum = 0;
	public Camera lookCamera;
	public Camera fridgeCamera;

	[SerializeField] private float speed = 5f;

	// Game-centric data

	[SerializeField] private const float baseDrunknessPerBeer = 1f/6f;
	[SerializeField] private const float basePeePerBeer = 1f/3f;

	public float drunkness = 0f; // [0, 1]
	public float pee = 0f; // [0, 1]

	float t = 0f;

	void Start () {

	}

	void Update () {
		t += Time.deltaTime;

		HandleMovement();
		DrunkWobble();

		Debug.DrawLine(transform.position, transform.position + transform.forward * 10f);
	}

	void HandleMovement() {
		Vector3 dir = Vector3.zero;
		Vector3 pos = transform.position;

		Vector3 dv = Vector3.zero;
		// Vector2 dr = Vector3.zero;

		if(inputMethod == PlayerInputMethod.KeyboardMouse){
			dv.x = Input.GetAxis("Horizontal");
			dv.z = Input.GetAxis("Vertical");

		}else if(inputMethod == PlayerInputMethod.Controller){
			dv.x = Input.GetAxis("J1X");
			dv.z = -Input.GetAxis("J1Y");

		}else if(inputMethod == PlayerInputMethod.Controller2){
			dv.x = Input.GetAxis("J2X");
			dv.z = -Input.GetAxis("J2Y");

		}

		dir = transform.forward * dv.z;
		dir += transform.right * dv.x;

		dir = dir*speed;
		dir.y = rigidbody.velocity.y;
		rigidbody.velocity = dir;
	}

	void OnFridgeCollide(Fridge fridge){
		if(fridge.hasBeer && pee >= 0.9f){
			drunkness += baseDrunknessPerBeer;
			pee += basePeePerBeer;
			fridge.hasBeer = false;
		}
	}

	void DrunkWobble() {
		float delta = ( t / 2 ) % ( 2 * Mathf.PI );
		float dizziness = drunkness * drunkWobbleAmt;

		lookCamera.transform.parent.transform.localEulerAngles = new Vector3(
			Mathf.Sin( delta ) * dizziness,
			Mathf.Sin( delta * 2 ) * dizziness,
			Mathf.Cos( delta ) * dizziness
		);
	}
}
