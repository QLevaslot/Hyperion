using UnityEngine;
using System.Collections;

public class PlayerScript : MoveScript {

	// Use this for initialization
	public override void Start () {
		base.Start();
		Debug.Log ("Player initialisation");
	}

	void Update(){
		// Axis information (pc = directional keys, xbox = left stick)
		xAcceleration = Input.GetAxis("Horizontal");
		float run = Input.GetAxis ("Run");
		
		// move left
		if(xAcceleration < 0f) 
		{ 
			xCurrentInputState = inputState.WalkLeft;
			facingDir = facing.Left;
		}
		
		// move right
		if (xAcceleration > 0 && xCurrentInputState != inputState.WalkLeft) 
		{ 
			xCurrentInputState = inputState.WalkRight;
			facingDir = facing.Right;
		}
		if (run != 0f) {
			moveVel = runVel;
		} else {
			moveVel = walkVel;
		}
		
		
		// jump
		if (Input.GetButtonDown("Jump")) 
		{ 
			yCurrentInputState = inputState.Jump;
		}
	}


	void FixedUpdate(){



		UpdatePhysics();
	}
}
