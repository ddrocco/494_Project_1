using UnityEngine;
using System.Collections;

public class Player_Info : MonoBehaviour {
	public GameObject playerPrefab;
	public static GameObject gameObj;
	public Vector3 spawnLocation = new Vector3(-6, 4, 0);

	// Use this for initialization
	void Awake () {
		GameObject player = Instantiate (playerPrefab, spawnLocation, Quaternion.identity) as GameObject;
		gameObj = player;
	}
}