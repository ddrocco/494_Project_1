using UnityEngine;
using System.Collections;

public class Arrow_Action_Super : MonoBehaviour {
	public int lifeTime = 10;
	public float speed = 0.3f;
	private int age = 0;
	public Vector3 move;
	
	public bool keyIsHeld = true;
	float surgeAge = 15f;
	public float surgeDuration = 1f;
	bool surging = false;
		
	void Start () {
		if (Player_Physics.facing == Player_Physics.dirState.upwards) {
			transform.localScale = new Vector3 (0.25f, 0.75f, 0.25f);
			move = new Vector3(0, speed, 0);
		}
		else if (Player_Physics.isLookingRight == true) {
			move = new Vector3(speed, 0, 0);
		}
		else {
			move = new Vector3(-speed, 0, 0);
		}
	}
		
	void FixedUpdate () {
		Vector3 screenBottomLeft = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0));
		Vector3 screenTopRight = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, 0));
		if (transform.position.x < screenBottomLeft.x || transform.position.x > screenTopRight.x) {
			GameObject.Destroy (this.gameObject);
		}
		transform.Translate (move);
		if (surging == true && keyIsHeld) {
			float ratio = surgeAge/surgeDuration;
			transform.localScale = new Vector3 (1.5f * ratio, 1.5f * ratio, 1);
			surgeAge += Time.fixedDeltaTime;
			if (surgeAge >= surgeDuration) {
				Relaunch();
			}
		} else if (++age >= lifeTime) {
			GameObject.Destroy (this.gameObject);
		}
		
		if ((Input.GetKey ("z") || Input.GetKey (",")) == false) {
			print ("You let go");
			keyIsHeld = false;
		}		
	}
		
	void OnTriggerEnter(Collider other) {
		if (keyIsHeld == false || other.gameObject.layer == Layerdefs.blockThick) {
			GameObject.Destroy(this.gameObject);
		} else if (surging == false) {
			transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
			move = new Vector3(0, 0, 0);
			surgeAge = 0;
			age = 0;
			surging = true;
		}
	}
		
	void OnDestroy() {
		Player_Shoot.shotArrow = false;
	}
	
	void Relaunch() {
		surging = false;
		age = 0;
		if (Player_Physics.facing == Player_Physics.dirState.upwards) {
			transform.localScale = new Vector3 (0.25f, 0.75f, 0.25f);
			move = new Vector3(0, speed, 0);
		}
		else if (Player_Physics.isLookingRight == true) {
			move = new Vector3(speed, 0, 0);
		}
		else {
			move = new Vector3(-speed, 0, 0);
		}
	}
}
