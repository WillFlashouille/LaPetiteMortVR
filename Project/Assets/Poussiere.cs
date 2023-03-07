using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poussiere : MonoBehaviour {
	private Animator _animator;

	void Start () {
		_animator = GetComponent<Animator> ();
	}

	public void Appear(){
		_animator.SetFloat ("AnimationStarter", 0.5f);
		FadeIn ();
	}

	private float fadeInTime;
	public void FadeIn(float time = 0.5f){
		fadeInTime = time;
		StartCoroutine ("fadeInCoroutine");
	}

	private IEnumerator fadeInCoroutine(){
		float time = 0f;
		while (time < fadeInTime) {
			foreach (Renderer rend in GetComponentsInChildren<Renderer>()) {
				Color col = rend.material.color;
				col.a =  time / fadeInTime;
				rend.material.color = col;
			}
			time += Time.deltaTime;
			yield return null;
		}
	}

	private float fadeOutTime;
	public void FadeOut(float time = 0.5f){
		fadeOutTime = time;
		StartCoroutine ("fadeOutCoroutine");
	}

	private IEnumerator fadeOutCoroutine(){
		float time = 0f;
		while (time < fadeInTime) {
			foreach (Renderer rend in GetComponentsInChildren<Renderer>()) {
				Color col = rend.material.color;
				col.a = (fadeInTime - time) / fadeInTime;
				rend.material.color = col;
			}
			time += Time.deltaTime;
			yield return null;
		}
		foreach (Renderer rend in GetComponentsInChildren<Renderer>()) {
			Color col = rend.material.color;
			col.a = 0f;
			rend.material.color = col;
		}
	}

}
