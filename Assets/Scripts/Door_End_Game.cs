using UnityEngine;
using System.Collections;

public class Door_End_Game : MonoBehaviour {
	public GameObject player;
	
	void OnTriggerEnter(Collider other) {
		Application.LoadLevel("_Title_Screen");
	}
	
}
