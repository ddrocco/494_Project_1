using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Block_Spawner : MonoBehaviour {
	public List<Vector3> blocksToCreate;
	public List<GameObject> worldBlocks;
	public List<GameObject> generatedBlocks;
	private List<GameObject> edgeBlocks;
	bool hit, finished = false;
	float completionTime = 2f;
	float currentTime = 0f;
	float distanceScale = 8f;
	
	void Hit() {
		foreach (Vector3 blockToCreate in blocksToCreate) {
			generatedBlocks.Add (Build(blockToCreate.x, blockToCreate.y, blockToCreate.z));
			if (blockToCreate.x == -8) {
				Build(8, blockToCreate.y, blockToCreate.z, useOffset: false);
			} else if (blockToCreate.x == 7) {
				Build(-9, blockToCreate.y, blockToCreate.z, useOffset: false);
			}
		}
		hit = true;
	}
	
	void FixedUpdate() {
		if (finished == false) {
			if (hit == true && currentTime <= completionTime) {
				UpdateBlocks();
			} else if (hit == true) {
				for (int i = 0; i < blocksToCreate.Count; ++i) {
					if (generatedBlocks[i] != null) {
						float yValue = blocksToCreate[i].y;
						generatedBlocks[i].transform.position = new Vector3(
							generatedBlocks[i].transform.position.x, yValue, 0);
					}
				}
				finished = true;
			}
		}
		 
	}
	
	void UpdateBlocks(){
		currentTime += Time.fixedDeltaTime;
		float yOffset = distanceScale * Mathf.Pow((1f-currentTime/completionTime),2);
		for (int i = 0; i < blocksToCreate.Count; ++i) {
			if (generatedBlocks[i] != null) {
				float yValue = blocksToCreate[i].y;
				generatedBlocks[i].transform.position = new Vector3(
						generatedBlocks[i].transform.position.x, yValue + yOffset, 0);
			}	
		}
	}
	
	GameObject Build(float x, float y, float ID, bool useOffset = true) {
		float offset;
		if (useOffset == true) {
			offset = distanceScale;
		} else {
			offset = 0;
		}
		GameObject newObject = Instantiate (worldBlocks[Mathf.FloorToInt(ID)]) as GameObject;
		newObject.transform.position = new Vector3(x, y + offset, 0);
		newObject.transform.parent = transform;
		return newObject;
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == Layerdefs.projectile && hit == false) {
			GetComponent<SpriteRenderer>().color = Color.red;
			Hit ();
		}
	}
}
