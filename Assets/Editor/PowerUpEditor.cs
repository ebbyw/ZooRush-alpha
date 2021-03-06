﻿using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(PowerUp))]
public class PowerUpEditor : Editor {

	private PowerUp thisPowerUp;
	private string[] powerUpTypesText = {"Pill Bottle", "Water Bottle"}; 
	private Vector3[] defaultScale = new Vector3[2]{
		new Vector3(0.125f,0.25f,1f),
		new Vector3(0.25f,0.5f,1f)
	};
	
	public override void OnInspectorGUI() {
		if(thisPowerUp == null){
			thisPowerUp = target as PowerUp;
		}
		switch(thisPowerUp.PowerUpType){
			case 0: //PillBottle
				thisPowerUp.materials[0] = Resources.Load("Materials/PowerUps/PillBottle",typeof(Material)) as Material;
				thisPowerUp.renderer.material = thisPowerUp.materials[0];
				break;
			case 1: //Water Bottle
				thisPowerUp.materials[0] = Resources.Load("Materials/PowerUps/WaterBottle",typeof(Material)) as Material;
				thisPowerUp.renderer.material = thisPowerUp.materials[0];
				if(thisPowerUp.GetComponent<AudioSource>() != null){
					DestroyImmediate(thisPowerUp.GetComponent<AudioSource>());
				}
				break;
			default:
				break;
			}
		
		GUILayout.Label("Obstacle Editor:");
		EditorGUILayout.BeginHorizontal();{
			EditorGUILayout.LabelField("PowerUp Type");
			thisPowerUp.PowerUpType = EditorGUILayout.Popup(thisPowerUp.PowerUpType,powerUpTypesText);
			thisPowerUp.transform.localScale = defaultScale[thisPowerUp.PowerUpType];
			
		}
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();{
			EditorGUILayout.LabelField("Row Number: ");
			thisPowerUp.RowNumber = EditorGUILayout.IntSlider(thisPowerUp.RowNumber,1,3);
		}
		EditorGUILayout.EndHorizontal();
		
		thisPowerUp.enabled = false;
		thisPowerUp.enabled = true;
	}
}
