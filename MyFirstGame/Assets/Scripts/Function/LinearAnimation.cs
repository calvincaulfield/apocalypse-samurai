using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearAnimation : MonoBehaviour {
	public float velocity;

	//private Vector3 initialPosition;

	// Use this for initialization
	void Start () {
		//initialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.up * Time.deltaTime * velocity, Space.World);
		//transform.position = new Vector3(125, 1, 37);
		//transform.position = Vector3.up;

	}
}
