using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Foe_Reaper_EdgeHandler : MonoBehaviour {
	public int collisions = 1;
	public List<Collider> collisionList;
	public Foe_Reaper parent;
	
	void Start () {
		parent = GetComponentInParent<Foe_Reaper>();
		parent.child = this;
	}
	
	void OnTriggerEnter(Collider other) {
		++collisions;
		collisionList.Add(other);
	}
	
	void OnTriggerStay(Collider other) {
		if (!collisionList.Contains (other)) {
			++collisions;
			collisionList.Add(other);
		}
	}
	
	void OnTriggerExit(Collider other) {
		--collisions;
		collisionList.Remove(other);
	}
	
	void FixedUpdate() {
		if (collisions < 2) {
			parent.edgeCollision();
		}
	}
}
