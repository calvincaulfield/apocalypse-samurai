using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour {

	public static Vector3 cameraOffset = new Vector3 (10, 14, -10);

	public PlayerController playerController;

	public Text text_Level;
	public Text text_Hp;
	public Text text_Exp;
	public int expPerLevel;


	public void SetLevel(int level) {
		text_Level.text = "レベル " + level;
	}

	public void SetHp(int hp) {
		text_Hp.text = "HP " + hp;
	}

	public void SetExp(int exp) {
		text_Exp.text = "経験値 " + exp;
	}

	void Update() {
		SetLevel (playerController.level);
		SetHp (playerController.GetComponent<WeakPoint>().hp);
		SetExp (playerController.exp);
	}
}
