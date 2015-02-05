using UnityEngine;
using System.Collections;

public class Player_Physics_Final : MonoBehaviour {
	//Sprites
	public Sprite stand, walk1, walk2, walk3, jump, floating, crouch, faceUp, dead;
	public Sprite Gstand, Gwalk1, Gwalk2, Gwalk3, Gjump, Gfloating, Gcrouch, GfaceUp, Gdead;
	public static Sprite duckingPit, upwardPit;
	public BoxCollider boxCollider;
	public Vector3 colliderSize;
	
	//Horizontal movement variables
	public float hMaxSpeed = 0.15f;
	public float hSpeed = 0f;
	public float hStartSpeed = .08f;
	public float hAirMaxSpeed = .07f;
	public float hAirStartSpeed = .04f;
	public int walkTime = 0;
	public int walkStartTime = 4;
	public int walkStopTime = 7;
	public int framesPerStep = 2;
	public int currStep = 0;
	
	//Vertical movement variables
	public float vSpeed = 0f;
	public float jumpSpeed = .22f;
	public int currentJumpTime = 0;
	public int jumpTimeMax = 5;
	public int jumpTimeMin = 3;
	public int thisJumpTime = 0;
	public float groundTime = 4;
	public float groundTimeMax = 4;
	public int ceilingHitStunTime = 15;
	public int ceilingHitTimer = 15;

	//State variables
	public enum jumpState {
		fastJumping,
		midFastJumping,
		midSlowJumping,
		slowJumping,
		floatingUp,
		floatingDown,
		slowFalling,
		midSlowFalling,
		midFastFalling,
		freeFalling,
		onGround
	}
	public enum walkState {
		starting,
		walking,
		slowDown,
		stopping,
		standing
	}
	public enum dirState {
		sideways,
		upwards,
		crouching
	}
	public static dirState facing = dirState.sideways;
	public dirState showFace = facing;
	public walkState wState = walkState.standing;
	public jumpState jState = jumpState.onGround;
	public bool upPressed = false;
	public bool downPressed = false;
	public bool leftPressed = false;
	public bool rightPressed = false;
	public bool jumpPressed = false;
	public static bool isDead = false;
	public bool startJump = false;
	public bool startWalk = false;
	public bool startSideJump = false;
	public bool grounded = false;
	public bool underSomething = false;
	public static int faceDir = 1;
			
	void Start() {
		boxCollider = GetComponent<BoxCollider>() as BoxCollider;
		colliderSize = boxCollider.size;
		UpdateSpriteColor();
	}

	void Update() {	
		GetHeldKeys();
		GetNewKeyPresses();
		MakeRayCasts();
		showFace = facing;
	}
	
	void FixedUpdate () {
		if (isDead) {
			GetComponent<SpriteRenderer>().sprite = dead;
			transform.Translate (-0.1f * Vector3.up);
			return;
		}
		UpdateFacing();
		UpdateHSpeed();
		UpdateVSpeed();
		UpdateHLocation();
		UpdateVLocation();
	}

	void GetHeldKeys() {
		upPressed = false;
		downPressed = false;
		leftPressed = false;
		rightPressed = false;
		if ((Input.GetKey (".") || Input.GetKey ("x")) == false)
			jumpPressed = false;
		if (Input.GetKey ("down") || Input.GetKey ("s"))
			downPressed = true;
		if ((Input.GetKey ("up") || Input.GetKey ("w")) && downPressed == false)
			upPressed = true;
		if (Input.GetKey ("right") || Input.GetKey ("d")) {
			faceDir = 1;
			rightPressed = true;
		}
		if (Input.GetKey ("left") || Input.GetKey ("a")) {
			faceDir = -1;
			leftPressed = true;
		}
		return;
	}
	
	void GetNewKeyPresses() {
		if (upPressed == true) return;
		if (downPressed == true) return;
		if ((Input.GetKeyDown (".") || Input.GetKeyDown ("x")) && grounded) {
			startJump = true;
			jumpPressed = true;
		}
		return;
	}
	
