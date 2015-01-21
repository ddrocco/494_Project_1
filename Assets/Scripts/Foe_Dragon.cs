using UnityEngine;
using System.Collections;

public class Foe_Dragon : Obj_Foe {
	public Player_Shoot player;

	public enum direction {
		left,
		right
	};
	
	public direction facing;
	public bool falling;
	private bool collidedSinceLastUpdate;
	public float horizontalSpeed = 3;
	public float fallingSpeed = 3;
	public float minTimeToFlip = 0;
	public int collisionPauseTime = 25; //500 ms
	public int collisionPauseTimer = 25;
	public int animationSteps = 3;
	public int stepsSinceAnimation = 0;
	
	public Sprite animation0;
	public Sprite animation1;
	
	void Start () {
		player = FindObjectOfType<Player_Shoot>();
		facePlayer ();
		falling = true;
		health = 1;
		itemDropOnDeath = item.smallHeart;
	}
	
	void FixedUpdate () { //enemy continues moving where it's moving
		animate ();
		move();
	}
	
	void OnTriggerEnter (Collider other) { //Turn around or stop falling and face player
		if (other.gameObject.layer == 10) { //Player
			other.GetComponent<Player_Action>().HitByEnemy();
		} else if (other.gameObject.layer == 13) { //Arrow
			GameObject.Destroy(other.gameObject);
			HitByArrow();
		} else if (collidedSinceLastUpdate == false) {
			collidedSinceLastUpdate = true;
			if (falling == true) { //Correct for falling; Height of collision block from center is 0.5
				transform.position += Vector3.up * (other.gameObject.transform.position.y + 1.01f - transform.position.y);
				falling = false;
			} else if (facing == direction.left) {
				transform.position += Vector3.right * (other.gameObject.transform.position.x + 1.01f - transform.position.x);
				flipDirection();
			} else {
				transform.position += Vector3.right * (other.gameObject.transform.position.x - 1.01f - transform.position.x);
				flipDirection();
			}
		}
	}
	
	private void animate() {
		if (++stepsSinceAnimation == animationSteps) {
			GetComponent<SpriteRenderer>().sprite = animation0;
		} else if (stepsSinceAnimation == 2*animationSteps) {
			GetComponent<SpriteRenderer>().sprite = animation1;
			stepsSinceAnimation = 0;
		}
	}
	
	private void move() {
		Vector3 translation = Vector3.zero; //Initialization doesn't matter
		collidedSinceLastUpdate = false;
		if (falling == true) {
			translation = Vector3.down * fallingSpeed;
			transform.position += translation * Time.fixedDeltaTime;
		} else if (collisionPauseTimer == collisionPauseTime) {
			if (facing == direction.left) {
				translation = Vector3.left * horizontalSpeed;
			} else {
				translation = Vector3.right * horizontalSpeed;
			}
			transform.position += translation * Time.fixedDeltaTime;
		} else {
			++collisionPauseTimer;
		}
	}
	
	public void facePlayer() {
		if (transform.position.x < player.transform.position.x && facing == direction.left) {
			flipDirection();
		} else if (transform.position.x >= player.transform.position.x && facing == direction.right) {
			flipDirection ();
		}
	}
	
	public void flipDirection() {
		collisionPauseTimer = 0;
		transform.localScale = new Vector3(-transform.localScale.x,1f,1f);
		if (facing == direction.right) {
			facing = direction.left;
		} else if (facing == direction.left) {
			facing = direction.right;
		}
	}
}