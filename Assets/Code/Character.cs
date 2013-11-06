using UnityEngine;
using System.Collections;

public class Character : GameElement
{
	public int characterType;
	public float distanceTraveled;
	public int RowNumber;
	private bool Moving = false;
	private int maxRows = 3;
	private int runNum = 2;
	private bool waiting = false;
	public bool flashing = false;
	public float xPosition;
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
		renderer.material = materials [0];
		RowNum = RowNumber;
		forward = true;
		fps = 0.05f;
		animalComponent = GameObject.FindGameObjectWithTag ("animal").GetComponent<Animal> ();
		paused = SceneManager.scenePaused;
	}
	
	public void FixedUpdate ()
	{ // All math related functions will occur under Fixed Update in order to insure synchronization and consistency
		if (!fainted && !Animal.captured) {
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
					if (InputManager.upKey || InputManager.downKey) {
						StartCoroutine (move (transform));
					}
				}
			}
		}
	}
	
	void Update ()
	{
		paused = SceneManager.scenePaused;
		if (!waiting && !fainted && !Animal.captured && !paused) {
			if (!animating) {
				StartCoroutine (ChangeMaterial (runNum, fps));
			}
			RunUpdate ();
		}
	}
	
	private void RunUpdate ()
	{
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
		if (InputManager.touchDetected) { // touch input detected
			waitTime = 0.5f;
		} else {
			waitTime = 0.1f;
		}
		Moving = true;
		if (InputManager.downKey && (RowNumber > 1)) {
			RowNumber--;
		} else if (InputManager.upKey && (RowNumber < maxRows)) {
			RowNumber++;
		}
		translate(RowNumber);
		yield return new WaitForSeconds(waitTime);
		Moving = false;
	}
	
	private void translate (int Row)
	{
		Vector3 temp = transform.localPosition;
		if (Row == 1) {// Character is in Row 1
			temp.y = -1.5f;
			temp.z = 0f;
		} else {
			if (Row == 2) {// Character is in Row 2
				temp.y = -0.5f;
				temp.z = 1f;
			} else {// Character is in Row 3
				temp.y = 0.5f;
				temp.z = 2f;
			}
		}
		transform.localPosition = temp;
	}
}
