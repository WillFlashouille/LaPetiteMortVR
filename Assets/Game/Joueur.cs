using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Joueur : MonoBehaviour 
{
	private bool _canTeleport;
	private Text _text;
	private Image _bulle; 

	private float _dureeMessage = 4f;
	private float _tempsMessage = 0f;
	private PersonnageTalking _persoTalking;
	private bool _affiche = false;

	private Queue<string> _textQueue;
	private Queue<float> _dureeQueue;
	private Queue<PersonnageTalking> _persoQueue;


	private Material _currentMat;
	private Material _GPMat;
	private Material _PoissonMat;
	private Material _ChatMat;

	public enum PersonnageTalking {
		GrandPere,
		Poisson,
		Chat
	}

	// Use this for initialization
	void Start () {
		_canTeleport = true;
		_text = this.GetComponentInChildren<Text>();
		_bulle = transform.GetComponentInChildren<Canvas>().transform.Find ("Bulle").GetComponent<Image>();
		Transform images = transform.GetComponentInChildren<Canvas>().transform.Find ("Images");
		_GPMat = images.Find ("GrandPereIcon").GetComponent<Image>().material;
		_PoissonMat = images.Find ("PoissonIcon").GetComponent<Image> ().material;
		_ChatMat = images.Find ("ChatIcon").GetComponent<Image> ().material;
		_textQueue = new Queue<string> ();
		_dureeQueue = new Queue<float> ();
		_persoQueue = new Queue<PersonnageTalking> ();

		RemoveUI ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//TODO Enlever
		transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Horizontal") * Time.deltaTime * 100);
		transform.RotateAround(transform.position, transform.right, -Input.GetAxis("Vertical") * Time.deltaTime * 100);


		if (_affiche) {
			_tempsMessage += Time.deltaTime;
			 if (_tempsMessage >= _dureeMessage) {
				_tempsMessage = 0f;
				if (_text.text != "") {
					StartCoroutine ("fadeText");
				} else {
					_affiche = false;
				}
			}
		}
		else if (_textQueue.Count != 0) {
			_affiche = true;
			_text.text = _textQueue.Dequeue ();
			_dureeMessage = _dureeQueue.Dequeue ();
			_persoTalking = _persoQueue.Dequeue ();
			switch (_persoTalking) {
			case PersonnageTalking.GrandPere: 
				_text.color = new Color (Color.black.r, Color.black.g, Color.black.b, _text.color.a);
				_currentMat = _GPMat;
				break;
			case PersonnageTalking.Poisson: 
				_text.color = new Color (Color.red.r, Color.red.g, Color.red.b, _text.color.a);
				_currentMat = _PoissonMat;
				break;
			case PersonnageTalking.Chat: 
				_text.color = new Color(Color.black.r,Color.black.g,Color.black.b, _text.color.a) ;
				_currentMat = _ChatMat;
				break;
			}
			if (_text.text != "") {
				StartCoroutine ("showText");
			}
		}
	}

	public void Teleporter(Vector3 position) {
		if (!_canTeleport)
			return;
		position.y = this.transform.position.y;
		this.transform.position = position;
	}

	public void afficher(string s, float duree = 4f, PersonnageTalking perso = PersonnageTalking.GrandPere) {
		_textQueue.Enqueue (s.Replace ("\\n", "\n"));
		_dureeQueue.Enqueue (duree);
		_persoQueue.Enqueue (perso);
	}

	public void afficher(Message mes){
		_textQueue.Enqueue (mes._message.Replace ("\\n", "\n"));
		_dureeQueue.Enqueue (mes._duration);
		_persoQueue.Enqueue (mes._perso);
	}

	public bool isTalking(){
		return _affiche || (_textQueue.Count != 0);
	}

	private float _showCounter = 0f;
	private IEnumerator showText(){
		while(_showCounter <= 0.5f){
			_showCounter += Time.deltaTime;
			Color bulle = _bulle.material.color, text = _text.color, mat =_currentMat.color;
			bulle.a = _showCounter * 2f;
			text.a = _showCounter * 2f;
			mat.a = _showCounter * 2f;
			_bulle.material.color = bulle;
			_text.color = text;
			_currentMat.color = mat;
			yield return null;
		}
	}
	private IEnumerator fadeText(){
		while(_showCounter > 0f){
			_showCounter -= Time.deltaTime;
			Color bulle = _bulle.material.color, text = _text.color, mat =_currentMat.color;
			bulle.a = _showCounter;
			text.a = _showCounter;
			mat.a = _showCounter;
			_bulle.material.color = bulle;
			_text.color = text;
			_currentMat.color = mat;
			yield return null;
		}
		_affiche = false;
	}

	public void setCanTeleport(bool val)
	{
		_canTeleport = val;
	}

	public Vector3 getPosition(){return this.transform.position;}
	public bool canTeleport(){return _canTeleport;}


	public void RemoveUI(){
		Color poi = _PoissonMat.color, chat = _ChatMat.color, gp = _GPMat.color, bulle = _bulle.material.color;
		poi.a = 0f; chat.a = 0f; gp.a = 0f; bulle.a = 0f;
		_PoissonMat.color = poi;
		_ChatMat.color = chat;
		_GPMat.color = gp;
		_bulle.material.color = bulle;
	}
}
