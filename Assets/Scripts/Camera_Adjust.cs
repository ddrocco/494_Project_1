using UnityEngine;
using System.Collections;

public class Camera_Adjust : MonoBehaviour {
	//rows of blocks from -8 to 7 -> camera at -.5
	//about 15 blocks tall -> camera at 7.5
	//public GameObject openDoor;

	void Awake () {
		Camera.main.aspect = 1.0f;
		Camera.main.orthographicSize = 8f;
		Vector3 newPos = new Vector3(Camera.main.transform.position.x,
									Camera.main.transform.position.y + 8.5f,
									Camera.main.transform.position.z);
		Camera.main.transform.position = newPos;
	}
	
	// Use this for initialization
	void Start () {
//		Instantiate(openDoor, new Vector3(6, 18.5f, 0), Quaternion.identity);
//		Instantiate(openDoor, new Vector3(-5, 95.5f, 0), Quaternion.identity);
//		Instantiate(openDoor, new Vector3(3, 109.5f, 0), Quaternion.identity);
		//Instantiate(openDoor, new Vector3(6, 169.5f, 0), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
