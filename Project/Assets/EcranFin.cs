using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EcranFin : MonoBehaviour{

	public Material _rectangleMat, _pubMat;

	void Start(){
		ResetMat ();
	}

	public void FadeInRectangle(){
		StartCoroutine (FadeIn (_rectangleMat));
	}

	public void FadeInPub(){
		StartCoroutine (FadeIn (_pubMat));
	}

	public void ResetMat(){
		Color RectangleCol = _rectangleMat.color, pubCol = _pubMat.color;
		RectangleCol.a = 0f;
		pubCol.a = 0f;
		_rectangleMat.color = RectangleCol;
		_pubMat.color = pubCol;
	}


	private IEnumerator FadeIn(Material mat){
		float showCounter = 0f;

//		yield return new WaitForSeconds (5f);
//		Game.player.RemoveUI ();
		while (showCounter <= 1f) {
			showCounter += Time.deltaTime;
			Color col = mat.color;
			col.a = showCounter;
			mat.color = col;
			yield return null;
		}

//		yield return new WaitForSeconds (10f);
//		Color col1 = GetComponent<Image> ().material.color;
//		col1.a = 0;
//		GetComponent <Image> ().material.color = col1;
//
//		SceneManager.LoadScene ("menu");

	}
}

