using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public bool inAttackMotion;

	// Collision with enemy
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Enemy") {
			Debug.Log ("Collided!");
			Vector3 collisionPoint = GetComponent<Collider> ().ClosestPointOnBounds (other.transform.position);
			Instantiate (bloodSpray, collisionPoint, Quaternion.identity);
		}
	}
}
