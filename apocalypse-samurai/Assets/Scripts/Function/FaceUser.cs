using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceUser : MonoBehaviour {

	public bool toLookUpside;

	// Update is called once per frame
	void Update () {

		Vector3 pointToLook = -GameController.cameraOffset;
		Quaternion look = Quaternion.LookRotation (-GameController.cameraOffset);
		Quaternion change90 = Quaternion.AngleAxis(-90, new Vector3(90, 0, 0));

		if (toLookUpside) { // point y axis to camera
			transform.rotation = look * change90;
		} else { // point z axis to cameera			
			transform.rotation = look;
		}
	}
}
