using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Foe_Eye_Movement : MonoBehaviour {
	public List<Vector3> pattern;
	public float velocity = 0.05f;
	public int currentTarget = 0;
	public float rotationSpeed = 2f;
	
	public Vector3 forwardVector;
	
	// Use this for initialization
	void Start () {
		pattern.Add (new Vector3(5, 5, 0));
		pattern.Add (new Vector3(5, 2, 0));
		pattern.Add (new Vector3(-5, 5, 0));
		pattern.Add (new Vector3(-5, 2, 0));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		Vector3 locationVector = pattern[currentTarget] - transform.position;
		
		transform.rotation = Quaternion.Slerp (transform.rotation,
				Quaternion.FromToRotation(Vector3.right,locationVector.normalized),
				rotationSpeed);
		
		transform.Translate (velocity * Vector3.right);
		
		if (Vector3.Distance(pattern[currentTarget], transform.position) < 0.5f) {
			currentTarget = (currentTarget + 1) % pattern.Count;
		}
		
		if (Mathf.Abs(transform.rotation.eulerAngles.z - 180f) > 90f) {
			transform.localScale = new Vector3(1, 1, 1);
		} else {
			transform.localScale = new Vector3(1, -1, 1);
		}
	}
}
