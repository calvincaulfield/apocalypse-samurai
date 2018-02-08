using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_01_Weapon : Weapon {

	public void Start() {
	}

	public override void Hit() {
		Debug.Log ("Hit");
		inAttackMotion = false;
	}
}
