using UnityEngine;
using System.Collections;

public class Pause_Menu : MonoBehaviour {
	bool paused = false;
	
	void Update() {
		if(Input.GetKeyDown(KeyCode.Return)) {
			paused = togglePause();
		}
	}
	
	void OnGUI() {
		if(paused) {
			GUILayout.Label("Game is paused!");
		}
	}
	
	bool togglePause() {
		if(Time.timeScale == 0f) {
			Time.timeScale = 1f;
			return(false);
		} else {
			Time.timeScale = 0f;
			return(true);    
		}
	}

}