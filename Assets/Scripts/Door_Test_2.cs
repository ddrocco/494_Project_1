using UnityEngine;
using System.Collections;

public class Door_Test_2 : MonoBehaviour {
	public GameObject pit;
	public string scenename;
	public GameObject spawnPt;

	void Awake () {
		DontDestroyOnLoad(pit);
		pit.transform.position = spawnPt.transform.position;
		pit = Instantiate(pit, spawnPt.transform.position, Quaternion.identity) as GameObject;
	}
	
	void OnTriggerEnter () {
		Application.LoadLevel(scenename);
	}
}
