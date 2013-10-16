using UnityEngine;
using System.Collections.Generic;

public class BuildingGenerator : GameObjectGenerator {
	Animal animalComponent = GameObject.FindGameObjectWithTag("animal").GetComponent<Animal>();
	GameObject animalPointer = GameObject.FindGameObjectWithTag("animal");
	Character characterComponent = GameObject.FindGameObjectWithTag("character").GetComponent<Character>();
	void Start () {
		numOfObjects = 6;
		elementType = "building";
		destroyOffset = element.transform.localScale.x + 35;
		characterPointer = GameObject.FindGameObjectWithTag("character");
		objectsInField = 0;
		position = new Vector3 (-55f, -15f, 196f);
	}
	
	void FixedUpdate () {
		if(!characterComponent.fainted || !animalComponent.captured){		
			CreateSceneElement(element.transform.localScale.x, GameSetUp.buildingRects, GameSetUp.buildingAtlas);
		}
	}
}
