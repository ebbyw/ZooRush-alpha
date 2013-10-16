using UnityEngine;
using System.Collections.Generic;

public class FgGenerator : GameObjectGenerator {
	Animal animalComponent = GameObject.FindGameObjectWithTag("animal").GetComponent<Animal>();
	Character characterComponent = GameObject.FindGameObjectWithTag("character").GetComponent<Character>();

	void Start () {
		numOfObjects = 11;
		elementType = "foreground";
		destroyOffset = element.transform.localScale.x + 35;
		characterPointer = GameObject.FindGameObjectWithTag("character");
		objectsInField = 0;
		position = new Vector3(-53f,-29f,-1f);

	}
	
	void FixedUpdate () {
		if(!characterComponent.fainted || !animalComponent.captured){
			CreateSceneElement(Random.Range(40,45),null,null);
		}
	}
}