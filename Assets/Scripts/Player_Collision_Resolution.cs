using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_Collision_Resolution : MonoBehaviour {
	public enum JumpState {
		jumping,
		floating,
		falling
	};

	public Player_Action parent;
	public List<Collider> blocksAdjacent;
	
	void Start () {
		blocksAdjacent = new List<Collider>();
		parent = GetComponentInParent<Player_Action>();
	}
	
	void OnTriggerEnter(Collider other) {
		blocksAdjacent.Add(other);
	}
	
	void OnTriggerExit(Collider other) {
		blocksAdjacent.Remove(other);
	}
}
