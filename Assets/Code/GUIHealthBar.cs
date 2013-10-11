using UnityEngine;
using System.Collections;

public class GUIHealthBar : MonoBehaviour
{
	
	public Texture2D[] textures;
	private float barInitialSize;
	private float barInitialX;
	static public float healthValue;
	private float healthDetRate = 0.05f;
	private Rect pixels;
	static public bool addToHealthBar;
	static public bool subtractFromHealthBar;
	static public int powerUpType;
	static public int obstacleType;
	private float[] adderArray = {50f,35f,20f}; // corresponds with powerUp types
	private float[] subtractArray = {20f, 20f, 50f, 50f, 35f}; //corresponds with obstacle types
	
	enum HealthState
	{
		Good,
		Worse,
		Danger,
		MAX_HEALTH_STATES
	};
	
	void Start ()
	{ //Creates the health bar textures and initializes the health value
		GUI.depth = 0;
		healthValue = 100f;
		guiTexture.texture = textures [(int)HealthState.Good];
		barInitialSize = transform.localScale.x;
		barInitialX = transform.localPosition.x;
	}
	
	void FixedUpdate ()
	{
		if (!Character.paused) { // if our character is moving
			if(addToHealthBar){ //addToHealthBar has been altered by the PowerUp class
				healthValue += adderArray[powerUpType];
				addToHealthBar = false;
			}
			if(subtractFromHealthBar){
				healthValue -= subtractArray[obstacleType];
				subtractFromHealthBar = false;
			}
			
			if(healthValue > 100f){ //Keep Health Value capped at 100 regardless of PowerUp Value
				healthValue = 100;
			}
			if (healthValue > 0.2f) { //If lower than this we can consider the character fainted
				
				healthValue -= healthDetRate;
				
				Vector3 newSize = transform.localScale;
				newSize.x = (healthValue / 100f) * barInitialSize;
					
				transform.localScale = newSize;
				
				Vector3 newPosition = transform.localPosition;
				newPosition.x = barInitialX - ((barInitialSize - newSize.x)/2f);
				
				transform.localPosition = newPosition;
		
				if (Mathf.Ceil (healthValue) > 45f) {
					guiTexture.texture = textures [(int)HealthState.Good];
				}
				
				if (Mathf.Ceil (healthValue) <= 45f && Mathf.Ceil (healthValue) > 15f ) {
					guiTexture.texture = textures [(int)HealthState.Worse];
				}
				if (Mathf.Ceil (healthValue) == 15f) {
					guiTexture.texture = textures [(int)HealthState.Danger];
				}
			} 
			else {
				Character.fainted = true;
				//Debug.Log ("Character Fainted!");
			}
	
		}
	}
	
}
