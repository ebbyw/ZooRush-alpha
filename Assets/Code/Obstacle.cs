using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour
{

	public int obstacleType;
	public int numObstacles;
	private Material obstacleMaterial;
	private Component[] filters;
	private Vector2[] uva;
	private bool animating = false; 
	
	enum obstacleTypes
	{
		Green1,    // Type 0 *** 
		Green2,    // Type 1
		Red1,      // Type 2 ***
		Red2,      // Type 3
		Yellow1,   // Type 4 ***
		Yellow2,   // Type 5
		MAX_OBSTACLE_TYPES
	}
	
	void Start ()
	{
		
		numObstacles = GameSetUp.obstacleRects.Length / 2;
		obstacleType = Random.Range (0, 6);//Chooses number between 0 and 5
		obstacleMaterial = new Material (Shader.Find ("Transparent/VertexLit"));
		filters = GetComponentsInChildren (typeof(MeshFilter));
		filters [0].gameObject.renderer.sharedMaterial = obstacleMaterial;
		uva = (Vector2[])(((MeshFilter)filters [0]).mesh.uv);
		if (obstacleType % 2 != 0) { //If an odd number, brin it down one to make it even
			obstacleType--;
		}
		ChangeSprite (obstacleType);
		renderer.sharedMaterial.SetTexture ("_MainTex", GameSetUp.obstacleAtlas);

	}
	
	void Update ()
	{
		if(!animating){
			StartCoroutine(ChangeSprite(0.25f));
		}
		
		
	}
		
	void OnTriggerEnter (Collider other)
	{ //detects if the Character has touched the obstacle
		GUIHealthBar.obstacleType = obstacleType;
		StartCoroutine (itemFlash ());
		Character.flashing = true;
		GUIHealthBar.subtractFromHealthBar = true;
	}
	
	public IEnumerator itemFlash ()
	{
		float newAlpha = 0.5f;
		float waitTime = 0.1f;
		Color originalColour = renderer.sharedMaterial.color;
		renderer.sharedMaterial.color = new Color (originalColour.r, originalColour.g, originalColour.b, newAlpha);
		yield return new WaitForSeconds(waitTime);
		newAlpha = 1f;
		renderer.sharedMaterial.color = new Color (originalColour.r, originalColour.g, originalColour.b, newAlpha);
		yield return new WaitForSeconds(waitTime);
		newAlpha = 0f;
		renderer.sharedMaterial.color = new Color (originalColour.r, originalColour.g, originalColour.b, newAlpha);
	}
	
	public void ResetAlpha ()
	{
		Color originalColour = renderer.material.color;
		renderer.sharedMaterial.color = new Color (originalColour.r, originalColour.g, originalColour.b, 1f);
	}
	
	public IEnumerator ChangeSprite (float time)
	{
		int j = obstacleType;
		animating = true;
		Vector2[] uvb;
		uvb = new Vector2[uva.Length];
		for (int k=0; k < uva.Length; k++) {
			uvb [k] = new Vector2 ((uva [k].x * GameSetUp.obstacleRects [j].width) + GameSetUp.obstacleRects [j].x, 
								   (uva [k].y * GameSetUp.obstacleRects [j].height) + GameSetUp.obstacleRects [j].y);
		}
		yield return new WaitForSeconds(time);
		((MeshFilter)filters [0]).mesh.uv = uvb;
		j++;
		for (int k=0; k < uva.Length; k++) {
			uvb [k] = new Vector2 ((uva [k].x * GameSetUp.obstacleRects [j].width) + GameSetUp.obstacleRects [j].x, 
								   (uva [k].y * GameSetUp.obstacleRects [j].height) + GameSetUp.obstacleRects [j].y);
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
			uvb [k] = new Vector2 ((uva [k].x * GameSetUp.obstacleRects [j].width) + GameSetUp.obstacleRects [j].x, 
								   (uva [k].y * GameSetUp.obstacleRects [j].height) + GameSetUp.obstacleRects [j].y);
		}
		((MeshFilter)filters [0]).mesh.uv = uvb;
	}
}