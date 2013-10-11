using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
	public float distanceTraveled;
	static public int characterValue;
	private int characterValuePriv = 0;
	private Material characterMaterial;
	private Component[] filters;
	private Vector2[] uva;
	static public bool paused;
	private bool pausedPriv = true;
	
	private enum Sprites //Specific to BoyZoo, must create generic form
	{
		NormalIdle,
		ZooIdle,
		Run1,
		Run2,
		Run3
	};
	
	void Awake ()
	{
		characterValue = characterValuePriv;
		paused = pausedPriv;
		RowNum = RowNumPriv;
		up = upPriv;
		down = downPriv;
		flashing = flashingPriv;
		RunSpeed = RunSpeedPriv;
		fainted = faintedPriv;
	}
	
	void Start ()
	{
		xPosition = transform.localPosition.x;
		distanceTraveled = transform.localPosition.x;
	
		characterMaterial = new Material (Shader.Find ("Transparent/VertexLit"));
		filters = GetComponentsInChildren (typeof(MeshFilter));
		filters [0].gameObject.renderer.sharedMaterial = characterMaterial;
		uva = (Vector2[])(((MeshFilter)filters [0]).mesh.uv);
			
		ChangeSprite ((int)Sprites.ZooIdle);//Specific to BoyZoo, must create generic form

		renderer.sharedMaterial.SetTexture ("_MainTex", GameSetUp.characterAtlases [characterValue]);
	}
	
	private bool Moving = false;
	static public int RowNum;
	private int RowNumPriv = 1;
	private int maxRows = 4;
	static public bool up;
	static public bool down;
	private bool upPriv = false;
	private bool downPriv = false;
	private int runNum = 2;
	private bool forward = true;
	private bool waiting = false;
	static public bool flashing;
	static public float xPosition;
	private bool flashingPriv = false;
	private Touch touch;
	private float dragY;
	static public float RunSpeed;
	private float RunSpeedPriv = 15f;
	public float fps = 0.1f;
	static public bool fainted;
	private bool faintedPriv = false;
	
	public void FixedUpdate ()
	{ // All math related functions will occur under Fixed Update in order to insure synchronization and consistency
		if (!fainted && !Animal.captured) {
			if (paused) {
				StartCoroutine (StartSequence (10f));
				GameObject.Find ("SceneManager").GetComponent<SceneManager> ().startTime = Time.time ;
			} else {
				xPosition = transform.localPosition.x;
				GameObject.Find ("SceneManager").GetComponent<SceneManager> ().elapsedTime = Time.time - GameObject.Find ("SceneManager").GetComponent<SceneManager> ().startTime;	
				distanceTraveled += Mathf.Abs (distanceTraveled - transform.localPosition.x);
				transform.Translate (Vector3.right * RunSpeed * Time.deltaTime);
				if (transform.localPosition.x >= 0) {
					GameObject.Find ("Main Camera").camera.transform.Translate (Vector3.right * RunSpeed * Time.deltaTime);
				}
			
				if (flashing) {
					StartCoroutine (characterFlash ());	
				}

				if (!Moving) {
					down = Input.GetKey ("down");
					up = Input.GetKey ("up");
			
					if (Input.touchCount != 0) { // touch input detected
						touch = Input.GetTouch (0);
						if (touch.deltaPosition.y > 0) {
							up = true;
							down = false;
						}
						if (touch.deltaPosition.y < 0) {
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
		else{
			GameObject.Find ("SceneManager").GetComponent<SceneManager> ().endTime = Time.time;
		}
	}
	
	void Update ()
	{
		if (!waiting && !fainted && !Animal.captured && !paused) {
			StartCoroutine (ChangeSprite (runNum, fps));
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
	
	public IEnumerator ChangeSprite (int j, float time)
	{
		waiting = true;
		Vector2[] uvb;
		uvb = new Vector2[uva.Length];
		for (int k=0; k < uva.Length; k++) {
			uvb [k] = new Vector2 ((uva [k].x * GameSetUp.characterRects [characterValue] [j].width) + GameSetUp.characterRects [characterValue] [j].x, (uva [k].y * GameSetUp.characterRects [characterValue] [j].height) + GameSetUp.characterRects [characterValue] [j].y);
		}
		yield return new WaitForSeconds(time);
		((MeshFilter)filters [0]).mesh.uv = uvb;
		waiting = false;
	}
	
	public IEnumerator StartSequence (float time)
	{
		yield return new WaitForSeconds(time);
		paused = false;
	}
	
	public IEnumerator characterFlash ()
	{
		float newAlpha = 0.5f;
		float waitTime = 0.1f;
		Color originalColour = renderer.sharedMaterial.color;
		renderer.sharedMaterial.color = new Color (originalColour.r, originalColour.g, originalColour.b, newAlpha);
		yield return new WaitForSeconds(waitTime);
		newAlpha = 1f;
		renderer.sharedMaterial.color = new Color (originalColour.r, originalColour.g, originalColour.b, newAlpha);
		yield return new WaitForSeconds(waitTime);
		newAlpha = 0.5f;
		renderer.sharedMaterial.color = new Color (originalColour.r, originalColour.g, originalColour.b, newAlpha);
		yield return new WaitForSeconds(waitTime);
		renderer.sharedMaterial.color = new Color (originalColour.r, originalColour.g, originalColour.b, 1f);
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
		if (down && (RowNum > 1)) {
			RowNum--;
			transform.Translate (Vector3.back * GameSetUp.gridSize);
		} else if (up && (RowNum < maxRows)) {
			RowNum++;
			transform.Translate (Vector3.forward * GameSetUp.gridSize);
		}
		yield return new WaitForSeconds(waitTime);
		Moving = false;
		
	}
	
	public void ChangeSprite (int j)
	{
		Vector2[] uvb;
		uvb = new Vector2[uva.Length];
		for (int k=0; k < uva.Length; k++) {
			uvb [k] = new Vector2 ((uva [k].x * GameSetUp.characterRects [characterValue] [j].width) + GameSetUp.characterRects [characterValue] [j].x, 
								   (uva [k].y * GameSetUp.characterRects [characterValue] [j].height) + GameSetUp.characterRects [characterValue] [j].y);
		}
		((MeshFilter)filters [0]).mesh.uv = uvb;
	}
	
	int levelPoints;
	
	
}
