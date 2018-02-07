using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public int exp;
	public GameObject infoDamage;

	void Update() {
		
	}

	public void TookDamage(int damage) {
		GameObject thisObject = Instantiate (infoDamage, infoDamage.transform.position, Quaternion.identity);
		thisObject.SetActive (true);
		thisObject.GetComponent<TextMesh> ().text = "" + damage;
	}

}
