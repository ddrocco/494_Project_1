using UnityEngine;
using System.Collections;

public class Foe_Eye_Sprite : MonoBehaviour {
	// Update is called once per frame
	void FixedUpdate () {
		if (Mathf.Abs(transform.parent.rotation.eulerAngles.z - 180f) < 90f) {
			transform.localScale = new Vector3(-1, 1, 1);
		} else {
			transform.localScale = new Vector3(1, 1, 1);
		}
		transform.rotation = Quaternion.Euler(Vector3.zero);
	}
}
