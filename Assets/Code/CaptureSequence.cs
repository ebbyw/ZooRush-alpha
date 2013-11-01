using UnityEngine;
using System.Collections;

public class CaptureSequence : MonoBehaviour
{
	Animal animalComponent;
	Character characterComponent;
	private bool inRange = false;
	private bool animating = false;
	private bool forward = true;
	public Texture2D[] buttonTextures; //TODO need to make this platform dependent, 
	//for now all systems will have the same image apprear
	private int frame;
	//private RaycastHit theHit;
	
	void Start ()
	{
		animalComponent = GameObject.FindGameObjectWithTag ("animal").GetComponent<Animal> ();
		characterComponent = GameObject.FindGameObjectWithTag ("character").GetComponent<Character> ();
		buttonTextures = new Texture2D[7];
		for (int i = 0; i < buttonTextures.Length; i++) {
			buttonTextures [i] = Resources.Load ("Textures/StageElements/RightButton" + i, typeof(Texture2D)) as Texture2D;
		}
	}
	
	void FixedUpdate ()
	{
		inRange = Mathf.Ceil (animalComponent.xPosition - characterComponent.xPosition) <= 50;
		if (inRange && !SceneManager.scenePaused) {
			characterComponent.RunSpeed = animalComponent.RunSpeed;
			if (!animating) {
				animateButton ();
			}
			
			if (InputManager.touchDetected) { // touch input detected
				if (characterComponent.RowNumber == animalComponent.RowNumber) {
					//if (Physics.Raycast (Camera.main.ScreenPointToRay (touch.position), out theHit, 83)) {
					//	Debug.Log ("I WAS TOUCHED");	
					//}
					GameObject.Find ("Cage").renderer.enabled = true;
					animalComponent.captured = true;
				}
			} else {
			
				if (InputManager.rightKey && characterComponent.RowNumber == animalComponent.RowNumber) {
					//GameObject.Find ("Cage").renderer.enabled = true;
					animalComponent.captured = true;
				}
			}
		}
		
	}
	
	public void animateButton ()
	{
		if ((frame == buttonTextures.Length - 1) && forward) {
			frame = buttonTextures.Length - 2;
			forward = false;
		} else { 
			if (frame == 0 && !forward) {
				frame++;
				forward = true;
			} else {
				if (forward) {
					frame++;
				}
				if (!forward) {
					frame--;
				}
			}
		}
				
		StartCoroutine (ChangeSprite (frame, 0.1f));
	}
	
	public IEnumerator ChangeSprite (int j, float time)
	{
		animating = true;
		renderer.material.SetTexture ("_MainTex", buttonTextures [j]);
		yield return new WaitForSeconds(time);
		animating = false;
	}
	
}
