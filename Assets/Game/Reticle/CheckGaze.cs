using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheckGaze : MonoBehaviour, IGazeResponder {
	public float timeToTrigger { get { return 0.5f;}}
	public bool isInteractionVisible { get; set; }
	public bool isEnabled { get { return true; } }

	void Start () {	
		isInteractionVisible = true;
	}

	void Update () { }

	public void OnGazeEnter () {
		GetComponent<Renderer> ().material.color = Color.blue;
	}

	public void OnGazeExit () {
		GetComponent<Renderer> ().material.color = Color.red;
	}

	public void OnGazeStay () {
		transform.Rotate (0, 10 * Time.deltaTime, 0);
	}

	public void OnGazeTrigger () {
		GetComponent<Renderer> ().material.color = GetComponent<Renderer> ().material.color / 2;
	}
}