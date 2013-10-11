using UnityEngine;
using System.Collections.Generic;

public class BgGenerator : GameObjectGenerator {
	public bool sky = false;
	
	void Start () {
		numOfObjects = 2;
		destroyOffset = element.transform.localScale.x + 35;
		characterPointer = GameObject.FindGameObjectWithTag("character");
		objectsInField = 0;
		position = new Vector3 (-85f, (sky)?-20:-45f, (sky)?199:198f);
	}
	
	void FixedUpdate () {
		if(!Character.fainted || !Animal.captured){		
			CreateSceneElement(element.transform.localScale.x);
		}
	}
}