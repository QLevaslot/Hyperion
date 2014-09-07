using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour {

	// Input received by PlayerInputScript
	public enum inputState 
	{ 
		None, 
		Walk, 
		Run, 
		Jump, 
	}
	
	// Character calculated state (from player input and his surrounding environement
	public enum characterState {
		Idle,
		Walk,
		Run,
		Jump,
		SlideWall
	}

	public enum characterDirection {
		Left,
		Right
	}

	[HideInInspector] public inputState xCurrentInputState;
	[HideInInspector] public inputState yCurrentInputState;
	
	[HideInInspector] public characterDirection currentCharacterDirection;
	[HideInInspector] public characterState currentCharacterState;
	

	public static SoundScript soundManager;
	
	//[HideInInspector] public Vector3 spawnPos;
	
	protected Transform _transform;
	protected Rigidbody2D _rigidbody;

	// Tune character movement	
	protected float runVel = 15f; 	
	protected float walkVel = 5f; 	
	protected float jumpVel = 10f; 	// jump velocity
	protected float jump2Vel = 5f; 	// double jump velocity
	protected float fallVel = 2f;	// fall velocity, (additional) gravity

	protected float moveVel;

	// Input defines part of the speed
	protected float xAcceleration = 1f;
	protected float yAcceleration = 1f;
	
	protected int jumps = 0;
	protected int maxJumps = 1;
	
	// raycast stuff
	private RaycastHit2D hit;
	private Vector2 physVel = new Vector2();
	[HideInInspector] public bool grounded = false;
	private int groundMask = 1 << 8;
	
	public virtual void Awake () {
		_transform = transform;
		_rigidbody = rigidbody2D;
	}
	
	// Use this for initialization
	public virtual void Start () {
		moveVel = walkVel;
	}

	// Fixed update from PlayerScript
	public virtual void UpdatePhysics (){ 

		currentCharacterState = characterState.Idle;
		
		physVel = Vector2.zero;

		// move left
		if(xCurrentInputState == inputState.Walk)
		{
			physVel.x = walkVel * xAcceleration;
			currentCharacterState = characterState.Walk;
		} else if(xCurrentInputState == inputState.Run){
			currentCharacterState = characterState.Run;
			physVel.x = runVel * xAcceleration;
		}

		if(detectFall())
		{
			grounded = false;
			currentCharacterState = characterState.Jump;
			_rigidbody.AddForce(-Vector3.up * fallVel);
		}
		else {
			grounded = true;
			jumps = 0;
		}

		//wall jump
		if (!grounded && detectWallSlide ()) {
			jumps = 0;
		}

		// jump (after raycasting to avoid unfortunate jump reset even before actually jumping)
		if(yCurrentInputState == inputState.Jump)
		{
			if(jumps < maxJumps)
			{
				GlobalHandlers.soundManager.PlayJump();
				jumps ++;
				if(jumps == 1)
				{
					_rigidbody.velocity = new Vector2(physVel.x, jumpVel);
				}
				else {// Keeping it for later, no more double jump for now
					_rigidbody.velocity = new Vector2(physVel.x, jump2Vel);
				}
			}
		}

		// actually move the player
		_rigidbody.velocity = new Vector2(physVel.x, _rigidbody.velocity.y);

		// inputstate reset once the command has been executed
		xCurrentInputState = inputState.None;
		yCurrentInputState = inputState.None;
	}

	// Detect if the character is falling. Update the character state accordingly
	bool detectFall () {
		Debug.DrawRay (new Vector2(_transform.position.x-0.26f,_transform.position.y-0.32f), -Vector2.up, Color.yellow);
		Debug.DrawRay (new Vector2(_transform.position.x+0.26f,_transform.position.y-0.32f), -Vector2.up, Color.yellow);
		
		// use raycasts to determine if the player is standing on the ground or not
		if (Physics2D.Raycast (new Vector2 (_transform.position.x - 0.26f, _transform.position.y - 0.32f), -Vector2.up, .03f, groundMask) 
			|| Physics2D.Raycast (new Vector2 (_transform.position.x + 0.26f, _transform.position.y - 0.32f), -Vector2.up, .03f, groundMask)) {
			return false;
		} else {
			return true;
		}
	}

	// Detect if the player is sliding against a wall. We also update the character state accordingly
	bool detectWallSlide () {
		Debug.DrawRay (new Vector2 (_transform.position.x + 0.32f, _transform.position.y - 0.26f), Vector2.right, Color.green);
		Debug.DrawRay (new Vector2(_transform.position.x+0.32f,_transform.position.y+0.26f), Vector2.right, Color.green);
		Debug.DrawRay (new Vector2(_transform.position.x-0.32f,_transform.position.y+0.26f), -Vector2.right, Color.green);
		Debug.DrawRay (new Vector2(_transform.position.x-0.32f,_transform.position.y-0.26f), -Vector2.right, Color.green);

		// Split it in 2. Need to know if we're against left or right wall.
		if (Physics2D.Raycast (new Vector2 (_transform.position.x + 0.32f, _transform.position.y - 0.26f), Vector2.right, .03f, groundMask) 
						|| Physics2D.Raycast (new Vector2 (_transform.position.x + 0.32f, _transform.position.y + 0.26f), Vector2.right, .03f, groundMask)) {
			currentCharacterState = characterState.SlideWall;
			currentCharacterDirection = characterDirection.Left;
			return true;
		} else if(Physics2D.Raycast (new Vector2 (_transform.position.x - 0.32f, _transform.position.y + 0.26f), -Vector2.right, .03f, groundMask)
			|| Physics2D.Raycast (new Vector2 (_transform.position.x - 0.32f, _transform.position.y - 0.26f), -Vector2.right, .03f, groundMask)) {
			currentCharacterState = characterState.SlideWall;
			currentCharacterDirection = characterDirection.Right;
			return true;		
		} else {
			return false;		
		}
	} 

}
