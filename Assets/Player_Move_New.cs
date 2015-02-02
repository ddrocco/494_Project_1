using UnityEngine;
using System.Collections;

public class Player_Move_New : MonoBehaviour {
	public Sprite stand, walk1, walk2, walk3, jump, floating;
	public float xDist = 0.15f;
	public int framesPerStep = 2;
	public int currStep = 0;
	private int stepState = 0;
	
	public float ySpeed = 0;
	public float jumpSpeed = 0.27f;
	public float gravity = .5f;
	public float maxHeight = 3f;
	public float groundHeight = 0;
	public float startSlowing = .05f;
	
	public bool grounded = true;
	
	public int framesPerJumpStep = 10;
	public int currJumpStep = 0;
	public float yDist = 0.15f;
			
	void Start() {
		groundHeight = transform.position.y;
	}
	
	void Update () {
		if (Input.GetKey("q")) {
			transform.localScale = new Vector3(-1f,transform.localScale.y,1f);
			takeStep (-1);
		} else if (Input.GetKey("e")) {
			transform.localScale = new Vector3(1f,transform.localScale.y,1f);
			takeStep (1);
		} else if (Input.GetKeyUp ("q")) {
			finishStep(-1);
		} else if (Input.GetKeyUp("e")) {
			finishStep (1);
		} else if (Input.GetKeyDown(KeyCode.Space)) {
			if (grounded) {
				ySpeed = jumpSpeed;
				grounded = false;
			}
			/*ySpeed = jumpSpeed;
			grounded = false;
			jumpSmooth();*/
			//takeJump ();
		} else if (Input.GetKeyUp (KeyCode.Space)) {
			if (ySpeed > .5f*jumpSpeed) ySpeed = .5f*jumpSpeed;
		}
		if (!grounded) {
		ySpeed -= gravity;
		transform.Translate(Vector3.up * ySpeed);}
		
		/*if (!grounded) {
			jumpSmooth();
		}*/
	}
	
	void jumpSmooth() {
		transform.Translate(Vector3.up * ySpeed);
		float currHeight = transform.position.y - groundHeight;
		if (maxHeight - currHeight < startSlowing) {
			ySpeed -= gravity;
		}
		return;
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == Layerdefs.blockThick) {
			ySpeed = 0f;
			grounded = true;
			groundHeight = transform.position.y;
		}
	}
	
	void takeJump () {
		if (currJumpStep < 4*framesPerJumpStep) {
			GetComponent<SpriteRenderer>().sprite = jump;
			transform.Translate(Vector3.up * yDist);
		} else if (currJumpStep < 6*framesPerJumpStep) {
			transform.Translate(Vector3.up * .5f * yDist);
		} else if (currJumpStep < 7*framesPerJumpStep) {
			GetComponent<SpriteRenderer>().sprite = floating;
			transform.Translate(Vector3.up * .1f * yDist);
		} else if (currJumpStep < 8*framesPerJumpStep) {
			transform.Translate(Vector3.up * -.1f * yDist);
		} else if (currJumpStep < 10*framesPerJumpStep) {
			transform.Translate(Vector3.up * -.5f * yDist);
		} else if (currJumpStep < 14*framesPerJumpStep) {
			transform.Translate(Vector3.up * -1f * yDist);
		} else if (currJumpStep < 16*framesPerJumpStep) {
			grounded = true;
			GetComponent<SpriteRenderer>().sprite = stand;
			transform.Translate(Vector3.up * 0);
			currJumpStep = 0;
			return;
		}
		++currJumpStep;
		return;
	}
	
	void takeStep (int dir) {
		if (currStep == framesPerStep) {
			transform.Translate (Vector3.right * dir * xDist);
			GetComponent<SpriteRenderer>().sprite = walk1;
			stepState = 1;
		} else if (currStep == 2*framesPerStep) {
			transform.Translate (Vector3.right * dir * xDist);
			GetComponent<SpriteRenderer>().sprite = walk2;
			stepState = 2;
		} else if (currStep == 3*framesPerStep) {
			transform.Translate (Vector3.right * dir * xDist);
			GetComponent<SpriteRenderer>().sprite = walk3;
			stepState = 3;
		} else if (currStep == 4*framesPerStep) {
			transform.Translate (Vector3.right * dir * xDist);
			GetComponent<SpriteRenderer>().sprite = stand;
			stepState = 0;
			currStep = -1;
		}
		++currStep;
		return;
	}
	
	void finishStep(int dir) {
		if (stepState != 0) {
			transform.Translate (Vector3.right * dir * xDist);
			GetComponent<SpriteRenderer>().sprite = stand;
			stepState = 0;
			currStep = 0;
			return;
		}
		else {
			currStep = 0;
			return;
		}
	}
	
}
