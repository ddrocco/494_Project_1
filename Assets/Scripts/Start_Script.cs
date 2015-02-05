using UnityEngine;
using System.Collections;

public class Start_Script : MonoBehaviour {
	public static bool godMode = false;
	public static int goToLevel = 1;

	void Update () {
		if (Input.GetKeyDown("g")) {
			godMode = !godMode;
			print (godMode);
		}
		if (Input.GetKey("1")) {
			Application.LoadLevel("_Level_1");
		}
		else if (Input.GetKey("2")) {
			Application.LoadLevel("_Level_2");
		}
	}

}
