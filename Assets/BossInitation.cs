using UnityEngine;
using System.Collections;

public class BossInitation : MonoBehaviour {
	public GameObject mario;
	
	public bool activated = false;
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == Layerdefs.projectile && activated == false) {
			Instantiate (mario);
			activated = true;
		}
	}
}
