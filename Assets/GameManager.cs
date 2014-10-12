using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	static public GameManager main;

	[SerializeField] private GameObject playerPrefab;

	public Player[] players;
	public Fridge[] fridges;

	public Transform[] spawnPoints;

	void Awake(){
		main = this;
	}

	void Start () {
		SpawnPlayers();
		fridges = FindObjectsOfType<Fridge>();
	}
	
	void Update () {
	
	}

	void SpawnPlayers() {
		PersistentGameState pgs = FindObjectOfType<PersistentGameState>();
		players = new Player[2];

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

			players[i] = p;
		}

		Destroy(pgs);
	}

	void SpawnBeer(){

	}

	public void Win(){
		print("WIN");
	}
}
