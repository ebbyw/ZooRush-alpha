using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Character))]
public class CharacterEditor : Editor {
	
	private Character thisCharacter;
	private string[] characterTypesText = {"Boy Zoo Keeper"};
	public override void OnInspectorGUI() {
		if(thisCharacter == null){
			thisCharacter = target as Character;
		}
		
		GUILayout.Label("Character Editor:");
		EditorGUILayout.BeginHorizontal();{
			EditorGUILayout.LabelField("Character Type");
			thisCharacter.characterType = EditorGUILayout.Popup(thisCharacter.characterType,characterTypesText);
			switch(thisCharacter.characterType){
			case 0: //Boy Zoo Keeper
				break;
			default:
				break;
			}
			thisCharacter.enabled = false;
			thisCharacter.enabled = true;
		}
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();{
			EditorGUILayout.LabelField("Run Speed: ");
			thisCharacter.RunSpeed = EditorGUILayout.FloatField(thisCharacter.RunSpeed);
			thisCharacter.enabled = false;
			thisCharacter.enabled = true;
		}
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();{
			EditorGUILayout.LabelField("Row Number: ");
			thisCharacter.RowNumber = EditorGUILayout.IntSlider(thisCharacter.RowNumber,1,3);
			thisCharacter.enabled = false;
			thisCharacter.enabled = true;
		}
		EditorGUILayout.EndHorizontal();
	}
}
