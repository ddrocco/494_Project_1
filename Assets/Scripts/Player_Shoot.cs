using UnityEngine;
using System.Collections;

public class Player_Shoot : MonoBehaviour {
	public GameObject arrowPrefab;
	static public bool shotArrow = false;
	
	void Update () {
		if (Input.GetKeyDown ("z") || Input.GetKeyDown (",")) {
			if (!Player_Action.faceDown && !shotArrow) {
				shotArrow = true;
				Instantiate (arrowPrefab, transform.position, transform.rotation);
			}
		}
	}

}
