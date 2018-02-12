using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeakBody : MonoBehaviour {
	public bool isPlayer;
	public bool isEnemy;
	public float hp;
	public float maxHp;
	public float armour;  // 0: no armour, 1: protected

	public GameObject hpInfo;
	public Unit owner;

	public GameObject tookDamageVfx;
	public GameObject explosionVfx;
	public AudioSource tookDamageSound;
	public AudioSource explosionSound;


	protected virtual void Start() {
		hp = maxHp;
	}

	protected virtual void Update() {		
		if (isEnemy) {
			hpInfo.GetComponent<TextMesh> ().text = "HP " + hp + "/" + maxHp;
		} else {

		}
	}

	// Collision with enemy
	protected virtual void OnTriggerEnter(Collider other) {
		if (other.GetComponent<Weapon>() && other.GetComponent<Weapon>().inAttackMotion) {
			// Weapon cannnot harm its owner even if there is collision
			if (!isPlayer ^ other.GetComponent<Weapon> ().playerWeapon) {
				return;
			}
			other.GetComponent<Weapon> ().Hit ();

			//Debug.Log ("WeakBody collided with weapon!");
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
				if (owner) {
					Destroy (owner.gameObject);
				}
				if (!isPlayer) {
					other.GetComponent<Weapon> ().wielder.GetComponent<PlayerController>().Kill (owner);
				}
			}
		}
	}

	public virtual void Heal(float amount) {
		hp += amount;
		if (hp > maxHp) {
			hp = maxHp;
		}
	}
}
