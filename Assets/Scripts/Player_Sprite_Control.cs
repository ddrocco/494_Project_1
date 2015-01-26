﻿using UnityEngine;
using System.Collections;

public class Player_Sprite_Control : MonoBehaviour {
	public Sprite standingPit;
	public Sprite duckingPit;
	public Sprite upwardPit;
	
	public void FixedUpdate() {
		print(renderer.material.color);
		GetComponent<SpriteRenderer>().renderer.material.color = renderer.material.color;
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
			GetComponent<SpriteRenderer>().sprite = standingPit;
		}
		
		print (Player_Physics.facing);
	}
}