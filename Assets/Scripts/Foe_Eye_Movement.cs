using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Foe_Eye_Movement : Obj_Foe {
	public float birthTime;
	
	public enum stage {
		preparing,
		defaulting,
		attacking,
		retreating,
		respawning
	};
	public stage currentStage = stage.defaulting;

	private int respawnTimer = 75;
	public int respawnMaxTime = 75;
	
	private Vector3 lastLocation;
	public float attackDistance = 4f;
	
	//CAN MODIFY STATIC VALUES; USE THESE FOR TWEAKING:
	//public float newHT = 3f;
	//public float newVT = 5f;
	
	//USED FOR ATTACKING:
	public float attackFallSpeed = 0.09f;
	public float attackOscillationSize = 2f;
	private float attackHCenter;
	public float horizPhase = 2f;
	
	//USED FOR PREPARING:
	public float entrySpeed = 0.09f;
	
	public bool respawnEnabled; //Gives eyes one "respawn"
	
	void Start() {
		birthTime = Time.time;
		lastLocation = transform.position;
		health = 1;
		transform.position = Foe_Eye_Cluster.SpawnPosition();
	}
	
	// Update is called once per frame
	new void FixedUpdate () {
		//Foe_Eye_Cluster.horizTime = newHT;
		//Foe_Eye_Cluster.verticalTime = newVT;
		if (currentStage == stage.preparing) {
			Vector3 destination = Foe_Eye_Cluster.WavePosition(6f);
			Vector3 displacement = destination - transform.position;
			if (displacement.magnitude < entrySpeed) {
				currentStage = stage.defaulting;
				birthTime = 6f;
			} else {
				transform.Translate (displacement.normalized * entrySpeed);
			}
		} else if (currentStage == stage.respawning && ++respawnTimer >= respawnMaxTime) {
			currentStage = stage.preparing;
			transform.position = Foe_Eye_Cluster.SpawnPosition();
			transform.localScale = new Vector3(1f, 1f, 1f);
		} else if (currentStage == stage.defaulting) {
			DefaultMovement();
		} else if (currentStage == stage.attacking) {
			AttackMovement();
		} else if (currentStage == stage.retreating) {
			transform.Translate (Vector3.right);
		}
	}
	
	void DefaultMovement() {
		if (Vector3.Distance (player.transform.position, transform.position) < attackDistance) {
			currentStage = stage.attacking;
			attackHCenter = transform.position.x;
			if (transform.position.x - lastLocation.x > 0f) { //Moving right
				birthTime = Time.time;
			} else {
				birthTime = Time.time + horizPhase / 2;
			}
			return;
		}
		birthTime += Time.fixedDeltaTime;
		transform.position = Foe_Eye_Cluster.WavePosition(birthTime);
		if (lastLocation.x > transform.position.x) {
			transform.localScale = new Vector3(-1f, 1f, 1f);
		} else {
			transform.localScale = new Vector3(1f, 1f, 1f);
		}
		lastLocation = transform.position;
	}
	
	void AttackMovement() {
		float phaseTime = 2f * Mathf.PI * (Time.time - birthTime) / horizPhase;
		//print (Time.time + " " + birthTime + " " + horizPhase);
		float hOffset = attackOscillationSize * Mathf.Sin(phaseTime);
		transform.position = new Vector3(attackHCenter + hOffset, transform.position.y - attackFallSpeed, 0);
		CheckScreenPositionAndDespawn();
	}
	
	void AdjustRotationAndMovement() {
		transform.Translate (Vector3.right);
	}
	
	void CheckScreenPositionAndDespawn() {
		if ((transform.position.x < Foe_Eye_Cluster.screenBottomLeft.x - 5f
				|| transform.position.x > Foe_Eye_Cluster.screenTopRight.x + 5f
				|| transform.position.y < Foe_Eye_Cluster.screenBottomLeft.y - 5f)) {
			if (respawnEnabled == true) { //Respawn eye
				currentStage = stage.respawning;
				respawnTimer = 0;
				respawnEnabled = false;
			} else { //Destroy reaper
				Destroy(gameObject);
			}
		}
	}
	
	void OnTriggerEnter(Collider other) {
		CollisionTrigger(other);
	}
}
