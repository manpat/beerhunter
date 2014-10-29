using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class EditModeCameraMove : MonoBehaviour {
	public MainMenuState state = MainMenuState.Start;
	public GameObject[] screens;

	void Update () {
		Camera.main.transform.position = screens[(int)state].transform.position;
	}
}
