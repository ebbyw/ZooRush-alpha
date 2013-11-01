using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Obstacle))]
public class ObstacleEditor : Editor {
	
	private Obstacle thisObstacle;
	private string[] obstacleTypesText = {"Green", "Red", "Yellow"}; 
	private string[] greenObstacleMaterialNames = {"GreenInfection1","GreenInfection2"};
	private string[] yellowObstacleMaterialNames = {"YellowInfection1","YellowInfection2"}; 
	private string[] redObstacleMaterialNames = {"RedInfection1","RedInfection2"}; 
	private Material[] obstacleMaterials;
	
	public override void OnInspectorGUI() {
		if(thisObstacle == null){
			thisObstacle = target as Obstacle;
		}
		GUILayout.Label("Obstacle Editor:");
		EditorGUILayout.BeginHorizontal();{
			EditorGUILayout.LabelField("Infection Type");
			thisObstacle.ObstacleType = EditorGUILayout.Popup(thisObstacle.ObstacleType,obstacleTypesText);
			switch(thisObstacle.ObstacleType){
			case 0: //Green
				thisObstacle.materials[0] = Resources.Load("Materials/Infections/"+greenObstacleMaterialNames[0],typeof(Material)) as Material;
				thisObstacle.materials[1] = Resources.Load("Materials/Infections/"+greenObstacleMaterialNames[1],typeof(Material)) as Material;
				thisObstacle.renderer.material = thisObstacle.materials[0];
				break;
			case 1: //Red
				thisObstacle.materials[0] = Resources.Load("Materials/Infections/"+redObstacleMaterialNames[0],typeof(Material)) as Material;
				thisObstacle.materials[1] = Resources.Load("Materials/Infections/"+redObstacleMaterialNames[1],typeof(Material)) as Material;
				thisObstacle.renderer.material = thisObstacle.materials[0];
				break;
			case 2: //Yellow
				thisObstacle.materials[0] = Resources.Load("Materials/Infections/"+yellowObstacleMaterialNames[0],typeof(Material)) as Material;
				thisObstacle.materials[1] = Resources.Load("Materials/Infections/"+yellowObstacleMaterialNames[1],typeof(Material)) as Material;
				thisObstacle.renderer.material = thisObstacle.materials[0];
				break;
			default:
				break;
			}
			thisObstacle.enabled = false;
			thisObstacle.enabled = true;
		}
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();{
			EditorGUILayout.LabelField("Row Number: ");
			thisObstacle.RowNumber = EditorGUILayout.IntSlider(thisObstacle.RowNumber,1,3);
			thisObstacle.enabled = false;
			thisObstacle.enabled = true;
		}
		EditorGUILayout.EndHorizontal();
	}
}
