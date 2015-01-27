using UnityEngine;
using System.Collections;

public class Mallet_drop : MonoBehaviour {

	public float dropSpeed = -.025f;
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate(new Vector3(0, dropSpeed, 0));
	}
}
