using UnityEngine;
using System.Collections;

// Script for Player 1
// Different script for different players since controls can't be the same
public class PlayerInputScript : CharacterScript {

	// Use this for initialization
	public override void Start () {
		base.Start();
	}

	// Get input ASAP
	void Update () {
		// Axis information (pc = directional keys, xbox = left stick)
		xAcceleration = Input.GetAxis("Horizontal");
		
		// move left
		if(xAcceleration < 0f) 
		{ 
			xCurrentInputState = inputState.Walk;
			if(currentCharacterState != CharacterScript.characterState.SlideWall){
				currentCharacterDirection = characterDirection.Left;
			}
		}

		// move right
		if (xAcceleration > 0 && xCurrentInputState != inputState.Walk) 
		{ 
			xCurrentInputState = inputState.Walk;
			if(currentCharacterState != CharacterScript.characterState.SlideWall){
				currentCharacterDirection = characterDirection.Right;
			}
		}

		// run
		if (Input.GetAxis ("Run") != 0f && xCurrentInputState == inputState.Walk) {
			xCurrentInputState = inputState.Run;
		} 
		
		// jump
		if (Input.GetButtonDown("Jump")) 
		{ 
			yCurrentInputState = inputState.Jump;
		}
	}

	// Update physics once in a while (could actually be moved back in update since we're modifying acceleration)
	void FixedUpdate(){
		UpdatePhysics();
	}
}
