using UnityEngine;
using System.Collections.Generic;

public class BuildingGenerator : GameObjectGenerator {
	
	void Start () {
		numOfObjects = 6;
		elementType = "building";
		destroyOffset = element.transform.localScale.x + 35;
		characterPointer = GameObject.FindGameObjectWithTag("character");
		objectsInField = 0;
		position = new Vector3 (-55f, -15f, 196f);
	}
	
	void FixedUpdate () {
		if(!Character.fainted || !Animal.captured){		
			CreateSceneElement(element.transform.localScale.x);
		}
	}
}
