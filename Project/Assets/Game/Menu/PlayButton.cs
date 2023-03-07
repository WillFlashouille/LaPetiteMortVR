using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour, IGazeResponder {

	public Sprite _chargement;

	// Use this for initialization
	void Start () {
//		GetComponent<Image> ().material.color = Color.white;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void OnGazeEnter () {}
	public void OnGazeExit () {}
	public void OnGazeStay () {}

	public void OnGazeTrigger () {
		GetComponent<Collider> ().enabled = false;
		GetComponent<Image> ().sprite = _chargement;
		SceneManager.LoadSceneAsync("application");

	}

	public float timeToTrigger { get { return 1f; } }
	public bool isInteractionVisible { get { return true; } }
	public bool isEnabled {	get { return true; } }

}
