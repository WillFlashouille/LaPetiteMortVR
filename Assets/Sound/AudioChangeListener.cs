using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioChangeListener : MonoBehaviour, OnNotifyBehaviour{

	public bool MainToDanger = false;
	public Game.States[] states;

	public void Start(){
		foreach (Game.States state in states) {
			Game.addListener (this, state);
		}
	}

	public void execute () {
		if (MainToDanger) {
			GetComponent<AudioController> ().StartCoroutine ("MainToDanger");
		} else {
			GetComponent<AudioController> ().StartCoroutine ("DangerToMain");
		}
	}
}
