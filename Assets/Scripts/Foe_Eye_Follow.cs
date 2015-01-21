using UnityEngine;
using System.Collections;

public class Foe_Eye_Follow : MonoBehaviour {
	public GameObject spawner;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 temp = spawner.transform.position;
		transform.position = Vector3.Lerp (transform.position, temp, .01f);
	}
}
