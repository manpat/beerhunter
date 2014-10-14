using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	static public GameManager main;

	[SerializeField] private GameObject playerPrefab;

	public Player[] players;
	public Fridge[] fridges;

	public Transform[] spawnPoints;

	private float timeTillNPCEvent = 0f;

	void Awake(){
		main = this;
	}

	void Start () {
		SpawnPlayers();

		fridges = FindObjectsOfType<Fridge>();
		SpawnBeer();

		timeTillNPCEvent = Random.Range(2f, 5f);
	}

	void Update(){
		timeTillNPCEvent -= Time.deltaTime;

		if(timeTillNPCEvent <= 0f){
			DoNPCEvent();

			timeTillNPCEvent = Random.Range(2f, 5f);
		}
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
			fl.inputMethod = pgs.inputMethods[ i ];

			var r = p.lookCamera.rect;
			r.x = i * 0.5f;
			p.lookCamera.rect = r;
			p.fridgeCamera.rect = r;
			// p.skin = pgs.skin[i];

			players[i] = p;
		}

		Destroy(pgs);
	}

	void SpawnBeer() {
		Fridge prevfridge = null;

		if ( prevfridge == null ) {
			foreach(Fridge f in fridges){
				if(f.hasBeer){
					prevfridge = f;
					f.hasBeer = false;
					break;
				}
			}
		}

		int idx = System.Array.IndexOf(fridges, prevfridge);
		int next = (idx + (int)Random.Range(1, 3)) % fridges.Length;

		fridges[next].hasBeer = true;
	}

	void DoNPCEvent(){
		const float npcCheckRadius = 8f;
		const float npcPanicCheckRadius = 50f;
		int npcLayerMask = LayerMask.GetMask("NPC");
		NPCBehaviour behaviour = NPCBehaviour.None;
		{
			float x = Random.value * 100f;

			if(x < 20f){ // 20% chance 
				behaviour = NPCBehaviour.Gather;
			}else{
				x -= 20f;
				if(x < 40f){ // 40% chance
					behaviour = NPCBehaviour.Converse;
				}else{ // 40% chance
					behaviour = NPCBehaviour.HitPlayer;
				}
			} 
		}

		Vector3 ppos = players[(Random.value > 0.5f)?0:1].transform.position;
		Collider[] npcs = Physics.OverlapSphere(ppos, npcCheckRadius, npcLayerMask);
		if(npcs == null || npcs.Length == 0){
			npcs = Physics.OverlapSphere(ppos, npcPanicCheckRadius, npcLayerMask);
		}

		float eventLength = Random.Range(5f, 15f);

		print("NPCEvent begun: "+ behaviour.ToString());

		switch(behaviour){
			case NPCBehaviour.Gather: {
				NPC centerNPC = npcs[(int)(Random.value * npcs.Length)].GetComponent<NPC>();
				Vector3 cpos = centerNPC.transform.position;
				npcs = Physics.OverlapSphere(cpos, npcCheckRadius, npcLayerMask);

				foreach(Collider c in npcs){
					NPC n = c.GetComponent<NPC>();
					n.toPos = cpos;
					n.behaviour = behaviour;
					n.behaviourLength = eventLength;
				}

				break;
			}
			case NPCBehaviour.Converse: {
				NPC npc1 = npcs[(int)(Random.value * npcs.Length)].GetComponent<NPC>();
				NPC npc2 = npcs[(int)(Random.value * npcs.Length)].GetComponent<NPC>();
				
				Vector3 midwaypoint = (npc1.transform.position + npc2.transform.position)/2f;
				
				npc1.toPos = midwaypoint;
				npc1.behaviour = behaviour;
				npc1.behaviourLength = eventLength;
				npc2.toPos = midwaypoint;
				npc2.behaviour = behaviour;
				npc2.behaviourLength = eventLength;

				break;
			}
			case NPCBehaviour.HitPlayer: {
				NPC npc = npcs[(int)(Random.value * npcs.Length)].GetComponent<NPC>();
				
				npc.toPos = ppos;
				npc.behaviour = behaviour;
				npc.behaviourLength = eventLength;

				break;
			}

			default:
				print("Unimplemented behaviour, " + behaviour.ToString());
				break;
		}
	}

	public void OnPlayerGetBeer(){
		if(players[0].drunkness == 1f || players[1].drunkness == 1f){
			Win();
		}else{
			SpawnBeer();
		}
	}

	public void Win(){
		print("WIN");
	}
}
