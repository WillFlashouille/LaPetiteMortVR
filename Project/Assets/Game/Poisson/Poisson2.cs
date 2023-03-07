using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ValiderEtape))]
public class Poisson2 : MonoBehaviour, OnNotifyBehaviour{
	private ValiderEtape _valider;
	private Poussiere _poussiere;
	private Transform _petitPoisson, _grandPoisson;

	public List<Message> _messages;

	// Use this for initialization
	void Start () {
		_valider = GetComponent<ValiderEtape> ();
		_poussiere= transform.GetComponentInChildren<Poussiere> ();
		_petitPoisson = transform.FindChild ("PetitPoisson");
		_grandPoisson = transform.FindChild ("GrandPoisson");
	}

	public void execute(){
		StartCoroutine ("WaitForSpeechEnd");
	}

	private void SortBocal(){
		_petitPoisson.GetComponentInChildren<Renderer> ().enabled = false;
		_grandPoisson.GetComponentInChildren<Renderer> ().enabled = true;
	}

	private IEnumerator WaitForSpeechEnd(){
		yield return new WaitForSeconds (0.5f);
		while (Game.player.isTalking ()) {
			yield return null;
		}
		FindObjectOfType<AudioController> ().StartCoroutine ("MainToDanger");
		_poussiere.Appear ();
		yield return new WaitForSeconds (0.8f);
		SortBocal ();
		foreach (Message mes in _messages) {
			Game.player.afficher (mes);
		}
		while (Game.player.isTalking ()) {
			yield return null;
		}
//		_poussiere.Appear ();
//		FindObjectOfType<AudioController> ().StartCoroutine ("DangerToMain");
//		yield return new WaitForSeconds (0.8f);
//		RentreBocal ();
		_valider.validate ();
	}

	private void RentreBocal(){
		_petitPoisson.GetComponentInChildren<Renderer> ().enabled = true;
		_grandPoisson.GetComponentInChildren<Renderer> ().enabled = false;
	}
}
