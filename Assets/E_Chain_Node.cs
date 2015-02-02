using UnityEngine;
using System.Collections;

public class E_Chain_Node : Obj_Foe {
	 float timer = 0;
	 const float cooldown = 1f;
	 
	// Use this for initialization
	void Start () {
		health = -1;
		itemDropOnDeath = item.smallHeart;
	}
	
	// Update is called once per frame
	new void FixedUpdate () {
		if (gameObject.layer == Layerdefs.transparentFX) {
			timer += Time.fixedDeltaTime;
			if (timer >= cooldown) {
				gameObject.layer = Layerdefs.node;
			}
		}
	}
	
	void OnTriggerEnter(Collider other) { //Turn around or stop falling and face player
		if (other.gameObject.layer == Layerdefs.projectile) {
			timer = 0;
			gameObject.layer = Layerdefs.transparentFX;
		}
	}
}
