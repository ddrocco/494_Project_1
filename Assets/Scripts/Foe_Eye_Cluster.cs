using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Foe_Eye_Cluster : Foe_Spawner {
	private int timer = 0;	//runs indefinitely
	public enum Cycle {
		firstTrans,
		rightLoc1,
		rightLoc2,
		rightLoc3,
		middleTrans,
		leftLoc1,
		leftLoc2,
		leftLoc3
	};
	private Cycle currentStage;
	private List<Vector3> targets;
	
	new void Start() {
		targets = new List<Vector3>[8];
		
	}
	
	// Update is called once per frame
	new void FixedUpdate () {
		++timer;
		//Pick desitnation points
		//Assign them at various times
	}
	
	void selectNewTarget() {
		
	}
}
