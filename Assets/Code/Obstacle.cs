using UnityEngine;
using System.Collections;

public class Obstacle : GameElement
{
	public int numObstacles;
	/** Infection Types:
	 *  Infection types are organized in alphabetical order of the infection 
	 *  texture file names in the Assets/Resources/Textures/Infections/ directory
	 * 	0 - Green Infection 1
	 *  1 - Green Infection 2 (for animation purposes)
	 *  2 - Red Infection 1
	 *  3 - Red Infection 2 (for animation purposes)
	 *  4 - Yellow Infection 1
	 *  5 - Yellow Infection 2 (for animation purposes)
	 */
	
	
	public override void Create ()
	{
		animating = false; 
		atlasRects = GameSetUp.obstacleRects;
		numObstacles = GameSetUp.obstacleRects.Length / 2;
		elementType = Random.Range (0, 6);//Chooses number between 0 and 5
		MaterialSetUp();
		if (elementType % 2 != 0) { //If an odd number, brin it down one to make it even
			elementType--;
		}
		ChangeSprite (elementType);
		spritesheet = GameSetUp.obstacleAtlas;
		renderer.sharedMaterial.SetTexture ("_MainTex", spritesheet);
	}
	
	void Update ()
	{
		if(!animating){
			StartCoroutine(ChangeSprite(0.25f));
		}
	}
		
	void OnTriggerEnter (Collider other)
	{ //detects if the Character has touched the obstacle
		GUIHealthBar.obstacleType = elementType;
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
}