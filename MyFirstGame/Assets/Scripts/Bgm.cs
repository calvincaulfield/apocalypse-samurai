using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bgm : MonoBehaviour {
	public AudioSource[] bgms;

	public void PlayBgm(int number) {
		bgms [number].volume = 0.4f;
		bgms [number].Play ();
	}
}
