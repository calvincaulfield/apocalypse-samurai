using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceUser : MonoBehaviour {


	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.LookRotation (-GameController.cameraOffset);	
	}
}
