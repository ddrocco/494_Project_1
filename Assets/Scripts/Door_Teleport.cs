using UnityEngine;
using System.Collections;

public class Door_Teleport : MonoBehaviour {
	public GameObject player;
	public GameObject otherEnd;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		Application.LoadLevel("_Level_1");
	}
	
}
