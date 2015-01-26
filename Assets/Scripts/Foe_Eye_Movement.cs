using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Foe_Eye_Movement : Obj_Foe {
	public int age = 0;
	public List<Vector3> pattern;
	public float velocityMax = 0.2f;
	public float velocity;
	public int rampFrames = 75;
	public float rotationSpeedRate = 0.001f;
	public float rotationSpeed;
	public Vector3 target;
	public int currentTarget;
	public enum stage {
		defaulting,
		attacking,
		retreating
	};
	public int attackTimer;
	public int attackTimeTotal = 150;
	
	public stage currentStage = stage.defaulting;
	
	public Vector3 forwardVector;
	
	//Screen stuff:
	private Vector3 screenBottomLeft;
	private Vector3 screenTopRight;
	private float screenWidth;
	private float screenHeight;
	
	private bool respawning = false;
	private int respawnTimer = 75;
	public int respawnMaxTime = 75;
	
	void Start () {
		velocity = velocityMax;
		rotationSpeed = 0f;
		currentTarget = 0;

		pattern.Add (Vector3.zero);
		pattern.Add (Vector3.zero);
		pattern.Add (Vector3.zero);
		pattern.Add (Vector3.zero);
		UpdateLocations();
		PositionSpawn();
	}
	
	// Update is called once per frame
	new void FixedUpdate () {
		if (respawning == true) {
			if (++respawnTimer >= respawnMaxTime) {
				PositionSpawn ();
			} else {
				return;
			}
		}
	
		if (currentStage != stage.retreating) {
			AdjustRotationAndMovement();
		} else {
			transform.Translate (velocityMax * Vector3.right);
		}
		CheckScreenPositionAndDespawn();
		
		if (currentStage == stage.defaulting) {
			//print (Vector3.Distance (player.transform.position, transform.position));
			if (++attackTimer > attackTimeTotal
			    		&& Vector3.Distance (player.transform.position, transform.position) < 5f) {
				AttackPlayer();
			} else if (Vector3.Distance(target, transform.position) < 0.5f) {
				target = pattern[currentTarget];
				++currentTarget;
				if (currentTarget >= pattern.Count) {
					currentTarget = 0;
				}
				velocity = velocity / 4;
				rotationSpeed = 0;
			}
		} else if (currentStage == stage.attacking) {
			if (attackTimer >= attackTimeTotal || Vector3.Distance(target, transform.position) < 0.25f) {
				currentStage = stage.retreating;
				if (--currentTarget < 0) {
					currentTarget = pattern.Count - 1;
				}
				target = pattern[currentTarget];
			} else if (Vector3.Distance(target, transform.position) < 2f) {
				++attackTimer;
			}
		}
		
		UpdateLocations();
	}
	
	void PositionSpawn() {
		print ("Spawn");
		transform.position = Vector3.up * (screenTopRight.y + 15f);
		float targetX = screenBottomLeft.x + 3 * screenWidth / 4;
		float targetY = screenBottomLeft.x + 3 * screenHeight / 4;
		target = new Vector3(targetX, targetY, 0);
		respawning = false;
	}
	
	void AttackPlayer() {
		currentStage = stage.attacking;
		target = player.transform.position + new Vector3(Random.Range(-1,1), Random.Range(-1,1), 0);
		attackTimer = 0;
	}
	
	void AdjustRotationAndMovement() {
		if (velocity < velocityMax) {
			velocity += velocityMax / rampFrames;
		}
		rotationSpeed += rotationSpeedRate;
		Vector3 locationVector = target - transform.position;
		transform.rotation = Quaternion.Slerp (transform.rotation,
       			Quaternion.FromToRotation(Vector3.right,locationVector.normalized),
             	rotationSpeed);
		transform.Translate (velocity * Vector3.right);
	}
	
	void CheckScreenPositionAndDespawn() {
		if (transform.position.x < screenBottomLeft.x - 5f
				|| transform.position.x > screenTopRight.x + 5f
				|| transform.position.y < screenBottomLeft.y - 5f) {
			currentStage = stage.defaulting;
			respawning = true;
			respawnTimer = 0;
			PositionSpawn();
		}
	}
	
	void UpdateLocations() {
		screenBottomLeft = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0));
		screenTopRight = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, 0));
		screenWidth = screenTopRight.x - screenBottomLeft.x;
		screenHeight = screenTopRight.y - screenBottomLeft.y;
		float targetX = screenBottomLeft.x + 3 * screenWidth / 4;
		float targetY = screenBottomLeft.x + 3 * screenHeight / 4;
		pattern[0] = new Vector3(targetX, targetY, 0);
		pattern[1] = new Vector3(targetX, targetY-3, 0);
		pattern[2] = new Vector3(-targetX, targetY, 0);
		pattern[3] = new Vector3(-targetY, targetY-3, 0);
	}
}
