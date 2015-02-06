using UnityEngine;
using System.Collections;

public class Arrow_Action_Super : MonoBehaviour {
	public int lifeTime = 20;
	public float speed = 0.3f;
	private int age = 0;
	public Vector3 move;
	
	public bool keyIsHeld = true;
	float surgeAge = 15f;
	public float surgeDuration = 1f;
	bool surging = false;
		
	void Start () {
		if (Player_Physics_Final.facing == Player_Physics_Final.dirState.upwards) {
			transform.localScale = new Vector3 (0.25f, 0.75f, 0.25f);
			move = new Vector3(0, speed, 0);
		}
		else if (Player_Physics_Final.faceDir == 1) {
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
			if (gameObject.layer == Layerdefs.projectile) {
				gameObject.layer = Layerdefs.transparentFX;
			}
			float ratio = surgeAge/surgeDuration;
			transform.rotation = Quaternion.Euler(90f * Vector3.back);
			transform.localScale = new Vector3 (0.8f * ratio, 0.8f * ratio, 1);
			surgeAge += Time.fixedDeltaTime;
			if (surgeAge >= surgeDuration) {
				Relaunch();
			}
		} else if (++age >= lifeTime) {
			GameObject.Destroy (this.gameObject);
		}
		
		if ((Input.GetKey ("z") || Input.GetKey (",")) == false) {
			keyIsHeld = false;
		}		
	}
		
	void OnTriggerEnter(Collider other) {
		if (keyIsHeld == false || other.gameObject.layer == Layerdefs.blockThick) {
			GameObject.Destroy(this.gameObject);
		}
		transform.position = other.transform.position;
		if (surging == false) {
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
		transform.localScale = new Vector3 (0.25f, 0.75f, 0.25f);
		move = new Vector3(0, speed, 0);
		gameObject.layer = Layerdefs.projectile;
		if (Player_Physics_Final.facing == Player_Physics_Final.dirState.upwards) {
			transform.rotation = Quaternion.Euler(90f * Vector3.up);
		} else if (Player_Physics_Final.facing == Player_Physics_Final.dirState.crouching) {
			transform.rotation = Quaternion.Euler(90f * Vector3.up + 180f * Vector3.back);
		} else if (Player_Physics_Final.faceDir == 1) {
			transform.rotation = Quaternion.Euler(90f * Vector3.back);
		} else {
			transform.rotation = Quaternion.Euler(90f * Vector3.forward);
		}
	}
}
