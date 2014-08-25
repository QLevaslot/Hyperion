using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour {

	//public MyTeam myTeam = MyTeam.Team1;
	
	public enum inputState 
	{ 
		None, 
		WalkLeft, 
		WalkRight, 
		Jump, 
	}
	[HideInInspector] public inputState currentInputState;
	
	[HideInInspector] public enum facing { Right, Left }
	[HideInInspector] public facing facingDir;
	
	[HideInInspector] public bool alive = true;
	[HideInInspector] public Vector3 spawnPos;
	
	protected Transform _transform;
	protected Rigidbody2D _rigidbody;
	
	// Tune character movement	
	protected float runVel = 6f; 	
	protected float walkVel = 2f; 	
	protected float jumpVel = 7f; 	// jump velocity
	protected float jump2Vel = 3f; 	// double jump velocity
	protected float fallVel = 2f;		// fall velocity, (additional) gravity

	protected float moveVel;

	// Input defines part of the speed
	protected float xAcceleration = 1f;
	protected float yAcceleration = 1f;
	
	private int jumps = 0;
	public int maxJumps = 1; 		// set to 1 for double jump
	
	// raycast stuff
	private RaycastHit2D hit;
	private Vector2 physVel = new Vector2();
	[HideInInspector] public bool grounded = false;
	private int groundMask = 1 << 8;
	
	public virtual void Awake()
	{
		_transform = transform;
		_rigidbody = rigidbody2D;
	}
	
	// Use this for initialization
	public virtual void Start () 
	{
		moveVel = walkVel;
	}
	
	// Update is called once per frame
	public virtual void UpdateMovement() 
	{
		// what happens when i do something wrong?

	}
	
	// Fixed update from PlayerScript
	public virtual void UpdatePhysics()
	{

		physVel = Vector2.zero;
		
		// move left
		if(currentInputState == inputState.WalkLeft || currentInputState == inputState.WalkRight)
		{
			physVel.x = moveVel * xAcceleration;
		}

		// jump
		if(currentInputState == inputState.Jump)
		{
			if(jumps < maxJumps)
			{
				jumps += 1;
				if(jumps == 1)
				{
					_rigidbody.velocity = new Vector2(physVel.x, jumpVel);
				}
				else if(jumps == 2)
				{
					_rigidbody.velocity = new Vector2(physVel.x, jump2Vel);
				}
			}
		}

		// use raycasts to determine if the player is standing on the ground or not
		if (Physics2D.Raycast(new Vector2(_transform.position.x-0.26f,_transform.position.y-0.36f), -Vector2.up, .26f, groundMask) 
		    || Physics2D.Raycast(new Vector2(_transform.position.x+0.26f,_transform.position.y-0.36f), -Vector2.up, .26f, groundMask))
		{
			grounded = true;
			jumps = 0;
		}
		else
		{
			grounded = false;
			_rigidbody.AddForce(-Vector3.up * fallVel);
		}
		//wall jump
		if (Physics2D.Raycast(new Vector2(_transform.position.x+0.36f,_transform.position.y-0.26f), Vector2.right, .26f, groundMask) 
		    || Physics2D.Raycast(new Vector2(_transform.position.x+0.36f,_transform.position.y-0.26f), Vector2.right, .26f, groundMask)
		    || Physics2D.Raycast(new Vector2(_transform.position.x-0.36f,_transform.position.y+0.26f), -Vector2.right, .26f, groundMask)
		    || Physics2D.Raycast(new Vector2(_transform.position.x-0.36f,_transform.position.y+0.26f), -Vector2.right, .26f, groundMask))
		{
			Debug.Log ("WallJump");
			jumps = 0;
		}

		// actually move the player
		_rigidbody.velocity = new Vector2(physVel.x, _rigidbody.velocity.y);
	}

}
