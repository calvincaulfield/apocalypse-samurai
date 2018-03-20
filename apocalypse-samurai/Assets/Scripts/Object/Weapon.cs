using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public float attackDamage;
	public bool inAttackMotion;
	public bool playerWeapon;
	public PlayerController wielder;

	public virtual void Start() {

	}

	public void StartAttack() {
		GetComponent<AudioSource> ().Play ();
		inAttackMotion = true;
	}

	public void StopAttack() {
		inAttackMotion = false;
	}  

	public virtual void Hit() {

	}

}
