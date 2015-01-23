using UnityEngine;
using System.Collections;

public class Camera_Adjust : MonoBehaviour {

	void Awake () {
		Camera.main.aspect = 1.0f;
		Camera.main.orthographicSize = 8f;
		Vector3 newPos = new Vector3(Camera.main.transform.position.x - .5f,
									Camera.main.transform.position.y + 8.5f,
									Camera.main.transform.position.z);
		Camera.main.transform.position = newPos;
	}
	
}
