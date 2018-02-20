using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	public bool isPlayer;

	public int exp;
	public GameObject infoDamage;
	public string id;
	public TextMesh namePlate;

	public float hitRecovery = 1.0f;
	public float hitRecoveryEnd;

	public Material normalMaterial;
	public Color normalColor;
	public Material woundedMeterial;
	public Color woundedColor;
	public GameObject colorBody;

	public virtual void Start() {
		if (namePlate) {
			namePlate.text = GameObject.FindWithTag("GameController").GetComponent<GameController>().GetWord(id);
		}
	}

	public virtual void TookDamage(int damage) {
		//Debug.Log ("Enemy Took Damage");
		if (isPlayer) {
			hitRecoveryEnd = Time.time + hitRecovery;
			colorBody.GetComponent<MeshRenderer> ().sharedMaterial.color = woundedColor;
		} else {
			GameObject thisObject = Instantiate (infoDamage, infoDamage.transform.position, Quaternion.identity);
			thisObject.SetActive (true);
			thisObject.GetComponent<TextMesh> ().text = "" + damage;
		}
	}

	public bool isInHitRecovery() {
		return Time.time < hitRecoveryEnd;
	}

	void Update() {
		if (isPlayer && !isInHitRecovery()) {
			colorBody.GetComponent<MeshRenderer>().sharedMaterial.color = normalColor;
		}
	}

}
