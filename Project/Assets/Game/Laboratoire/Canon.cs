/**
 * Effet: L'objet envoie des "Projectiles" vers un objet donné. 
 * Utilisation:
 * 	Attacher le script a l'Objet qui envoie
 * 	donner un type de projectile, le type d'objet qui sera envoyé sur la cible (Objet qui doit avoir le script Projectile)
 *  donner une cible vers laquelle les objets seront envoyés
 */
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ValiderEtape))]
public class Canon : MonoBehaviour, OnNotifyBehaviour	 {

	// Règle la dispersion autour de la cible (0 = tout exactement sur la cible
	// Règle la cadence de tir du canon (= temps entre deux projectiles).
	public float _cadence = 3.0f;
	// Point a partir duquel les projectiles partent (en coordonnées locales). Sert pour eviter que les projectiles se detruisent dans le canon
	public Vector3 _sortie = new Vector3 (0, 0, 0);
	// Projectile utilisé (Peut etre un prefab) Doit avoir le script Projectile attaché
	public GameObject _balle;

	// Nombre de projectiles envoyés 
	public int _nombreBalles = 5;
	private ValiderEtape _validerEtape;
	private int _nombreBallesEnvoyees= 0;
	private int _nombreBallesDetruites = 0;

	// Use this for initialization
	void Start () {
		_validerEtape = GetComponent<ValiderEtape> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void ProjectileDestroyed (){
		_nombreBallesDetruites++;
		GetComponent<AudioSource> ().Play ();
		if (_nombreBallesDetruites == _nombreBalles) {
			_validerEtape.validate ();
		}
	}
		
	public void execute(){
		InvokeRepeating ("LaunchProjectile", _cadence, _cadence);
	}

	void LaunchProjectile (){
		if (_nombreBallesEnvoyees < _nombreBalles) {
			_nombreBallesEnvoyees++;
			GameObject objectProjectile = GameObject.Instantiate (_balle);
			objectProjectile.transform.position = this.transform.position + _sortie;
			objectProjectile.GetComponent<Projectile> ()._associatedCanon = this;
			objectProjectile.GetComponent<Projectile> ().setDirection (Random.insideUnitSphere);
		} else {
			CancelInvoke ("LaunchProjectile");
		}
	}
		
}
