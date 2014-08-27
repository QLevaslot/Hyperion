using UnityEngine;
using System.Collections;

public class GlobalHandlers : MonoBehaviour {

	// this script creates a bunch of static public variables that can be seen by all the other scripts in the game
	public static SoundScript soundManager;

	void Start()
	{
		// cache these so they can be accessed by other scripts
		soundManager = gameObject.GetComponent<SoundScript>();
	}
}
