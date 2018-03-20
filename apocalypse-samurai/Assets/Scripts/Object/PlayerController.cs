using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float stamina;
	public float maxStamina;
	public float staminaRecoveryTime;
	public float staminaRecoveryPerSec;
	public int level;
	public int exp;
	public int expPerLevel;

	public string infoExpColor;
	public string infoHealColor;

	public float qStamina;
	public float wStamina;
	public float qDamage;
	public float wDamage;

	public AudioSource attackVoice;

	public Weapon weapon;
	public GameObject info;
	public GameController gameController;
	public Collider terrain;

	public float qSpeed = 2.0f;
	public float qPreparePartRatio = 0.7f;
	public float qRecoveryTime = 0.3f;

	public float wSpeed = 1.0f;
	public float wRecoveryTime = 0.3f;
	private float wStartYRotation = 0.0f;
	public int qAttackTotalNumber = 0;
	public int wAttackTotalNumber = 0;

	private float attackBeginTime;
	private float attackEndTime;
	private bool inAttackPrepareMotion;
	private string attackType;

	private WeakBody weakBody;	

	void Start() {
		weakBody = GetComponent<WeakBody>();
		level = 1;
		exp = 0;
		stamina = maxStamina;
		inAttackPrepareMotion = false;

		if (GameController.isRetry)
		{
			GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
			transform.rotation = Quaternion.identity;
			transform.position = new Vector3(129, 3, 115);
			GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
		}
	}

	public void EarnExp(int earnedExp) {
		GameObject thisObject = Instantiate(info, info.transform.position, Quaternion.identity);
		thisObject.SetActive(true);
		thisObject.GetComponent<TextMesh>().color = ColorFromHex(infoExpColor);
		thisObject.GetComponent<TextMesh>().text = "Exp +" + earnedExp;

		exp += earnedExp;
		while (exp >= level * expPerLevel) {			
			exp -= level * expPerLevel;
			level += 1;

			float hpIncrease = 10.0f;
			float staminaIncrease = 2.0f;
			float damageIncrease = 0.2f;

			gameController.AlertLevelUp(level, hpIncrease, staminaIncrease);
			weakBody.hp += hpIncrease;
			weakBody.maxHp += hpIncrease;
			maxStamina += staminaIncrease;

			qDamage += damageIncrease;
			wDamage += damageIncrease;
		}
	}

	public void Kill(Unit enemy) {
		EarnExp(enemy.exp);
		if (enemy.id == "Enemy_05")
		{
			Debug.Log("win");
			gameController.winGame();
		}
	}

	public void TakeItem(Item item) {
		if (item.type == "heal") {
			if (weakBody.hp == weakBody.maxHp) {
				return;
			}
			Heal(item.intensity);
		}

		item.takenEffect.Play();
		item.gameObject.SetActive(false);
	}

	void Heal(float amount) {
		GameObject thisObject = Instantiate(info, info.transform.position, Quaternion.identity);
		thisObject.SetActive(true);
		thisObject.GetComponent<TextMesh>().color = ColorFromHex(infoHealColor);
		thisObject.GetComponent<TextMesh>().text = "HP +" + Mathf.RoundToInt(amount);

		weakBody.Heal(amount);
	}

	void ShowAttackMotion() {
		float currentTime = Time.time;
		float attackDuration = GetAttackDuration();
		if (currentTime > attackBeginTime && currentTime < attackEndTime) {
			if (attackType == "Q") {
				float prepareTime = (attackDuration - qRecoveryTime) * qPreparePartRatio;
				float realAttackTime = (attackDuration - qRecoveryTime) - prepareTime;
				GameObject rightArm = GameObject.FindWithTag("RightArm");
				Vector3 original = rightArm.transform.localRotation.eulerAngles;
				if (currentTime < attackBeginTime + prepareTime) { // prepare motion
					float beginAngle = 0;
					float endAngle = -90;
					float progressRatio = (currentTime - attackBeginTime) / prepareTime;
					rightArm.transform.localRotation = Quaternion.Euler(new Vector3(
						beginAngle + (endAngle - beginAngle) * progressRatio, original.y, original.z));
				} else {
					// Player will be freezed during recovery time
					if (currentTime > attackBeginTime + attackDuration - qRecoveryTime) { 
						return;
					}

					if (inAttackPrepareMotion) {
						inAttackPrepareMotion = false;
						weapon.StartAttack();
					}
					float beginAngle = -90;
					float endAngle = 20;
					float progressRatio = (currentTime - (attackBeginTime + prepareTime)) / realAttackTime;
					rightArm.transform.localRotation = Quaternion.Euler(new Vector3(
						beginAngle + (endAngle - beginAngle) * progressRatio, original.y, original.z));
					//Debug.Log("Here2:  " + progressRatio);
				}
			} else if (attackType == "W") {
				// Player will be freezed during recovery time
				if (currentTime > attackBeginTime + attackDuration - wRecoveryTime) { 
					return;
				}
				if (inAttackPrepareMotion) {
					weapon.StartAttack();
					inAttackPrepareMotion = false;					
					wStartYRotation = transform.localRotation.eulerAngles.y;
				}
				// W attack is piercing; it can hit multiple enemies with single swing
				weapon.inAttackMotion = true;
				float beginAngle = 30;
				float endAngle = 360;
				float progressRatio = (currentTime - attackBeginTime) / (attackDuration - wRecoveryTime);
				transform.localRotation = Quaternion.Euler(new Vector3(
					0, wStartYRotation + beginAngle + (endAngle - beginAngle) * progressRatio, 0));
			}
		} else {
		}
	}

	float GetAttackDuration() {
		if (attackType == "Q") {
			return 1 / qSpeed + qRecoveryTime;
		} else if (attackType == "W") {
			return 1 / wSpeed + wRecoveryTime;
		} else {
			return 5;
		}
	}

	void Update() {
		ShowAttackMotion();

		manageStamina();

		gameController.SetLevel(level);
		gameController.SetHp(weakBody.hp, weakBody.maxHp);
		gameController.SetExp(exp, level * expPerLevel);
		gameController.SetStamina(stamina, maxStamina);

		bool isInAttackMotion = IsInAttackMotion();
		
		if (isInAttackMotion) {
			// Ignore input while in certain attack motions
			if (attackType == "Q") {
				return;
			}
		} else {
			if (weapon.inAttackMotion) {
				weapon.StopAttack();
			}
		}

		// Process input command 
		UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		agent.enabled = true;
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (terrain.Raycast(ray, out hit, 1000)) {
			if (Input.GetMouseButton(1)) { // right click: move 
				// Immediately face destination
				// Face(hit.point);
				agent.destination = hit.point;
			}

			if (!isInAttackMotion) {
				if (Input.GetKey(KeyCode.Q)) {
					weapon.attackDamage = qDamage;
					attackType = "Q";
					Attack(hit.point);
				} else if (Input.GetKey(KeyCode.W)) {
					weapon.attackDamage = wDamage;
					attackType = "W";
					Attack(hit.point);
				} else if (Input.GetKey(KeyCode.E)) {
					attackType = "E";
					Attack(hit.point);
				}				
			}
		}
	}

	bool IsInAttackMotion() {
		return Time.time < attackEndTime;
	}

	void Face(Vector3 destination) {
		Vector3 direction = destination - transform.position;
		transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		GetComponent<Rigidbody>().velocity = Vector3.zero;
	}


	void Attack(Vector3 destination) {
		float currentTime = Time.time;
		if (GetComponent<Unit>().isInHitRecovery()) {
			return;
		}

		Face(destination);
		float attackDuration = GetAttackDuration();
		if (stamina > 0) {
			if (attackType == "Q")
			{
				stamina -= qStamina;
			} else if (attackType == "W")
			{
				stamina -= wStamina;
			} else
			{
				Debug.Log("Attack error");
			}
			attackBeginTime = currentTime;
			attackEndTime = currentTime + attackDuration;
			inAttackPrepareMotion = true;

			GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
			attackVoice.Play();

			// Tutorial progress
			if (attackType == "Q") {
				qAttackTotalNumber++;
				if (qAttackTotalNumber == 3) {
					gameController.TutorialObjectiveComplete(40);
				}
			} else if (attackType == "W")
			{
				wAttackTotalNumber++;
				if (wAttackTotalNumber == 3)
				{
					gameController.TutorialObjectiveComplete(70);
				}
			}
		} else {
			notEnoughStamina();
		}
	}


	void manageStamina() {
		if (Time.time > attackEndTime + staminaRecoveryTime) {
			stamina += Time.deltaTime * staminaRecoveryPerSec;
			if (stamina > maxStamina) {
				stamina = maxStamina;
			}
		}
	}

	void notEnoughStamina() {

	}

	Color ColorFromHex(string hex) {
		Color color;
		ColorUtility.TryParseHtmlString(hex, out color);
		return color;
	}
}