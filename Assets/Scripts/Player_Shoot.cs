using UnityEngine;
using System.Collections;

public class Player_Shoot : MonoBehaviour {
	public GameObject arrowPrefab, superArrowPrefab;
	static public bool shotArrow = false;
	
	public int cooldownWaitTime = 15;
	public int cooldownTimer = 15;
	
	public static bool hasSuperArrow = false;
	public bool hasSuperArrowVariable = false;
	
	void Awake() {
		hasSuperArrow = hasSuperArrowVariable;
	}
	
	void Update () {
		if (++cooldownTimer >= cooldownWaitTime
				&& (Input.GetKeyDown ("z") || Input.GetKeyDown (","))) {
			if (Player_Physics.facing != Player_Physics.dirState.crouching && !shotArrow) {
				shotArrow = true;
				cooldownTimer = 0;
				if (hasSuperArrow == true) {
					Instantiate (superArrowPrefab, transform.position, Quaternion.identity);
				} else {
					Instantiate (arrowPrefab, transform.position, Quaternion.identity);
				}
			}
		}
	}

}
