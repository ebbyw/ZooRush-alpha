using UnityEngine;
using System.Collections;

public class CaptureSequence : MonoBehaviour
{
	
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
		inRange = Mathf.Ceil (Animal.xPosition - Character.xPosition) <= 50;
		if (inRange && !Character.paused) {
			Character.RunSpeed = Animal.RunSpeed;
			if (!animating) {
				animateButton ();
			}
			
			if (Input.touchCount != 0) { // touch input detected
				touch = Input.GetTouch (0);
				if (touch.deltaPosition.x > 0 && Character.RowNum == Animal.RowNum && touch.deltaPosition.y < 10) {
					GameObject.Find ("Cage").renderer.enabled = true;
					Animal.captured = true;
				}
			} else {
			
				if (Input.GetKey ("right") && Character.RowNum == Animal.RowNum) {
					GameObject.Find ("Cage").renderer.enabled = true;
					Animal.captured = true;
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
