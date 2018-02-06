using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public AudioSource swordSwing;
	public int level;
	public int exp;
	public int expPerLevel;

	public GameObject weapon;

	private float attackDuration = 0.5f;
	private float attackPreparePartRatio = 0.7f; 
	private GameObject camera;
	private float attackBeginTime;
	private float attackEndTime;

	void Start () {
		camera = GameObject.FindWithTag ("MainCamera");
		level = 1;
		exp = 0;
	}
		
	public void Kill(GameObject enemy) {
		exp += enemy.GetComponent<WeakPoint> ().exp;
		if (exp >= level * expPerLevel) {
			level += 1;
			exp = 0;
		}
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
				weapon.GetComponent<Weapon> ().inAttackMotion = true;
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
		weapon.GetComponent<Weapon> ().inAttackMotion = false;

		UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		agent.enabled = true;
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit)) {
			if (Input.GetMouseButton(1)) { // move unit
				// Immediately face destination
				Face(hit.point);
				agent.destination = hit.point;	
			}

			if (Input.GetKey(KeyCode.Q)) {
				Attack(hit.point);
			}
		}
		MoveCamera ();
	}

	void Face(Vector3 destination) {
		Vector3 direction = destination - transform.position;
		transform.rotation = Quaternion.LookRotation (new Vector3(direction.x, 0, direction.z));
		GetComponent<Rigidbody>().velocity = Vector3.zero;
	}


	void Attack(Vector3 destination) {
		Face (destination);
		attackBeginTime = Time.time;
		attackEndTime = Time.time + attackDuration;
		GetComponent<UnityEngine.AI.NavMeshAgent> ().enabled = false;
		swordSwing.Play ();
	}

	void MoveCamera() {
		camera.transform.position = transform.position + GameController.cameraOffset;
	}
}