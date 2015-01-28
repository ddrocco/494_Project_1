using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Foe_Eye_Cluster : MonoBehaviour {
	//VERTICAL: follows a sin wave (updated frequently to match camera height)
		//Recorcd camera stuff
	//HORIZONTAL: bezier interpolation between edges
		//Fast in middle, slow down at edges
	
	//inherited public List<Obj_Foe> foesList;
		//Give commands to different foes based on their spawn times
		//Perhaps they call return-valuing functions from the Foe_Eye_Cluster to figure out where to go?
	
	public static float horizTime = 15f; //Follows interpolative methods, dictates full cycle
	public static float verticalTime = 4f; //Follows a sin wave
	public static float horizontalLeft;
	public static float horizontalRight;
	public static float verticalCenter;
	public static float verticalVariance = 3f;
	
	public static Vector3 screenBottomLeft;
	public static Vector3 screenTopRight;
	
	public static Vector3 newScreenBottomLeft;
	public static Vector3 newScreenTopRight;
	
	private static float horizontalCutoff = 0.9f;
	private static float verticalRatio = 0.75f;
	
	private static float apparentScreenClimbRate = 1f;
	
	void Start () {
		screenBottomLeft = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0));
		screenTopRight = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, 0));
		horizontalLeft = (1 - horizontalCutoff) * screenTopRight.x + horizontalCutoff * screenBottomLeft.x;
		horizontalRight = horizontalCutoff * screenTopRight.x + (1 - horizontalCutoff) * screenBottomLeft.x;
	}
	
	void FixedUpdate () {
		newScreenBottomLeft = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0));
		newScreenTopRight = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, 0));
		float difference = newScreenBottomLeft.y - screenBottomLeft.y;
		screenBottomLeft += apparentScreenClimbRate * difference * Vector3.up;
		screenTopRight += apparentScreenClimbRate * difference * Vector3.up;
		verticalCenter = (1 - verticalRatio) * screenBottomLeft.y + verticalRatio * screenTopRight.y;
	}
	
	public static Vector3 WavePosition(float time) {
		float hRatio = 2 * (time % horizTime) / horizTime;
		float vRatio = (time % verticalTime) / verticalTime;
		
		if (hRatio > 1f) {
			hRatio = (2f - hRatio);
		}
		float hRatioEased = Ease(hRatio);
		
		float vPos = verticalCenter + verticalVariance * Mathf.Sin (vRatio * 2 * Mathf.PI);
		float hPos = horizontalLeft * hRatioEased + horizontalRight * (1 - hRatioEased);
		return new Vector3(hPos, vPos, 0);
	}
	
	public static Vector3 SpawnPosition() {
		return Vector3.up * (screenTopRight.y + 2f) - 2f * Vector3.right;
	}
	
	private static float Ease(float value) {
		float modifiedValue = 2f*value;
		if (modifiedValue > 1f) {
			modifiedValue = 2f - modifiedValue;
		}
		return (modifiedValue - 0.15f * Mathf.Sin (modifiedValue * 2f * Mathf.PI));
	}
}
