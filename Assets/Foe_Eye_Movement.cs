using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Foe_Eye_Movement : MonoBehaviour {
	public List<Vector3> pattern;
	public float velocity = 0.05f;
	public int currentTarget = 0;
	public float rotationSpeed = 2f;
	
	// Use this for initialization
	void Start () {
		pattern.Add (new Vector3(5, 5, 0));
		pattern.Add (new Vector3(5, 2, 0));
		pattern.Add (new Vector3(-5, 5, 0));
		pattern.Add (new Vector3(-5, 2, 0));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 direction = (pattern[currentTarget] - transform.position);
		direction.z = 0f;
		Quaternion lookRotation = Quaternion.LookRotation(direction);
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed);
		transform.Translate (velocity * Vector3.forward);
		transform.position = transform.position.x * Vector3.right + transform.position.y * Vector3.up;
		/*direction -= direction.z * Vector3.forward;
		//print (direction.x + " " + direction.y + " " + direction.z);
		direction = direction.normalized;
		print (direction.x + " " + direction.y + " " + direction.z);
		Quaternion lookRotation = Quaternion.LookRotation(-direction);
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed);
		transform.Translate (velocity * Vector3.up);
		*/
		if (Vector3.Distance(pattern[currentTarget], transform.position) < 0.5f) {
			currentTarget = (currentTarget + 1) % pattern.Count;
		}
	}
}
