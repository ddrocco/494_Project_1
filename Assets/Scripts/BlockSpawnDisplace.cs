using UnityEngine;
using System.Collections;

public class BlockSpawnDisplace : MonoBehaviour {
	public float displacement;
	
	void Start () {
		transform.Translate(Vector3.up * displacement);
	}

}