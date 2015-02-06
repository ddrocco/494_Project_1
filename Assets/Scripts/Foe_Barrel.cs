using UnityEngine;
using System.Collections;

public class Foe_Barrel : Obj_Foe {
	public enum barrelState {
		falling,
		bouncing,
		rolling
	}
	public barrelState state;

	public float vSpeed, hSpeed, vSpeedMax;
	
	public bool collisionReady = false;
	
	
	// Update is called once per frame
	new void FixedUpdate () {
		//transform.Rotate(3f * Vector3.forward);
		
		transform.Translate(new Vector3(hSpeed, -vSpeed, 0));
		if (state != barrelState.rolling && vSpeed < vSpeedMax) {
			vSpeed += 0.002f;
		}
		collisionReady = true;
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.layer == Layerdefs.foeGround) {
			print ("done");
			Destroy (this.gameObject);
		}
		if (other.gameObject.layer == Layerdefs.blockThick) {
			BlockCollision(other);
		}
		CollisionTrigger(other);
	}
	
	void BlockCollision(Collider other) {
		if (collisionReady == false) {
			return;
		}
		collisionReady = false;
		if (state != barrelState.rolling) {
			float blockTopEdge = other.gameObject.transform.position.y + 0.5f;
			float barrelBottomEdge = transform.position.y - 0.5f;
			transform.Translate (Vector3.up *
    					(blockTopEdge - barrelBottomEdge + 0.01f));
			if (state == barrelState.falling) {
				if (transform.position.x > 0) {
					hSpeed = -0.1f;
				} else {
					hSpeed = 0.1f;
				}
				state = barrelState.bouncing;
				vSpeed = 0.5f * -vSpeed;
			} else if (state == barrelState.bouncing) {
				state = barrelState.rolling;
				vSpeed = 0;
    		}
		} else {
			if (hSpeed > 0) {
				float blockLeftEdge = other.gameObject.transform.position.x - 0.5f;
				float barrelRightEdge = transform.position.x + 0.5f;
				transform.Translate (Vector3.right *
				                     (blockLeftEdge - barrelRightEdge));
			} else {
				float blockRightEdge = other.gameObject.transform.position.x + 0.5f;
				float barrelLeftEdge = transform.position.x + 0.5f;
				transform.Translate (Vector3.right *
				                     (barrelLeftEdge - blockRightEdge));
			}
			hSpeed = -hSpeed;
		}				
	}
}
