using UnityEngine;
using System.Collections;

public class Camera_Adjust : MonoBehaviour {

	void Awake () {
		Camera.main.aspect = 5/4f;
		Camera.main.orthographicSize = 7f;
		Vector3 newPos = new Vector3(Camera.main.transform.position.x - .5f,
									Camera.main.transform.position.y + 7f,
									Camera.main.transform.position.z);
		Camera.main.transform.position = newPos;
	}
	
}
