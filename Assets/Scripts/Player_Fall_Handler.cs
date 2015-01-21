using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_Fall_Handler : MonoBehaviour {
	public enum JumpState {
		jumping,
		floating,
		falling
	};
	public JumpState jumpState;
	public bool buttonHeld;
	public int jumpTimeMin = 10; //in steps
	public int jumpTimeMax = 18; //in steps
	public int floatTime = 15; //in steps
	public int currentTime;
	public float jumpSpeed = 0.2f;
	public float floatSpeed = 0.05f;
	public float floatGravity = 0.005f;
	public float fallGravity = 0.15f;
	public float maxFallSpeed = 0.15f;
	public Player_Action parent;
	public List<Collider> blocksBeneath;
	
	void Start () {
		jumpState = JumpState.floating;
		blocksBeneath = new List<Collider>();
		currentTime = 0;
		parent = GetComponentInParent<Player_Action>();
	}
	
	void OnTriggerEnter(Collider other) {
		blocksBeneath.Add(other);
	}
	
	void OnTriggerExit(Collider other) {
		blocksBeneath.Remove(other);
	}
	
	void FixedUpdate() {
		if (blocksBeneath.Count == 0) {
			++currentTime;
		} else {
			if (jumpState == JumpState.falling) {
				parent.vSpeed = 0;
				jumpState = JumpState.floating;
			}
			if (parent.vSpeed < 0.01) {
				currentTime = 0;
			}
		}
		if (Input.GetKey (".") || Input.GetKey ("x")) {
			buttonHeld = true;
		} else {
			buttonHeld = false;
		}
		
		if ((Input.GetKeyDown (".") || Input.GetKeyDown ("x"))
					&& (blocksBeneath.Count > 0 && jumpState == JumpState.floating)) {;
			parent.vSpeed = jumpSpeed;
			jumpState = JumpState.jumping;
			buttonHeld = true;
			currentTime = 0;
		} 
		
		switch(jumpState) {
			case JumpState.jumping:
				if (currentTime >= jumpTimeMax || (!buttonHeld && currentTime >= jumpTimeMin)) {
					jumpState = JumpState.floating;
					parent.vSpeed = floatSpeed;
					currentTime = 0;
				}	
				break;
			case JumpState.floating:
				parent.vSpeed -= floatGravity;
				if (currentTime >= floatTime) {
					jumpState = JumpState.falling;
				}
				break;
			case JumpState.falling:
				if (blocksBeneath.Count == 0) {
					parent.vSpeed -= fallGravity;
					if (parent.vSpeed < -maxFallSpeed) {
						parent.vSpeed = -maxFallSpeed;
					}
				}
				break;
		}
	}
}
