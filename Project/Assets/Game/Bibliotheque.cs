using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ValiderEtape))]
public class Bibliotheque : MonoBehaviour, OnNotifyBehaviour {

	public Poussiere _poussiere; 

	// Use this for initialization
	void Start () {	}
	

	public void execute () {
		_poussiere.Appear ();
		Invoke ("Remove", 0.5f);
	}

	public void Remove(){
		GetComponent<ValiderEtape> ().validate ();
		Destroy (gameObject);
	}
}
