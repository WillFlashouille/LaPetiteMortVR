using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ValiderEtape))]
public class EndGame : MonoBehaviour, OnNotifyBehaviour {
	public EcranFin _ecranFin;
	public Message _DepecheMessage;
	public Compagnon _chat, _grandPere;
	public ValiderEtape _valider;
	private VignetteModel _vignette;


	public Message _onKillMessage;
	private float _maxIntensity = 1.2f;
	private float _step = 0.10f;
	public bool _done = false;

	void Start () {
		_valider = GetComponent<ValiderEtape> ();
		_vignette = FindObjectOfType<PostProcessingBehaviour> ().profile.vignette;
	}

	void Update () {
	}

	private IEnumerator Vignette(){
		while (!_done) {
			if (_vignette.settings.intensity <= _maxIntensity) {
				changeVignetteValue (_vignette.settings.intensity + _step * Time.deltaTime);
			} else {
				_done = true;
				if (_chat.getAngle () <= _grandPere.getAngle ()) {
					_chat.Meurs ();
				} else {
					_grandPere.Meurs ();
				}
			}
			yield return null;
		}
		yield return new WaitForSeconds(3f);
		_ecranFin.FadeInRectangle ();
		yield return new WaitForSeconds(1f);
		changeVignetteValue (0f);
		Game.player.afficher (_onKillMessage);
		Debug.Log ("Avant");
		yield return new WaitForSeconds (_onKillMessage._duration);
		Debug.Log ("Apres");
		_ecranFin.FadeInPub ();
		yield return new WaitForSeconds (10f);
		SceneManager.LoadScene("menu");
	}

	private void changeVignetteValue(float intensity){
		VignetteModel.Settings sett = _vignette.settings;
		sett.intensity = Mathf.Abs(intensity);
		_vignette.settings = sett;
	}

	public void execute () {
		Game.player.afficher (_DepecheMessage);
		StartCoroutine (Vignette ());

	}
}
