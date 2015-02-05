using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Camera_Adjust : MonoBehaviour {
	public static bool mitchellMode = false;	//Immune to Falling
	public static bool austinMode = false;		//Immune to Damage
	private Text mode;
	//Jeremy mode: BOTH
	
	void Start() {
		mode = GameObject.FindGameObjectWithTag("Mode_Text").GetComponent<Text>();
	}
	
	void Update() {
		if (Input.GetKeyDown ("o")) {
			mitchellMode = !mitchellMode;
			PrintMode ();
		}
		if (Input.GetKeyDown ("p")) {
			austinMode = !austinMode;
			PrintMode ();
		}
	}

	void Awake() {
		Camera.main.aspect = 5/4f;
		Camera.main.orthographicSize = 6.4f;
		Vector3 newPos = new Vector3(Camera.main.transform.position.x - 0.5f,
									Camera.main.transform.position.y + 7f,
									Camera.main.transform.position.z);
		Camera.main.transform.position = newPos;
	}
	
	void PrintMode() {
		if (mitchellMode == true) {
			if (austinMode == true) {
				mode.text = "  | Jeremy Mode";
			} else {
				mode.text = "  | Mitchell Mode";
			}
		} else {
			if (austinMode == true) {
				mode.text = "  | Austin Mode";			
			} else {
				mode.text = "  | Normal Mode";
			}
		}
	}
}
