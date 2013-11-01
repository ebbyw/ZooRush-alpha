using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Animal))]
public class AnimalEditor : Editor {
	
	private Animal thisAnimal;
	private string[] animalTypesText = {"Crocodile", "Tortoise"};
	//private string[] animalTypesText = {"Crocodile", "Flamingo", "Gorilla","Rhino","Tortoise","Zebra"}; 
	private string[] crocodileMaterialNames = {"Crocodile1","Crocodile2","Crocodile3"};
	private string[] tortoiseMaterialNames = {"Tortoise1","Tortoise2","Tortoise3"};
	
	private Vector3[] animalSizes = new Vector3[2] {
		new Vector3 (3f, 1f, 0f),
		new Vector3 (1.5f, 1f, 0f)
	};
	
	public override void OnInspectorGUI() {
		if(thisAnimal == null){
			thisAnimal = target as Animal;
		}
		
		GUILayout.Label("Animal Editor:");
		EditorGUILayout.BeginHorizontal();{
			EditorGUILayout.LabelField("Animal Type");
			thisAnimal.animalType = EditorGUILayout.Popup(thisAnimal.animalType,animalTypesText);
			switch(thisAnimal.animalType){
			case 0: //Crocodile
				thisAnimal.materials[0] = Resources.Load("Materials/Animals/Crocodile/"+crocodileMaterialNames[0],typeof(Material)) as Material;
				thisAnimal.materials[1] = Resources.Load("Materials/Animals/Crocodile/"+crocodileMaterialNames[1],typeof(Material)) as Material;
				thisAnimal.materials[2] = Resources.Load("Materials/Animals/Crocodile/"+crocodileMaterialNames[2],typeof(Material)) as Material;
				thisAnimal.renderer.material = thisAnimal.materials[0];
				break;
			case 1: //Tortoise
				thisAnimal.materials[0] = Resources.Load("Materials/Animals/Tortoise/"+tortoiseMaterialNames[0],typeof(Material)) as Material;
				thisAnimal.materials[1] = Resources.Load("Materials/Animals/Tortoise/"+tortoiseMaterialNames[1],typeof(Material)) as Material;
				thisAnimal.materials[2] = Resources.Load("Materials/Animals/Tortoise/"+tortoiseMaterialNames[2],typeof(Material)) as Material;
				thisAnimal.renderer.material = thisAnimal.materials[0];
				break;
			default:
				break;
			}
			thisAnimal.transform.localScale = animalSizes[thisAnimal.animalType];
			thisAnimal.enabled = false;
			thisAnimal.enabled = true;
		}
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();{
			EditorGUILayout.LabelField("Run Speed: ");
			thisAnimal.RunSpeed = EditorGUILayout.FloatField(thisAnimal.RunSpeed);
			thisAnimal.enabled = false;
			thisAnimal.enabled = true;
		}
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();{
			EditorGUILayout.LabelField("Row Number: ");
			thisAnimal.RowNumber = EditorGUILayout.IntSlider(thisAnimal.RowNumber,1,3);
			thisAnimal.enabled = false;
			thisAnimal.enabled = true;
		}
		EditorGUILayout.EndHorizontal();
	}
	
}
