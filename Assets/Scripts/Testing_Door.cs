using UnityEngine;
using System.Collections;

public class Testing_Door : MonoBehaviour {
	public GameObject dest;
	public float xDisp;
	public GameObject camDest;
	
	float compDist (Vector3 a, Vector3 b) {
		float temp1 = a.x - b.x;
		float temp2 = a.y - b.y;
		float temp3 = temp1 * temp1;
		float temp4 = temp2 * temp2;
		float dist = Mathf.Sqrt(temp3 + temp4);
		return dist;
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.layer == Layerdefs.pit) {
			Vector3 newpos = dest.transform.position;
			Vector3 oldpos = other.transform.position;
			newpos.x += xDisp;
			other.transform.position = newpos;
			if (compDist(other.transform.position, oldpos) > compDist(other.transform.position, newpos)) {
				Camera.main.transform.position = camDest.transform.position;
			}
		}
	}
}
