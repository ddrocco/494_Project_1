using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Foe_Eye : Obj_Foe {
	public GameObject eye;
	//private Vector3 spawnPoint;
	//private Transform[] eyes = new Transform[3];
	public float mSpeed;
	public float mXScale;
	public float mYScale;
	
	private Vector3 mPivot;
	private Vector3 mPivotOffset;
	private float mPhase;
	private bool mInvert = false;
	private float m2Pi = Mathf.PI * 2;
	
	// Use this for initialization
	void Awake () {
//		Vector3 screenTopLeft = Camera.main.ViewportToWorldPoint (new Vector3 (.25f, 1, 0));
//		spawnPoint = new Vector3(screenTopLeft.x, screenTopLeft.y, 0);
	}
	
	void Start() {
		mPivot = transform.position;
		health = 1;
		itemDropOnDeath = item.halfHeart;
	}
	
	// Update is called once per frame
	void Update () {
		if (5 - Mathf.Abs(transform.position.y) < .25) {
			mSpeed -= .01f;
		} else {
			mSpeed = 2f;
		}
		mPivotOffset = Vector3.right * 2 * mXScale;
		mPhase += mSpeed * Time.deltaTime;
		if (mPhase > m2Pi) {
			mInvert = !mInvert;
			mPhase -= m2Pi;
		}
		if (mPhase < 0) {
			mPhase += m2Pi;
		}
		
		transform.position = mPivot + (mInvert ? mPivotOffset : Vector3.zero);
		Vector3 newPos = transform.position;
		newPos.y += Mathf.Sin (mPhase) * mYScale;
		newPos.x += Mathf.Cos (mPhase) * (mInvert ? -1 : 1) * mXScale;
		transform.position = newPos;
	}
}
