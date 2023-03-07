using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorteDeverouille : MonoBehaviour, OnNotifyBehaviour {

	public Game.States _unlockState; 

	// Use this for initialization
	void Start () {
		Game.addListener (this, _unlockState);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void execute () {
		GetComponent<Porte> ()._locked = false;
		GetComponent<PorteGlow> ().LaunchGlow ();
	}
}
