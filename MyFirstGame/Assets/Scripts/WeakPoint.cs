using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeakPoint : MonoBehaviour {
	public bool isPlayer;
	public bool isEnemy;
	public int hp;
	public int maxHp;
	public int armour;  // 0: no armour, 1: protected

	public GameObject display2;
	public EnemyController owner;

	public GameObject tookDamageVfx;
	public GameObject explosionVfx;
	public AudioSource tookDamageSound;
	public AudioSource explosionSound;


	protected virtual void Start() {
		hp = maxHp;
	}

	protected virtual void Update() {		
		if (isEnemy) {
			display2.GetComponent<TextMesh> ().text = "" + hp + "/" + maxHp;
		} else {

		}
	}

	// Collision with enemy
	protected virtual void OnTriggerEnter(Collider other) {
		if (other.tag == "Weapon" && other.GetComponent<Weapon>().inAttackMotion) {
			// Weapon cannnot harm its owner even if there is collision
			if (!isPlayer ^ other.GetComponent<Weapon> ().playerWeapon) {
				return;
			}
	
			Debug.Log ("Weakpoint collided with weapon!");
			Vector3 collisionPoint = GetComponent<Collider> ().ClosestPointOnBounds (other.transform.position);
			if (tookDamageVfx) {
				Instantiate (tookDamageVfx, collisionPoint, Quaternion.identity);
				//Instantiate (tookDamageVfx, transform.position, Quaternion.identity);
			}
			if (tookDamageSound) {
				tookDamageSound.Play ();
			}
			int damage = Mathf.RoundToInt(other.GetComponent<Weapon> ().attackDamage * (1 - armour));
			hp -= damage;
			if (owner) {
				owner.TookDamage (damage);
			}
			if (hp <= 0) {
				if (explosionVfx) {
					Instantiate (explosionVfx, collisionPoint, Quaternion.identity);
				}
				if (explosionSound) {
					explosionSound.Play ();
				}
				Destroy (owner.gameObject);
				if (!isPlayer) {
					other.GetComponent<Weapon> ().wielder.GetComponent<PlayerController>().Kill (owner);
				}
			}

			other.GetComponent<Weapon> ().inAttackMotion = false;
		}
	}
}
