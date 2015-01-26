using UnityEngine;
using System.Collections;

public class Player_Shoot : MonoBehaviour {
	public GameObject arrowPrefab;
	static public bool shotArrow = false;
	
	public int cooldownWaitTime = 15;
	public int cooldownTimer = 15;
	
	void Update () {
		if (++cooldownTimer >= cooldownWaitTime
				&& (Input.GetKeyDown ("z") || Input.GetKeyDown (","))) {
			if (Player_Physics.facing != Player_Physics.dirState.crouching && !shotArrow) {
				Quaternion arrowRotation;
				if (Player_Physics.facing == Player_Physics.dirState.upwards) {
					arrowRotation = Quaternion.Euler(Vector3.up);
				} else if (Player_Physics.isLookingRight == true){
					arrowRotation = Quaternion.Euler (Vector3.right);
				} else {
					arrowRotation = Quaternion.Euler (Vector3.left);
				}
				shotArrow = true;
				Instantiate (arrowPrefab, transform.position, Quaternion.identity);
				cooldownTimer = 0;
			}
		}
	}

}