	void MakeRayCasts() {
		RaycastHit hitInfo1, hitInfo2, hitInfo3, hitInfo4;
		float vDist = .45f;
		float rLength = .5f;
		if (faceDir == -1) rLength += .075f;
		float lLength = .5f;
		if (faceDir == 1) lLength += .075f;
//		float hDist = 0;
//		if (faceDir == -1) hDist = .075f;
		if (facing == dirState.crouching) vDist = .25f;
//		Debug.DrawRay(transform.position + new Vector3(.4f + hDist, .35f, 0), Vector3.up * .75f);
//		Debug.DrawRay(transform.position + new Vector3(-.475f + hDist, .35f, 0), Vector3.up * .75f);
//		Debug.DrawRay(transform.position + new Vector3(hDist, .35f, 0), Vector3.up * .75f);
		Debug.DrawRay(transform.position + new Vector3(0, vDist, 0), Vector3.right * rLength);
		Debug.DrawRay(transform.position + new Vector3(0, -vDist, 0), Vector3.right * rLength);
		Debug.DrawRay(transform.position + new Vector3(0, vDist, 0), Vector3.left * lLength);
		Debug.DrawRay(transform.position + new Vector3(0, -vDist, 0), Vector3.left * lLength);
//		Debug.DrawRay(transform.position, Vector3.down * .75f); 
		
		//possibly 3 raycasts, top middle bottom
		if (Mathf.Abs (hSpeed) > 0 &&
		    (Physics.Raycast(transform.position + new Vector3(0, vDist, 0),
		                 Vector3.right, out hitInfo1, rLength) ||
			 Physics.Raycast(transform.position + new Vector3(0, -vDist, 0),
		                Vector3.right, out hitInfo2, rLength) ||
			 Physics.Raycast(transform.position + new Vector3(0, vDist, 0),
		                Vector3.left, out hitInfo3, lLength) ||
			 Physics.Raycast(transform.position + new Vector3(0, -vDist, 0),
		                Vector3.left, out hitInfo4, lLength))) {
		                print ("rays");
			if (hitInfo1.collider != null) {
				if (hitInfo1.collider.gameObject.layer == Layerdefs.blockThick) {
					BlockCollision(hitInfo1.collider);
					print ("1");
				}
				else if (hitInfo1.collider.gameObject.layer == Layerdefs.blockThin
			         	 && facing != dirState.crouching){
					BlockCollision(hitInfo1.collider, thinBlock:true);
					print ("1b");	
				}
			} else if (hitInfo2.collider != null) {
			    if (hitInfo2.collider.gameObject.layer == Layerdefs.blockThick){
					BlockCollision(hitInfo2.collider);
					print ("2");
				}
		        else if (hitInfo2.collider.gameObject.layer == Layerdefs.blockThin
			         && facing != dirState.crouching){
					BlockCollision(hitInfo2.collider, thinBlock:true);
					print ("2b");	
				}
			} else if (hitInfo3.collider != null) {
			    if (hitInfo3.collider.gameObject.layer == Layerdefs.blockThick){
					BlockCollision(hitInfo3.collider);
					print ("3");
				}
			    else if (hitInfo3.collider.gameObject.layer == Layerdefs.blockThin
			         && facing != dirState.crouching){
					BlockCollision(hitInfo3.collider, thinBlock:true);
					print ("3b");
				}
			} else if (hitInfo4.collider != null) {
			    if (hitInfo4.collider.gameObject.layer == Layerdefs.blockThick){
					BlockCollision(hitInfo4.collider);
					print ("4");
				}
			    else if (hitInfo4.collider.gameObject.layer == Layerdefs.blockThin
			         && facing != dirState.crouching){
					BlockCollision(hitInfo4.collider, thinBlock:true);
					print ("4b");
				}
			}
		}
	}

	public void UpdateSpriteColor () {
		//print ("Squee");
		if (Player_Shoot.hasSuperArrow == false) {
			duckingPit = crouch;
			upwardPit = faceUp;
		} else {
			stand = Gstand;
			duckingPit = Gcrouch;
			upwardPit = GfaceUp;
			dead = Gdead;
			walk1 = Gwalk1;
			walk2 = Gwalk2;
			walk3 = Gwalk3;
			jump = Gjump;
			floating = Gfloating;
		}
	}

