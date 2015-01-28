using UnityEngine;
using System.Collections;
using System;

public class Player_Physics : MonoBehaviour {
	//For teleportin
	private float teleTimer = 0;
	
	//Variable walking
	public float vSpeed = 0;
	public float hSpeed = 0;
	public float hSpeedMax = 0.1f;
	public float hFriction = 0.05f;
	public float hAccelerationGround = 0.8f;
	public float hAccelerationAir = 0.1f;
	
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
	public static dirState facing;
	public static bool isLookingRight = true;
	public bool grounded = false;
	private float newVSpeed = 0;
	private float newHSpeed = 0;
	
	//Key press controls
	public bool startJump;
	public bool jumpPressed;
	public bool upPressed;
	public bool downPressed;
	public bool rightPressed;
	public bool leftPressed;
	
	public static bool isDead = false;
	
	//For getting keys
	void Update() {
		GetHeldKeys();
		GetNewKeyPresses();
	}
	
	void GetHeldKeys() {
		upPressed = false;
		downPressed = false;
		leftPressed = false;
		rightPressed = false;
		if ((Input.GetKey (".") || Input.GetKey ("x")) == false) {
			jumpPressed = false;
		}
		if (Input.GetKey ("up") || Input.GetKey ("w")) {
			//Take precedence over everything
			upPressed = true;
			return;
		}
		if ((Input.GetKey ("down") || Input.GetKey ("s"))) {
			downPressed = true;
		}
		if ((Input.GetKey ("right") || Input.GetKey ("d"))) {
			rightPressed = true;
		}
		if ((Input.GetKey ("left") || Input.GetKey ("a"))) {
			leftPressed = true;
		}
	}
	
	void GetNewKeyPresses() {
		if (upPressed == true) {
			return;
		}
		if ((Input.GetKeyDown ("right") || Input.GetKeyDown ("d")) && Player_Physics.isLookingRight == false) {
			leftPressed = false;
			isLookingRight = true;
		}
		if ((Input.GetKeyDown ("left") || Input.GetKeyDown ("a")) && Player_Physics.isLookingRight == true) {
			rightPressed = false;
			isLookingRight = false;
		}
		if (downPressed == true) {
			return;
		}
		if ((Input.GetKeyDown (".") || Input.GetKeyDown ("x")) && grounded == true) {
			startJump = true;
			jumpPressed = true;
		}
	}
	
	//For running game
	void FixedUpdate () {
		if (isDead) {
			transform.Translate (-0.1f * Vector3.up);
			return;
		}
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
		
		if (upPressed == true) {
			if (facing == dirState.crouching) {
				transform.localScale = new Vector3(transform.localScale.x, 1.5f, 1f);
				transform.Translate (Vector3.up * 0.25f);
			}
			facing = dirState.upwards;
		} else {
			if (downPressed == true) {
				if (facing != dirState.crouching) {
					facing = dirState.crouching;
					transform.localScale = new Vector3(transform.localScale.x, 1f, 1f);
					transform.Translate (Vector3.down * 0.25f);
				}
			} else {
				if (facing == dirState.crouching) {
					transform.localScale = new Vector3(transform.localScale.x, 1.5f, 1f);
					transform.Translate (Vector3.up * 0.25f);
				}
				facing = dirState.sideways;
			}
			
			if (rightPressed == true) {
				if (grounded == true) {
					hSpeed += hAccelerationGround;
				} else {
					hSpeed += hAccelerationAir;
				}
			}
			if (leftPressed == true) {
				if (grounded == true) {
					hSpeed -= hAccelerationGround;
				} else {
					hSpeed -= hAccelerationAir;
				}
			}
			
			if (hSpeed > hSpeedMax) {
				hSpeed = hSpeedMax;
			} else if (hSpeed < -hSpeedMax) {
				hSpeed = -hSpeedMax;
			}
		}
		
		if (hSpeed > 0) {
			transform.localScale = new Vector3(1f,transform.localScale.y,1f);
		} else if (hSpeed < 0) {
			transform.localScale = new Vector3(-1f,transform.localScale.y,1f);
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
		
		if (facing == dirState.sideways && startJump == true) {
			startJump = false;
			state = jumpState.jumping;
			currentJumpTime = 0;
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
						(currentJumpTime >= jumpTimeMin && !jumpPressed)) {
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
		if (isDead) {
			return;
		}
		
		if (other.gameObject.layer == Layerdefs.blockThick) {
			BlockCollision (other);
		} else if (other.gameObject.layer == Layerdefs.blockThin
				&& facing != dirState.crouching) {
			BlockCollision(other, thinBlock:true);
		} /*else if (other.gameObject.layer == Layerdefs.door) {
//			if (Time.time - teleTimer > 1) {
				print ("teleport!");
				teleTimer = Time.time;
				Testing_Door temp = other.GetComponent<Testing_Door>();
				Vector3 newpos = temp.dest.transform.position;
				newpos.x += temp.xDisp;
				Vector3 newCamPos = Camera.main.transform.position;
				//float xDisp = transform.position.x - newpos.x;
				//float yDisp = transform.position.y - newpos.y;
				//newCamPos.x -= xDisp;
				//newCamPos.y -= yDisp;
				newCamPos.x += 11 * -1 * (temp.xDisp/1.1f);
				transform.position = newpos;
				//Camera.main.transform.position = newCamPos;
//			} else print ("no teleport :(");
		}*/
	}
	
	void BlockCollision(Collider other, bool thinBlock = false) {
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
		
		//Ignores thinblock collisions unless Pit is in a narrow collision window:
		if (thinBlock == true
				&& (topEdge + heightOffset - yLoc < 0f
		    	|| topEdge + heightOffset - yLoc > 0.6f)) {
			return ;
		}
		
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
				if (other.gameObject.layer == Layerdefs.blockThin) {
					return;
				}
				//resolve collision from bottom
				//print (xEntryRatio + " " + yEntryRatio + " " + "BOTTOM COLLISION");
				transform.Translate (Vector3.up *
				                     (bottomEdge - yLoc - 1.01f * heightOffset));
				ceilingHitTimer = 0;
			}
		} else if (xEntryRatio < yEntryRatio){
			if (other.gameObject.layer == Layerdefs.blockThin) {
				return;
			}
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
