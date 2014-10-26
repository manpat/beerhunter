using UnityEngine;
using System.Collections;

public enum PlayerInputMethod {
	None,
	KeyboardMouse,
	Controller,
	Controller2,
};

public class Player : MonoBehaviour {
	public PlayerInputMethod inputMethod = PlayerInputMethod.KeyboardMouse;
	public int playerNum = 0;
	public Camera lookCamera;
	public Camera fridgeCamera;
	public GameObject mesh;

	public FreeLook freeLook;

	[SerializeField] private float drunkWobbleAmt = 10f;
	[SerializeField] private float speed = 5f;

	// Game-centric data
	[SerializeField] private const float baseDrunknessPerBeer = 1f/6f;
	[SerializeField] private const float basePeePerBeer = 1f/3f;

	public float drunkness = 0f; // [0, 1]
	public float pee = 0f; // [0, 1]

	public float lookHintTimeout = 30f;

	float t = 0f;

	void Start(){
		freeLook = lookCamera.GetComponent<FreeLook>();
	}

	void Update () {
		t += Time.deltaTime;

		HandleMovement();
		DrunkWobble();
	}

	float timeWithoutLook = 0f;

	void HandleMovement() {
		Vector3 vel = rigidbody.velocity;
		Vector3 dir = Vector3.zero;
		Vector3 dv = Vector3.zero;

		Vector2 lookV = Vector2.zero;

		if(inputMethod == PlayerInputMethod.KeyboardMouse){
			dv.x = Input.GetAxis("Horizontal");
			dv.z = Input.GetAxis("Vertical");

			lookV.x = Input.GetAxis("Mouse X");
			lookV.y = Input.GetAxis("Mouse Y");

		}else if(inputMethod == PlayerInputMethod.Controller){
			dv.x = Input.GetAxis("J1X");
			dv.z = -Input.GetAxis("J1Y");

			lookV.x = Input.GetAxis("J1Z");
			lookV.y = Input.GetAxis("J1W");

		}else if(inputMethod == PlayerInputMethod.Controller2){
			dv.x = Input.GetAxis("J2X");
			dv.z = -Input.GetAxis("J2Y");

			lookV.x = Input.GetAxis("J2Z");
			lookV.y = Input.GetAxis("J2W");
		}

		if(lookV.magnitude < 1e-6){
			timeWithoutLook += Time.deltaTime;

			if(timeWithoutLook > lookHintTimeout){
				switch(inputMethod){
					case PlayerInputMethod.KeyboardMouse:
						GameManager.main.ShowPlayerMessage(playerNum, "look with mouse");
						break;

					case PlayerInputMethod.Controller:
					case PlayerInputMethod.Controller2:
						GameManager.main.ShowPlayerMessage(playerNum, "look with right stick");
						break;

					default:
					break;
				}
			}
		}else{
			timeWithoutLook = 0f;
		}

		float lookAng = -freeLook.rotation.y * Mathf.PI/180f;
		Vector3 forward = new Vector3(-Mathf.Sin(lookAng), 0f, Mathf.Cos(lookAng));
		Vector3 right = new Vector3(Mathf.Cos(lookAng), 0f, Mathf.Sin(lookAng));

		dir = forward * dv.z + right * dv.x;

		if(dir.magnitude > 0.01f){
			float velAng = Mathf.Atan2(dir.x, dir.z)*180f/Mathf.PI;
			mesh.transform.rotation = Quaternion.Euler(0f, velAng, 0f);
		}

		dir = dir*speed;
		dir.y = vel.y;
		rigidbody.velocity = dir;
	}

	void OnFridgeCollide(Fridge fridge){
		if(fridge.hasBeer){
			if(pee + basePeePerBeer <= 1.01f){
				drunkness += baseDrunknessPerBeer;
				pee += basePeePerBeer;

				drunkness = Mathf.Clamp01(drunkness);
				pee = Mathf.Clamp01(pee);

				GameManager.main.OnPlayerGetBeer();
				GameManager.main.ShowPlayerMessage(playerNum, "got beer");

				if(drunkness + baseDrunknessPerBeer >= 1f && !GameManager.main.win){
					GameManager.main.ShowPlayerMessage(1-playerNum, "yo' better hurry!");
				}
			}else{
				GameManager.main.ShowPlayerMessage(playerNum, "you need to pee");
			}
		}
	}

	void WhileInToilet(){
		pee = Mathf.Clamp01(pee - Toilet.peeDrainPerSecond * Time.deltaTime);
		GameManager.main.ShowPlayerMessage(playerNum, "aaaahhhhhh!");
	}

	void DrunkWobble() {
		float delta = ( t / 2 ) % ( 2 * Mathf.PI );
		float dizziness = drunkness * drunkWobbleAmt;

		lookCamera.transform.parent.transform.localEulerAngles =
			new Vector3(
				Mathf.Sin( delta ) * dizziness,
				Mathf.Sin( delta * 2 ) * dizziness,
				Mathf.Cos( delta ) * dizziness );
	}
}
