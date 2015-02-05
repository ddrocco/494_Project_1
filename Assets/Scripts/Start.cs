using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Start : MonoBehaviour {
	public static bool godMode = false;
	public GameObject level1pit, level2pit, level1text, level2text;
	public bool firstLevel = true;
	
	private Color orange, red;
	
	void Awake () {
		level2pit.GetComponent<Image>().color = Color.black;
		red = new Color(80f/255, 0, 0);
		orange = new Color (179f/255, 97f/255, 16f/255);
	}
	
	void Update () {
		if (Input.GetKeyDown("s") || Input.GetKeyDown ("down")) {
			firstLevel = false;
			level1pit.GetComponent<Image>().color = Color.black;
			level2pit.GetComponent<Image>().color = Color.white;
			level1text.GetComponent<Text>().color = red;
			level2text.GetComponent<Text>().color = orange;
		}
		if (Input.GetKeyDown ("w") || Input.GetKeyDown ("up")) {
			firstLevel = true;
			level1pit.GetComponent<Image>().color = Color.white;
			level2pit.GetComponent<Image>().color = Color.black;
			level1text.GetComponent<Text>().color = orange;
			level2text.GetComponent<Text>().color = red;
		}
		if (Input.GetKeyDown (KeyCode.Return)) {
			if (firstLevel == true) {
				Application.LoadLevel("_Level_1");
			} else {
				Application.LoadLevel("_Level_2");
			}
		}
		
	}

}
