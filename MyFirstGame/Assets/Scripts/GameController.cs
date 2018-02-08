using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour {

	public static Vector3 cameraOffset = new Vector3 (10, 14, -10);

	public GameObject camera;
	public PlayerController playerController;

	public Text text_Hp;
	public Text text_Stamina;
	public Text text_Level;
	public Text text_Exp;

	public Slider hpSlider;
	public Slider staminaSlider;

	public Text text_Alert;
	public float alertDuration;
	private float alertEndTime;

	void Start() {
		GetComponent<Bgm> ().PlayBgm (1);
	}

	public void SetLevel(int level) {
		text_Level.text = "Level " + level;
	}

	public void SetHp(float hp, float maxHp) {
		text_Hp.text = "HP " + Beautify(hp) + " / " + Beautify(maxHp);
		hpSlider.maxValue = maxHp;
		hpSlider.value = hp;
		hpSlider.GetComponent<RectTransform> ().sizeDelta = new Vector2 (maxHp * 2, 100);
	}

	public void SetExp(float exp, float maxExp) {
		text_Exp.text = "Exp " + exp + " / " + maxExp;
	}

	public void SetStamina(float stamina, float maxStamina) {
		text_Stamina.text = "Stamina " + Beautify(stamina) + " / " + Beautify(maxStamina);
		staminaSlider.maxValue = maxStamina;
		staminaSlider.value = stamina;
		staminaSlider.GetComponent<RectTransform> ().sizeDelta = new Vector2 (maxStamina * 5, 100);
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

	int Beautify(float number) {
		int integer = Mathf.RoundToInt (number);
		if (integer < 0) {
			integer = 0;
		}
		return integer;
	}
}
