using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Foe_Reaper : Obj_Foe {
	public bool freakingOut = false;
	public bool movingRight = false;
	public bool moving = true;
	public float initialXValue;
	public float calmMoveSpeed = 0.03f;
	public float freakoutMoveSpeed = 0.08f;
	public Foe_Reaper_EdgeHandler child;
	public int timer;
	public int calmPhaseTime = 45;
	public int freakoutPhaseTime = 100;
	public GameObject player;
	
	public Sprite animation0;
	public Sprite animation1;
	
	public Vector3 spawnLocation;
	public List<Obj_Foe> foesList;
	public Obj_Foe foePrefab;
	public int spawnTimer;
	public int spawnDelay = 40;
	public List<int> respawnTimers;
	public int respawnTime = 600;
	
	public float freakoutDistance = 3f;

	// Use this for initialization
	void Start () {
		initialXValue = transform.position.x;
		timer = 0;
		player = FindObjectOfType<Player_Shoot>().gameObject;
		GetComponent<SpriteRenderer>().sprite = animation0;
		health = 7;
		itemDropOnDeath = item.fullHeart;
		
		spawnTimer = 40;
		spawnLocation = transform.position.y * Vector3.up + spawnLocation;
		respawnTimers = new List<int>{respawnTime, respawnTime, respawnTime, respawnTime};
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (freakingOut == true) {
			FreakoutUpdate();
		} else {
			CalmUpdate();
		}
	}
	
	private void CalmUpdate() {
		playerDetection();
		if (++timer >= calmPhaseTime) {
			timer = 0;
			float rand = Random.Range (0, 10f);
			if (rand < 3) {
				movingRight = true;
				moving = true;
				transform.localScale = new Vector3(-1f, 1f, 1f);
			} else if (rand < 6) {
				movingRight = false;
				moving = true;
				transform.localScale = new Vector3(1f, 1f, 1f);
			} else if (rand < 9) {
				moving = false;
			} else {
				moving = false;
				flipAround ();
			}
		}
		if (moving) {
			if (movingRight == true) {
				transform.Translate (calmMoveSpeed * Vector3.right);
			} else {
				transform.Translate (calmMoveSpeed * Vector3.left);
			}	
		}
	}
	
	private void FreakoutUpdate() {
		spawnMonsters();
		if (++timer >= freakoutPhaseTime) {
			timer = calmPhaseTime;
			freakingOut = false;
			GetComponent<SpriteRenderer>().sprite = animation0;
		}
		if (!moving) {
			if (player.transform.position.x > transform.position.x) {
				transform.Translate (freakoutMoveSpeed * Vector3.right);
				movingRight = true;
			} else {
				transform.Translate (freakoutMoveSpeed * Vector3.left);
				movingRight = false;
			}
		} else {
			if (movingRight == true) {
				transform.Translate (freakoutMoveSpeed * Vector3.right);
			} else {
				transform.Translate (freakoutMoveSpeed * Vector3.left);
			}
			if (Vector3.Distance (player.transform.position, transform.position) < 5f) {
				moving = false;
			}
		}
	}
	
	public void edgeCollision() {
		if (freakingOut == true) {
			moving = true;
		}
		if (transform.position.x < initialXValue) {
			movingRight = true;
			transform.localScale = new Vector3(-1f, 1f, 1f);
		} else {
			movingRight = false;
			transform.localScale = new Vector3(1f, 1f, 1f);
		}
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == 13) { //Arrow
			GameObject.Destroy(other.gameObject);
			HitByArrow();
			return;
		}
		flipAround ();
	}
	
	void flipAround() {
		if (freakingOut == true) {
			moving = true;
		}
		movingRight = !movingRight;
		transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
	}
	
	void playerDetection() {
		if (Vector3.Distance (player.transform.position, transform.position) < freakoutDistance
				&& ((player.transform.position.x > transform.position.x
				&& movingRight == true)
		    	|| (player.transform.position.x < transform.position.x
		    	&& movingRight == true))) {
			freakingOut = true;
			GetComponent<SpriteRenderer>().sprite = animation1;
			moving = false;
		    timer = 0	;
		}
	}
	
	void spawnMonsters() {
		//Update respawn rates
		for (int i = 0; i < respawnTimers.Count; ++i) {
			++respawnTimers[i];
		}
		
		//Check that nothing has spawned for enough time
		if (++spawnTimer > spawnDelay) {
			//Check if respawn rate has elapsed
			for (int i = 0; i < respawnTimers.Count; ++i) {
				if (respawnTimers[i] > respawnTime) {
					//Check if too many monsters already exist
					if (foesList.Count < 4) {
						Obj_Foe newFoe = Instantiate(foePrefab, spawnLocation, Quaternion.identity) as Obj_Foe;
						newFoe.reaper = this;
						newFoe.spawned = false;
						foesList.Add (newFoe);
						spawnTimer = 0;
						respawnTimers[i] = 0;
					}
					break;
				}
			}
		}
	}
}
