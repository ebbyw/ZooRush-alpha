using UnityEngine;
using System.Collections;

public class OptionsMenu : MonoBehaviour
{
	public static bool SoundOn = true;
	public static bool bgMusicOn = true;
	private GameObject Volume;
	private GameObject SoundFX;
	private GameObject BGMusic;
	private GameObject BackButton;
	private GameObject[] SoundOnOff;
	private GameObject[] bgMusicOnOff;
	private GameObject[] optionsRows;
	private bool flashing = false;
	private bool up = false;
	private bool down = false;
	private bool spaceBar = false;
	private int currentRow = -1;
	
	void Start ()
	{
		if (Volume == null) {
			Volume = GameObject.Find ("Volume");
		}
		
		if (SoundFX == null) {
			SoundFX = GameObject.Find ("Sound FX");
			SoundOnOff = new GameObject[]{SoundFX.transform.FindChild ("On").gameObject,
									  	  SoundFX.transform.FindChild ("Off").gameObject};
		}
		
		if (BGMusic == null) {
			BGMusic = GameObject.Find ("BG Music");
			bgMusicOnOff = new GameObject[]{BGMusic.transform.FindChild ("On").gameObject,
											BGMusic.transform.FindChild ("Off").gameObject};
		}
		
		if (BackButton == null) {
			BackButton = GameObject.Find ("Back");
		}
		
		optionsRows = new GameObject[]{Volume, SoundFX, BGMusic, BackButton};
		
	}
	
	void FixedUpdate ()
	{
		if (up) {
			if (currentRow > 0) {
				selectOption (currentRow - 1);
				currentRow--;
			}
		}
		if (down) {
			if (currentRow < 3) {
				selectOption (currentRow + 1);
				currentRow ++;
			}
		}
		
		if (spaceBar) {
			switch (currentRow) {
			case 0:
				//Add Slider Actions here
				break;
			case 1:
				activateButtonOnOff (SoundOnOff, !SoundOn);
				SoundOn = !SoundOn;
				break;
			case 2:
				activateButtonOnOff (bgMusicOnOff, !bgMusicOn);
				bgMusicOn = !bgMusicOn;
				break;
			case 3:
				if (!flashing) {
					StartCoroutine (clickedButton (BackButton));
					Application.LoadLevel ("Start");
				}
				break;
			default:
				break;
			}
		}
		
		if (Input.mousePresent) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit touched;
			if (Physics.Raycast (ray, out touched, 100f)) {
				Debug.Log (touched.collider.gameObject.name);
				//touched.collider.gameObject.Equals (playButton)
				for (int i = 0; i < optionsRows.Length; i++) {
					if (touched.collider.gameObject.Equals (optionsRows [i].transform.FindChild ("Selection").gameObject)) {
						selectOption (i);
						currentRow = i;
					}
				}
			}
//			if (SoundOnOff [0]) { // Mouse is over Sound FX "ON"
//				if (Input.GetMouseButtonUp (0)) {
//					activateButtonOnOff (SoundOnOff, true);
//					SoundOn = true;
//				}
//			}
//			if (touched.collider.gameObject.Equals (SoundOnOff [1])) { // Mouse is over Sound FX "OFF"
//				if (Input.GetMouseButtonUp (0)) {
//					Debug.Log ("CLICK DETECTED");
//					activateButtonOnOff (SoundOnOff, false);
//					SoundOn = false;
//				}
//			}
//			if (touched.collider.gameObject.Equals (bgMusicOnOff [0])) { // Mouse is over BG Music "ON"
//				if (Input.GetMouseButtonUp (0)) {
//					activateButtonOnOff (bgMusicOnOff, true);
//					bgMusicOn = true;
//				}
//			}
//			if (touched.collider.gameObject.Equals (bgMusicOnOff [1])) { // Mouse is over BG Music "OFF"
//				if (Input.GetMouseButtonUp (0)) {
//					activateButtonOnOff (bgMusicOnOff, true);
//					bgMusicOn = true;
//				}
//			}
		}
		if (Input.touchCount > 0) {
			Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
			RaycastHit touched;
			if (Physics.Raycast (ray, out touched, 100f)) {
				
			}

		}
	}
	
	void Update ()
	{
		up = Input.GetKeyUp ("up");
		down = Input.GetKeyUp ("down");
		spaceBar = Input.GetKeyUp ("space");
	}
	
	private void activateButtonOnOff (GameObject[] OnOffButtons, bool On)
	{
		if (On) {
			OnOffButtons [0].GetComponent<TextMesh> ().color = Color.yellow;
			OnOffButtons [1].GetComponent<TextMesh> ().color = Color.white;
		} else {
			OnOffButtons [0].GetComponent<TextMesh> ().color = Color.white;
			OnOffButtons [1].GetComponent<TextMesh> ().color = Color.yellow;
		}
	}
	
	private void selectOption (int option)
	{
		optionsRows [option].transform.FindChild ("Selection").renderer.enabled = true;
		for (int i = 0; i < optionsRows.Length; i++) {
			if (i != option) {
				optionsRows [i].transform.FindChild ("Selection").renderer.enabled = false;
			}
		}
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
	
}
