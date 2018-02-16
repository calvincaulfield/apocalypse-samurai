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

	public Tutorial tutorial;
	public int tutorialStage;
	public string language;
	public UiLanguage uiLanguage;

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

	void Update() {
		MoveCamera ();
		clearAlertIfNeeded ();
		DoTutorial();
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
	void showTutorialText(int stage) {
		tutorial.ShowGuide(stage, GetWord(GetTutorialKey(stage)));
	} 

	void InitTutorial() {
		tutorialStage = 1;

		terrain.SetActive(false);
		light.gameObject.SetActive(false);
		map.SetActive(false);
		statusUi.SetActive(false);
		enemy.SetActive(false);
		item.SetActive(false);
		playerController.gameObject.SetActive(false);
		tutorialObject.SetActive(false);
	}

	void DoTutorial() {
		if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
			showTutorialText(tutorialStage);
			if (tutorialStage == 3) {
				light.gameObject.SetActive(true);
			} else if (tutorialStage == 4) {
				terrain.SetActive(true);
			} else if (tutorialStage == 5) {
				playerController.gameObject.SetActive(true);
			}

			tutorialStage++;
		}
	}

	public void TutorialEnded(int tutorialStage) {
		//light.intensity = 1.0f;
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
