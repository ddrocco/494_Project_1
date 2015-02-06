using UnityEngine;
using System.Collections;

public class Arrow_Upgrade : MonoBehaviour {
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == Layerdefs.pit) {
			Player_Shoot.hasSuperArrow = true;
			//other.gameObject.GetComponentInChildren<Player_Sprite_Control>().UpdateSpriteColor();
			Destroy(this.gameObject);
		}
	}
}
