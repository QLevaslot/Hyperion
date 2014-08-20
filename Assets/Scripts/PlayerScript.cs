using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	// The speed of the player
	public Vector2 speed = new Vector2(50, 50);
	
	// Store the movement
	private Vector2 movement;

	// Use this for initialization
	void Start () {
	
	}

	void Update(){
		// Retrieve axis information
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");
		
		movement = new Vector2(
			speed.x * inputX,
			speed.y * inputY);

	}

	void FixedUpdate(){
		// Move the game object
		rigidbody2D.velocity = movement;
	}
}
