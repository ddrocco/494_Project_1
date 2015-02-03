using UnityEngine;
using System.Collections;

public class Player_Sprite_Control : MonoBehaviour {

	public Sprite currentSprite, standingPit, wDuckingPit, wUpwardPit, dyingPit, walk1, walk2, walk3, jump1, jump2,
		gStandingPit, gDuckingPit, gUpwardPit, gDyingPit, gWalk1, gWalk2, gWalk3, gJump1, gJump2;
		
	public static Sprite duckingPit, upwardPit;
	
	public int totalAnimationSteps = 2;
	public static int currentAnimationSteps = 0;
	public static bool isWalkingRight;
	
	void Start() {
		UpdateSpriteColor()	;
	}
	
	public void FixedUpdate() {
		//print (Time.time + " " + currentAnimationSteps);
		//print(currentAnimationSteps);
		
		if (Player_Physics.isDead) {
			return;
		}
		
		UpdateAnimationSteps();
		//print (currentAnimationSteps);
		
		if ((Player_Physics.facing != Player_Physics.dirState.upwards)
				&& (currentAnimationSteps == -1)) {
			if (Player_Physics.leftPressed) {
				isWalkingRight = false;
				currentAnimationSteps = 0;
			} else if (Player_Physics.rightPressed) {
				isWalkingRight = true;
				currentAnimationSteps = 0;
			}
		}
	}
	
	void UpdateAnimationSteps() {
		//Handles all animations and keeps track of animation / movement counter.
		
		if (Player_Physics.facing == Player_Physics.dirState.crouching) {
			//Handle crouching Pit sprites
			transform.localScale = new Vector3(transform.localScale.x, 1, 1);
			GetComponent<SpriteRenderer>().sprite = duckingPit;
		} else if (Player_Physics.facing == Player_Physics.dirState.upwards) {
			//Facing up stops Pit dead in his tracks.
			transform.localScale = new Vector3(transform.localScale.x, 2f/3, 1);
			currentAnimationSteps = -1;
			GetComponent<SpriteRenderer>().sprite = upwardPit;
			return;
		} else {
			transform.localScale = new Vector3(transform.localScale.x, 2f/3, 1);
			GetComponent<SpriteRenderer>().sprite = currentSprite;
		}
		
		
		
		if (Player_Physics.groundTime > 4) {
			//Handle jumping animation
			
			if (Player_Physics.facing == Player_Physics.dirState.crouching) {
				return;
			}
			
			if (Player_Physics.jumpPressed == true && Player_Physics.state == Player_Physics.jumpState.jumping) {
				currentSprite = jump1;
				print ("uhh:)");
			} else {
				currentSprite = jump2;
			}
			return;
		}
		
		if (currentAnimationSteps != -1) {
			if (currentAnimationSteps == totalAnimationSteps) {
				currentSprite = walk1;
				//print ("hupp");
			} else if (currentAnimationSteps == 2*totalAnimationSteps) {
				currentSprite = walk2;
				//print ("two");
			} else if (currentAnimationSteps == 3*totalAnimationSteps) {
				currentSprite = walk3;
				//print ("three");
			} else if (currentAnimationSteps == 4*totalAnimationSteps) {
				currentSprite = standingPit;
				currentAnimationSteps = -1;
				//print ("four");
				return;
			}			
			//Increment currentAnimationSteps if they're not maxed.
			++currentAnimationSteps;
			//print ("Incremented!");
		}
	}
	
	public void UpdateSpriteColor () {
		print ("Squee");
		if (Player_Shoot.hasSuperArrow == false) {
			duckingPit = wDuckingPit;
			upwardPit = wUpwardPit;
		} else {
			standingPit = gStandingPit;
			duckingPit = gDuckingPit;
			upwardPit = gUpwardPit;
			dyingPit = gDyingPit;
			walk1 = gWalk1;
			walk2 = gWalk2;
			walk3 = gWalk3;
			jump1 = gJump1;
			jump2 = gJump2;
		}
		currentSprite = standingPit;
	}
}
