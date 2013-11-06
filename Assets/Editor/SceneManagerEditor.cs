using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(SceneManager))]
public class SceneManagerEditor : Editor {

	private SceneManager thisSceneManager;
	
	public override void OnInspectorGUI() {
		if(thisSceneManager == null){
			thisSceneManager = target as SceneManager;
		}
		
		GUILayout.Label("Scene Manager Options:");
		
		EditorGUILayout.BeginHorizontal();{
			EditorGUILayout.LabelField("Scene Paused: " );
			EditorGUILayout.Toggle(SceneManager.scenePaused);
		}
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();{
			EditorGUILayout.LabelField("Start Time:" );
			EditorGUILayout.LabelField(""+thisSceneManager.startTime);
		}
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();{
			EditorGUILayout.LabelField("Elapsed Time:" );
			EditorGUILayout.LabelField(""+thisSceneManager.elapsedTime);
		}
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();{
			EditorGUILayout.LabelField("Level Number:" );
			thisSceneManager.lvlNum = EditorGUILayout.IntSlider(thisSceneManager.lvlNum,0,1);
		}
		EditorGUILayout.EndHorizontal();
		thisSceneManager.enabled = false;
		thisSceneManager.enabled = true;
	}
	
}
