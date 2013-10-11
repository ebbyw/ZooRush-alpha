using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {
	
	public GUISkin myskin;
	public Texture2D background;
	private bool down = false;
	private bool up = false;
	private bool waiting =false;
	int currentSelection = 0;
	
	string[] menuOptions = {
		"Play",
		"Options",
	};
	
	// Use this for initialization
	void OnGUI () {
		
		if(background != null){
			GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), background);
		}
		
		GUI.skin = myskin;
		float buttonWidth = Screen.width/3;
		float buttonHeight = Screen.height/10;
		float buttonX = (Screen.width - buttonWidth)/2;
		
		GUI.Label (new Rect(0,0.30f*Screen.height,Screen.width,buttonHeight), "Zoo Escape");

#if UNITY_STANDALONE || UNITY_WEBPLAYER ||UNITY_EDITOR
		GUI.Button (new Rect(0,0.8f*Screen.height,Screen.width,buttonHeight), "(Press Space Bar)");
#endif		

		GUI.SetNextControlName("Play");
		if(GUI.Button (new Rect( buttonX ,(Screen.height/2),buttonWidth,buttonHeight), "Play Game")){
			Application.LoadLevel(1);
		}
		
		GUI.SetNextControlName("Options");
		if(GUI.Button (new Rect( buttonX ,0.60f*Screen.height,buttonWidth,buttonHeight), "Options")){
			Debug.Log ("Options");
		}
		
#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
		GUI.FocusControl(menuOptions[currentSelection]);
#endif
	}
	
	void Update(){
		if(!waiting){
			
		down = Input.GetKey ("down");
		up = Input.GetKey ("up");
		if( down ){
			if(currentSelection == (int) menuOptions.Length-1 ){
				currentSelection = 0;
			}
			else{
				currentSelection++;
			}
		}
		if( up ){
			if(currentSelection == 0 ){
				currentSelection = (int) menuOptions.Length -1;
			}
			else{
				currentSelection--;
			}
		}
			StartCoroutine(waitBetweenKeyStrokes(0.1f));
		}
	}
	
	public IEnumerator waitBetweenKeyStrokes (float time)
	{
		waiting = true;
		yield return new WaitForSeconds(time);
		waiting = false;
	}

}
