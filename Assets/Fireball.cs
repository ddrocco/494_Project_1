using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {
	public float velocity;
	
	void FixedUpdate() {
		transform.Translate (Vector3.right * velocity);
		
		if (transform.position.x > 9f || transform.position.x < -10f) {
			Destroy(this.gameObject);
		}
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == Layerdefs.pit) { //Player
			other.gameObject.GetComponent<Player_Enemy_Collision>().HitByEnemy();
			Destroy(this.gameObject);
		}
	}
}
