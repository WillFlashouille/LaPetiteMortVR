using UnityEngine;
using System.Collections;

public class Compagnon : MonoBehaviour{
	private EndGame _vignette;
	public Message _OnDieMessage;
	public bool _enabled = false;
	void Start () {
		_vignette = FindObjectOfType<EndGame> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Meurs(){
		GetComponentInChildren<Animator>().SetTrigger ("Meurs");
		Game.player.afficher (_OnDieMessage);
	}

	public float getAngle(){
		return Vector3.Angle (Vector3.forward, Game.reticle.transform.worldToLocalMatrix.MultiplyPoint (this.transform.position));
	}
}
