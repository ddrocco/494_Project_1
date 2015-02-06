using UnityEngine;
using System.Collections;

public class Foe_Flameboy : Obj_Foe {
	public GameObject fireballPrefab;
	public Sprite appearing, present;
	public Vector3 hostFloor;
	public enum flameState {
		burrowing,
		attacking,
		vulnerable,
		retreating
	};
	public flameState state;
	
	public int burrowTimer, burrowSteps, attackTimer, attackSteps, vulnerableTimer, vulnerableSteps, retreatTimer, retreatSteps;
	
	// Use this for initialization
	void Start () {
		hostFloor = transform.position;
		GetComponent<SpriteRenderer>().sprite = null;
	}
	
	void Update() {
		Debug.DrawRay (transform.position + new Vector3(2f/3, 0, 0), Vector3.right / 2);
		Debug.DrawRay (transform.position + new Vector3(-2f/3, 0, 0), Vector3.left / 2);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (state == flameState.burrowing && ++burrowTimer >= burrowSteps) {
			BurrowAction ();
		} else if (state == flameState.attacking) {
			if (++attackTimer >= attackSteps) {
				AttackAction();
			} else if (attackTimer % 4 == 0){
				transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
			}		
		} else if (state == flameState.vulnerable) {
			if (++vulnerableTimer >= vulnerableSteps) {
				VulnerableAction();
			} else if (vulnerableTimer % 4 == 0) {
				transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
			}
		} else if (state == flameState.retreating) {
			if (++retreatTimer >= retreatSteps) {
				RetreatAction();
			} else if (retreatTimer % 4 == 0) {
				transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
			}
		}
	}
	
	void BurrowAction() {
		if ((transform.position - player.transform.position).magnitude <= 2f) {
			state = flameState.attacking;
			attackTimer = 0;
			transform.Translate (Vector3.up);
			GetComponent<SpriteRenderer>().sprite = appearing;
			GetComponent<BoxCollider>().size = new Vector3(1, 0.4f, 1);
			GetComponent<BoxCollider>().center = new Vector3(0, -0.3f, 0);
		} else {
			burrowTimer = 0;
			if (player.transform.position.x - transform.position.x > 0) {
				RaycastHit raycastHit;
				bool hit = Physics.Raycast(transform.position + new Vector3(2f/3, 0, 0), Vector3.right / 2, out raycastHit);
				print (raycastHit.transform.position);
				if (hit == true && raycastHit.collider.gameObject.layer == Layerdefs.blockThick
						&& raycastHit.collider.transform.position.x - transform.position.x < 1f) {
					hostFloor = hostFloor + Vector3.right;
					transform.position = hostFloor;
				}
			} else {
				RaycastHit raycastHit;
				bool hit = Physics.Raycast(transform.position + new Vector3(-2f/3, 0, 0), Vector3.left / 2, out raycastHit);
				print (raycastHit.transform.position);
				if (hit == true && raycastHit.collider.gameObject.layer == Layerdefs.blockThick
				    	&& raycastHit.collider.transform.position.x - transform.position.x > -1f) {
					hostFloor = hostFloor + Vector3.left;
					transform.position = hostFloor;
				}
			}
		}
	}
	
	void AttackAction() {
		if ((transform.position.y - player.transform.position.y) <= 1f) {
			GameObject fireball = Instantiate (fireballPrefab, transform.position, Quaternion.identity) as GameObject;
			if (player.gameObject.transform.position.x > transform.position.x) {
				fireball.GetComponent<Fireball>().velocity = 0.1f;
			} else {
				fireball.GetComponent<Fireball>().velocity = -0.1f;
			}
		}
		state = flameState.vulnerable;
		vulnerableTimer = 0;
		GetComponent<SpriteRenderer>().sprite = present;
		GetComponent<BoxCollider>().size = new Vector3(1, 1, 1);
		GetComponent<BoxCollider>().center = new Vector3(0, 0, 0);
	}
	
	void VulnerableAction() {
		state = flameState.retreating;
		retreatTimer = 0;
		GetComponent<SpriteRenderer>().sprite = appearing;
		GetComponent<BoxCollider>().size = new Vector3(1, 0.4f, 1);
		GetComponent<BoxCollider>().center = new Vector3(0, -0.3f, 0);
	}
	
	void RetreatAction() {
		state = flameState.burrowing;
		burrowTimer = 0;
		GetComponent<SpriteRenderer>().sprite = null;
		transform.Translate (Vector3.down);
		GetComponent<BoxCollider>().center = new Vector3(0, 0, 0);
		GetComponent<BoxCollider>().size = new Vector3(0.2f, 0.2f, 1);
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == Layerdefs.pit && state != flameState.attacking && state != flameState.retreating) { //Player
			player.GetComponentInParent<Player_Enemy_Collision>().HitByEnemy();
		} else if (other.gameObject.layer == Layerdefs.projectile && state == flameState.vulnerable) { //Arrow
			HitByArrow();
		}
	}
	
	void OnTriggerStay(Collider other) {
		OnTriggerEnter(other);
	}
}
