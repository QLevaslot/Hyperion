using UnityEngine;
using System.Collections;

public class PlayerScript : MoveScript {

	// Use this for initialization
	void Start () {
		base.Start();
		Debug.Log ("Player initialisation");
	}

	void Update(){
		UpdateMovement();
	}


	void FixedUpdate(){
		// inputstate is none unless one of the movement keys are pressed
		currentInputState = inputState.None;

		// Axis information (pc = directional keys, xbox = left stick)
		xAcceleration = Input.GetAxis("Horizontal");
		//float inputY = Input.GetAxis("Vertical");

		// move left
		if(xAcceleration < 0) 
		{ 
			currentInputState = inputState.WalkLeft;
			facingDir = facing.Left;
		}
		
		// move right
		if (xAcceleration > 0 && currentInputState != inputState.WalkLeft) 
		{ 
			currentInputState = inputState.WalkRight;
			facingDir = facing.Right;
		}
		
		// jump
		if (Input.GetButtonDown("Jump")) 
		{ 
			currentInputState = inputState.Jump;
		}

		UpdatePhysics();
	}
}
