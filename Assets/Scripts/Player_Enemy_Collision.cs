using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player_Enemy_Collision : MonoBehaviour {
	public int currency;
	public int mallets = 0;
	public static int health = 7;
	public static int invulnTime = 50; //2 seconds
	public static int timeSinceHit = 51;
	public static bool invulnerable = false;
	public AudioClip hitByEnemy;
	public static GameObject healthRenderer;
	public static GameObject heartsRenderer;
	public Sprite dead, Gdead;
	
	void Update () {
		if (dead == Gdead) return;
		if (Player_Shoot.hasSuperArrow) dead = Gdead;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (invulnerable == true) {
			UpdateHitControl();
		} else {	
			GetComponent<SpriteRenderer>().color = Color.white;
		}
		if (transform.position.y > Camera.main.transform.position.y) {
			Camera.main.transform.Translate (new Vector3(0, 0.075f, 0));
		}
		
		Vector3 screenBottomLeft = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0));		
		if (transform.position.y < screenBottomLeft.y) {
			if (Camera_Adjust.mitchellMode == false || health <= 0) {
				Application.LoadLevel("_Finished_Screen");
			} else {
				Camera.main.transform.Translate (new Vector3(0, -0.075f, 0));
			}
		}
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.layer == Layerdefs.collectible) { //Collectible
			Heart_Pickup_Script collected = other.gameObject.GetComponent<Heart_Pickup_Script>();
			currency += collected.value;
			Destroy(other.gameObject);
			UpdateUI();
			return;
		}
		else if (other.gameObject.layer == Layerdefs.chalice) { //Chalice
			health = 7;
			Destroy (other.gameObject);
			UpdateUI();
			return;
		}
		else if (other.gameObject.layer == Layerdefs.mallet) { //Mallet
			++mallets;
			Destroy (other.gameObject);
			return;
		}
		
	}
	
	void UpdateHitControl() {
		if (++timeSinceHit >= invulnTime) {
			invulnerable = false;
		} else if (timeSinceHit % 6 == 4) {
			GetComponent<SpriteRenderer>().color = Color.yellow;
		} else if (timeSinceHit % 6 == 1) {
			GetComponent<SpriteRenderer>().color = Color.red;
		}
	}
	
	public void HitByEnemy() {
		if (invulnerable == false) {
			AudioSource.PlayClipAtPoint(hitByEnemy, FindObjectOfType<Camera>().transform.position);
			if (Camera_Adjust.austinMode == false) {
				--health;
				UpdateUI();
			}
			invulnerable = true;
			timeSinceHit = 0;
		}
	}
	
	public void UpdateUI() {
		if (healthRenderer == null || heartsRenderer == null) {
			healthRenderer = GameObject.FindWithTag("Health_Renderer");
			heartsRenderer = GameObject.FindWithTag("Hearts_Renderer");
		}
		if (health <= 0) {
			Player_Physics_Final.isDead = true;
			GetComponent<SpriteRenderer>().sprite = dead;
		}
		healthRenderer.GetComponent<Healthbar_Renderer>().SetHealthCount(health);
		heartsRenderer.GetComponent<Text>().text = toThreeDigits (currency);
	}
	
	public string toThreeDigits(int hearts) {
		if (hearts < 10) {
			return ("00" + hearts.ToString());
		} else if (hearts < 100) {
			return ("0" + hearts.ToString());
		} else {
			return hearts.ToString();
		}
	}
}
