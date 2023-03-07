using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ValiderEtape))]
public class Enigme : MonoBehaviour, OnNotifyBehaviour{

	public Game.States[] _states;


	public List<Message> _OnEnigmeUnlockText;
	public List<Message> _OnEnigmeDoneText;

	private bool _unlocked = false;

	// Use this for initialization
	void Start () {
		foreach(Game.States state in _states){
			Game.addListener (this, state);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void execute () {
		if (!_unlocked) {
			_unlocked = true;
			foreach (Message mes in _OnEnigmeUnlockText) {
				Game.player.afficher (mes);
			}
		}

		bool isFinished = true;
		foreach (Game.States state in _states) {
			if (!Game.getState (state)) {
				isFinished = false;
				break;
			}
		}

		if (isFinished) {
			foreach (Message mes in _OnEnigmeDoneText) {
				Game.player.afficher (mes);
			}
			GetComponent<ValiderEtape> ().validate ();
		}


	}
}
