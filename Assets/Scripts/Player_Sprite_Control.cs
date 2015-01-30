using UnityEngine;
using System.Collections;

public class Player_Sprite_Control : MonoBehaviour {

	public Sprite standingPit;
	public Sprite duckingPit;
	public Sprite upwardPit;
	public Sprite dyingPit;
	
	public Sprite walk1;
	public Sprite walk2;
	public Sprite walk3;
	public int animationSteps = 3;
	public int stepsSinceAnimation = 0;
	
	public void FixedUpdate() {
		//print(renderer.material.color);
		//print (GetComponent<SpriteRenderer>().renderer.material.color);
		//AlterColor(Color.black);
		if (Player_Physics.isDead) {
			return;
		}
		if (Player_Physics.facing == Player_Physics.dirState.crouching) {
			transform.localScale = new Vector3(transform.localScale.x, 1, 1);
			GetComponent<SpriteRenderer>().sprite = duckingPit;
		}
		else if (Player_Physics.facing == Player_Physics.dirState.upwards) {
			transform.localScale = new Vector3(transform.localScale.x, 2f/3, 1);
			GetComponent<SpriteRenderer>().sprite = upwardPit;
		}
		else if (Player_Physics.facing == Player_Physics.dirState.sideways) {
			transform.localScale = new Vector3(transform.localScale.x, 2f/3, 1);
			if (Player_Physics.leftPressed || Player_Physics.rightPressed) {
				if (stepsSinceAnimation == animationSteps) {
					GetComponent<SpriteRenderer>().sprite = walk1;
					standingPit = walk1;
				} else if (stepsSinceAnimation == 2*animationSteps) {
					GetComponent<SpriteRenderer>().sprite = walk2;
					standingPit = walk2;
				} else if (stepsSinceAnimation == 3*animationSteps) {
					GetComponent<SpriteRenderer>().sprite = walk3;
					standingPit = walk3;
					stepsSinceAnimation = -1;
				}
				++stepsSinceAnimation;
			}
			else GetComponent<SpriteRenderer>().sprite = standingPit;
		}
	}
}
