using UnityEngine;
using System.Collections;

public class Foe_Dragon_Fall_Handler : MonoBehaviour {
	public int collisions = 0;
	private Foe_Dragon parent;
	
	void Start () {
		collisions = 0;
		parent = GetComponentInParent<Foe_Dragon>();
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == Layerdefs.blockThick
		    	|| other.gameObject.layer == Layerdefs.blockThin) {
			++collisions;
		}
	}
	
	void OnTriggerExit(Collider other) {
		if (other.gameObject.layer == Layerdefs.blockThick
				|| other.gameObject.layer == Layerdefs.blockThin)
		{
			--collisions;
		}
	}
	
	void FixedUpdate() {
		if (collisions < 2 && parent.falling == false) {
			parent.falling = true;
			parent.FacePlayer();
//			print (parent.transform.position.x < parent.player.transform.position.x);
//			print ("PTPX: " + parent.transform.position.x + " + PPTPX " + parent.player.transform.position.x);
		}
	}

}
