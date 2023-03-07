/**
 * Effet: Permet de deplacer un objet en le manipulant du regard
 * Utilisation: Attacher ce script a l'objet à deplacer
 * 	Créer N positions possibles pour l'objet (des objets vides avec un transform)
 * 	Les transform doivent être en coordonnées monde
 *  Le premier de la liste devrait correspondre a la position initial de l'objet (Pour pouvoir le replacer en cas d'echec)
 *  Le second est l'objectif, d'autres peuvent être mis pour "brouiller les pistes"
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(ValiderEtape))]
public class Snapping : MonoBehaviour, IGazeResponder, OnNotifyBehaviour {
	
	private static bool _active = false;
	private AudioSource _audio;
	public GameObject _cible;
	public Vector3 _relativePosition;

	// Liste des positons possibles
	public List<Transform> positions;
	// Distance entre la position voulue et l'objet tenue a partir de laquelle l'objet se place bien
	public float distance;
	public float timeToTrigger {get {return 1.0f;}}
	private bool _isInteractionVisible = true;
	public bool isInteractionVisible { get { return _isInteractionVisible; } }

	private bool _glowing = false;
	private Coroutine _glowCoroutine;

	private enum SnapState {
		Untouched,
		Moving,
		Snapped
	}
	private SnapState _state = SnapState.Untouched;

	public bool _isEnabled;
	public bool isEnabled { get; set;}

	private ValiderEtape validerEtape;

	public List<Message> _OnSnappingUnlockedText;
	public List<Message> _OnObjectTakenText;
	public List<Message> _OnSnappingDoneText;

	public bool _stayRed = false;
	// Use this for initialization
	void Awake () {
		isEnabled = _isEnabled;
		_state = SnapState.Untouched;
		validerEtape = GetComponent<ValiderEtape> ();
		_audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (validerEtape && !validerEtape.isValidated ()) {
			move ();
		}
	}

	public void OnGazeEnter() {
		if (_active && _state == SnapState.Untouched) {
			_isInteractionVisible = false;
		}
	}

	public void OnGazeExit() {
		_isInteractionVisible = true;	
	}
	public void OnGazeStay () {}
	public void OnGazeTrigger()	{
		switch (_state) {
		case SnapState.Untouched:
			if (!_active) {
				_active = true;	
				_state = SnapState.Moving;
				_audio.Play ();
				foreach (Message s in _OnObjectTakenText) {
					Game.player.afficher (s);
				}
			}
			break;
		case SnapState.Moving:
			break;
		case SnapState.Snapped:
			_active = false;
			_state = SnapState.Untouched;
			if (this.transform.position == positions [0].position) {
				validerEtape.validate ();
				_audio.Play ();
				foreach (Message s in _OnSnappingDoneText) {
					Game.player.afficher (s);
				}

//				GetComponent<Renderer> ().material.SetColor ("_OutlineColor", Color.black);
//				foreach (GameObject cible in _cibles) {
//					cible.GetComponent<Renderer> ().material.SetColor ("_OutlineColor", Color.black);
//				}
				_glowing = false;
				isEnabled = false;
				if( _cible ){
					transform.SetParent (_cible.transform);
					transform.localPosition = _relativePosition;//new Vector3(0.6f,-0.5f,-0.7f);
				}
			}
			break;
		}
	}

	private void move(){
		Transform position;
		switch (_state) {
		case SnapState.Moving:
			position = getClosestPosition ();
			if (position != null) {
				_state = SnapState.Snapped;
			} else {
				Transform reticleTransform = Game.reticle.transform;
				this.transform.position = reticleTransform.position + (reticleTransform.forward * 4f - reticleTransform.up * 2f);  
				this.transform.rotation = reticleTransform.rotation;
			}
			break;
		case SnapState.Snapped:
			position = getClosestPosition ();
			if (position != null) {
				transform.position = position.position;
				transform.rotation = position.rotation;
				OnGazeTrigger ();
			} else {
				_state = SnapState.Moving;
			}
			break;
		}
	}

	private Transform getClosestPosition(){
		Vector3 hit = Game.reticle.IntersectionPosition;
		Transform closest = null;
		float closestDist = float.MaxValue;

		foreach (Transform position in positions) {
			float dist = Vector3.Distance (hit, position.position);
			if (dist <= distance && dist < closestDist) {
				closest = position;
				closestDist = dist;
			}
		}

		return closest;
	}

	public void execute () {
		_isEnabled = true;
		isEnabled = true;
		_glowCoroutine = StartCoroutine ("glow");
//		GetComponent<Renderer> ().material.SetColor ("_OutlineColor", Color.red);
//		foreach (GameObject cible in _cibles) {
//			cible.GetComponent<Renderer> ().material.SetColor ("_OutlineColor", Color.red);
//		}	

		foreach (Message s in _OnSnappingUnlockedText) {
			Game.player.afficher (s);
		}
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

			mat.SetColor ("_Color", Color.red * (counter / 2) + (1 - (counter / 2) ) * Color.white);

			yield return null;
		}
		if (_stayRed) {
			mat.SetColor ("_Color", Color.red);
		} else {
			mat.SetColor ("_Color", Color.white);
			mat.SetFloat ("_TintColor", 0f);
		}
	}
}
