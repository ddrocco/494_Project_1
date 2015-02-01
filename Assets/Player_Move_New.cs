using UnityEngine;
using System.Collections;

public class Player_Move_New : MonoBehaviour {
	public Sprite stand, walk1, walk2, walk3;
	public float xDist = 0.15f;
	public int framesPerStep = 2;
	public int currStep = 0;
	private int stepState = 0;
	
	//finding framerate
	private int frameCtr = 0;
	private float timeCtr = 0.0f;
	public float frameRate = 0.0f;
	public float refreshTime = 0.5f;
	public float timePerFrame = 0.0f;
	
	void Update () {
		//framerate calculations
		if (timeCtr < refreshTime) {
			timeCtr += Time.deltaTime;
			++frameCtr;
		} else {
			frameRate = (float)frameCtr/timeCtr;
			timePerFrame = (float)timeCtr/frameCtr;
			frameCtr = 0;
			timeCtr = 0.0f;
		}
	
		if (Input.GetKey("q")) {
			transform.localScale = new Vector3(-1f,transform.localScale.y,1f);
			takeStep (-1);
		} else if (Input.GetKey("e")) {
			transform.localScale = new Vector3(1f,transform.localScale.y,1f);
			takeStep (1);
		} /*else if (Input.GetKeyUp ("q")) {
			finishStep (-1);
		} else if (Input.GetKeyUp("e")) {
			finishStep (1);
		}*/
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
	
	void finishStep (int dir) {
		if (stepState == 0) return;
		int stepsLeft = 0;
		if (stepState == 1) stepsLeft = 3;
		else if (stepState == 2) stepsLeft = 2;
		else if (stepState == 3) stepsLeft = 1;
		
		float timer = 0;
		int lastStepState;
		while (stepsLeft > 0) {
			lastStepState = stepState;
			if (Time.time - timer >= timePerFrame) {
				takeStep(dir);
				timer = Time.time;
			}
			if (stepState != lastStepState) {
				--stepsLeft;
			}
		}
		return;
	}
}
