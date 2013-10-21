using UnityEngine;
using System.Collections;

public class StorySlideManager : MonoBehaviour
{
	public int SceneNumber;
	public int currentSlide;
	private Texture2D[] slides;
	private string[] directories = new string[3]{"Intro","Level1","Level2"};
	private bool goNext;
	
	void Start ()
	{
		currentSlide = 0;
		Object[] load = Resources.LoadAll ("Textures/Storyboard/" + directories [SceneNumber]);
		slides = new Texture2D[load.Length];
		for (int i =0; i < load.Length; i++) {
			slides [i] = load [i] as Texture2D;
		}
		renderer.material.mainTexture = slides [currentSlide];
	}
	
	void Update ()
	{
		if (Input.touchCount != 0) {
			goNext = true;
		}
		if (Input.GetKeyUp ("space")) {
			goNext = true;
		}
		if ((currentSlide < slides.Length - 1) && goNext) {
			//goNext = false;
			StartCoroutine (nextSlide ());
		}
		else{
			Application.LoadLevel("Level1-Test");
		}
	}
	
	private IEnumerator nextSlide ()
	{
		goNext = false;
		currentSlide+=1;
		yield return new WaitForSeconds(0.5f);
		renderer.material.mainTexture = slides [currentSlide];
		
	}
}
