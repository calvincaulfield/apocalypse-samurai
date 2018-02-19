using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour {
	public PlayerController player;
	public float chasingDistance;

	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(transform.position, player.transform.position) < chasingDistance) {
			UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
			agent.destination = player.transform.position;
		}
	}
}