	void UpdateFacing() {
		if (upPressed) {
			if (facing == dirState.crouching) {
				boxCollider.size = new Vector3(colliderSize.x, colliderSize.y, 1f);
				boxCollider.center = new Vector3(0, 0, 0);
				transform.Translate (Vector3.up * 0.3f);
			}
			facing = dirState.upwards;
			GetComponent<SpriteRenderer>().sprite = faceUp;
		} else if (downPressed) {
			if (facing != dirState.crouching) {
				facing = dirState.crouching;
				GetComponent<SpriteRenderer>().sprite = crouch;
				boxCollider.size = new Vector3(colliderSize.x, colliderSize.y * .6f, 1f);
				boxCollider.center = new Vector3(0, -.05f, 0);
				transform.Translate (Vector3.down * 0.3f);
			}
		} else {
			if (facing == dirState.crouching) {
				facing = dirState.sideways;
				GetComponent<SpriteRenderer>().sprite = stand;
				boxCollider.size = new Vector3(colliderSize.x, colliderSize.y, 1f);
				boxCollider.center = new Vector3(0, 0, 0);
				transform.Translate (Vector3.up * 0.3f);
			}
			if (facing == dirState.upwards) {
				facing = dirState.sideways;
				GetComponent<SpriteRenderer>().sprite = stand;
			}
			if (leftPressed) faceDir = -1;
			if (rightPressed) faceDir = 1;
		}
		return;
	}

	void UpdateHSpeed () {
		if ((leftPressed || rightPressed) && wState == walkState.standing) {
			if (grounded) startWalk = true;
			else startSideJump = true;
		}
		if (!leftPressed && !rightPressed && wState == walkState.walking) {
			wState = walkState.slowDown;
			walkTime = 0;
		}
		if (leftPressed && rightPressed) faceDir = -1;
		if (startWalk) {
			startWalk = false;
			wState = walkState.starting;
			walkTime = 0;
		}
		if (startSideJump) {
			startSideJump = false;
			wState = walkState.starting;
			walkTime = 0;
		}
		if (grounded && facing == dirState.upwards) {
			wState = walkState.standing;
		}
		
		if (wState == walkState.standing)
			hSpeed = 0;
		else if (wState == walkState.starting) {
			if (grounded) hSpeed = hStartSpeed * faceDir;
			if (++walkTime >= walkStartTime) {
				wState = walkState.walking;
				walkTime = 0;
			}
		} else if (wState == walkState.walking) {
			if (grounded) hSpeed = hMaxSpeed * faceDir;
			else hSpeed = hAirMaxSpeed * faceDir;
		} else if (wState == walkState.stopping) {
			if (grounded) hSpeed = hStartSpeed * faceDir;
			else hSpeed = hAirStartSpeed * faceDir;
			if (++walkTime >= walkStartTime) {
				wState = walkState.standing;
				if (facing != dirState.crouching)
					GetComponent<SpriteRenderer>().sprite = stand;
				hSpeed = 0;
				walkTime = 0;
			}
		} else if (wState == walkState.slowDown) {
			if (grounded) {
				hSpeed = hMaxSpeed * faceDir;
				if (++walkTime >= walkStopTime) {
					if (leftPressed && rightPressed) wState = walkState.starting;
					else wState = walkState.stopping;
					walkTime = 0;
				}
			}
			else hSpeed = hAirMaxSpeed * faceDir;
		}
		
		if (hSpeed > 0) {
			transform.localScale = new Vector3(1f,transform.localScale.y,1f);
		} else if (hSpeed < 0) {
			transform.localScale = new Vector3(-1f,transform.localScale.y,1f);
		}
		
		return;
	}

