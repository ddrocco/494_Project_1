﻿using UnityEngine;
using System.Collections;

public class Start : MonoBehaviour {

	void FixedUpdate () {
		if (Input.GetKey(KeyCode.Return)) {
			Application.LoadLevel("_Level_1");
		}
	}

}