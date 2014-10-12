using UnityEngine;
using System.Collections;

public class PersistentGameState : MonoBehaviour {
	public PlayerInputMethod[] inputMethods = new PlayerInputMethod[2];
	// skin data

	public static PersistentGameState CreateDefault(){
		GameObject o = new GameObject("DefaultGameSettings", typeof(PersistentGameState));
		PersistentGameState pgs = o.GetComponent<PersistentGameState>();
		pgs.inputMethods[0] = PlayerInputMethod.KeyboardMouse;
		pgs.inputMethods[1] = PlayerInputMethod.Controller;

		return pgs;
	}
}
