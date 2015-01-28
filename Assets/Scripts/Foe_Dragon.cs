using UnityEngine;
using System.Collections;

public class Foe_Dragon : Obj_Foe {

	public enum direction {
		left,
		right
	};
	
	public direction facing;
	public bool falling;
	private bool collidedSinceLastUpdate;
	public float horizontalSpeed = 3;
	public float fallingSpeed = 8;
	public float minTimeToFlip = 0;
	public int collisionPauseTime = 25; //500 ms
	public int collisionPauseTimer = 25;
	public int animationSteps = 3;
	public int stepsSinceAnimation = 0;
	
	public Sprite animation0;
	public Sprite animation1;
	
	void Start () {
		FacePlayer ();
		falling = true;
		health = 1;
		itemDropOnDeath = item.smallHeart;
	}
	
	new void FixedUpdate () { //enemy continues moving where it's moving
		Animate ();
		Move();
	}
	
	void OnTriggerEnter(Collider other) { //Turn around or stop falling and face player
		if (other.gameObject.layer == Layerdefs.blockThick
				&& collidedSinceLastUpdate == false) {
			BlockCollision(other);
		}
		CollisionTrigger(other);
	}
	
	private void Animate() {
		if (++stepsSinceAnimation == animationSteps) {
			GetComponent<SpriteRenderer>().sprite = animation0;
		} else if (stepsSinceAnimation == 2*animationSteps) {
			GetComponent<SpriteRenderer>().sprite = animation1;
			stepsSinceAnimation = 0;
		}
	}
	
	private void Move() {
		collidedSinceLastUpdate = false;
		if (falling == true) {
			transform.Translate(Vector3.down * fallingSpeed);
		} else if (collisionPauseTimer >= collisionPauseTime) {
			if (facing == direction.left) {
				transform.Translate (Vector3.right * -horizontalSpeed);
			} else {
				transform.Translate (Vector3.right * horizontalSpeed);
			}
		} else {
			++collisionPauseTimer;
		}
	}
	
	public void FacePlayer() {
		if (transform.position.x < player.transform.position.x && facing == direction.left) {
			FlipDirection();
		} else if (transform.position.x >= player.transform.position.x && facing == direction.right) {
			FlipDirection ();
		}
	}
	
	public void FlipDirection() {
		collisionPauseTimer = 0;
		transform.localScale = new Vector3(-transform.localScale.x,1f,1f);
		if (facing == direction.right) {
			facing = direction.left;
		} else if (facing == direction.left) {
			facing = direction.right;
		}
	}
	
	public void BlockCollision(Collider other) {
		Bounds dragonBounds = GetComponent<BoxCollider>().collider.bounds;
		Bounds blockBounds = other.collider.bounds;
		if (falling == true) {
			//float dragonBottomEdge = dragonBounds.center.y - dragonBounds.extents.y;
			//float blockTopEdge = blockBounds.center.y + blockBounds.extents.y;
			//print (blockTopEdge - dragonBottomEdge - 0.2f);
			//transform.Translate(Vector3.up * (blockTopEdge - dragonBottomEdge + 1.05f));
			transform.Translate(Vector3.up * (0.2f));
			falling = false;
		} else if (facing == direction.left) {
			float dragonLeftEdge = dragonBounds.center.x - dragonBounds.extents.x;
			float blockRightEdge = blockBounds.center.x + blockBounds.extents.x;
			transform.Translate(Vector3.right * (blockRightEdge - dragonLeftEdge + 0.01f));
			FlipDirection();
		} else {
			float dragonRightEdge = dragonBounds.center.x + dragonBounds.extents.x;
			float blockLeftEdge = blockBounds.center.x - blockBounds.extents.x;
			transform.Translate(Vector3.right * (blockLeftEdge - dragonRightEdge - 0.01f));
			FlipDirection();
		}
	}

}