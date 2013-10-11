using UnityEngine;
using System.Collections.Generic;

public class FgGenerator : GameObjectGenerator {
	
	void Start () {
		numOfObjects = 11;
		elementType = "foreground";
		destroyOffset = element.transform.localScale.x + 35;
		characterPointer = GameObject.FindGameObjectWithTag("character");
		objectsInField = 0;
		position = new Vector3(-53f,-29f,-1f);

	}
	
	void FixedUpdate () {
		if(!Character.fainted || !Animal.captured){
			CreateSceneElement(Random.Range(40,45));
		}
	}
}