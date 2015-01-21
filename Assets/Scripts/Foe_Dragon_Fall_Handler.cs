using UnityEngine;
using System.Collections;

public class Foe_Dragon_Fall_Handler : MonoBehaviour {
	private int collisions = 0;
	private Foe_Dragon parent;
	
	void Start () {
		collisions = 0;
		parent = GetComponentInParent<Foe_Dragon>();
	}
	
	void OnTriggerEnter() {
		++collisions;
	}
	
	void OnTriggerExit() {
		--collisions;
	}
	
	void FixedUpdate() {
		if (collisions == 0 && parent.falling == false) {
			parent.falling = true;
			parent.facePlayer();
		}
	}
}
