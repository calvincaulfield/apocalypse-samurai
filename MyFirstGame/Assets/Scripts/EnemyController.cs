using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public GameObject display;
	public int exp;

	void Update() {
		display.GetComponent<TextMesh> ().text = "" + GetComponent<WeakPoint>().hp + "/" + GetComponent<WeakPoint>().maxHp;
		gameObject.transform.Find ("Info").rotation = Quaternion.LookRotation (-GameController.cameraOffset);
	}

}
