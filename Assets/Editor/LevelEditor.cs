using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

public class LevelEditor : EditorWindow
{	
	GameObject SceneManagerObject;
	
	[MenuItem("Zoo Rush Tools/Level Editor %l")]
	private static void showEditor ()
	{
		EditorWindow.GetWindow<LevelEditor> (false, "Level Editor");
	}
        
	[MenuItem("Zoo Rush Tools/Level Editor %l", true)]
	private static bool showEditorValidator ()
	{ // Enables or disables the menu option
		return true;
	}
	
	void Update ()
	{
		ohSnap ();
		if(SceneManagerObject == null){
			SceneManagerObject = new GameObject("SceneManager",typeof(SceneManager));
			SceneManagerObject.GetComponent<SceneManager>().myskin = Resources.Load("FaintedOrCaptured", typeof(GUISkin)) as GUISkin;
		}
		if(Camera.main.GetComponentInChildren<PainIndicator>() == null){
			//GameObject Pain = new GameObject.Instantiate(Resources.Load("Prefabs/PainIndicator",typeof(PrefabType)));
			//Transform pTrans = Pain.transform;
			//pTrans.parent = Camera.main.transform;
		}
	}
	
	void OnGUI ()
	{
		EditorGUILayout.LabelField ("Please keep this tab visible.");
		if(GameObject.FindGameObjectWithTag("character") == null){
			EditorGUILayout.LabelField("Please include a Character Object");
		}
		if(GameObject.FindGameObjectWithTag("animal") == null){
			EditorGUILayout.LabelField("Please include an Animal Object");
		}
		if(GameObject.FindGameObjectWithTag("powerUp") == null){
			EditorGUILayout.LabelField("Please include at least one PowerUp Object");
		}
		if(GameObject.FindGameObjectWithTag("obstacle") == null){
			EditorGUILayout.LabelField("Please include at least one Infection Object");
		}
		if(GameObject.FindGameObjectWithTag("sky") == null){
			EditorGUILayout.LabelField("Please include at least one Sky Object");
		}
		if(GameObject.FindGameObjectWithTag("ground") == null){
			EditorGUILayout.LabelField("Please include at least one Ground Object");
		}
		if(GameObject.FindGameObjectWithTag("building") == null){
			EditorGUILayout.LabelField("Please include at least one Building Object");
		}
	}
	
	void ohSnap ()
	{
		GameObject[] allobjects = GameObject.FindObjectsOfType (typeof(GameObject)) as GameObject[];
		foreach (GameObject go in allobjects) {
			if (go.GetComponent (typeof(Camera)) != null) {
				Vector3 temp = go.transform.localPosition;
				temp.y = 0;
				temp.z = 0;
				go.transform.localPosition = temp;
				go.transform.localScale = new Vector3 (1, 1, 1);
				go.GetComponent<Camera> ().isOrthoGraphic = true;
				go.GetComponent<Camera> ().orthographicSize = 2.5f;
				go.GetComponent<Camera> ().nearClipPlane = 0f;
				go.GetComponent<Camera> ().farClipPlane = 21f;
			}
			if (go.GetComponent (typeof(PainIndicator)) != null) {
				go.tag = "pain";
			}
			if (go.GetComponent (typeof(Building)) != null) {//Object is a Building
				go.tag = "building";
				Vector3 temp = go.transform.localPosition;
				if (go.GetComponent<Building> ().BuildingType == 6) {// Element is a Hospital
					temp.y = 1.933f;
					temp.z = 5f;
				} else {
					temp.y = 1.7f;
					temp.z = 4f;
				}
				go.transform.localPosition = temp;
			}
			if (go.GetComponent (typeof(Animal)) != null) {//Object is an Animal
				go.tag = "animal";
				Vector3 temp = go.transform.localPosition;
				if (go.GetComponent<Animal> ().RowNumber == 1) {// Element is in Row 1
					temp.y = -1.75f;
					temp.z = 0f;
				} else {
					if (go.GetComponent<Animal> ().RowNumber == 2) {// Element is in Row 2
						temp.y = -0.75f;
						temp.z = 1f;
					} else {// Element is in Row 3
						temp.y = 0.25f;
						temp.z = 2f;
					}
				}
				go.transform.localPosition = temp;
			}
			if (go.GetComponent (typeof(Character)) != null) {//Object is a character
				go.tag = "character";
				Vector3 temp = go.transform.localPosition;
				if (go.GetComponent<Character> ().RowNumber == 1) {// Element is in Row 1
					temp.y = -1.5f;
					temp.z = 0f;
				} else {
					if (go.GetComponent<Character> ().RowNumber == 2) {// Element is in Row 2
						temp.y = -0.5f;
						temp.z = 1f;
					} else {// Element is in Row 3
						temp.y = 0.5f;
						temp.z = 2f;
					}
				}
				go.transform.localPosition = temp;
			}
			if (go.GetComponent (typeof(Obstacle)) != null) {//Object is a obstacle
				go.tag = "obstacle";
				Vector3 temp = go.transform.localPosition;
				if (go.GetComponent<Obstacle> ().RowNumber == 1) {// Element is in Row 1
					temp.y = -2.125f;
					temp.z = 0.5f;
				} else {
					if (go.GetComponent<Obstacle> ().RowNumber == 2) {// Element is in Row 2
						temp.y = -1.125f;
						temp.z = 1.5f;
					} else {// Element is in Row 3
						temp.y = -0.125f;
						temp.z = 2.5f;
					}
				}
				go.transform.localPosition = temp;
			}
			if (go.GetComponent (typeof(PowerUp)) != null) {//Object is a powerUp
				go.tag = "powerUp";
				Vector3 temp = go.transform.localPosition;
				if (go.GetComponent<PowerUp> ().PowerUpType == 0) {//element is a pill bottle
					if (go.GetComponent<PowerUp> ().RowNumber == 1) {// Element is in Row 1
						temp.y = -2.125f;
						temp.z = 0.5f;
					} else {
						if (go.GetComponent<PowerUp> ().RowNumber == 2) {// Element is in Row 2
							temp.y = -1.125f;
							temp.z = 1.5f;
						} else {// Element is in Row 3
							temp.y = -0.125f;
							temp.z = 2.5f;
						}
					}
				} else {
					if (go.GetComponent<PowerUp> ().RowNumber == 1) {// Element is in Row 1
						temp.y = -2f;
						temp.z = 0.5f;
					} else {
						if (go.GetComponent<PowerUp> ().RowNumber == 2) {// Element is in Row 2
							temp.y = -1;
							temp.z = 1.5f;
						} else {// Element is in Row 3
							temp.y = 0f;
							temp.z = 2.5f;
						}
					}
				}
				go.transform.localPosition = temp;
			}
			
			if (go.name.Equals ("Ground")) {// Object is a ground element
				go.tag = "ground";
				Vector3 temp = go.transform.localPosition;
				temp.y = -0.75f;
				temp.z = 6f;
				go.transform.localPosition = temp;
			}
			
			if (go.name.Equals ("Sky")) {// Object is a sky element
				go.tag = "sky";
				Vector3 temp = go.transform.localPosition;
				temp.y = 1.75f;
				temp.z = 8f;
				go.transform.localPosition = temp;
			}
			
		}
	}
	
}
