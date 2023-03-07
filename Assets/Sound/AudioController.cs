using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

	private AudioSource _main, _danger;

	// Use this for initialization
	void Start () {
		_main = transform.FindChild ("MainMusic").GetComponent<AudioSource>();
		_danger = transform.FindChild ("DangerTheme").GetComponent<AudioSource>();
	}


	private IEnumerator MainToDanger(){
		float counter = 0f;
		while (counter < 0.6f) {
			counter += Time.deltaTime;
			_main.volume = 0.6f - counter;
			_danger.volume = counter;
			yield return null;
		}
	}

	private IEnumerator DangerToMain(){
		float counter = 0f;
		while (counter < 0.6f) {
			counter += Time.deltaTime;
			_danger.volume = 0.6f - counter;
			_main.volume = counter;
			yield return null;
		}
	}

}
