using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ValiderEtape))]
public class PoigneBaignoire : MonoBehaviour, IGazeResponder, OnNotifyBehaviour{

	private ValiderEtape _valider;
	public Animator _baignoire;
	public ParticleSystem _water1, _water2;

	public List<Message> _onTriggeredMessage;

	// Use this for initialization
	void Start () {
		_valider = GetComponent<ValiderEtape> ();	
	}

	public void OnGazeEnter (){}
	public void OnGazeExit (){}
	public void OnGazeStay (){}
	public void OnGazeTrigger (){
		GetComponent<Collider> ().enabled = false;
		_glowing = false;
		foreach (Message mes in _onTriggeredMessage) {
			Game.player.afficher (mes);
		}
		GetComponent<Animator>().SetFloat("AnimationStarter",1f);
		_baignoire.SetFloat ("AnimationStarter", 1f);
		_valider.validate ();

		_water1.Play();
		_water2.Play ();
		StartCoroutine ("close");
	}

	public IEnumerator close(){
		yield return new WaitForSeconds (6f);

		_water1.Pause ();
		_water2.Pause ();

		_water1.Clear ();
		_water2.Clear ();
	}

	public float timeToTrigger {get {return 1f;}}
	public bool isInteractionVisible {get {return true;}}
	public bool isEnabled {get {return Game.getState (_valider.requiredState);}}

	public void execute () {
		LaunchGlow ();
	}

	public bool _glowing = false;

	public void LaunchGlow(){
		_glowing = true;
		StartCoroutine ("glow");

	}

	private IEnumerator glow(){
		Material mat = (GetComponent<Renderer> () ? GetComponent<Renderer>() : GetComponentInChildren<Renderer>()).material;
		mat.SetFloat ("_TintColor", 1f);
		_glowing = true;
		bool increase = true;
		float counter = 0f;

		mat.SetColor ("_Color", Color.white);
		while (_glowing) {
			if (increase) {
				counter += Time.deltaTime;
			} else {
				counter -= Time.deltaTime;
			}
			if (counter >= 1f || counter < 0f) {
				increase = !increase;
			}

			mat.SetColor ("_Color", Color.red * (counter / 3f) + (1 - (counter / 3f) ) * Color.white);

			yield return null;
		}

		while (counter > 0f) {
			counter -= Time.deltaTime;
			mat.SetColor ("_Color", Color.red * (counter / 3f) + (1 - (counter / 3f) ) * Color.white);
			yield return null;
		}
		mat.SetColor ("_Color", Color.white);
		mat.SetFloat ("_TintColor", 0f);
	}

}
