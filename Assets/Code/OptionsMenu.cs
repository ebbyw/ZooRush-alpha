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
	
	private bool up = false;
	private bool down = false;
	private bool spaceBar = false;
	
	private int currentRow = 0;
	
	void Start ()
	{
		if(Volume == null){
			Volume = GameObject.Find ("Volume");
		}
		
		if (SoundFX == null) {
			SoundFX = GameObject.Find ("Sound FX");
			SoundOnOff = new GameObject[]{SoundFX.transform.GetChild (0).gameObject,
									  	  SoundFX.transform.GetChild (1).gameObject};
		}
		
		if (BGMusic == null) {
			BGMusic = GameObject.Find ("BG Music");
			bgMusicOnOff = new GameObject[]{BGMusic.transform.GetChild (0).gameObject,
											BGMusic.transform.GetChild (1).gameObject};
		}
		
		if(BackButton == null){
			BackButton = GameObject.Find("Back");
		}
		
		optionsRows = new GameObject[]{Volume, SoundFX, BGMusic, BackButton};
		
	}
	
	void FixedUpdate ()
	{
		if (up) {
			if (currentRow > 0) {
				currentRow--;
			}
		}
		if (down) {
			if (currentRow < 1) {
				currentRow ++;
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
		if(On){
			OnOffButtons[0].GetComponent<TextMesh>().color = Color.yellow;
			OnOffButtons[1].GetComponent<TextMesh>().color = Color.white;
		}
		else{
			OnOffButtons[0].GetComponent<TextMesh>().color = Color.white;
			OnOffButtons[1].GetComponent<TextMesh>().color = Color.yellow;
		}
	}
	

	
}
