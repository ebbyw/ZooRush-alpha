using UnityEngine;
using System.Collections;

public class PainIndicator : MonoBehaviour
{
	Animal animalComponent = GameObject.FindGameObjectWithTag("animal").GetComponent<Animal>();
	Character characterComponent = GameObject.FindGameObjectWithTag("character").GetComponent<Character>();
	static public float PainLevel = 0f;
	static public bool Crisis = false;
	private float PainIncrement = 0.1f;
	private GameObject[] PainBars = new GameObject[5];
	private bool flashing = false;
	private bool addToPain;
	private bool subtractFromPain;
	static public int powerUpType;
	static public int obstacleType;
	private float[] powerUpArray = {35f,20f}; // corresponds with powerUp types
	private float[] obstacleArray = {20f, 20f, 50f, 50f, 35f}; //corresponds with obstacle types
	Color red = new Color (1f, 0.5f, 0.5f, 1f);
	Color red2 = new Color (1f, 0.6f, 0.6f, 1f);
	Color red3 = new Color (1f, 0.7f, 0.7f, 1f);
	Color red4 = new Color (1f, 0.8f, 0.8f, 1f);
	Color red5 = new Color (1f, 0.9f, 0.9f, 1f);
	Color white = new Color (1f, 1f, 1f, 1f);
	
	void Start ()
	{
		PainBars [0] = GameObject.Find ("Pain1");
		PainBars [1] = GameObject.Find ("Pain2");
		PainBars [2] = GameObject.Find ("Pain3");
		PainBars [3] = GameObject.Find ("Pain4");
		PainBars [4] = GameObject.Find ("Pain5");
		for (int i = 0; i < 5; i++) {
			PainBars [i].renderer.enabled = false;
		}
	}
	
	void FixedUpdate ()
	{
		//if (!Character.paused) { // if our character is moving
		if (subtractFromPain) { //addToHealthBar has been altered by the PowerUp class
			PainLevel -= powerUpArray [powerUpType];
			subtractFromPain = false;
		}
		if (addToPain) {
			PainLevel += obstacleArray [obstacleType];
			addToPain = false;
		}
			
		if (PainLevel > 100f) { //Keep Health Value capped at 100 regardless of PowerUp Value
			PainLevel = 100;
		}
		if (PainLevel < 100f) { //If lower than this we can consider the character fainted
				
			PainLevel += PainIncrement;
				
		} else {
			characterComponent.fainted = true;
			//Debug.Log ("Character Fainted!");
		}
	
	}
	//}
	
	void Update ()
	{
		if (!characterComponent.fainted) {
			if (PainLevel > 20f) {
				PainBars [0].renderer.enabled = true;
			} else {
				PainBars [0].renderer.enabled = false;
			}
			if (PainLevel > 40f) {
				PainBars [1].renderer.enabled = true;
			} else {
				PainBars [1].renderer.enabled = false;
			}
			if (PainLevel > 50f) {
				PainBars [2].renderer.enabled = true;
			} else {
				PainBars [2].renderer.enabled = false;
			}
			if (PainLevel > 75f) {
				PainBars [3].renderer.enabled = true;
			} else {
				PainBars [3].renderer.enabled = false;
			}
			if (PainLevel > 80f) {
				PainBars [4].renderer.enabled = true;
				Crisis = true;
				if (!flashing) {
					StartCoroutine (flashScreen ());
				}
			} else {
				PainBars [4].renderer.enabled = false;
				Crisis = false;
			}
		}
	}
	
	IEnumerator flashScreen ()
	{
		flashing = true;
		RenderSettings.ambientLight = red5;
		yield return new WaitForSeconds(0.05f);
		RenderSettings.ambientLight = red4;
		yield return new WaitForSeconds(0.05f);
		RenderSettings.ambientLight = red3;
		yield return new WaitForSeconds(0.05f);
		RenderSettings.ambientLight = red2;
		yield return new WaitForSeconds(0.05f);
		RenderSettings.ambientLight = red;
		yield return new WaitForSeconds(0.1f);
		RenderSettings.ambientLight = red2;
		yield return new WaitForSeconds(0.05f);
		RenderSettings.ambientLight = red3;
		yield return new WaitForSeconds(0.05f);
		RenderSettings.ambientLight = red4;
		yield return new WaitForSeconds(0.05f);
		RenderSettings.ambientLight = red5;
		yield return new WaitForSeconds(0.05f);
		RenderSettings.ambientLight = white;
		yield return new WaitForSeconds(0.05f);
		flashing = false;
	}
}
