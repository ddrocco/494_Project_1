using UnityEngine;
using System.Collections;

public class Door_End_Game : MonoBehaviour {	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == Layerdefs.pit) {
			Application.LoadLevel("_Victory_Screen");
		}
	}
	
}
