using UnityEngine;
using System;

public class PlayerScript : MonoBehaviour {

	// The speed of the player
	public Vector2 speed = new Vector2(50, 50);
	
	// Store the movement
	private Vector2 movement;

	private float jumpTime = 0;

	// Use this for initialization
	void Start () {
		Debug.Log ("Player initialisation");
	}

	void Update(){
		// Retrieve axis information
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");

		// Trying to re-use that input for now
		bool shoot = Input.GetButtonDown("Fire1");
		shoot |= Input.GetButtonDown("Fire2");

		if (shoot) {
			jumpTime = 1f;		
		}

		float jump = 0;
		if (jumpTime > 0f) {
			jump = 1;		
		}
		movement = new Vector2(
			speed.x * inputX,
			speed.y * jump);

	}


	void FixedUpdate(){
		// Move the game object
		rigidbody2D.velocity = movement;

		// Jumptime decrease
		if (jumpTime > 0) {
			jumpTime = jumpTime - 0.1f;		
		}
	}
}
