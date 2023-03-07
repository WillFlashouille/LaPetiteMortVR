using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {
	public static Game instance;

	public States _debugStartingState, _debugOnGState;

	public enum States{
		START,
		QUIT,
		MENU,
		CAT_FOLLOWING,
		GRAND_DAD_FOLLOWING,
		GRAND_DAD_OUT_OF_CLOSET,
		FISH_TALKING_1,
		BOOK_ENIGM_BEGIN,
		BOOK_1,
		BOOK_2,
		BOOK_3,
		BOOK_ENIGM_ENDED,
		BOOKSHELF_REMOVED,
		SOULS_DESTROYED,
		ROUAGE_ENIGM_BEGIN,
		ROUAGE_1,
		ROUAGE_2,
		ROUAGE_3,
		ROUAGE_ENIGM_ENDED,
		TUYAU_ENIGM_BEGIN,
		TUYAU_1,
		TUYAU_2,
		TUYAU_ENIGM_ENDED,
		GRAND_DAD_WATER_ADVICE,
		FIND_WATER_SOURCE,
		PUT_IN_WATER,
		BIG_CHOICE,
		CHOICE_DONE
	};

	private static bool[] gameStates;
	private static List<List<OnNotifyBehaviour>> notifyList; 
	public static GameObject grandDad, cat;
	public static Reticle reticle;
	public static PointDeplacement currentPoint;
	public static Joueur player;
	
	// Use this for initialization
	void Awake(){
//		if (notifyList == null) {
			notifyList = new List<List<OnNotifyBehaviour>> (System.Enum.GetValues (typeof(States)).Length);
			foreach (States state in System.Enum.GetValues(typeof(States))) {
				notifyList.Add (new List<OnNotifyBehaviour> ());
			}
//		}

		gameStates = new bool[System.Enum.GetValues(typeof(States)).Length];
		player = GameObject.Find ("Joueur").GetComponent<Joueur>();
		
		grandDad = (GameObject)(GameObject.Find ("GrandPere"));
		cat = (GameObject)(GameObject.Find ("Chat"));
		reticle = (Reticle)(GameObject.Find ("Reticle")).GetComponent (typeof(Reticle));

	}

	void Start () 
	{
		/*
		notifyList = new List<OnNotifyBehaviour>[System.Enum.GetValues(typeof(States)).Length];
		foreach(States state in System.Enum.GetValues(typeof(States))){
			notifyList [(int)state] = new List<OnNotifyBehaviour> ();
		}
		*/
		StartCoroutine ("starter");
		GameObject.Find ("Deplacement/sphere-deplacement").GetComponent<PointDeplacement>().GetComponent<PointDeplacement> ().OnGazeTrigger ();
	}

	private IEnumerator starter(){
		yield return null;
		setState (States.START, true);
		setState (States.CAT_FOLLOWING, true);
		setState (_debugStartingState, true); // TODO enlever
	}

	void Update(){ 
		if (Input.GetKeyDown (KeyCode.G)) {
			setState (_debugOnGState, true);
		}

	}

	public static bool getState(States state){
		return gameStates [(int)state];
	}

	public static void setState(States state, bool b){
		gameStates [(int)state] = b;
		Debug.Log (""+state+" : "+b);

		if (b) {
			foreach (OnNotifyBehaviour notify in notifyList[(int)state]) {
				notify.execute ();
			}
		}
	}

	public static void print(){
		Debug.Log ("GameStates : ");
		foreach (States state in System.Enum.GetValues(typeof(States)))
		{
			Debug.Log (state.ToString() + " : " + gameStates [(int)state]);
		}
	}

	public static void addListener(OnNotifyBehaviour onNotify, States onState){
		notifyList [(int)onState].Add (onNotify);
	}
}
