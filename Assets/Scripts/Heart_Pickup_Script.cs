using UnityEngine;
using System.Collections;

public class Heart_Pickup_Script : MonoBehaviour {
	public int value;
	public Sprite fullHeart;
	public Sprite halfHeart;
	
	public Heart_Pickup_Script(int val) {
		value = val;
	}

	// Use this for initialization
	void Start () {
		if (value == 1) {
			GetComponent<SpriteRenderer>().sprite = fullHeart;
			transform.localScale = new Vector3(0.5f,0.5f,1f);
		} else if (value == 5) {
			GetComponent<SpriteRenderer>().sprite = halfHeart;
			transform.localScale = new Vector3(1f,1f,1f);
		} else {
			GetComponent<SpriteRenderer>().sprite = fullHeart;
			transform.localScale = new Vector3(1f,1f,1f);
		}
	}
}
