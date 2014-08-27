using UnityEngine;
using System.Collections;

public class SoundScript : MonoBehaviour {

	public AudioSource audioSource;
	
	public AudioClip jump;
	public AudioClip doubleJump;

	public void PlayJump() 
	{
		audioSource.PlayOneShot(jump, 1f);
	}

	public void PlayDouble() 
	{
		audioSource.PlayOneShot(doubleJump, 1f);
	}
}
