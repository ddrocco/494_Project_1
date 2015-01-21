using UnityEngine;
using System.Collections;

public class Arrow_Action : MonoBehaviour {
	public float lifeTime = 0.5f;
	public float speed = 1;
	private float timer = 0f;
	private Vector3 move;

	// Use this for initialization
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
	
	// Update is called once per frame
	void Update () {
		timer += 1.0f * Time.deltaTime;
		transform.Translate (move);
		if (timer >= lifeTime) {
			GameObject.Destroy (this.gameObject);
		}
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == 9) {
			GameObject.Destroy(this.gameObject);
		}
	}

	void OnDestroy() {
		Player_Shoot.shotArrow = false;
	}
}