using UnityEngine;
using System.Collections;

public class Character : GameElement
{
	public int characterType;
	public float distanceTraveled;
	public int RowNumber;
	private bool Moving = false;
	private int maxRows = 3;
	public bool up;
	public bool down;
	private int runNum = 2;
	private bool waiting = false;
	public bool flashing = false;
	public float xPosition;
	private Touch touch;
	private float dragY;
	public float RunSpeed;
	public float defaultRunSpeed;
	public bool fainted = false;
	private bool paused;
	
	/** Character Material Set Up:
	 *   0 - LevelIdle,
	 *   1 - Run1,
	 *   2 - Run2,
	 *   3 - Run3, 
	 *   4 - NormalIdle
	*/
	void Start ()
	{
		defaultRunSpeed = RunSpeed;
		xPosition = transform.localPosition.x;
		distanceTraveled = transform.localPosition.x;
		renderer.material = materials[0];
		RowNum = RowNumber;
		forward = true;
		fps = 0.05f;
		animalComponent = GameObject.FindGameObjectWithTag("animal").GetComponent<Animal>();
		paused = SceneManager.scenePaused;
	}
	
	public void FixedUpdate ()
	{ // All math related functions will occur under Fixed Update in order to insure synchronization and consistency
		if (!fainted && !animalComponent.captured) {
			if (paused) {
				GameObject.Find ("SceneManager").GetComponent<SceneManager> ().startTime = Time.time;
			} else {
				xPosition = transform.localPosition.x;
				GameObject.Find ("SceneManager").GetComponent<SceneManager> ().elapsedTime = Time.time - GameObject.Find ("SceneManager").GetComponent<SceneManager> ().startTime;	
				distanceTraveled += Mathf.Abs (distanceTraveled - transform.localPosition.x);
				transform.Translate (Vector3.right * RunSpeed * Time.deltaTime);
				if (flashing) {
					StartCoroutine (characterFlash ());	
				}
				if (!Moving) {
					down = Input.GetKey ("down");
					up = Input.GetKey ("up");
			
					if (Input.touchCount != 0) { // touch input detected
						touch = Input.GetTouch (0);
						if (touch.position.y < Screen.height/2) {
							up = true;
							down = false;
						}
						if (touch.position.y > Screen.height/2) {
							up = false;
							down = true;
						}
					}

					if (up || down) {
						StartCoroutine (move (transform));
					}
				}
			}
		}
	}
	
	void Update ()
	{
		paused = SceneManager.scenePaused;
		if (!waiting && !fainted && !animalComponent.captured && !paused) {
			if(!animating){
					StartCoroutine (ChangeMaterial (runNum, fps));
				}
			
			if (runNum == 4 && forward) {
				runNum--;
				forward = false;
			} else {
				if (runNum == 2 && !forward) {
					runNum++;
					forward = true;
				} else {
					if (forward) {
						runNum++;
					}
					if (!forward) {
						runNum--;
					}
				}
			}
		}
	}

	
	public IEnumerator characterFlash ()
	{
		float newAlpha = 0.5f;
		float waitTime = 0.1f;
		Color originalColour = renderer.material.color;
		renderer.material.color = new Color (originalColour.r, originalColour.g, originalColour.b, newAlpha);
		yield return new WaitForSeconds(waitTime);
		newAlpha = 1f;
		renderer.material.color = new Color (originalColour.r, originalColour.g, originalColour.b, newAlpha);
		yield return new WaitForSeconds(waitTime);
		newAlpha = 0.5f;
		renderer.material.color = new Color (originalColour.r, originalColour.g, originalColour.b, newAlpha);
		yield return new WaitForSeconds(waitTime);
		renderer.material.color = new Color (originalColour.r, originalColour.g, originalColour.b, 1f);
		yield return new WaitForSeconds(waitTime);
		newAlpha = 0.5f;
		renderer.material.color = new Color (originalColour.r, originalColour.g, originalColour.b, newAlpha);
		yield return new WaitForSeconds(waitTime);
		renderer.material.color = new Color (originalColour.r, originalColour.g, originalColour.b, 1f);
		flashing = false;
	}
				
	public IEnumerator move (Transform transform)
	{
		float waitTime;
		if (Input.touchCount != 0) { // touch input detected
			waitTime = 0.5f;
		} else {
			waitTime = 0.1f;
		}
		Moving = true;
		if (down && (RowNumber > 1)) {
			RowNumber--;
			transform.Translate (Vector3.back * RunSpeed * Time.deltaTime);
		} else if (up && (RowNumber < maxRows)) {
			RowNumber++;
			transform.Translate (Vector3.forward * RunSpeed * Time.deltaTime);
			//transform.localPosition += new Vector3(0,gridSize,gridSize);
		}
		yield return new WaitForSeconds(waitTime);
		Moving = false;
	}
}
