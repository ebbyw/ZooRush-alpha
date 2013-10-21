using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Building))]
public class BuildingEditor : Editor {
	
	private Building thisBuilding;
	private string[] buildingTypesText = {"Balloon Stand", "Cage1","Cage2","Cage3","Cage4","Cage5","Doctor's Office","Hot Dog Stand"};
	private string[] buildingMaterialNames = {"BalloonStand", "Cage1","Cage2","Cage3","Cage4","Cage5","DoctorOffice1", "DoctorOffice2","HotDogsStand"};
	public override void OnInspectorGUI() {
		if(thisBuilding == null){
			thisBuilding = target as Building;
		}
		GUILayout.Label("Building Editor:");
		EditorGUILayout.BeginHorizontal();{
			EditorGUILayout.LabelField("Building Type");
			thisBuilding.buildingType = EditorGUILayout.Popup(thisBuilding.buildingType,buildingTypesText);
			if(thisBuilding.buildingType == 6){
				thisBuilding.materials = new Material[2]{
					Resources.Load("Materials/Buildings/"+buildingMaterialNames[thisBuilding.buildingType],typeof(Material)) as Material,
					Resources.Load("Materials/Buildings/"+buildingMaterialNames[thisBuilding.buildingType+1],typeof(Material)) as Material
				};
			}
			else{
				if(thisBuilding.buildingType < 6){
					thisBuilding.materials[0] = Resources.Load("Materials/Buildings/"+buildingMaterialNames[thisBuilding.buildingType],typeof(Material)) as Material;
				}
				else{
					thisBuilding.materials[0] = Resources.Load("Materials/Buildings/"+buildingMaterialNames[thisBuilding.buildingType+1],typeof(Material)) as Material;
				}
			}
			thisBuilding.renderer.material = thisBuilding.materials[0];
			thisBuilding.enabled = false;
			thisBuilding.enabled = true;
		}
		EditorGUILayout.EndHorizontal();
		
	}
	
}
