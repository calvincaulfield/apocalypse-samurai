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

	public AudioSource attackVoice;

	public Weapon weapon;
	public GameObject info;
	public GameController gameController;
	public Collider terrain;

	private float attackDuration = 0.5f;
	private float attackPreparePartRatio = 0.7f; 
	private GameObject camera;
	private float attackBeginTime;
	private float attackEndTime;
	private WeakBody weakBody;

	void Start () {
		camera = GameObject.FindWithTag ("MainCamera");
		weakBody = GetComponent<WeakBody> ();
		level = 1;
		exp = 0;
		stamina = maxStamina;
	}
		
	public void Kill(EnemyController enemy) {
		GameObject thisObject = Instantiate (info, info.transform.position, Quaternion.identity);
		thisObject.SetActive (true);
		thisObject.GetComponent<TextMesh> ().color = ColorFromHex(infoExpColor);
		thisObject.GetComponent<TextMesh> ().text = "Exp +" + enemy.exp;

		exp += enemy.exp;
		if (exp >= level * expPerLevel) {
			level += 1;
			exp = 0;
			gameController.AlertLevelUp ();
		}
	}

	public void TakeItem(Item item) {
		if (item.type == "heal") {
			if (weakBody.hp == weakBody.maxHp) {
				return;
			}
			Heal (item.intensity);		}

		item.takenEffect.Play ();
		Destroy (item.gameObject);
	}

	void Heal(float amount) {
		GameObject thisObject = Instantiate (info, info.transform.position, Quaternion.identity);
		thisObject.SetActive (true);
		thisObject.GetComponent<TextMesh> ().color = ColorFromHex(infoHealColor);
		thisObject.GetComponent<TextMesh> ().text = "HP +" + Mathf.RoundToInt(amount);

		weakBody.Heal (amount);
	}

	void FixedUpdate() {
		// Attack motion
		float currentTime = Time.time;
		if (currentTime > attackBeginTime && currentTime < attackEndTime) {
			float prepareTime = attackDuration * attackPreparePartRatio;
			float realAttackTime = attackDuration - prepareTime;
			GameObject rightArm = GameObject.FindWithTag ("RightArm");
			Vector3 original = rightArm.transform.localRotation.eulerAngles;
			if (currentTime < attackBeginTime + prepareTime) { // prepare motion
				float beginAngle = 0;
				float endAngle = -90;
				float progressRatio = (currentTime - attackBeginTime) / prepareTime;
				rightArm.transform.localRotation = Quaternion.Euler (new Vector3 (beginAngle + (endAngle - beginAngle) * progressRatio, original.y, original.z));
				//rightArm.transform.localScale = new Vector3(10, 10, 10);
				//Destroy (rightArm);
				//Debug.Log ("Here1:  " + progressRatio + rightArm);
			} else {
				if (!weapon.inAttackMotion) {
					weapon.StartAttack ();
				}
				float beginAngle = -90;
				float endAngle = 20;
				float progressRatio = (currentTime - (attackBeginTime + prepareTime)) / realAttackTime;
				rightArm.transform.localRotation = Quaternion.Euler (new Vector3 (beginAngle + (endAngle - beginAngle) * progressRatio, original.y, original.z));
				//Debug.Log("Here2:  " + progressRatio);
			}
		} else {

		}
	}
		
	void Update() {
		float currentTime = Time.time;
		// Ignore input while in attack motion
		if (currentTime < attackEndTime) {
			return;
		}
		weapon.StopAttack ();

		UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		agent.enabled = true;
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (terrain.Raycast(ray, out hit, 1000)) {
			if (Input.GetMouseButton(1)) { // move unit
				// Immediately face destination
				Face(hit.point);
				agent.destination = hit.point;	
			}

			if (Input.GetKey(KeyCode.Q)) {
				Attack(hit.point);
			}
		}

		manageStamina ();

		gameController.SetLevel (level);
		gameController.SetHp (weakBody.hp, weakBody.maxHp);
		gameController.SetExp (exp, level * expPerLevel);
		gameController.SetStamina (stamina, maxStamina);
	}

	void Face(Vector3 destination) {
		Vector3 direction = destination - transform.position;
		transform.rotation = Quaternion.LookRotation (new Vector3(direction.x, 0, direction.z));
		GetComponent<Rigidbody>().velocity = Vector3.zero;
	}


	void Attack(Vector3 destination) {
		Face (destination);
		if (stamina > 0) {
			stamina -= weapon.staminaCost;
			attackBeginTime = Time.time;
			attackEndTime = Time.time + attackDuration;
			GetComponent<UnityEngine.AI.NavMeshAgent> ().enabled = false;
			attackVoice.Play ();
		} else {
			notEnoughStamina ();
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
		ColorUtility.TryParseHtmlString (hex, out color);
		return color;
	}
}