using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PointDeplacement : MonoBehaviour, IGazeResponder {

	private Transform positionGP;
	private Transform positionChat;
	private Transform positionJoueur;

	private bool _displayed = false;
	public bool _returnOnStep = false;
	public List<Message> _OnStepMessage;

	void Awake () {
		positionGP = this.transform.Find ("PositionGP");
		positionChat = this.transform.Find ("PositionChat");
		positionJoueur = this.transform.Find ("PositionJoueur");
	}
	
	void Update () {}

	public bool isEnabled {get {return true;}}
	public float timeToTrigger {get { return 1.0f;}}
	public bool isInteractionVisible {get {return true;}}
	public void OnGazeEnter() {}
	public void OnGazeExit() {}
	public void OnGazeStay () {}
	public void OnGazeTrigger() 
	{
		Game.currentPoint = this;
		Game.player.transform.position = positionJoueur.position + new Vector3(0, 8f, 0);
		if (_returnOnStep) {
			Game.player.transform.RotateAround(Game.player.transform.position, Vector3.up, 180f);
		}
		moveCat ();
		moveGrandDad ();
		if (!_displayed) {
			_displayed = true;
			foreach (Message mes in  _OnStepMessage){
				Game.player.afficher (mes);
			}
		}
	}

	public void moveGrandDad()
	{
		if (Game.getState (Game.States.GRAND_DAD_FOLLOWING)) {
			Game.grandDad.transform.position = positionGP.position;
			Game.grandDad.transform.rotation = positionGP.rotation;

		}
		
	}

	public void moveCat()
	{
		if (Game.getState (Game.States.CAT_FOLLOWING)) {
			Game.cat.transform.position = positionChat.position;
			Game.cat.transform.rotation = positionChat.rotation;
		}
	}
}
