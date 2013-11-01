using UnityEngine;
using System.Collections;

/** Input Manager: Detects and input for the user and sends appropriate signals 
 *  			   to the proper objects in the game.
 * 
 */ 
public class InputManager : MonoBehaviour
{
	
	private static Touch touch;
	public static bool touchDetected;
	public static bool upKey;
	public static bool downKey;
	public static bool rightKey;
	public static bool spaceBar;
	
	void Update ()
	{
		upKey = Input.GetKeyUp ("up");
		downKey = Input.GetKeyUp ("down");
		rightKey = Input.GetKeyUp ("right");
		spaceBar = Input.GetKeyUp ("space");
		if (Input.touchCount > 0) {
			touchDetected = true;
			touch = Input.GetTouch (0);
		}
		if (touchDetected) {
			if (touch.position.y < Screen.height / 2) {
				upKey = true;
				downKey = false;
			}
			if (touch.position.y > Screen.height / 2) {
				upKey = false;
				downKey = true;
			}
		}
		DisplayValues();
	}
	
	void DisplayValues(){
		Debug.Log("UpKey:" + upKey + "\n" +
				  "DownKey:" + downKey + "\n" +
				  "RightKey:" + rightKey + "\n" +
				  "SpaceKey:" + spaceBar + "\n" +
		 		  "Touched Detected:" + touchDetected);
		
	}
	
}
