using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	[SerializeField] private GameObject playerPrefab;

	public Transform[] spawnPoints;

	void Start () {
		SpawnPlayers();
	}
	
	void Update () {
	
	}

	void SpawnPlayers() {
		PersistentGameState pgs = FindObjectOfType<PersistentGameState>();

		if(!pgs){ // This will ONLY happen when testing in unity
			pgs = PersistentGameState.CreateDefault();
		}

		for(int i = 0; i < 2; i++){
			GameObject o = (GameObject)Instantiate(playerPrefab, spawnPoints[i].position, Quaternion.identity);
			
			Player p = o.GetComponent<Player>();
			p.playerNum = i;
			p.inputMethod = pgs.inputMethods[i];
			
			FreeLook fl = p.lookCamera.GetComponent< FreeLook >();
			fl.inputMethod = pgs.inputMethods[i];
			
			var r = p.lookCamera.rect;
			r.x = i * 0.5f;
			p.lookCamera.rect = r;
			// p.skin = pgs.skin[i];
		}

		Destroy(pgs);
	}
}
