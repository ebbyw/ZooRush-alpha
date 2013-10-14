using UnityEngine;
using System.Collections;

public class PowerUp : GameElement
{
	public int numPowerUps;
	
	enum elementTypes
	{
		PillBottle,  // Type 0
		WaterBottle, // Type 1
		MAX_POWERUP_TYPES
	};
	
	public override void Create ()
	{
		atlasRects = GameSetUp.powerUpRects;
		numPowerUps = atlasRects.Length;
		elementType = Random.Range(0,2);//Chooses number between 0 and 1
		MaterialSetUp();
		ChangeSprite (elementType);
		spritesheet = GameSetUp.powerUpAtlas;
		renderer.sharedMaterial.SetTexture ("_MainTex", spritesheet);

	}
		
	void OnTriggerEnter( Collider other ){ //detects if the Character has touched the PowerUp
		GUIHealthBar.powerUpType = elementType;
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
}
