using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {

	public int number;

	public GameController gameController;

	private void OnTriggerEnter(Collider other) {
		gameController.WaypoinReached(number);

	}
}
