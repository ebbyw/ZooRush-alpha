using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour
{
	
	GameObject playButton;
	GameObject optionsButton;
	GameObject spaceBarLabel;
	GameObject[] buttonArray;
	
	string[] sceneDestination = {"tester","Options"};
	
	private bool flashing = false;
	private bool up = false;
	private bool down = false;
	private bool spaceBar = false;
	int currentButton = 0;
	/** Values for Current Button
	 *  0  - Play
	 *  1  - Options
	 */ 
	
	void Start ()
	{
		if (playButton == null) {
			playButton = GameObject.Find ("Play");
		}
		if (optionsButton == null) {
			optionsButton = GameObject.Find ("Options");
		}
		if (spaceBarLabel == null) {
			spaceBarLabel = GameObject.Find ("Play");
		}
		buttonArray = new GameObject[] {playButton, optionsButton};
		
#if !UNITY_STANDALONE && !UNITY_WEBPLAYER && !UNITY_EDITOR
		spaceBarLabel.renderer.enabled = false;
#endif
		
	}
	
	void FixedUpdate ()
	{	
		if (up) {
			if (currentButton > 0) {
				activateButton (currentButton - 1);
				currentButton--;
			}
		}
		if (down) {
			if (currentButton < 1) {
				activateButton (currentButton + 1);
				currentButton ++;
			}
		}
		if (spaceBar) {
			StartCoroutine (clickedButton (buttonArray [currentButton]));
			Application.LoadLevel (sceneDestination [currentButton]);
		}
		if (Input.mousePresent) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit touched;
			if (Physics.Raycast (ray, out touched, 100f)) {
				if (touched.collider.gameObject.Equals (playButton)) {
					if (!flashing) {
						currentButton = 0;
						activateButton (currentButton);
					}
					if (Input.GetMouseButtonUp (0)) {
						StartCoroutine (clickedButton (playButton));
						Application.LoadLevel (sceneDestination [currentButton]);
					}
				} 
				if (touched.collider.gameObject.Equals (optionsButton)) {
					if (!flashing) {
						currentButton = 1;
						activateButton (currentButton);
					}
					if (Input.GetMouseButtonUp (0)) {
						StartCoroutine (clickedButton (optionsButton));
						Application.LoadLevel (sceneDestination [currentButton]);
					}
				}
			}
		}
		if (Input.touchCount > 0) {
			Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch (0).deltaPosition);
			RaycastHit touched;
			if (Physics.Raycast (ray, out touched, 100f)) {
				if (touched.collider.gameObject.Equals (playButton)) {
					if (!flashing) {
						currentButton = 0;
						StartCoroutine (clickedButton (playButton));
						Application.LoadLevel (sceneDestination [currentButton]);
					} 
					if (touched.collider.gameObject.Equals (optionsButton)) {
						if (!flashing) {
							currentButton = 1;
							StartCoroutine (clickedButton (optionsButton));
							Application.LoadLevel (sceneDestination [currentButton]);
						}
					}
				}
			}

		}
	}
	
	void Update ()
	{
		up = Input.GetKeyUp ("up");
		down = Input.GetKeyUp ("down");
		spaceBar = Input.GetKeyUp ("space");
	}
	
	private IEnumerator clickedButton (GameObject button)
	{
		flashing = true;
		TextMesh text = button.GetComponent<TextMesh> ();
		text.color = Color.yellow;
		yield return new WaitForSeconds(0.1f);
		text.color = Color.white;
		yield return new WaitForSeconds(0.1f);
		text.color = Color.yellow;
		yield return new WaitForSeconds(0.1f);
		text.color = Color.white;
		yield return new WaitForSeconds(0.1f);
		text.color = Color.yellow;
		flashing = false;
	}
	
	private void activateButton (int buttonIndex)
	{
		GameObject[] buttonArray = {playButton, optionsButton};
		TextMesh[] textMeshArray = {buttonArray [0].GetComponent<TextMesh> (), 
								   buttonArray [1].GetComponent<TextMesh> ()};
		textMeshArray [buttonIndex].color = Color.yellow;
		for (int i = 0; i < buttonArray.Length; i++) {
			if (i != buttonIndex) {
				textMeshArray [i].color = Color.white;
			}
		}
	}

}
