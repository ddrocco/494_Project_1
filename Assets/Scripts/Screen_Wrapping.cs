using UnityEngine;
using System.Collections;

public class Screen_Wrapping : MonoBehaviour {
	private Renderer[] renderers;
	private bool isWrappingX = false;
	private float screenWidth;
	private Transform[] ghosts = new Transform[2];
	private Vector3 screenBottomLeft;
	private Vector3 screenTopRight;

	// Use this for initialization
	void Start () {
		renderers = GetComponentsInChildren<Renderer>();
		screenBottomLeft = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0));
		screenTopRight = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, 0));
		screenWidth = screenTopRight.x - screenBottomLeft.x;
		CreateGhosts ();
		PositionGhosts ();
	}
	
	bool CheckRenderers() {
		foreach (Renderer renderer in renderers) {
			if(renderer.isVisible) {
				return true;
			}
		}
		return false;
	}
	
	void CreateGhosts() {
		for (int i = 0; i < 2; ++i) {
			ghosts[i] = Instantiate (transform, Vector3.zero, Quaternion.identity) as Transform;
			DestroyImmediate(ghosts[i].GetComponent<Screen_Wrapping>());
			DestroyImmediate(ghosts[i].GetComponent<Player_Shoot>());
		}
	}
	
	void PositionGhosts() {
		Vector3 ghostPosition = transform.position;
		
		//right ghost
		ghostPosition.x = transform.position.x + screenWidth;
		ghostPosition.y = transform.position.y;
		ghosts [0].position = ghostPosition;
		
		//left ghost
		ghostPosition.x = transform.position.x - screenWidth;
		ghostPosition.y = transform.position.y;
		ghosts [1].position = ghostPosition;
	}
	
	void Swap() {
		foreach (Transform ghost in ghosts) {
			if (ghost.position.x < screenWidth &&
			    ghost.position.x > -screenWidth)
			{
				transform.position = ghost.position;
				break;
			}
		}
	}
	
	void ScreenWrap() {
		if (CheckRenderers ()) {
			isWrappingX = false;
			return;
		}
		if (isWrappingX) {
			return;
		}
		Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
		Vector3 newPos = transform.position;
		if (!isWrappingX && (viewportPos.x > 1 || viewportPos.x < 0)) {
			newPos.x = -newPos.x;
			isWrappingX = true;
		}
		transform.position = newPos;
	}
	

	// Update is called once per frame
	void Update () {
		ScreenWrap ();
		if (transform.position.x < screenBottomLeft.x || transform.position.x > screenTopRight.x) {
			Swap();
		}
		PositionGhosts ();
	}
}
