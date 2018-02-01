using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_01_Controller : MonoBehaviour {

	public float minHeight;
	public float maxHeight;
	public float frequency;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float period = 1 / frequency;
		float ratio = 2 * (Time.time % period) / period; // 0 ~ 2 per period sec
		float height = minHeight + (maxHeight - minHeight) * ratio;
		if (height > maxHeight) {
			height -= 2 * (height - maxHeight);
		}
		transform.position = new Vector3(transform.position.x, height, transform.position.z);
	}

	void OnTriggerEnter (Collider other) {
		
		if (other.tag == "Sword") {			
			Weapon weapon = other.GetComponent<Weapon> ();
			if (weapon.inAttackMotion) {
				Debug.Log ("collide");
				//Destroy (gameObject);
			}
		} else {


		}
	}
}
