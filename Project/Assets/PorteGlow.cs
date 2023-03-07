using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorteGlow : MonoBehaviour{

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

			mat.SetColor ("_Color", Color.red * (counter / 5f) + (1 - (counter / 5f) ) * Color.white);

			yield return null;
		}

		while (counter > 0f) {
			counter -= Time.deltaTime;
			mat.SetColor ("_Color", Color.red * (counter / 5f) + (1 - (counter / 5f) ) * Color.white);
			yield return null;
		}
		mat.SetColor ("_Color", Color.white);
		mat.SetFloat ("_TintColor", 0f);
	}
}
