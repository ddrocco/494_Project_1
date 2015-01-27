using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player_Enemy_Collision : MonoBehaviour {
	public int currency;
	public int mallets = 0;
	public static float health = 7;
	public static int invulnTime = 50; //2 seconds
	public static int timeSinceHit = 51;
	public static bool invulnerable = false;
	
	public AudioClip hitByEnemy;
	
	public static GameObject healthRenderer;
	public static GameObject heartsRenderer;
	
	// Update is called once per frame
	void FixedUpdate () {
		if (invulnerable == true) {
			UpdateHitControl();
		} else {	
			GetComponentInChildren<SpriteRenderer>().color = Color.white;
		}
		if (transform.position.y > Camera.main.transform.position.y) {
			Camera.main.transform.Translate (new Vector3(0, 0.1f, 0));
		}
		
		Vector3 screenBottomLeft = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0));		
		if (transform.position.y < screenBottomLeft.y) {
			Application.LoadLevel("_Finished_Screen");
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
			++health;
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
			GetComponentInChildren<SpriteRenderer>().color = Color.yellow;
		} else if (timeSinceHit % 6 == 1) {
			GetComponentInChildren<SpriteRenderer>().color = Color.red;
		}
	}
	
	public void HitByEnemy() {
		if (invulnerable == false) {
			AudioSource.PlayClipAtPoint(hitByEnemy, FindObjectOfType<Camera>().transform.position);
			--health;
			invulnerable = true;
			timeSinceHit = 0;
			UpdateUI();
		}
	}
	
	public void UpdateUI() {
		if (healthRenderer == null || heartsRenderer == null) {
			healthRenderer = GameObject.FindWithTag("Health_Renderer");
			heartsRenderer = GameObject.FindWithTag("Hearts_Renderer");
		}
		healthRenderer.GetComponent<Text>().text = "Health: " + health.ToString();
		heartsRenderer.GetComponent<Text>().text = "Hearts: " + toThreeDigits (currency);
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
