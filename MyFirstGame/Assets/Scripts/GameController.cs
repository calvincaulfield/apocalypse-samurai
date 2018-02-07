using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour {

	public static Vector3 cameraOffset = new Vector3 (10, 14, -10);

	public GameObject camera;
	public PlayerController playerController;

	public Text text_Level;
	public Text text_Hp;
	public Text text_Exp;
	public Text text_Alert;

	public float alertDuration;

	private float alertEndTime;

	public void SetLevel(int level) {
		text_Level.text = "レベル " + level;
	}

	public void SetHp(int hp, int maxHp) {
		text_Hp.text = "HP " + hp + " / " + maxHp;
	}

	public void SetExp(int exp, int maxExp) {
		text_Exp.text = "経験値 " + exp + " / " + maxExp;
	}

	public void AlertLevelUp() {

		text_Alert.text = "Level Up!";
		alertEndTime = Time.time + alertDuration;
	}

	void Update() {
		MoveCamera ();
		clearAlertIfNeeded ();
	}

	void clearAlertIfNeeded() {
		if (Time.time > alertEndTime) {
			text_Alert.text = "";
		}	
	}

	void MoveCamera() {
		camera.transform.position = playerController.gameObject.transform.position + cameraOffset;
	}
}
