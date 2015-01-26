using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player_Enemy_Collision : MonoBehaviour {
	public int currency;
	
	public float health = 7;
	public int invulnTime = 50; //2 seconds
	public int timeSinceHit = 51;
	public bool invulnerable = false;
	
	public AudioClip hitByEnemy;
	
	public GameObject healthRenderer;
	public GameObject heartsRenderer;
	
	public Player_Sprite_Control playerSprite;
	
	// Use this for initialization
	void Start () {
		playerSprite = GetComponentInChildren<Player_Sprite_Control>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (invulnerable == true) {
			UpdateHitControl();
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
	}
	
	void UpdateHitControl() {
		if (++timeSinceHit >= invulnTime) {
			invulnerable = false;
			playerSprite.renderer.material.color = Color.white;
		} else if (timeSinceHit % 2 != 0) {
			playerSprite.renderer.material.color = Color.red;
		} else {
			playerSprite.renderer.material.color = Color.yellow;
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
