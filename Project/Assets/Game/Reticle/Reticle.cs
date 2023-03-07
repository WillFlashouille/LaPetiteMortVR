using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Reticle : MonoBehaviour {
	private IGazeResponder _formerGaze;
	private float _timeGazed = 0.0f;
	private bool _triggered = false;
	private Image _radialProgress;

	private float _interactionMaxDistance = 100.0f;
	public float InteractionMaxDistance {get{ return _interactionMaxDistance;} set { _interactionMaxDistance = value;}}

	private int _layerMask = -1;
	public int ReticleLayerMask{ get {return _layerMask;} set {_layerMask = value;} }

	private Vector3 _intersectionPosition = Vector3.zero;

	public bool Triggered { get{return _triggered;}}
	public Vector3 IntersectionPosition { get { return _intersectionPosition; } }

	void Start () {
		_radialProgress = transform.Find ("Canvas/UIProgressBar").GetComponent<Image>();
	}

	void LateUpdate () {
		Ray ray = new Ray (transform.position, transform.forward);
		Debug.DrawRay (ray.origin, ray.direction,Color.red);
		RaycastHit hit;
		IGazeResponder currentGaze = null;

		if (Physics.Raycast (ray, out hit, InteractionMaxDistance, ReticleLayerMask)) {
			_intersectionPosition = hit.point;
			currentGaze = hit.collider.GetComponent<IGazeResponder> ();
			if (currentGaze != null && !currentGaze.isEnabled) {
				currentGaze = null;
			}
			if (currentGaze != null) {
				if (!currentGaze.Equals (_formerGaze)) {
					_radialProgress.fillAmount = 0.0f;
					_triggered = false;
					_timeGazed = Time.deltaTime;
					currentGaze.OnGazeEnter ();
					if (_formerGaze != null) {
						_formerGaze.OnGazeExit ();
					}
				} else {
					_timeGazed += Time.deltaTime;
					if (currentGaze.isInteractionVisible) {
						_radialProgress.fillAmount = _timeGazed / currentGaze.timeToTrigger;
					}
					currentGaze.OnGazeStay ();
				}
				if (_timeGazed >= currentGaze.timeToTrigger) {
					if (!_triggered) {
						currentGaze.OnGazeTrigger ();
					}
					_triggered = true;
				}
			} else {
				if (_formerGaze != null) {
					_formerGaze.OnGazeExit ();
				}
				_radialProgress.fillAmount = 0.0f;
				_timeGazed = 0.0f;
			}
		} else {
			_radialProgress.fillAmount = 0.0f;
			_timeGazed = 0.0f;
			_triggered = false;
			if (_formerGaze != null) {
				_formerGaze.OnGazeExit ();
			}
		}
		_formerGaze = currentGaze;
	}

	public void resetReticle(){
		_radialProgress.fillAmount = 0.0f;
		_timeGazed = 0.0f;
		_triggered = false;
	}
}
