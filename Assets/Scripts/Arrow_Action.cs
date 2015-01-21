using UnityEngine;
using System.Collections;

public class Arrow_Action : MonoBehaviour {
	public float lifeTime = 0.3f;
	public float speed = 0.3f;
	private float timer = 0f;
	private Vector3 move;

	void Start () {
		if (Player_Action.faceUp) {
			transform.localScale = new Vector3 (0.25f, 0.75f, 0.25f);
			move = new Vector3(0, speed, 0);
		}
		else if (Player_Action.faceRight) {
			move = new Vector3(speed, 0, 0);
		}
		else {
			move = new Vector3(-speed, 0, 0);
		}
	}
	
	void Update () {
		Vector3 screenBottomLeft = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0));
		Vector3 screenTopRight = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, 0));
		if (transform.position.x < screenBottomLeft.x || transform.position.x > screenTopRight.x) {
			GameObject.Destroy (this.gameObject);
		}
		timer += 1.0f * Time.deltaTime;
		transform.Translate (move);
		if (timer >= lifeTime) {
			GameObject.Destroy (this.gameObject);
		}
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == Layerdefs.blockThick) {
			GameObject.Destroy(this.gameObject);
		}
	}

	void OnDestroy() {
		Player_Shoot.shotArrow = false;
	}

}