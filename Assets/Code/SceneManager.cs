using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneManager : MonoBehaviour
{
	Character characterComponent;
	//PainIndicator painBarComponent;
	public float startTime;
	public float elapsedTime;
	private int minutes;
	private int seconds;
	private bool animalHeadStart;
	public static bool scenePaused;
	public static bool characterFainted;
	public int lvlNum;
	public static int levelNumber;
	private int countGreenInfections;
	private int countYellowInfections;
	private int countRedInfections;
	private int countWaterBottles;
	
//GUI RELATED VARIABLES
	public GUISkin myskin;
#if !UNITY_STANDALONE && !UNITY_WEBPLAYER && !UNITY_EDITOR
	private Rect MainMenuButtonLocation;
#endif
#if UNITY_STANDALONE || UNITY_WEBPLAYER ||UNITY_EDITOR
	private Rect SpaceBarNotificationLocation;
#endif
	private Rect NotificationLocation;
	
	void Awake(){
		levelNumber = lvlNum;
		characterFainted = false;
	}
	
	void Start ()
	{
		levelNumber = lvlNum;
		characterFainted = false;
		startTime = Time.time;
		RenderSettings.ambientLight = Color.white;
		characterComponent = GameObject.FindGameObjectWithTag ("character").GetComponent<Character> ();
		//painBarComponent = GameObject.FindGameObjectWithTag ("pain").GetComponent<PainIndicator> ();
		elapsedTime = 0f;
		animalHeadStart = true;
		
#if !UNITY_STANDALONE && !UNITY_WEBPLAYER && !UNITY_EDITOR
		MainMenuButtonLocation = new Rect (Screen.width * 0.33f, Screen.height * 0.66f, Screen.width / 3, Screen.height / 6);
#endif
		NotificationLocation = new Rect (0, 0, Screen.width + 2, Screen.height + 2);
#if UNITY_STANDALONE || UNITY_WEBPLAYER ||UNITY_EDITOR
		SpaceBarNotificationLocation = new Rect (0, Screen.height * 0.66f, Screen.width, Screen.height / 6);
#endif
		myskin = Resources.Load ("FaintedOrCaptured", typeof(GUISkin)) as GUISkin;
	}
	
	void Update ()
	{
		if (animalHeadStart) {
			scenePaused = true;
			Vector3 temp = GameObject.Find ("Main Camera").camera.transform.localPosition;
			temp.x = 3.5f;
			GameObject.Find ("Main Camera").camera.transform.localPosition = temp;
			StartCoroutine (StartSequence (5f));
		} else {
			if (!scenePaused) {
				addFirstToLast ();
				minutes = (int)(elapsedTime / 60);
				seconds = (int)(elapsedTime % 60);
				if (Input.GetKeyUp (KeyCode.Escape)) {
					if (Input.GetKeyUp (KeyCode.Escape)) {
						Application.Quit ();
					}
				}
				if (characterComponent.xPosition >= 1.4f) {
					Vector3 temp = GameObject.Find ("Main Camera").camera.transform.localPosition;
					temp.x = GameObject.FindGameObjectWithTag ("character").transform.localPosition.x + 2f;
					GameObject.Find ("Main Camera").camera.transform.localPosition = temp;
				}
			}
		}
	}
	
	void OnGUI ()
	{
		GUI.skin = myskin;
		GUI.Box (new Rect (0.1f, 0.1f, 0.5f * Screen.width, 0.1f * Screen.height), new GUIContent ("Time: " + ((minutes < 10) ? "0" : "") + minutes + ":" + ((seconds < 10) ? "0" : "") + seconds));
		
		if (Animal.captured) {
			GUI.Box (NotificationLocation, new GUIContent ("YOU CAUGHT IT!"));
#if UNITY_STANDALONE || UNITY_WEBPLAYER ||UNITY_EDITOR
		GUI.Label (SpaceBarNotificationLocation, "(Press Space Bar)");
		if(Input.GetKeyUp ("space")){
			goBackToMenu ();
		}
#endif	

#if !UNITY_STANDALONE && !UNITY_WEBPLAYER && !UNITY_EDITOR
			GUI.SetNextControlName ("Main Menu");
			if (GUI.Button (MainMenuButtonLocation, "Quit"/*"Main Menu"*/)) {
				goBackToMenu ();
			}
#endif
		} else {
			if (characterComponent.fainted) {
				characterFainted = true;
				GUI.Box (NotificationLocation, new GUIContent ("YOU FAINTED!"));
#if UNITY_STANDALONE || UNITY_WEBPLAYER ||UNITY_EDITOR
		GUI.Label (SpaceBarNotificationLocation, "(Press Space Bar)");
		if(Input.GetKeyUp ("space")){
			goBackToMenu ();
		}
#endif	
					
#if !UNITY_STANDALONE && !UNITY_WEBPLAYER && !UNITY_EDITOR
				GUI.SetNextControlName ("Main Menu");
				if (GUI.Button (MainMenuButtonLocation, "Quit"/*"Main Menu"*/)) {
					goBackToMenu ();
				}
#endif
				
			}
		}
		
	}
	
	public void goBackToMenu ()
	{
		Application.LoadLevel ("Welcome");
	}
	
	
	public void addFirstToLast ()
	{
		GameObject[] allobjects = GameObject.FindObjectsOfType (typeof(GameObject)) as GameObject[];
		List <GameObject> buildings = new List<GameObject> ();
		List <GameObject> elements = new List<GameObject> ();
		List <GameObject> ground = new List<GameObject> ();
		List <GameObject> sky = new List<GameObject> ();
		
		foreach (GameObject go in allobjects) {
			if (go.GetComponent (typeof(Building)) != null) {
				buildings.Add (go);
			}
			if (go.GetComponent (typeof(Obstacle)) != null) {
				elements.Add (go);
			}
			if (go.GetComponent (typeof(PowerUp)) != null) {
				elements.Add (go);
			}
			if (go.name.Equals ("Ground")) {
				ground.Add (go);
			}
			if (go.name.Equals ("Sky")) {
				sky.Add (go);
			}
		}
		
		float farthestbldgx = -100000f;
		GameObject farthestbldg = buildings [buildings.Count - 1];
		float closestbldgx = 100000f;
		GameObject closestbldg = buildings [0];
		foreach (GameObject bldg in buildings) {
			if (farthestbldgx < bldg.transform.localPosition.x) {
				farthestbldgx = bldg.transform.localPosition.x;
				farthestbldg = bldg;
			}
			if (closestbldgx > bldg.transform.localPosition.x) {
				closestbldgx = bldg.transform.localPosition.x;
				closestbldg = bldg;
			}
		}
		float farthestGroundx = -100000f;
		GameObject farthestGround = ground [ground.Count - 1];
		float closestGroundx = 100000f;
		GameObject closestGround = ground [0];
		foreach (GameObject gnd in ground) {
			if (farthestGroundx < gnd.transform.localPosition.x) {
				farthestGroundx = gnd.transform.localPosition.x;
				farthestGround = gnd;
			}
			if (closestGroundx > gnd.transform.localPosition.x) {
				closestGroundx = gnd.transform.localPosition.x;
				closestGround = gnd;
			}
		}
		float farthestSkyx = -100000f;
		GameObject farthestSky = sky [sky.Count - 1];
		float closestSkyx = 100000f;
		GameObject closestSky = sky [0];
		foreach (GameObject sk in sky) {
			if (farthestSkyx < sk.transform.localPosition.x) {
				farthestSkyx = sk.transform.localPosition.x;
				farthestSky = sk;
			}
			if (closestSkyx > sk.transform.localPosition.x) {
				closestSkyx = sk.transform.localPosition.x;
				closestSky = sk;
			}
		}
		float farthestElementx = -100000f;
		//GameObject farthestElement = elements[elements.Count-1];
		float closestElementx = 100000f;
		GameObject closestElement = elements [0];
		foreach (GameObject el in elements) {
			if (farthestElementx < el.transform.localPosition.x) {
				farthestElementx = el.transform.localPosition.x;
				//farthestElement = el;
			}
			if (closestElementx > el.transform.localPosition.x) {
				closestElementx = el.transform.localPosition.x;
				closestElement = el;
			}
		}
		
		if (characterComponent.xPosition >= farthestbldgx - 5f) {
			Vector3 temp = closestbldg.transform.localPosition;
			temp.x = farthestbldgx + (farthestbldg.transform.localScale.x * 0.5f) + (closestbldg.transform.localScale.x * 0.5f);
			closestbldg.transform.localPosition = temp;
			closestbldg.GetComponent<Building> ().resetTouch ();
		}
		if (characterComponent.xPosition >= farthestGroundx - 2f) {
			Vector3 temp = closestGround.transform.localPosition;
			temp.x = farthestGroundx + (farthestGround.transform.localScale.x * 0.5f) + (closestGround.transform.localScale.x * 0.5f);
			closestGround.transform.localPosition = temp;
		}
		if (characterComponent.xPosition >= farthestSkyx - 2f) {
			Vector3 temp = closestSky.transform.localPosition;
			temp.x = farthestSkyx + (farthestSky.transform.localScale.x * 0.5f) + (closestSky.transform.localScale.x * 0.5f);
			closestSky.transform.localPosition = temp;
		}
		float elementOffset = 5f;
		float deltaDistance = farthestElementx - closestElementx;
		if (characterComponent.xPosition >= farthestElementx - 0.5f) {
			Vector3 temp = closestElement.transform.localPosition;
			temp.x += deltaDistance + elementOffset;
			closestElement.transform.localPosition = temp;
			closestElement.renderer.enabled = true;
		}
	}
	
	public IEnumerator StartSequence (float time)
	{
		yield return new WaitForSeconds(time);
		animalHeadStart = false;
		scenePaused = false;
	}
}
