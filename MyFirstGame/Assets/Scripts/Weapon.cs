using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public bool inAttackMotion;
	public GameObject explosion;
	public AudioSource explosionSound;

	// Collision with enemy
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Enemy" && inAttackMotion) {
			//Debug.Log ("Collided!");
			Vector3 collisionPoint = GetComponent<Collider> ().ClosestPointOnBounds (other.transform.position);
			Instantiate (explosion, collisionPoint, Quaternion.identity);
			explosionSound.Play ();
			Destroy (other.gameObject);
		}
	}
}
