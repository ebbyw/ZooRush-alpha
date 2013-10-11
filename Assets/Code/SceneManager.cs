using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour
{
	public float startTime;
	public float elapsedTime;
	public float endTime;
	
	private int minutes;
	private int seconds;
	
//GUI RELATED VAREIABLES
	public GUISkin myskin;
	private bool waiting = false;
#if !UNITY_STANDALONE && !UNITY_WEBPLAYER && !UNITY_EDITOR
	private Rect MainMenuButtonLocation;
#endif
#if UNITY_STANDALONE || UNITY_WEBPLAYER ||UNITY_EDITOR
	private Rect SpaceBarNotificationLocation;
#endif
	private Rect NotificationLocation;
	
	void Start ()
	{
		elapsedTime = 0f;
		
#if !UNITY_STANDALONE && !UNITY_WEBPLAYER && !UNITY_EDITOR
		MainMenuButtonLocation = new Rect (Screen.width * 0.33f, Screen.height * 0.66f, Screen.width / 3, Screen.height / 6);
#endif
		NotificationLocation = new Rect (0, 0, Screen.width + 2, Screen.height + 2);
#if UNITY_STANDALONE || UNITY_WEBPLAYER ||UNITY_EDITOR
		SpaceBarNotificationLocation = new Rect (0, Screen.height * 0.66f, Screen.width, Screen.height / 6);
#endif
	}
	
	void Update ()
	{
		minutes = (int)(elapsedTime/60);
		seconds = (int)(elapsedTime%60);
		if (Input.GetKeyUp(KeyCode.Escape)){
			if (Input.GetKeyUp(KeyCode.Escape)){
				Application.Quit();
			}
		}
	}
	
	void OnGUI ()
	{
		GUI.skin = myskin;
		GUI.Box(new Rect(0.1f,0.1f,0.5f*Screen.width,0.1f*Screen.height),new GUIContent("Time: " + ((minutes < 10)?"0":"") + minutes + ":" + ((seconds < 10)?"0":"") + seconds));
		
		if (Animal.captured) {
			if (!waiting) {
				StartCoroutine (wait ());
			} else {
				GUI.Box (NotificationLocation, new GUIContent ("YOU CAUGHT IT!"));
#if UNITY_STANDALONE || UNITY_WEBPLAYER ||UNITY_EDITOR
		GUI.Label (SpaceBarNotificationLocation, "(Press Space Bar)");
		if(Input.GetKey ("space")){
			goBackToMenu ();
		}
#endif	

#if !UNITY_STANDALONE && !UNITY_WEBPLAYER && !UNITY_EDITOR
				GUI.SetNextControlName ("Main Menu");
				if (GUI.Button (MainMenuButtonLocation, "Quit"/*"Main Menu"*/)) {
					goBackToMenu();
				}
#endif
			}
		} else {
			if (Character.fainted) {
				if (!waiting) {
					StartCoroutine (wait ());
					
				} else {
					GUI.Box (NotificationLocation, new GUIContent ("YOU FAINTED!"));
#if UNITY_STANDALONE || UNITY_WEBPLAYER ||UNITY_EDITOR
		GUI.Label (SpaceBarNotificationLocation, "(Press Space Bar)");
		if(Input.GetKey ("space")){
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
		
	}
	
	public void goBackToMenu(){
		Application.LoadLevel (0);
		//Application.Quit();
	}
	
	public IEnumerator wait ()
	{
		yield return new WaitForSeconds(1f);
		waiting = true;
	}
}
