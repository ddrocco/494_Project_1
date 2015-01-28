using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Healthbar_Renderer : MonoBehaviour {
	private List<GameObject> barSegments = new List<GameObject>();
	public GameObject healthbarPrefab;
	public Sprite healthReserve;
	public Sprite healthMax;
	public Sprite healthMissing;
	
	// Use this for initialization
	void Start () {
		for (int i = 0; i < 7; ++i) {
			barSegments.Add (Instantiate(healthbarPrefab) as GameObject);
			barSegments[i].transform.SetParent(transform);
			barSegments[i].GetComponentInChildren<Image>().sprite = healthReserve;
			barSegments[i].transform.localScale = new Vector3(1f,1f,1f);
		}
		barSegments[6].GetComponentInChildren<Image>().sprite = healthMax;
	}
	
	public void SetHealthCount(int health) {
		if (health > 7 || health <= 0) {
			return;
		}
		for (int i = 0; i < health - 1; ++i) {
			barSegments[i].GetComponentInChildren<Image>().sprite = healthReserve;
		}
		barSegments[health - 1].GetComponentInChildren<Image>().sprite = healthMax;
		for (int i = health; i < 7; ++i) {
			barSegments[i].GetComponentInChildren<Image>().sprite = healthMissing;
		}
	}
}
