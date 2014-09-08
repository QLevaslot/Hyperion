using UnityEngine;
using System.Collections;

public class PlayerAnimScript : MonoBehaviour {

	private Transform _transform;
	private Animator _animator;
	private CharacterScript character;
	
	public enum anim 
	{ 
		Idle,
		WalkLeft,
		WalkRight,
		RunLeft,
		RunRight,
		JumpLeft,
		JumpRight,
		SlideWall
	}

	private anim currentAnim = anim.Idle;
	private int _p1AnimState = Animator.StringToHash("Player1AnimState");

	protected Vector2 characterScale ;

	void Awake()
	{
		_transform = transform;
		_animator = this.GetComponent<Animator>();
		character = this.GetComponent<CharacterScript>();
		
		characterScale = _transform.localScale;
	}

	void Update() {
		// Ugh, sorry, code is a bit of a mess here 
		// TODO add more anim states
		if(character.currentCharacterState == CharacterScript.characterState.Idle && currentAnim != anim.Idle) {
			// Idle 
			currentAnim = anim.Idle;
			_animator.SetInteger(_p1AnimState, 0);
			_transform.localScale = new Vector2(characterScale.x, characterScale.y);
		} else if (character.currentCharacterState == CharacterScript.characterState.Walk && character.currentCharacterDirection == CharacterScript.characterDirection.Left && currentAnim != anim.WalkLeft) {
			// Walk Left
			currentAnim = anim.WalkLeft;
			_animator.SetInteger(_p1AnimState, 1);
			_transform.localScale = new Vector3(characterScale.x, characterScale.y);	
		}  else if (character.currentCharacterState == CharacterScript.characterState.Walk && character.currentCharacterDirection == CharacterScript.characterDirection.Right && currentAnim != anim.WalkRight) {
			// Walk Right
			currentAnim = anim.WalkRight;
			_animator.SetInteger(_p1AnimState, 1);
			_transform.localScale = new Vector3(-characterScale.x, characterScale.y);	
		} else if (character.currentCharacterState == CharacterScript.characterState.Run && character.currentCharacterDirection == CharacterScript.characterDirection.Left && currentAnim != anim.RunLeft) {
			// Run Left
			currentAnim = anim.RunLeft;
			_animator.SetInteger(_p1AnimState, 1);
			_transform.localScale = new Vector3(characterScale.x, characterScale.y);	
		}  else if (character.currentCharacterState == CharacterScript.characterState.Run && character.currentCharacterDirection == CharacterScript.characterDirection.Right && currentAnim != anim.RunRight) {
			// Run Right
			currentAnim = anim.RunRight;
			_animator.SetInteger(_p1AnimState, 1);
			_transform.localScale = new Vector3(-characterScale.x, characterScale.y);	
		} else if (character.currentCharacterState == CharacterScript.characterState.Jump && character.currentCharacterDirection == CharacterScript.characterDirection.Left && currentAnim != anim.JumpLeft) {
			// Jump Left
			currentAnim = anim.JumpLeft;
			_animator.SetInteger(_p1AnimState, 1);
			_transform.localScale = new Vector3(characterScale.x, characterScale.y);	
		}  else if (character.currentCharacterState == CharacterScript.characterState.Jump && character.currentCharacterDirection == CharacterScript.characterDirection.Right && currentAnim != anim.JumpRight) {
			// Jump Right
			currentAnim = anim.JumpRight;
			_animator.SetInteger(_p1AnimState, 1);
			_transform.localScale = new Vector3(-characterScale.x, characterScale.y);	
		} else if (character.currentCharacterState == CharacterScript.characterState.SlideWall && character.currentCharacterDirection == CharacterScript.characterDirection.Left  && currentAnim != anim.SlideWall) {
			// Slide Right Wall
			currentAnim = anim.SlideWall;
			_animator.SetInteger(_p1AnimState, 1);
			_transform.localScale = new Vector3(characterScale.x, characterScale.y);
		}  else if (character.currentCharacterState == CharacterScript.characterState.SlideWall && character.currentCharacterDirection == CharacterScript.characterDirection.Right  && currentAnim != anim.SlideWall) {
			// Slide Left Wall
			currentAnim = anim.SlideWall;
			_animator.SetInteger(_p1AnimState, 1);
			_transform.localScale = new Vector3(-characterScale.x, characterScale.y);	
		} 
	}


}
