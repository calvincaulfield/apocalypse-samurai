using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoint : MonoBehaviour {
	public bool isPlayer;
	public int hp;
	public int maxHp;
	public int armour;  // 0: no armour, 1: protected
	public int exp; // earned when killed

	public GameObject tookDamageVfx;
	public GameObject explosionVfx;
	public AudioSource tookDamageSound;
	public AudioSource explosionSound;
	public GameController gameController;

	void Start() {
		hp = maxHp;
	}

	// Collision with enemy
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Weapon" && other.GetComponent<Weapon>().inAttackMotion) {
			if (!isPlayer ^ other.GetComponent<Weapon> ().playerWeapon) {
				return;
			}
	
			Debug.Log ("Weakpoint collided with weapon!");
			Vector3 collisionPoint = GetComponent<Collider> ().ClosestPointOnBounds (other.transform.position);
			if (tookDamageVfx) {
				Instantiate (tookDamageVfx, collisionPoint, Quaternion.identity);
			}
			if (tookDamageSound) {
				tookDamageSound.Play ();
			}
			hp -= other.GetComponent<Weapon> ().attackDamage * (1 - armour);
			if (hp <= 0) {
				if (explosionVfx) {
					Instantiate (explosionVfx, collisionPoint, Quaternion.identity);
				}
				if (explosionSound) {
					explosionSound.Play ();
				}
				Destroy (gameObject);
				if (!isPlayer) {
					other.GetComponent<Weapon> ().wielder.GetComponent<PlayerController>().Kill (gameObject);
				}
			}

			other.GetComponent<Weapon> ().inAttackMotion = false;
		}
	}
}
