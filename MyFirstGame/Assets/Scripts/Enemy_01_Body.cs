using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_01_Body : WeakPoint {

	public float minHeight;
	public float maxHeight;
	public float frequency;
	public Weapon weapon;

	private float previousRatio = 0;

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		float period = 1 / frequency;
		float ratio = 2 * (Time.time % period) / period; // 0 ~ 2 per period sec
		float height = minHeight + (maxHeight - minHeight) * ratio;
		if (height > maxHeight) {
			height -= 2 * (height - maxHeight);
		} 
		transform.position = new Vector3(transform.position.x, height, transform.position.z);
		if (previousRatio > ratio) { // happens once per period
			Reload();
		}
		previousRatio = ratio;
	}

	void Reload() {
		weapon.inAttackMotion = true;
	}
}
