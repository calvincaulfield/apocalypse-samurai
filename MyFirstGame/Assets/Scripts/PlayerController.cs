using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	//public float accelaration = 5;
	public float speed = 5.0f;
	public float maxSpeed = 30.0f;

	public AudioSource swordSwing;

	private float attackDuration = 0.5f;
	private float attackPreparePartRatio = 0.7f; 

	private int count;
	private GameObject camera;
	//private bool isSelected; 
	private float attackBeginTime;
	private float attackEndTime;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		count = 0;
		//isSelected = false;
		camera = GameObject.FindWithTag ("MainCamera");
	}


	void FixedUpdate() {
		/*
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		rb.AddForce (new Vector3(moveHorizontal, 0.0f, moveVertical ) * speed);
		if (rb.velocity.magnitude > maxSpeed) {
			rb.velocity = rb.velocity.normalized * maxSpeed;
		}*/

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
				GameObject.FindWithTag ("Sword").GetComponent<Weapon> ().inAttackMotion = true;
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
		GameObject.FindWithTag ("Sword").GetComponent<Weapon> ().inAttackMotion = false;

		UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		agent.enabled = true;
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit)) {
			/*
			if (Input.GetMouseButtonDown (0)) { // select unit
				//Debug.Log("Left click");
				bool isClicked = hit.collider == this.GetComponent<Collider>();
				//Debug.Log("Clicked");
				if (Input.GetKey(KeyCode.LeftShift)) { // addition, subtraction
					if (isClicked) {
						selectSelf(!isSelected);
					}
				} else { // reset selection
					if (isClicked) {
						selectSelf(true);
					} else {
						selectSelf(false);
					}
				}
			}
			*/
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



	/*
	void SelectSelf(bool toBeSelected) {
		isSelected = toBeSelected;
		if (toBeSelected) {
			Debug.Log ("Selected");
			GetComponent<Renderer>().material.SetColor("_Color", Color.blue);


		} else {
			Debug.Log ("Unselected");
			GetComponent<Renderer>().material.SetColor("_Color", Color.red);
		}
	}*/
	void Face(Vector3 destination) {
		Vector3 direction = destination - transform.position;
		transform.rotation = Quaternion.LookRotation (new Vector3(direction.x, 0, direction.z));
		rb.velocity = Vector3.zero;
	}


	void moveTo(Vector3 destination) {
		Vector3 direction = destination - transform.position;
		Vector3 velocity = new Vector3 (direction.x, 0, direction.z);
		rb.velocity = velocity.normalized * speed;
	}

	void Attack(Vector3 destination) {
		Face (destination);
		attackBeginTime = Time.time;
		attackEndTime = Time.time + attackDuration;
		GetComponent<UnityEngine.AI.NavMeshAgent> ().enabled = false;
		swordSwing.Play ();
	}

	void MoveCamera() {
		Vector3 currentCameraPosition = camera.transform.position;
		camera.transform.position = new Vector3 (transform.position.x + 10, 14, transform.position.z - 10);
	}
}