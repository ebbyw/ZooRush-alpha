using UnityEngine;
using System.Collections;

public class CaptureSequence : MonoBehaviour
{
	Animal animalComponent = GameObject.FindGameObjectWithTag("animal").GetComponent<Animal>();
	Character characterComponent = GameObject.FindGameObjectWithTag("character").GetComponent<Character>();
	private bool inRange = false;
	private bool animating = false;
	private bool forward = true;
	private Touch touch;
	public Texture2D[] buttonTextures; //TODO need to make this platform dependent, 
							   		   //for now all systems will have the same image apprear
	private int frame;
	
	void Start ()
	{
	}
	
	void FixedUpdate ()
	{
		inRange = Mathf.Ceil (animalComponent.xPosition - characterComponent.xPosition) <= 50;
		if (inRange && !SceneManager.scenePaused) {
			characterComponent.RunSpeed = animalComponent.RunSpeed;
			if (!animating) {
				animateButton ();
			}
			
			if (Input.touchCount != 0) { // touch input detected
				touch = Input.GetTouch (0);
				if (touch.deltaPosition.x > 0 && characterComponent.RowNumber == animalComponent.RowNumber && touch.deltaPosition.y < 10) {
					GameObject.Find ("Cage").renderer.enabled = true;
					animalComponent.captured = true;
				}
			} else {
			
				if (Input.GetKeyUp ("right") && characterComponent.RowNumber == animalComponent.RowNumber) {
					GameObject.Find ("Cage").renderer.enabled = true;
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
