using UnityEngine;
using System.Collections;

public class Sounds : MonoBehaviour {
	public static Sounds inst;

	public AudioClip drink;
	public AudioClip pee;
	public AudioClip flush;

	AudioSource audioSource;
	
	void Awake() {
		inst = this;
		audioSource = GetComponent< AudioSource >();
	}
	
	public void OnDrink() {
		audio.PlayOneShot( drink );
	}
	
	public void OnPee() {
		audioSource.PlayOneShot( pee );
	}
	
	public void OnFlush() {
		audioSource.Stop();
		audioSource.PlayOneShot( flush );
	}
}
