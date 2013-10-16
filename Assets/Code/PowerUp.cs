using UnityEngine;
using System.Collections;

public class PowerUp : GameElement
{
	public int RowNumber;
	public int PowerUpType;
	private Vector3 defaultScale = new Vector3(0.25f,0.5f,1f);
	
	/** POWER UP TYPES:
	 *  0 - Pill Bottle
	 *  1 - Water Bottle
	 */ 
	
	void Start ()
	{
		elementType = PowerUpType;//Chooses number between 0 and 1
		renderer.material = materials[0];
		transform.localScale = defaultScale;
		RowNum = RowNumber;
	}
	void Update ()
	{
		Vector3 defaultPosition = transform.localPosition;
		defaultPosition.y = -3f + RowNumber;
		defaultPosition.z = -0.5f + RowNumber;
		transform.localPosition = defaultPosition;
	}
		
	void OnTriggerEnter( Collider other ){ //detects if the Character has touched the PowerUp
		GUIHealthBar.powerUpType = elementType;
		StartCoroutine (itemFlash ());
		GUIHealthBar.addToHealthBar = true;
	}
}
