using UnityEngine;
using System.Collections;

public class Animal : GameElement
{
	public int animalType;
	public bool captured;
	public int RowNumber;
	public float xPosition;
	private int runNum = 1;
	public float RunSpeed;
	
	/** Animal Types:
	 *  0 - Alligator
	 *  1 - Tortoise
	 */ 
	
	/** Sprite Sequence:
	 *  0 - Run 1
	 *  1 - Run 2
	 *  2 - Run 3
	 */
	
	private Vector3[] animalSizes = new Vector3[2] ;
	
	
	void Awake ()
	{
		RowNum = RowNumber;
		xPosition = transform.localPosition.x;
		RunSpeed = 3f;
		captured = false;
		characterComponent = GameObject.FindGameObjectWithTag("character").GetComponent<Character>();
	}
	
	void Start ()
	{
		animalSizes [0] = new Vector3 (3f, 1f, 0f);
		animalSizes [1] = new Vector3 (1.5f, 1f, 0f);
		renderer.material = materials[0];	
		transform.localScale = animalSizes [animalType];
		forward = true;
		fps = 0.1f;
	}
	
	
	
	void FixedUpdate ()
	{
		
		if (!characterComponent.fainted && !captured) {
			transform.Translate (Vector3.right * RunSpeed * Time.deltaTime);
			xPosition = transform.localPosition.x;
			if (!animating) {
				StartCoroutine (ChangeMaterial (runNum, fps));
				if (runNum == 2 && forward) {
					runNum--;
					forward = false;
				} else {
					if (runNum == 0 && !forward) {
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
		
	}

	
}
