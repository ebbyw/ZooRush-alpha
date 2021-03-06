﻿using UnityEngine;
using System.Collections;

public class PowerUp : GameElement
{
	public int RowNumber;
	public int PowerUpType;
	private Vector3[] defaultScale = new Vector3[2]{
		new Vector3(0.125f,0.25f,1f),
		new Vector3(0.25f,0.5f,1f)
	};
	
	/** POWER UP TYPES:
	 *  0 - Pill Bottle
	 *  1 - Water Bottle
	 */ 
	
	void Start ()
	{
		elementType = PowerUpType;
		renderer.material = materials[0];
		transform.localScale = defaultScale[PowerUpType];
		RowNum = RowNumber;
		characterComponent = GameObject.FindGameObjectWithTag("character").GetComponent<Character>();
		animalComponent = GameObject.FindGameObjectWithTag("animal").GetComponent<Animal>();
		painComponent = GameObject.FindGameObjectWithTag("pain").GetComponent<PainIndicator>();
	}
	void Update ()
	{
		Vector3 defaultPosition = transform.localPosition;
		defaultPosition.y = -3f + RowNumber;
		defaultPosition.z = -0.5f + RowNumber;
		transform.localPosition = defaultPosition;
	}
		
	void OnTriggerEnter( Collider other ){ //detects if the Character has touched the PowerUp
		if(PowerUpType == 0){//Pill Bottle
			GameObject.Find("AudioManager").GetComponent<AudioEventHandler>().playPill();
		}
		painComponent.powerUpType = elementType;
		StartCoroutine (itemFlash ());
		painComponent.subtractFromPain= true;
	}
}
