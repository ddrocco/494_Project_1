using UnityEngine;
using System.Collections;

public class Danky_Kang_Death : MonoBehaviour {
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate(0, -0.2f, 0);
		if (transform.position.y < 100) {
			Application.LoadLevel("_Victory_Screen");
		}
	}
}
