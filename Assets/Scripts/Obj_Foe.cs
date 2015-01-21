using UnityEngine;
using System.Collections;

public class Obj_Foe : MonoBehaviour {
	public int health;
	public Foe_Spawner spawner;
	public Foe_Reaper reaper;
	public bool spawned; //debug
	
	public enum item {
		smallHeart,
		halfHeart,
		fullHeart
	}
	
	public item itemDropOnDeath;
	public GameObject heartPrefab;
	
	public int invulnTime = 25;
	public int timeSinceHit = 0;
	public bool invulnerable = false;

	/*// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	*/
	void FixedUpdate () {
		if (++timeSinceHit >= invulnTime) {
			invulnerable = false;
			renderer.material.color = Color.white;
		} else if (timeSinceHit % 2 != 0) {
			renderer.material.color = Color.red;
		} else {
			renderer.material.color = Color.yellow;
		}
	}
	
	public void HitByArrow() {
		if (invulnerable == true) {
			return;
		}
		if (--health == 0) {
			GameObject heart;
			heart = Instantiate(heartPrefab,transform.position,Quaternion.identity) as GameObject;
			Heart_Pickup_Script heartScript = heart.GetComponent<Heart_Pickup_Script>();
			if (itemDropOnDeath == item.smallHeart) {
				heartScript.value = 1;
			} else if (itemDropOnDeath == item.halfHeart) {
				heartScript.value = 5;
			} else {
				heartScript.value = 10;
			}
			
			Destroy (this.gameObject);
		}
	}
	
	void OnDestroy() {
		if (spawned) {
			spawner.foesList.Remove (this);
		} else {
			//reaper.foesList.Remove (this);
		}
		//Drop item:		
		//Puff of smoke (should spawn item when smoke clears
	}
}
