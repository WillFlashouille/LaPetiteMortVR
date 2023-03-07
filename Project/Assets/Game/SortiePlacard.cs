using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortiePlacard : MonoBehaviour, OnNotifyBehaviour {

	public GameObject _grandPere;
	// Use this for initialization
	void Start () {
		Game.addListener (this, Game.States.GRAND_DAD_OUT_OF_CLOSET);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void execute () {
		GetComponent<Animator> ().SetTrigger ("Open");
	}

	public void OnOpened(){
		_grandPere.GetComponent<Animator> ().SetFloat ("AnimationStarter", 1f);
		Game.setState (Game.States.GRAND_DAD_FOLLOWING, true);
	}
}
