using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTarget : MonoBehaviour {

	public int number;
	public TextMesh text;
	public Tutorial tutorial;
	public UiLanguage uiLanguage;

	// Use this for initialization
	void Start () {
		text.text = "" + number;
	}

	private void OnTriggerEnter(Collider other) {
		//Debug.Log(other.tag);
		if (other.gameObject.tag == "Player")
		{
			if (tutorial.touched(this))
			{
				gameObject.SetActive(false);
			}
		}
	}
}
