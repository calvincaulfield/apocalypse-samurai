using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

	public int targetToTouch;
	public AudioSource touchSound;
	public int bonusTargetNumber;
	public int bonusExp;
	public PlayerController player;
	public Text guide;
	public GameController gameController;

	// Use this for initialization
	void Start () {
		targetToTouch = 1;

		guide.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public bool touched(TutorialTarget target) {
		if (target.number == targetToTouch) {
			targetToTouch++;
			touchSound.Play();
			if (target.number == bonusTargetNumber) {
				player.EarnExp(bonusExp);
			} else if (target.number == (bonusTargetNumber - 1)) {
				
				gameController.TutorialObjectiveComplete();
			}
			return true;
		} else {
			return false;
		}
	}

	
	public void ShowGuide(int tutorialStage, string text) {
		guide.text = text;
	}
}
