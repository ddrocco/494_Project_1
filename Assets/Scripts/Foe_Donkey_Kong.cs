using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Foe_Donkey_Kong : Obj_Foe {
	public GameObject deadDankyKang;
	public AudioClip brickCrush, hitSound, dieSound;
	public Sprite dk_dancing, dk_throwing, dk_standing;
	public GameObject barrelPrefab;

	private int danceSteps, danceTimer, barrelSteps, barrelTimer, tantrumSteps, tantrumTimer, initSteps, initTimer;
	private int screenShake1 = 57;
	private List<float> xLocations = new List<float>();
	private int xLocation;
	private int numXLocations = 2;
	
	private int phaseTwoHealth = 7;
	
	private Vector3 startPos = new Vector3(-0.5f, 158.5f, 0);
	
	
	private List<GameObject> barrels = new List<GameObject>();

	public enum dkState {
		tossing,
		tantruming,
		fastTantruming
	};
	public dkState state;

	// Use this for initialization
	void Start () {
		health = phaseTwoHealth + 4;
		state = dkState.tossing;
		
		danceTimer = 0;
		barrelTimer = 100;
		tantrumTimer = 0;
		initTimer = 0;
		danceSteps = 15;
		barrelSteps = 150;
		tantrumSteps = 100;
		initSteps = 40;
		
		xLocations.Add(-5);
		xLocations.Add(4);
		xLocation = 0;
	}
	
	// Update is called once per frame
	new void FixedUpdate () {
		if (++initTimer <= initSteps) {
			float ratio = (float)initTimer / initSteps;
			float yDisplace = 10 * (1 - Mathf.Pow (ratio, 2));
			transform.position = startPos + Vector3.up * yDisplace;
			if (initTimer == initSteps) {
				AudioSource.PlayClipAtPoint(brickCrush, Camera.main.transform.position);
				GetComponent<SpriteRenderer>().sprite = dk_throwing;
			}
			return;
		}
		Maintenance();
		if (state == dkState.tossing) {
			if (++barrelTimer >= barrelSteps) {
				ThrowBarrel();
				barrelTimer = 0;
			}
		} else if (state == dkState.fastTantruming && ++tantrumTimer <= tantrumSteps) {
			float ratio = (float)tantrumTimer / tantrumSteps;
			float yDisplace = 7 * (Mathf.Abs((1-ratio) + Mathf.Sin (2*Mathf.PI*ratio))-1);
			transform.position = startPos + Vector3.up * yDisplace;
			if (tantrumTimer == screenShake1) {
				FindObjectOfType<Camera_Adjust>().gameObject.transform.Translate(Vector3.down + Vector3.left);
				AudioSource.PlayClipAtPoint(brickCrush, Camera.main.transform.position);
			} else if (tantrumTimer == screenShake1 + 1) {
				FindObjectOfType<Camera_Adjust>().gameObject.transform.Translate(Vector3.up + Vector3.right);
			} else if (tantrumTimer == tantrumSteps -1) {
				FindObjectOfType<Camera_Adjust>().gameObject.transform.Translate(2 * Vector3.down + Vector3.right);
				AudioSource.PlayClipAtPoint(brickCrush, Camera.main.transform.position);
			} else if (tantrumTimer == tantrumSteps) {
				FindObjectOfType<Camera_Adjust>().gameObject.transform.Translate(2 * Vector3.up + Vector3.left);
			}
		} else {
			if (++danceTimer >= danceSteps) {
				transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
				danceTimer = 0;
			}
			if (++barrelTimer >= barrelSteps) {
				SpawnBarrel();
				barrelTimer = 0;
			}
		}
	}
	
	void ThrowBarrel() {
		transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
		Vector3 location = transform.position + 3f * transform.localScale.x * Vector3.right;
		GameObject barrel = Instantiate(barrelPrefab,location,Quaternion.identity) as GameObject;
		Foe_Barrel barrelScript = barrel.GetComponent<Foe_Barrel>();
		barrelScript.vSpeed = 0.0f;
		barrelScript.vSpeedMax = 0.12f;
		barrelScript.hSpeed = 0;
		barrelScript.state = Foe_Barrel.barrelState.falling;
		if (++xLocation >= numXLocations) {
			xLocation = 0;
		}
		barrels.Add(barrel);
	}
	
	void SpawnBarrel() {
		//(-7.5, -2.5) or (1.5, 6.5)
		Vector3 location = new Vector3(xLocations[xLocation], 163, 0);
		GameObject barrel = Instantiate(barrelPrefab,location,Quaternion.identity) as GameObject;
		Foe_Barrel barrelScript = barrel.GetComponent<Foe_Barrel>();
		barrelScript.vSpeed = 0.12f;
		barrelScript.vSpeedMax = 0.12f;
		barrelScript.hSpeed = 0;
		barrelScript.state = Foe_Barrel.barrelState.falling;
		if (++xLocation >= numXLocations) {
			xLocation = 0;
		}
		barrels.Add (barrel);
	}
	
	void OnTriggerEnter(Collider other) {
		AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position);
		CollisionTrigger(other);
		if (other.gameObject.layer == Layerdefs.projectile) {
			other.gameObject.GetComponent<Arrow_Action_Super>().keyIsHeld = false;
		}
		if (health == phaseTwoHealth + 3) {
			barrelSteps = 100;
		} else if (health == phaseTwoHealth + 2) {
			barrelSteps = 60;
		} else if (health == phaseTwoHealth + 1) {
			GetComponent<SpriteRenderer>().sprite = dk_dancing;
			state = dkState.tantruming;
			barrelSteps = 50;
		} else if (health == phaseTwoHealth) {
			state = dkState.fastTantruming;
			barrelSteps = 20;
			xLocations.Add(-5);
			xLocations.Add(4);
			xLocations.Add(-6);
			xLocations.Add(3);
			xLocations.Add(-4);
			xLocations.Add(5);
			numXLocations = 8;
			danceSteps = 5;
		}
	}
	
	void OnDestroy() {
		foreach (GameObject barrel in barrels) {
			if (barrel != null) {
				Destroy (barrel.gameObject);
			}
		}
		FindObjectOfType<Camera_Adjust>().gameObject.audio.Stop();
		Instantiate(deadDankyKang, transform.position, Quaternion.identity);
		AudioSource.PlayClipAtPoint(dieSound, Camera.main.transform.position);
	}
}
