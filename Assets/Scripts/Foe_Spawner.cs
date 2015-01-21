using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Foe_Spawner : MonoBehaviour {
	public int yMargin = 5;
	public Player_Shoot player;
	public Vector3 relativeSpawnLocation;
	private Vector3 spawnLocation;
	public List<Obj_Foe> foesList;
	public Obj_Foe foePrefab;
	public int timer;
	public int spawnDelay = 40;
	
	public List<int> respawnTimers;
	public int respawnTime = 600;
	
	// Use this for initialization
	void Start () {
		player = FindObjectOfType<Player_Shoot>();
		timer = 40;
		spawnLocation = relativeSpawnLocation + transform.position.y * Vector3.up;
		respawnTimers = new List<int>{respawnTime, respawnTime, respawnTime, respawnTime};
	}
	
	
	void FixedUpdate () {
		//Update respawn rates
		for (int i = 0; i < respawnTimers.Count; ++i) {
			++respawnTimers[i];
		}
		
		//Check if player is in correct position and that nothing has spawned for enough time
		if (player.gameObject.transform.position.y > transform.position.y
		    	&& player.gameObject.transform.position.y < transform.position.y + yMargin
		    	&& ++timer > spawnDelay) {
		    //Check if respawn rate has elapsed
			for (int i = 0; i < respawnTimers.Count; ++i) {
				if (respawnTimers[i] > respawnTime) {
					//Check if too many monsters already exist
					if (foesList.Count < 4) {
						Obj_Foe newFoe = Instantiate(foePrefab, spawnLocation, Quaternion.identity) as Obj_Foe;
						newFoe.spawner = this;
						newFoe.spawned = true;
						foesList.Add (newFoe);
						timer = 0;
						respawnTimers[i] = 0;
					}
					break;
				}
			}
		}
	}
}
