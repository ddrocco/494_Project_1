using UnityEngine;
using System.Collections;
using System;

public class Player_Physics : MonoBehaviour {
	//Variable walking
	public float vSpeed = 0;
	public float hSpeed = 0;
	public float hSpeedMax = 0.1f;
	public float hFriction = 0.05f;
	public float hAcceleration = 0.8f;
	
	
	//Variable jumping
	public float fallSpeedMax = 0.15f;
	public float fallGravity = 0.3f;
	public float floatGravity = 0.005f;
	public int jumpTimeMin = 10; //in steps
	public int jumpTimeMax = 14; //in steps
	public int floatTime = 15; //in steps
	public float jumpSpeed = 0.23f;
	public float floatSpeed = 0.005f;
	
	public float groundTime = 4;
	public float groundTimeMax = 4;
	public int ceilingHitStunTime = 15;
	public int ceilingHitTimer = 15;
	
	//Control for walking and jumping
	public enum jumpState {
		floating,
		jumping,
		falling
	}
	public jumpState state;
	public int currentJumpTime;
	public enum dirState {
		sideways,
		upwards,
		crouching
	}
	public dirState facing;
	public bool isLookingRight;
	public bool grounded = false;
	public bool buttonHeld = false;
	private float newVSpeed = 0;
	private float newHSpeed = 0;
	
	// Update is called once per frame
	void FixedUpdate () {
		updateHSpeed();
		updateVSpeed();
		updateHLocation();
		updateVLocation();
	}
	
	void updateHSpeed() {
		hSpeed = newHSpeed;
		if (grounded) {	//"Friction"
			if (hSpeed > hFriction) {
				hSpeed -= hFriction;
			} else if (hSpeed > -hFriction) {
				hSpeed = 0;
			} else {
				hSpeed += hFriction;
			}
		}
		
		if ((Input.GetKey ("up") || Input.GetKey ("w"))) {
			facing = dirState.upwards;
		} else {
			if ((Input.GetKey ("down") || Input.GetKey ("s"))) {
				facing = dirState.crouching;
			} else {
				facing = dirState.sideways;
			}
			
			if ((Input.GetKey ("right") || Input.GetKey ("d"))) {
				hSpeed += hAcceleration;
				isLookingRight = true;
			}
			if ((Input.GetKey ("left") || Input.GetKey ("a"))) {
				hSpeed -= hAcceleration;
				isLookingRight = false;
			}
			
			if (hSpeed > hSpeedMax) {
				hSpeed = hSpeedMax;
			} else if (hSpeed < -hSpeedMax) {
				hSpeed = -hSpeedMax;
			}
		}
		
		if (hSpeed > 0) {
			transform.localScale = new Vector3(1f,1.5f,1f);
		} else if (hSpeed < 0) {
			transform.localScale = new Vector3(-1f,1.5f,1f);
		}
		newHSpeed = hSpeed;
	}
	
	void updateHLocation() {
		if (++ceilingHitTimer < ceilingHitStunTime) {
			return;
		}
		transform.Translate (Vector3.right * hSpeed);
		if (++groundTime >= groundTimeMax)
		{
			grounded = false;
		}
	}
	
	void updateVSpeed() {
		vSpeed = newVSpeed;
		
		if (facing == dirState.sideways) {
			if ((Input.GetKeyDown (".") || Input.GetKeyDown ("x")) && grounded == true) {
				if (state != jumpState.jumping) {
					buttonHeld = true;
				}
				state = jumpState.jumping;
				currentJumpTime = 0;
			} else if ((!Input.GetKey (".") && !Input.GetKey ("x"))) {
				buttonHeld = false;
			}
		}
		
		if (state == jumpState.falling) {
			vSpeed -= fallGravity;
			if (vSpeed > fallSpeedMax) {
				vSpeed = fallSpeedMax;
			} else if (vSpeed < -fallSpeedMax) {
				vSpeed = -fallSpeedMax;
			}
		} else if (state == jumpState.floating) {
			vSpeed -= floatGravity;
			if (++currentJumpTime >= floatTime) {
				state = jumpState.falling;
			}
		} else if (state == jumpState.jumping) {
			vSpeed = jumpSpeed;
			if (++currentJumpTime >= jumpTimeMax ||
						(currentJumpTime >= jumpTimeMin && !buttonHeld)) {
				state = jumpState.floating;
				vSpeed = floatSpeed;
				currentJumpTime = 0;
			}
		}
		
		newVSpeed = vSpeed;
		return;
	}
	
