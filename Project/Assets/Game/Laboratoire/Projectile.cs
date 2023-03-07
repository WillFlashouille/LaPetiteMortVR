/**
 * Effet : Décris le comportement des projectiles lancés par le "Canon"; Dans le jeu : des âmes
 * Utilisation : à attacher au prefab ou objet qui sera envoyé par le Canon
 *  
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Projectile : MonoBehaviour, IGazeResponder{
	// Doit etre initialiser par le Canon qui lance
	public Canon _associatedCanon;

	public Texture[] _textures;
	private Renderer _renderer;

	public Color _fullHealthColor, _emptyHealthColor;


	public float timeToTrigger {get {return float.MaxValue;}}
	public bool isInteractionVisible {get {return false;}}
	public bool isEnabled {get {return true;}}


	public float _changeDirectionTimer = 1f;

	private Vector3 _oldDirection;
	private Vector3 _newDirection;
	private float _timer;

	// ordre de grandeur pour la vitesse de déplacement du projectile
	[SerializeField]
	private float _vitesse = 1f;
	[SerializeField]
	private float _intensity = 0.1f;

	private float _health = 0;  // TODO rename 
	// Use this for initialization
	void Start () {	
		_renderer = GetComponentInChildren<Renderer> ();
		_oldDirection = Random.insideUnitSphere;
	}

	// Update is called once per frame
	void Update ()	{
		_timer += Time.deltaTime;
		if (_timer >= _changeDirectionTimer) {
			setDirection (Random.insideUnitSphere);
		}
		Vector3 _actualDirection = (_newDirection * (_timer / _changeDirectionTimer) + _oldDirection * ((_changeDirectionTimer - _timer) / _changeDirectionTimer));
		Vector3 joueurPos = (Game.player.transform.position)-  transform.position ;
		joueurPos.y = 0f;
		transform.LookAt (transform.position + (new Vector3(_actualDirection.x, 0f, _actualDirection.z)).normalized + (joueurPos.normalized * 1.5f));
		transform.position += _actualDirection * _vitesse;


	}


	public void OnCollisionEnter(Collision collision){
		if (!collision.collider.GetComponent<Projectile> ()) {
			_newDirection = collision.contacts[0].normal + _newDirection;
			_oldDirection = collision.contacts[0].normal + _oldDirection;
		}
	}

	public void setDirection(Vector3 direction){
		_oldDirection = _newDirection;
		_newDirection = direction;
		_timer = 0f;
	}

	public void OnGazeEnter() {}
	public void OnGazeExit() {}
	public void OnGazeStay () {
		_health += 0.5f * Time.deltaTime;
		if (_health >= 1) {
			_associatedCanon.ProjectileDestroyed ();

			Destroy (gameObject);
		} else {
			if (_health <= 1f / _textures.Length) {
				_renderer.material.SetTexture ("_MainTex", _textures[0]);
			} else if (_health <= 2f / _textures.Length) {
				_renderer.material.SetTexture ("_MainTex", _textures[1]);
			} else {
				_renderer.material.SetTexture ("_MainTex", _textures[2]);
			}

			Renderer rend = GetComponentInChildren<Renderer> ();
			Color col = rend.material.color;
			col = (_fullHealthColor * (1f - _health) + (_emptyHealthColor * _health));
			rend.material.color = col;
			foreach (ParticleSystem ps in GetComponentsInChildren<ParticleSystem>()) {
				ParticleSystem.MainModule settings = ps.main;
				settings.startColor = new ParticleSystem.MinMaxGradient ((_fullHealthColor * (1f - _health) + (_emptyHealthColor * _health)));
			}
		}
	}

	public void OnGazeTrigger(){}
}

