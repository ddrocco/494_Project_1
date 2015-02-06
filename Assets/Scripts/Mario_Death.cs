using UnityEngine;
using System.Collections;

public class Mario_Death : MonoBehaviour {
	public AudioClip marioDeath, bossBattle;
	private AudioSource audioPlayer;
	public GameObject dk;
	
	void Awake() {
		audioPlayer = FindObjectOfType<Camera_Adjust>().gameObject.GetComponent<AudioSource>();
		audioPlayer.audio.clip = marioDeath;
		audioPlayer.loop = false;
		audioPlayer.audio.Play ();
	}
	
	void FixedUpdate () {
		transform.Translate (0.15f * Vector3.down);
		if (transform.position.y < 140) {
			Destroy (this.gameObject);
		}
	}
	
	void OnDestroy() {
		audioPlayer.audio.clip = bossBattle;
		audioPlayer.loop = true;
		audioPlayer.audio.Play ();
		Instantiate (dk, transform.position + Vector3.up * 100f, Quaternion.identity);
		
	}
}