	void UpdateVSpeed () {
		if (facing == dirState.sideways && startJump) {
			startJump = false;
			jState = jumpState.fastJumping;
			grounded = false;
			currentJumpTime = 0;
		}

	
		if (jState == jumpState.onGround) {
			vSpeed -= .001f;
			if (++currentJumpTime >= 15) {
				//what speed does he begin falling at?
				jState = jumpState.floatingDown;
				thisJumpTime = 3;
				currentJumpTime = 0;
			}
		} else if (jState == jumpState.fastJumping) {
			if (facing == dirState.sideways)
				GetComponent<SpriteRenderer>().sprite = jump;
			vSpeed = jumpSpeed;
			if (++currentJumpTime >= jumpTimeMax ||
			    (currentJumpTime >= jumpTimeMin && !jumpPressed)) {
				jState = jumpState.midFastJumping;
				thisJumpTime = currentJumpTime;
				currentJumpTime = 0;
			}
		} else if (jState == jumpState.midFastJumping) {
			if (facing == dirState.sideways)
				GetComponent<SpriteRenderer>().sprite = jump;
			vSpeed = .83f * jumpSpeed;
			if (++currentJumpTime >= thisJumpTime) {
				jState = jumpState.midSlowJumping;
				currentJumpTime = 0;
			}
		} else if (jState == jumpState.midSlowJumping) {
			if (facing == dirState.sideways)
				GetComponent<SpriteRenderer>().sprite = jump;
			vSpeed = .66f * jumpSpeed;
			if (++currentJumpTime >= thisJumpTime) {
				if (thisJumpTime > 4) jState = jumpState.slowJumping;
				else jState = jumpState.slowJumping;
				currentJumpTime = 0;
			}
		} else if (jState == jumpState.slowJumping) { 
			if (facing == dirState.sideways)
				GetComponent<SpriteRenderer>().sprite = jump;
			vSpeed = .33f * jumpSpeed;
			if (++currentJumpTime >= thisJumpTime) {
				jState = jumpState.floatingUp;
				currentJumpTime = 0;
			}
		} else if (jState == jumpState.floatingUp) {
			if (facing == dirState.sideways)
				GetComponent<SpriteRenderer>().sprite = floating;
			vSpeed = .17f * jumpSpeed;
			if (++currentJumpTime >= thisJumpTime) {
				jState = jumpState.floatingDown;
				currentJumpTime = 0;
			}
		} else if (jState == jumpState.floatingDown) {
			if (facing == dirState.sideways && !grounded)
				GetComponent<SpriteRenderer>().sprite = floating;
			vSpeed = -.17f * jumpSpeed;
			if (++currentJumpTime >= thisJumpTime) {
				jState = jumpState.slowFalling;
				currentJumpTime = 0;
			}
		} else if (jState == jumpState.slowFalling) {
			if (facing == dirState.sideways)
				GetComponent<SpriteRenderer>().sprite = floating;
			vSpeed = -.25f * jumpSpeed;
			if (++currentJumpTime >= thisJumpTime) {
				jState = jumpState.midSlowFalling;
				currentJumpTime = 0;
			}
		} else if (jState == jumpState.midSlowFalling) {
			if (facing == dirState.sideways)
				GetComponent<SpriteRenderer>().sprite = floating;
			vSpeed = -.5f * jumpSpeed;
			if (++currentJumpTime >= thisJumpTime) {
				if (thisJumpTime > 4) jState = jumpState.midFastFalling;
				else jState = jumpState.freeFalling;
				currentJumpTime = 0;
			}
		} else if (jState == jumpState.midFastFalling) {
			if (facing == dirState.sideways)
				GetComponent<SpriteRenderer>().sprite = floating;
			vSpeed = -.83f * jumpSpeed;
			if (++currentJumpTime >= thisJumpTime) {
				jState = jumpState.freeFalling;
				currentJumpTime = 0;
			}
		} else if (jState == jumpState.freeFalling) {
			if (facing == dirState.sideways)
				GetComponent<SpriteRenderer>().sprite = floating;
			vSpeed = -jumpSpeed;
		}
		return;
	}

/*	float calcHeight(float x) {
		return -1.465f * Mathf.Pow (10, -14) * Mathf.Pow (x, 5)
			   - 2.914f * Mathf.Pow (10, -4) * Mathf.Pow (x, 4)
			   + 5.828f * Mathf.Pow (10, -3) * Mathf.Pow (x, 3)
			   - 3.721f * Mathf.Pow (10, -1) * Mathf.Pow (x, 2)
			   + 3.429f * x - 1.049 * Mathf.Pow (10, -2);
	}*/

