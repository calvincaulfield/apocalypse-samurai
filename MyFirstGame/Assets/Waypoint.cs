using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {

	public int tutorialStage;

	public GameController gameController;

	private void OnTriggerEnter(Collider other) {
		if (gameController.tutorialStage == tutorialStage) {
			gameController.TutorialObjectiveComplete(tutorialStage);
			Destroy(gameObject);
		}
	}
}
