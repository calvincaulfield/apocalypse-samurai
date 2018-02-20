using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour {

	public static Vector3 cameraOffset = new Vector3 (10, 14, -10);

	public GameObject camera;
	public GameObject terrain;
	public Light light;
	public GameObject map;
	public GameObject statusUi;
	public GameObject enemy;
	public GameObject item;	
	public PlayerController playerController;
	public GameObject tutorialObject;

	public Text text_Hp;
	public Text text_Stamina;
	public Text text_Level;
	public Text text_Exp;

	public Slider hpSlider;
	public Slider staminaSlider;

	public Text text_Alert;
	public float alertDuration;
	private float alertEndTime;

	public bool noTutorialMode;
	public Tutorial tutorial;
	public int tutorialStage;
	public string language;
	public UiLanguage uiLanguage;
	public bool inTutorialMode;


	void Start() {
		GetComponent<Bgm> ().PlayBgm (1);
		InitTutorial();
	}

	public string GetWord(string key) {
		return uiLanguage.GetWord(language, key);
	}

	public void SetLevel(int level) {
		text_Level.text = "Level " + level;
	}

	public void SetHp(float hp, float maxHp) {
		text_Hp.text = GetWord("Hp") + " " + Beautify(hp) + " / " + Beautify(maxHp);
		hpSlider.maxValue = maxHp;
		hpSlider.value = hp;
		hpSlider.GetComponent<RectTransform> ().sizeDelta = new Vector2 (maxHp * 2, 100);
	}

	public void SetExp(float exp, float maxExp) {
		text_Exp.text = GetWord("Exp") + " " + exp + " / " + maxExp;
	}

	public void SetStamina(float stamina, float maxStamina) {
		text_Stamina.text = GetWord("Stamina") + " " + Beautify(stamina) + " / " + Beautify(maxStamina);
		staminaSlider.maxValue = maxStamina;
		staminaSlider.value = stamina;
		staminaSlider.GetComponent<RectTransform> ().sizeDelta = new Vector2 (maxStamina * 5, 100);
	}

	public void AlertLevelUp(int level) {
		text_Alert.text = GetWord("Level") + " " + level;
		alertEndTime = Time.time + alertDuration;
	}

	bool ProceedButtonTouched() {
		return Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);
	}

	void Update() {
		MoveCamera ();
		clearAlertIfNeeded ();
		if (inTutorialMode && ProceedButtonTouched()) {
			ProceedTutorial();
		}
	}

	void clearAlertIfNeeded() {
		if (Time.time > alertEndTime) {
			text_Alert.text = "";
		}	
	}

	string GetTutorialKey(int number) {
		if (number < 10) {
			return "Tutorial_0" + number;
		} else {
			return "Tutorial_" + number;
		}
	}

	void InitTutorial() {
		if (noTutorialMode) {
			return;
		}

		//tutorialStage = 0;
		inTutorialMode = true;

		if (tutorialStage == 0) {
			light.gameObject.SetActive(false);
			terrain.SetActive(false);
			playerController.gameObject.SetActive(false);
			map.SetActive(false);

			statusUi.SetActive(false);
			enemy.SetActive(false);
			item.SetActive(false);

			tutorialObject.SetActive(false);
		}

	}

	void StopTutorial() {
		inTutorialMode = false;
		Time.timeScale = 1.0f;
	} 

	bool isLastMessage(int tutorialStage) {
		return tutorialStage == 11 || tutorialStage == 22 || tutorialStage == 33;
	}

	void ProceedTutorial() {
		if (noTutorialMode) {
			return;
		}

		Time.timeScale = 0.0f;

		switch (tutorialStage) {
			case 11:
			case 22:
			case 33:
			case 41:
				StopTutorial();
				break;
		}	
		
		// Manage transition
		switch (tutorialStage) {
			case 11: 
				tutorialStage = 20;
				break;
			case 22:
				tutorialStage = 30;
				break;
			case 33:
				tutorialStage = 40;
				playerController.qAttackTotalNumber = 0;
				break;
			case 41:
				tutorialStage = 50;
				break;
			default:
				tutorialStage++;
				break;
		}

		// Do things for the new stage
		switch (tutorialStage) {
			case 3:
				light.gameObject.SetActive(true);
				break;
			case 4: 
				terrain.SetActive(true);
				break;
			case 5:
				playerController.gameObject.SetActive(true);
				break;
			case 6:
				map.SetActive(true);
				break;
			case 20:
				tutorialObject.SetActive(true);
				break;
			case 30:
				enemy.SetActive(true);
				statusUi.SetActive(true);
				break;
		}

		showTutorialText(tutorialStage);		
	}

	void showTutorialText(int stage) {
		tutorial.ShowGuide(stage, uiLanguage.GetWord(language, GetTutorialKey(stage)));
	}

	public void TutorialObjectiveComplete(int stage) {
		if (tutorialStage == stage) {
			inTutorialMode = true;
			ProceedTutorial();
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
