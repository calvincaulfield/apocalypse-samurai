using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTarget : MonoBehaviour {

	public int number;
	public TextMesh text;
	public Tutorial tutorial;

	// Use this for initialization
	void Start () {
		text.text = "" + number;
	}

	private void OnTriggerEnter(Collider other) {
		if (tutorial.touched(this)) {
			Destroy(gameObject);
		}
	}
}
