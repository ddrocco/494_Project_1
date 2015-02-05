using UnityEngine;
using System.Collections;

public class Obj_Foe : MonoBehaviour {
	public int health;
	public Foe_Spawner spawner;
	public Foe_Reaper reaper;
	public bool spawned; //debug
	
	public enum item {
		smallHeart,
		halfHeart,
		fullHeart
	}
	
	public item itemDropOnDeath;
	public GameObject heartPrefab;
	
	public int invulnTime = 25;
	public int timeSinceHit;
	public bool invulnerable = false;
	
	public Player_Shoot player;
	
	public void Awake() {
		player = FindObjectOfType<Player_Shoot>();
		timeSinceHit = invulnTime;
	}

	public void FixedUpdate () {
		if (++timeSinceHit >= invulnTime) {
			invulnerable = false;
			renderer.material.color = Color.white;
		} else if (timeSinceHit % 6 > 3) {
			renderer.material.color = Color.red;
		} else {
			renderer.material.color = Color.yellow;
		}
		
		if (transform.position.y < player.transform.position.y - 5f) {
			Destroy (gameObject);
		}
	}
	
	public void Maintenance() {
		FixedUpdate ();
	}
	
	public void HitByArrow() {
		if (invulnerable == true) {
			return;
		}
		if (--health <= 0) {
			if (heartPrefab != null) {
				GameObject heart;
				heart = Instantiate(heartPrefab,transform.position,Quaternion.identity) as GameObject;
				Heart_Pickup_Script heartScript = heart.GetComponent<Heart_Pickup_Script>();
				if (itemDropOnDeath == item.smallHeart) {
					heartScript.value = 1;
				} else if (itemDropOnDeath == item.halfHeart) {
					heartScript.value = 5;
				} else {
					heartScript.value = 10;
				}
			}
			Destroy (this.gameObject);
		} else {
			invulnerable = true;
			timeSinceHit = 0;
		}
	}
	
	void OnDestroy() {
		if (spawned) {
			spawner.foesList.Remove (this);
		} else {
			//reaper.foesList.Remove (this);
		}
		//Drop item:		
		//Puff of smoke (should spawn item when smoke clears
	}
	
	public void CollisionTrigger(Collider other) {
		if (other.gameObject.layer == Layerdefs.pit) { //Player
			player.GetComponentInParent<Player_Enemy_Collision>().HitByEnemy();
		} else if (other.gameObject.layer == Layerdefs.projectile) { //Arrow
			HitByArrow();
		}
	}
	
	void CollisionStay(Collider other) {
		CollisionTrigger (other);
	}

}
