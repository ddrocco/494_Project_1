using UnityEngine;
using System.Collections;

public class Restart : MonoBehaviour {

	void FixedUpdate () {
		Player_Physics.isDead = false;
		Player_Enemy_Collision.health = 7;
		if (Input.GetKey(KeyCode.Return)) {
			Application.LoadLevel("_Level_1");
		}
	}

}
