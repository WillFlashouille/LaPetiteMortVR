using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engrenage : MonoBehaviour, OnNotifyBehaviour {
	public Game.States _notifyState;

	public bool _sensHoraire = false;

	public void Start(){
		Game.addListener (this, _notifyState);
		GetComponent<Animator> ().SetBool ("SensHoraire",_sensHoraire);
	}

	public void execute () {
		GetComponent<Animator> ().SetFloat ("AnimationStarter", 1f);
	}

	
}
