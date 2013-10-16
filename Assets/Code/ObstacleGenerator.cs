using UnityEngine;
using System.Collections;

public class ObstacleGenerator : GameObjectGenerator {
		Animal animalComponent = GameObject.FindGameObjectWithTag("animal").GetComponent<Animal>();
		Character characterComponent = GameObject.FindGameObjectWithTag("character").GetComponent<Character>();

	void Start () {
		numOfObjects = 6;
		elementType = "obstacle";
		destroyOffset = element.transform.localScale.x + 40;
		characterPointer = GameObject.FindGameObjectWithTag("character");
		objectsInField = 0;
		position = new Vector3 (characterPointer.transform.localPosition.x + Random.Range(46,87),-20f,Random.Range(0,3)*GameSetUp.gridSize+3);
	}
	
	void FixedUpdate () {
		if(!characterComponent.fainted || !animalComponent.captured){
			CreateSceneElement(Random.Range(0,66), GameSetUp.obstacleRects, GameSetUp.obstacleAtlas);
			position.z = Random.Range(0,3)*GameSetUp.gridSize+3; // chooses rows 1, 2, or 3
		}
	}
}
