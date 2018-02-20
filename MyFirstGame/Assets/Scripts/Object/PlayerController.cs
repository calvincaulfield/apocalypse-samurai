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
			gameController.AlertLevelUp(level);
		}
	}

	public void Kill(Unit enemy) {
		EarnExp(enemy.exp);
	}

	public void TakeItem(Item item) {
		if (item.type == "heal") {
			if (weakBody.hp == weakBody.maxHp) {
				return;
			}
			Heal(item.intensity);
		}

		item.takenEffect.Play();
		Destroy(item.gameObject);
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
					inAttackPrepareMotion = false;
					weapon.StartAttack();
					wStartYRotation = transform.localRotation.eulerAngles.y;
				}
				float beginAngle = 60;
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
					attackType = "Q";
					Attack(hit.point);
				} else if (Input.GetKey(KeyCode.W)) {
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
			stamina -= weapon.staminaCost;
			attackBeginTime = currentTime;
			attackEndTime = currentTime + attackDuration;
			inAttackPrepareMotion = true;

			GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
			attackVoice.Play();
			if (attackType == "Q") {
				qAttackTotalNumber++;
				if (qAttackTotalNumber == 3) {
					gameController.TutorialObjectiveComplete(40);
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