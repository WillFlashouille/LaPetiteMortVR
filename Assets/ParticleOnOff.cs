using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleOnOff : MonoBehaviour, OnNotifyBehaviour{

	public Game.States _On, _Off;
	public bool On = false;

	// Use this for initialization
	void Start () {
		Game.addListener (this, _On);
		Game.addListener (this, _Off);
	}

	public void execute () {
		if (!On) {
			GetComponent<ParticleSystem> ().Play ();
		} else {
			GetComponent<ParticleSystem> ().Pause ();
			GetComponent<ParticleSystem> ().Clear();
		}
		On = !On;
	}
}
