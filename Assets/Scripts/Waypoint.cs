using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {

	public int tutorialStage;

	public GameController gameController;


	private void OnTriggerEnter(Collider other) {
		//Debug.Log(other.tag);
		if (gameController.noTutorialMode)
		{
			gameObject.SetActive(false);
		}

		if (other.gameObject.tag == "Player" && gameController.tutorialStage == tutorialStage) {
			gameController.TutorialObjectiveComplete(tutorialStage);
			gameObject.SetActive(false);
		}
	}
}
