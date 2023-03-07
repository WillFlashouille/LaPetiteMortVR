using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porte : MonoBehaviour , IGazeResponder{

	public bool _opened = false;
	public bool _sensDirect = false;
	public  bool _locked = false;

	private Animator _animator;

	// Use this for initialization
	void Start () {
		_animator = GetComponentInParent<Animator> ();
		_animator.SetBool ("SensDirect", _sensDirect);
		_animator.SetBool ("Opened", _opened);
//		_animator.SetTrigger ("Move");
	}
	
	// Update is called once per frame
	void Update () {}

	public void OnGazeEnter () {}

	public void OnGazeExit () {}

	public void OnGazeStay () {}

	public void OnGazeTrigger () {
		if (!_locked){
			GetComponent<PorteGlow> ()._glowing = false;
			if (_opened) {
				Close ();
			} else {
				Open ();
			}
		}
	}

	public void Open (){
		if (!_opened) {
			_opened = true;
			_animator.SetBool ("Opened", _opened);
			_animator.SetTrigger ("Move");
		}
	}

	public void Close(){
		if (_opened) {
			_opened = false;
			_animator.SetBool ("Opened", _opened);
			_animator.SetTrigger ("Move");
		}
	}

	public float timeToTrigger {
		get {return 1.0f;}
	}

	public bool isInteractionVisible {
		get {return true;}
	}

	public bool isEnabled {
		get {return !_locked;}
	}
}