	void UpdateVLocation() {
		if (++ceilingHitTimer < ceilingHitStunTime) {
			return;
		}		
		transform.Translate (Vector3.up * vSpeed);
		return;
	}

	void UpdateHLocation () {
		if (++ceilingHitTimer < ceilingHitStunTime) {
			return;
		}
		if (wState == walkState.standing) return;
		
		if (!grounded) {
			transform.Translate (Vector3.right * hSpeed);
			if (facing == dirState.upwards)
				GetComponent<SpriteRenderer>().sprite = faceUp;
		} else {
			if (currStep == framesPerStep) {
				transform.Translate (Vector3.right * hSpeed);
				if (facing != dirState.crouching)
					GetComponent<SpriteRenderer>().sprite = walk1;
			} else if (currStep == 2*framesPerStep) {
				transform.Translate (Vector3.right * hSpeed);
				if (facing != dirState.crouching)
					GetComponent<SpriteRenderer>().sprite = walk2;
			} else if (currStep == 3*framesPerStep) {
				transform.Translate (Vector3.right * hSpeed);
				if (facing != dirState.crouching)
					GetComponent<SpriteRenderer>().sprite = walk3;
			} else if (currStep == 4*framesPerStep) {
				transform.Translate (Vector3.right * hSpeed);
				if (facing != dirState.crouching)
					GetComponent<SpriteRenderer>().sprite = stand;
				currStep = -1;
			}
			++currStep;
		}
		
		if (++groundTime >= groundTimeMax) grounded = false;

		return;
	}
	
	void OnTriggerEnter (Collider other) {
		//print ("trigger");
//		if (ceilingHitTimer < ceilingHitStunTime) return;
		if (isDead)	return;
		if (other.gameObject.layer == Layerdefs.blockThick) {
			BlockCollision (other);
		} else if (other.gameObject.layer == Layerdefs.blockThin
		           && facing != dirState.crouching) {
			BlockCollision(other, thinBlock:true);
		}
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
		float widthOffset = Mathf.Abs (boxCollider.size.x) / 2f;
		float heightOffset = Mathf.Abs (boxCollider.size.y) / 2f;
		
		//Ignores thinblock collisions unless Pit is in a narrow collision window:
		if (thinBlock == true
		    && (topEdge + heightOffset - yLoc < 0f
		    || topEdge + heightOffset - yLoc > 0.6f))
			return ;
		if (hSpeed < -0.01f) { //moving left, use rightedge
			right = true;
			float objEdge = xLoc - widthOffset;
			xEntryRatio = (rightEdge - objEdge) / -hSpeed;
		} else if (hSpeed > 0.01f) { //moving right, use leftedge
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
			if (above == true) {
				//resolve ground collision
				Ground ();
				transform.Translate (Vector3.up *
				                     (topEdge - yLoc + 1.01f * heightOffset));
			} else {
				if (other.gameObject.layer == Layerdefs.blockThin || underSomething)
					return;
				//resolve ceiling collision
				transform.Translate (Vector3.up *
				                     (bottomEdge - yLoc - 1.01f * heightOffset));
				ceilingHitTimer = 0;
				jState = jumpState.floatingDown;
			}
		} else if (xEntryRatio < yEntryRatio) { //x "entered" first 
			//print ("such collision");
			if (other.gameObject.layer == Layerdefs.blockThin)
				return;
			if (right == true) {
				//resolve collision at Pit's left
				transform.Translate (Vector3.right *
				                     (rightEdge - xLoc + 1.01f * widthOffset));
			} else {
				//resolve collision at Pit's right
				transform.Translate (Vector3.right *
				                     (leftEdge - xLoc - 1.01f * widthOffset));
			}
		} else
			Ground ();
		return;
	}
	
	//CALLED BY: OnTriggerEnter()
	//	Sets all variables relevant to grounding
	void Ground() {
		grounded = true;
		groundTime = 0;
		if (jState == jumpState.freeFalling && facing == dirState.sideways)
			GetComponent<SpriteRenderer>().sprite = stand;
		jState = jumpState.floatingDown;
		currentJumpTime = 0;
		return;
	}
	
	void OnTriggerStay (Collider other) {
		OnTriggerEnter(other);
	}

}