	void updateVLocation() {
		if (++ceilingHitTimer < ceilingHitStunTime) {
			return;
		}
		transform.Translate (Vector3.up * vSpeed);
		return;
	}
	
	void OnTriggerEnter (Collider other) {
		Vector3 otherLoc = other.gameObject.transform.position;
		float leftEdge = otherLoc.x - 0.5f;
		float rightEdge = otherLoc.x + 0.5f;
		float bottomEdge = otherLoc.y - 0.5f;
		float topEdge = otherLoc.y + 0.5f;
		float xLoc = transform.position.x;
		float yLoc = transform.position.y;
		bool above = false;
		bool right = false;
		float xEntryRatio = float.MaxValue;
		float yEntryRatio = float.MaxValue;
		float widthOffset = Math.Abs (transform.localScale.x) / 2f;
		float heightOffset = Math.Abs (transform.localScale.y) / 2f;
		
		if (hSpeed < -0.01f) { //moving left, use rightedge
			right = true;
			float objEdge = xLoc - widthOffset;
			xEntryRatio = (rightEdge - objEdge) / -hSpeed;
		} else if (hSpeed > 0.01f) {
			right = false;
			float objEdge = xLoc + widthOffset;
			xEntryRatio = (objEdge - leftEdge) / hSpeed;
		}
		if (vSpeed < 0) { //falling, use topedge
			above = true;
			float objEdge = yLoc - heightOffset;
			yEntryRatio = (topEdge - objEdge) / -vSpeed;
		} else if (vSpeed > 0) { //ascending, use bottomedge
			above = false;
			float objEdge = yLoc + heightOffset;
			yEntryRatio = (objEdge - bottomEdge) / vSpeed;
		}

		
		if (xEntryRatio > yEntryRatio) { //y "entered" first
			Ground (); //See below for funciton details
			//state = jumpState.floating;
			if (above == true) {
				//resolve collision from top
				//print (xEntryRatio + " " + yEntryRatio + " " + "TOP COLLISION");
				transform.Translate (Vector3.up *
				                     (topEdge - yLoc + 1.01f * heightOffset));
			} else {
				//resolve collision from bottom
				//print (xEntryRatio + " " + yEntryRatio + " " + "BOTTOM COLLISION");
				transform.Translate (Vector3.up *
				                     (bottomEdge - yLoc - 1.01f * heightOffset));
				ceilingHitTimer = 0;
			}
		} else if (xEntryRatio < yEntryRatio){
			newHSpeed = 0;
			if (right == true) {
				//resolve collision from right
				//print (xEntryRatio + " " + yEntryRatio + " " + "RIGHT COLLISION");
				transform.Translate (Vector3.right *
             				(rightEdge - xLoc + 1.01f * widthOffset));
			} else {
				//resolve collision from left
				//print (xEntryRatio + " " + yEntryRatio + " " + "LEFT COLLISION");
				transform.Translate (Vector3.right *
             				(leftEdge - xLoc - 1.01f * widthOffset));
			}
		} else {
			newHSpeed = 0;
			Ground ();
			
		}
	return;
	}
	
	//CALLED BY: OnTriggerEnter()
	//	Sets all variables relevant to grounding
	void Ground() {
		newVSpeed = 0;
		grounded = true;
		groundTime = 0;
		state = jumpState.floating;
		currentJumpTime = 0;
	}
	
	void OnTriggerStay (Collider other) {
		OnTriggerEnter(other);
	}
}
