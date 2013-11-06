using UnityEngine; 
using System.Collections;

public class CaptureSequence : MonoBehaviour
{
	Animal animalComponent;
	Character characterComponent;
	private bool inRange = false;
	private bool animating = false;
	private bool forward = true;
	
	//Net Related Variables:
	public GameObject net;
	private Texture2D[] netTex;
	
	//Indicator Related Variables:
	public GameObject button;
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
		netTex = new Texture2D[2];
		netTex [0] = Resources.Load ("Textures/StageElements/net1", typeof(Texture2D)) as Texture2D;
		netTex [1] = Resources.Load ("Textures/StageElements/net2", typeof(Texture2D)) as Texture2D;
		
		net = GameObject.CreatePrimitive (PrimitiveType.Quad);
		net.renderer.enabled = false;
		DestroyImmediate (net.collider);
		DestroyImmediate (net.rigidbody);
		net.name = "Net";
		net.transform.parent = GameObject.FindGameObjectWithTag ("character").transform; // makes the net a child of the character
		net.renderer.material.mainTexture = netTex [0];
		net.renderer.material.shader = Shader.Find ("Transparent/VertexLit");
		net.transform.localPosition = new Vector3 (0, 0, 0);
		net.transform.localScale = new Vector3 (1, 1, 1);
		
		button = GameObject.CreatePrimitive (PrimitiveType.Quad);
		button.renderer.enabled = false;
		button.name = "Capture Indicator";
		DestroyImmediate (button.collider);
		DestroyImmediate (button.rigidbody);
		button.transform.parent = Camera.main.transform; // makes Button a child of the camera
		button.renderer.material.shader = Shader.Find ("Transparent/VertexLit");
		button.renderer.material.mainTexture = buttonTextures [0];
		
	}
	
	void FixedUpdate ()
	{
		inRange = Mathf.Ceil (animalComponent.xPosition - characterComponent.xPosition) <= 2.5f;
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
					netThrowAnimation ();
					Animal.captured = true;
				}
			} else {
			
				if (InputManager.rightKey && characterComponent.RowNumber == animalComponent.RowNumber) {
					//GameObject.Find ("Cage").renderer.enabled = true;
					netThrowAnimation ();
					Animal.captured = true;
				}
			}
		}
		
	}
	
	public void animateButton ()
	{
		if (!button.renderer.enabled) {
			button.renderer.enabled = true;
		}
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
		button.renderer.material.mainTexture = buttonTextures [j];
		yield return new WaitForSeconds(time);
		animating = false;
	}
	
	private void netThrowAnimation ()
	{
		if (!net.renderer.enabled) {
			net.renderer.enabled = true;
		}
		
	}
	
}
