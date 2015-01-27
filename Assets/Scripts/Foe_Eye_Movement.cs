using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Foe_Eye_Movement : Obj_Foe {
	public float birthTime;
	
	public enum stage {
		defaulting,
		attacking,
		retreating,
		respawning
	};
	public int attackTimer;
	public int attackTimeTotal = 150;
	public stage currentStage = stage.defaulting;

	private int respawnTimer = 75;
	public int respawnMaxTime = 75;
	
	private Vector3 lastLocation;
	
	//CAN MODIFY STATIC VALUES; USE THESE FOR TWEAKING:
	//public float newHT = 3f;
	//public float newVT = 5f;
	
	void Start() {
		birthTime = Time.time;
		lastLocation = transform.position;
	}
	
	// Update is called once per frame
	new void FixedUpdate () {
		//Foe_Eye_Cluster.horizTime = newHT;
		//Foe_Eye_Cluster.verticalTime = newVT;
		if (currentStage == stage.respawning && ++respawnTimer >= respawnMaxTime) {
			birthTime = Time.time;
			currentStage = stage.defaulting;
		} else if (currentStage == stage.defaulting) {
			//print (Vector3.Distance (player.transform.position, transform.position));
			/*if (++attackTimer > attackTimeTotal
			    		&& Vector3.Distance (player.transform.position, transform.position) < 5f) {
				AttackPlayer();
			} else {*/
				transform.position = Foe_Eye_Cluster.WavePosition(Time.time - birthTime);
				if (lastLocation.x > transform.position.x) {
					transform.localScale = new Vector3(-1f, 1f, 1f);
				} else {
					transform.localScale = new Vector3(1f, 1f, 1f);
				}
				lastLocation = transform.position;
			//}
		} else if (currentStage == stage.attacking) {
			/*if (attackTimer >= attackTimeTotal || Vector3.Distance(, transform.position) < 0.25f) {
				currentStage = stage.retreating;
			} else if (Vector3.Distance(target, transform.position) < 2f) {
				++attackTimer;
			}*/
			transform.Translate (Vector3.right);
		} else if (currentStage == stage.retreating) {
			transform.Translate (Vector3.right);
		}
		CheckScreenPositionAndDespawn();
	}
	
	void AttackPlayer() {
		currentStage = stage.attacking;
		transform.Translate (Vector3.right);
		//target = player.transform.position + new Vector3(Random.Range(-1,1), Random.Range(-1,1), 0);
		attackTimer = 0;
	}
	
	void AdjustRotationAndMovement() {
		transform.Translate (Vector3.right);
	}
	
	void CheckScreenPositionAndDespawn() {
		if (currentStage != stage.respawning
				&& (transform.position.x < Foe_Eye_Cluster.screenBottomLeft.x - 5f
				|| transform.position.x > Foe_Eye_Cluster.screenTopRight.x + 5f
				|| transform.position.y < Foe_Eye_Cluster.screenBottomLeft.y - 5f)) {
			currentStage = stage.respawning;
			respawnTimer = 0;
		}
	}
}
