using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Harp_Script : MonoBehaviour {
	public List<GameObject> allFoes;
	public GameObject mallet;
	
	void OnTriggerEnter (Collider other) {
		
		if (other.gameObject.layer == Layerdefs.pit) {
			print ("hello");
			
			Vector3 screenBottomLeft = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0));
			Vector3 screenTopRight = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, 0));
			float screenHeight = screenTopRight.y - screenBottomLeft.y;
			
			GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
			foreach(GameObject go in allObjects) {
				if (go.activeInHierarchy && (go.layer == Layerdefs.foeSky || go.layer == Layerdefs.foeGround)) {
					allFoes.Add (go);
				}
			}
			
			foreach (GameObject foe in allFoes) {
				if (foe.transform.position.y - transform.position.y < 1.5f * screenHeight) {
						Instantiate(mallet, foe.transform.position, foe.transform.rotation);
						GameObject.Destroy(foe.gameObject);
				}	
			}
			GameObject.Destroy(this.gameObject);
		}

	}
	
}
