using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(SceneManager))]
public class SceneManagerEditor : Editor
{
	
	SceneManager thisScene;
	string[] levelNames = {"Level 1"};
	
	public override void OnInspectorGUI ()
	{
		if (thisScene == null) {
			thisScene = target as SceneManager;
		}
		
		EditorGUILayout.BeginHorizontal ();{
			EditorGUILayout.LabelField ("Start Time: ");
			EditorGUILayout.LabelField ("" + thisScene.startTime);
		}
		EditorGUILayout.EndHorizontal ();
		
		EditorGUILayout.BeginHorizontal ();{
			EditorGUILayout.LabelField ("Elapsed Time: ");
			EditorGUILayout.LabelField ("" + thisScene.elapsedTime);
		}
		EditorGUILayout.EndHorizontal ();
		
		EditorGUILayout.BeginHorizontal ();{
			EditorGUILayout.LabelField ("Level Number: ");
			thisScene.levelNumber = EditorGUILayout.Popup (thisScene.levelNumber, levelNames);
			thisScene.sceneSoundSource.clip = thisScene.levelSounds [thisScene.levelNumber + 4];
		}
		EditorGUILayout.EndHorizontal ();
		
		thisScene.enabled = false;
		thisScene.enabled = true;
		
	}
	

}
