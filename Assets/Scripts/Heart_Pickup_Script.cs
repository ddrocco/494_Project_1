using UnityEngine;
using System.Collections;

public class Heart_Pickup_Script : MonoBehaviour {
	public int value;
	public Sprite fullHeart;
	public Sprite halfHeart;
	public Sprite smallHeart;
	public int lifetime = 100;
	public int birthTime;
	
	public Heart_Pickup_Script(int val) {
		value = val;
	}

	void Start () {
		if (value == 1) {
			GetComponent<SpriteRenderer>().sprite = fullHeart;
		} else if (value == 5) {
			GetComponent<SpriteRenderer>().sprite = halfHeart;
		} else {
			GetComponent<SpriteRenderer>().sprite = fullHeart;
		}
		birthTime = 0;
	}
	
	void FixedUpdate() {
		if (++birthTime >= lifetime) {
			Destroy (gameObject);
		}	
	}
}
