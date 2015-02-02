using UnityEngine;
using System.Collections;

public class Start : MonoBehaviour {
	public static bool godMode = false;

	void Update () {
		if (Input.GetKeyDown("g")) {
			godMode = !godMode;
		}
		if (Input.GetKey("1")) {
			Application.LoadLevel("_Level_1");
		}
		/*else if (Input.GetKey("2")) {
			Application.LoadLevel("_Level_2");
		}*/
	}

}
