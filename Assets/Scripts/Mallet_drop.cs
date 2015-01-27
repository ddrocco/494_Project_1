using UnityEngine;
using System.Collections;

public class Mallet_drop : MonoBehaviour {

	public float dropSpeed = -.025f;
	
	void FixedUpdate () {
		transform.Translate(new Vector3(0, dropSpeed, 0));
		Vector3 screenBottomLeft = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0));
		if (transform.position.y < screenBottomLeft.y) {
			GameObject.Destroy(this.gameObject);
		}
	}
}
