using UnityEngine;
using System.Collections;

public class Animal : MonoBehaviour
{
	private Material animalMaterial;
	private Component[] filters;
	private Vector2[] uva;
	public int animalType;
	private enum Sprites
	{
		Run1,
		Idle,
		Run2
	};
	
	private Vector3[] animalSizes ;
	
	void Awake ()
	{
		RowNum = RowNumpriv;
		xPosition = transform.localPosition.x;
		RunSpeed = RunSpeedPriv;
		captured = capturedPriv;
	}
	
	void Start ()
	{
		
		animalSizes = new Vector3[2];
		animalSizes [0] = new Vector3 (40, 10, 0);
		animalSizes [1] = new Vector3 (20, 10, 0);
	
		animalMaterial = new Material (Shader.Find ("Transparent/VertexLit"));
		filters = GetComponentsInChildren (typeof(MeshFilter));
		filters [0].gameObject.renderer.sharedMaterial = animalMaterial;
		uva = (Vector2[])(((MeshFilter)filters [0]).mesh.uv);
			
		ChangeSprite ((int)Sprites.Idle);
			
		transform.localScale = animalSizes [animalType];
		renderer.sharedMaterial.SetTexture ("_MainTex", GameSetUp.animalAtlases [animalType]);
	}
	
	static public int RowNum;
	private int RowNumpriv = 2;
	private int runNum = 1;
	static public float xPosition;
	private bool animating = false;
	private bool forward = true;
	static public float RunSpeed;
	private float RunSpeedPriv = 13f;
	private float fps = 0.1f;
	static public bool captured;
	private bool capturedPriv = false;
	
	void FixedUpdate ()
	{
		
		if (!Character.fainted && !captured) {
			transform.Translate (Vector3.right * RunSpeed * Time.deltaTime);
			xPosition = transform.localPosition.x;
			if (!animating) {
				StartCoroutine (ChangeSprite (runNum, fps));
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
	
	public IEnumerator ChangeSprite (int j, float time)
	{
		animating = true;
		Vector2[] uvb;
		uvb = new Vector2[uva.Length];
		for (int k=0; k < uva.Length; k++) {
			uvb [k] = new Vector2 ((uva [k].x * GameSetUp.animalRects [animalType] [j].width) + GameSetUp.animalRects [animalType] [j].x, (uva [k].y * GameSetUp.animalRects [animalType] [j].height) + GameSetUp.animalRects [animalType] [j].y);
		}
		yield return new WaitForSeconds(time);
		((MeshFilter)filters [0]).mesh.uv = uvb;
		animating = false;
	}
	
	public void ChangeSprite (int j)
	{
		Vector2[] uvb;
		uvb = new Vector2[uva.Length];
		for (int k=0; k < uva.Length; k++) {
			uvb [k] = new Vector2 ((uva [k].x * GameSetUp.animalRects [animalType] [j].width) + GameSetUp.animalRects [animalType] [j].x, 
								   (uva [k].y * GameSetUp.animalRects [animalType] [j].height) + GameSetUp.animalRects [animalType] [j].y);
		}
		((MeshFilter)filters [0]).mesh.uv = uvb;
	}
	
}
