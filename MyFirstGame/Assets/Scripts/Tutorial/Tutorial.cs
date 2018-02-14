using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

	public int targetToTouch;
	public AudioSource touchSound;

	// Use this for initialization
	void Start () {
		targetToTouch = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool touched(TutorialTarget target) {
		if (target.number == targetToTouch) {
			targetToTouch++;
			touchSound.Play();
			return true;
		} else {
			return false;
		}
	}
}
