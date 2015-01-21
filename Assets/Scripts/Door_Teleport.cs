using UnityEngine;
using System.Collections;

public class Door_Teleport : MonoBehaviour {
	public GameObject player;
	
	void OnTriggerEnter(Collider other) {
		Application.LoadLevel("_Level_1");
	}
	
}
