using UnityEngine;
using System.Collections;


public class Foe_Eye_Alt : Obj_Foe {
	public enum location {
		tRight,
		tLeft,
		bRight,
		bLeft,
	};

	public GameObject player;
	//public GameObject foeEye;
	public float xSpeed;
	public float ySpeed;
	public float timer;
	private location goHere;
	private location wasHere;
	private float savedTime;
	//private GameObject[] eyes;

	// Use this for initialization
	void Start () {
		//init movement variables
		ySpeed = -.12f;
		xSpeed = 0;
		timer = 1f;
		wasHere = location.tLeft;
		goHere = location.bLeft;
		savedTime = Time.time;
		//spawn new eyes
		//int x = Random.Range(1, 5);
		//eyes = new GameObject[x];
		//Vector3 spawnPt = new Vector3(transform.position.x, transform.position.y + 1f, 0f);
		/*for (int i = 0; i < x; ++i) {
			//eyes[i] = Instantiate(foeEye, spawnPt, Quaternion.identity) as GameObject;
			Vector3 tempPt = spawnPt;
			spawnPt = new Vector3(tempPt.x, tempPt.y + 1, 0);
		}*/
		health = 1;
		itemDropOnDeath = item.halfHeart;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 temp  =  new Vector3(transform.position.x + xSpeed, transform.position.y + ySpeed, 0);
		transform.position = temp;
		if (transform.position.y == player.transform.position.y) {
			//print ("Saw you!");
		}
		if (Time.time - savedTime >= timer) {
			if (goHere == location.bLeft && wasHere == location.tLeft) {
				wasHere = goHere;
				goHere = location.tRight;
				ySpeed = 0.05f;
				xSpeed = 0.3f;
			}
			else if (goHere == location.tRight && wasHere == location.bLeft) {
				wasHere = goHere;
				goHere = location.bRight;
				ySpeed = -0.05f;
				xSpeed = 0;
			}
			else if (goHere == location.bRight && wasHere == location.tRight) {
				wasHere = goHere;
				goHere = location.tLeft;
				ySpeed = 0.05f;
				xSpeed = -0.3f;
			}
			else if (goHere == location.tLeft && wasHere == location.bRight) {
				wasHere = goHere;
				goHere = location.bLeft;
				ySpeed = -0.05f;
				xSpeed = 0;
			}
			savedTime = Time.time;
		}
	}
}
