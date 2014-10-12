using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {
	public Transform drunkBar1;
	public Transform drunkBar2;

	public Transform peeBar1;
	public Transform peeBar2;

	public Transform drunkProgressBar1;
	public Transform drunkProgressBar2;

	public Transform peeProgressBar1;
	public Transform peeProgressBar2;

	float aspectRatio;

	void Update() {
		aspectRatio = ( float )Screen.width / Screen.height;

		drunkBar1.position = new Vector3( -aspectRatio, drunkBar1.position.y, drunkBar1.position.z );
		drunkBar2.position = new Vector3( aspectRatio, drunkBar2.position.y, drunkBar2.position.z );

		peeBar1.position = new Vector3( -aspectRatio, peeBar1.position.y, peeBar1.position.z );
		peeBar2.position = new Vector3( aspectRatio, peeBar2.position.y, peeBar2.position.z );

		if ( GameManager.main.players != null && GameManager.main.players.Length >= 2 ) {
			drunkProgressBar1.localScale = new Vector3( GameManager.main.players[ 0 ].drunkness, 1, 1 );
			drunkProgressBar2.localScale = new Vector3( GameManager.main.players[ 1 ].drunkness, 1, 1 );

			peeProgressBar1.localScale = new Vector3( GameManager.main.players[ 0 ].pee, 1, 1 );
			peeProgressBar2.localScale = new Vector3( GameManager.main.players[ 1 ].pee, 1, 1 );
		}
	}
}
