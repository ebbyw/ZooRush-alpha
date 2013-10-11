using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{
	public int powerUpType;
	public int numPowerUps;
	private Material powerUpMaterial;
	private Component[] filters;
	private Vector2[] uva;
	
	enum powerUpTypes
	{
		OxygenTank,  // Type 0
		PillBottle,  // Type 1
		WaterBottle, // Type 2
		MAX_POWERUP_TYPES
	};
	
	void Start ()
	{
		numPowerUps = GameSetUp.powerUpRects.Length;
		powerUpType = Random.Range(0,3);//Chooses number between 0 and 2
		powerUpMaterial = new Material (Shader.Find ("Transparent/VertexLit"));
		filters = GetComponentsInChildren (typeof(MeshFilter));
		filters [0].gameObject.renderer.sharedMaterial = powerUpMaterial;
		uva = (Vector2[])(((MeshFilter)filters [0]).mesh.uv);
		ChangeSprite (powerUpType);
		renderer.sharedMaterial.SetTexture ("_MainTex", GameSetUp.powerUpAtlas);

	}
	
	void Update ()
	{
	
	}
		
	void OnTriggerEnter( Collider other ){ //detects if the Character has touched the PowerUp
		GUIHealthBar.powerUpType = powerUpType;
		StartCoroutine (itemFlash ());
		GUIHealthBar.addToHealthBar = true;
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
	
	public void ChangeSprite (int j)
	{
		Vector2[] uvb;
		uvb = new Vector2[uva.Length];
		for (int k=0; k < uva.Length; k++) {
			uvb [k] = new Vector2 ((uva [k].x * GameSetUp.powerUpRects [j].width) + GameSetUp.powerUpRects [j].x, 
								   (uva [k].y * GameSetUp.powerUpRects [j].height) + GameSetUp.powerUpRects [j].y);
		}
		((MeshFilter)filters [0]).mesh.uv = uvb;
	}
	
	
}
