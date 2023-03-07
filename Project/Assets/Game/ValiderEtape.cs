/**
 * Effet : Permet a un autre script de valider une étape du jeu
 * Utilisisation : a attacher a un gameobject avec un autre script. Dans l'autre script, recuperer le ValiderEtape dans Start puis appeler validate au moment voulue
 */


using UnityEngine;
using System.Collections;

public class ValiderEtape : MonoBehaviour {

	// Etat requis pour effectuer la validation
	public Game.States requiredState;
	// Etat a valider
	public Game.States state;

	// Use this for initialization
	void Start () {
		OnNotifyBehaviour notify = GetComponent<OnNotifyBehaviour> ();
		if (notify != null) {
			Game.addListener (notify, requiredState);
		}
	}
	
	// Update is called once per frame
	void Update () {}

	public bool validate()
	{
		if (Game.getState (requiredState)) {
			Game.setState (state, true);
			return true;
		}
		Debug.Log (requiredState.ToString () + " required");
		return false;
	}

	public bool isValidated(){
		return Game.getState (state);
	}
}
