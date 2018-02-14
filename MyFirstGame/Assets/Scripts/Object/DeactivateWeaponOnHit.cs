using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateWeaponOnHit : Weapon {

	public override void Hit() {
		base.Hit ();
		inAttackMotion = false;
	}
}
