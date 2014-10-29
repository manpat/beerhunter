// By Tim Volp
// Updated 12/10/2014

using UnityEngine;
using System.Collections;

public class FreeLook : MonoBehaviour {
	public LayerMask layerMask;
	public PlayerInputMethod inputMethod;

	public Vector2 sensitivity;

	public Vector2 minimum;
	public Vector2 maximum;

	public float distance;
	public float radius;

	public Vector2 rotation;

	void Start() {
		//Screen.showCursor = false;
		Screen.lockCursor = true;

		// Make the rigid body not change rotation
		if ( rigidbody ) {
			rigidbody.freezeRotation = true;
		}
	}

	void Update() {
		float horizontal;
		float vertical;

		if ( inputMethod == PlayerInputMethod.KeyboardMouse ) {
			horizontal = Input.GetAxis( "Mouse X" );
			vertical = Input.GetAxis( "Mouse Y" );
		} else if ( inputMethod == PlayerInputMethod.Controller ) {
			horizontal = Input.GetAxis( "J1Z" );
			vertical = -Input.GetAxis( "J1W" );
		} else if ( inputMethod == PlayerInputMethod.Controller2 ) {
			horizontal = Input.GetAxis( "J2Z" );
			vertical = -Input.GetAxis( "J2W" );
		} else {
			return;
		}

		// Multiply the mouse axes by their sensitivity before clamping to their minimum and maximum extents.
		// Negative or zero sensitivity can be used to respectively invert an axis or leave it unchanged.
		if ( sensitivity.y != 0 ) {
			rotation.x = Mathf.Clamp( WrapEulerAngle( vertical * sensitivity.y + rotation.x ), minimum.y, maximum.y );
		} else {
			rotation.x = transform.localEulerAngles.x;
		}

		if ( sensitivity.x != 0 ) {
			rotation.y = Mathf.Clamp( WrapEulerAngle( horizontal * sensitivity.x + rotation.y ), minimum.x, maximum.x );
		} else {
			rotation.y = transform.localEulerAngles.y;
		}

		transform.localEulerAngles = new Vector3( rotation.x, rotation.y, transform.localEulerAngles.z );

		if ( distance != 0 ) {
			// Move the point of view the distance away from the target.
			transform.localPosition = transform.localRotation * Vector3.forward * distance;

			// When there is an obstruction detected at the point of view.
			//if ( Physics.CheckSphere( transform.position, radius ) ) {
				RaycastHit hit;

				// Detect the location of the obstruction between the target and the point of view.
				if ( Physics.SphereCast( transform.forward * -distance + transform.position, radius, -transform.forward, out hit, Mathf.Abs( distance ), layerMask ) ) {
					// Clear the point of view of the obstruction by moving it to the detected location.
					transform.localPosition = transform.localRotation * Vector3.forward * hit.distance * Mathf.Sign( distance );
				}
			//}
		}
	}

	float WrapEulerAngle( float angle ) {
		if ( angle <= -360 ) {
			angle += 360;
		}

		if ( angle >= 360 ) {
			angle -= 360;
		}

		return angle;
	}
}
