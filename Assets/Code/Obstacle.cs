using UnityEngine;
using System.Collections;

public class Obstacle : GameElement
{
	
	public int RowNumber;
	public int ObstacleType;
	private Vector3 defaultScale = new Vector3(0.25f,0.25f,1f);
	
	/** Obstacle Types:
	 * 	0 - Green Infection
	 *  1 - Red Infection
	 *  2 - Yellow Infection
	 */ 
	
	void Start(){
		animating = false;
		renderer.material = materials[0];
		elementType = ObstacleType;
		transform.localScale = defaultScale;
		RowNum = RowNumber;
		fps = 0.25f;
		characterComponent = GameObject.FindGameObjectWithTag("character").GetComponent<Character>();
		animalComponent = GameObject.FindGameObjectWithTag("animal").GetComponent<Animal>();
		painComponent = GameObject.FindGameObjectWithTag("pain").GetComponent<PainIndicator>();
	}
	
	void Update ()
	{
		animate(fps);
		Vector3 defaultPosition = transform.localPosition;
		defaultPosition.y = -3f + RowNumber;
		defaultPosition.z = -0.5f + RowNumber;
		transform.localPosition = defaultPosition;
	}
		
	void OnTriggerEnter (Collider other)
	{ //detects if the Character has touched the obstacle
		GameObject.Find("AudioManager").GetComponent<AudioEventHandler>().playInfection();
		painComponent.obstacleType = elementType;
		StartCoroutine (itemFlash ());
		characterComponent.flashing = true;
		painComponent.addToPain = true;
	}
	
	
}