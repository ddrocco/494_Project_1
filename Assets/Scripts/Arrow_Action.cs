using UnityEngine;
using System.Collections;

public class Arrow_Action : MonoBehaviour {
	public int lifeTime = 10;
	public float speed = 0.3f;
	private int age = 0;
	public Vector3 move;

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
		if (++age >= lifeTime) {
			GameObject.Destroy (this.gameObject);
		}
	}
	
	void OnTriggerEnter(Collider other) {
		GameObject.Destroy(this.gameObject);
		//if (other.gameObject.layer == Layerdefs.blockThick
		//		|| other.gameObject.layer == Layerdefs.blockThin) {
		//	
		//}
	}

	void OnDestroy() {
		Player_Shoot.shotArrow = false;
	}

}