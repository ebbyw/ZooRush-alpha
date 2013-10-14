using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class LevelEditor : EditorWindow
{	
	private string[] ElementCategories = {"Building","Obstacle","Power Up","Scene Element"};
	/** Element Categories will manipulate which value "currentCategory" contains
	 *  0 - Building
	 *  1 - Obstacle
	 *  2 - Power Up
	 *  3 - Scene Element
	 */ 
	private string[] GameElementsBuildings = {"Balloon Stand","Cage 1","Cage 2","Cage 3","Cage 4","Cage 5","Doctor's Office","Hot Dog Stand"};
	/**  Building Types:
	 *  Building types are organized in alphabetical order of the building 
	 *  texture file names in the Assets/Resources/Textures/Buildings/ directory
	 * 
	 * 	0 - Balloon Stand
	 *  1 - Cage 1 (Aviary Cage)
	 *  2 - Cage 2 (Swamp/Wetlands)
	 *  3 - Cage 3 (Lake)
	 *  4 - Cage 4 (Plains)
	 *  5 - Cage 5 (General Purpose)
	 *  6 - Doctor Office (Doors Closed)
	 *    - Doctor Office (Doors Open) **not represented in the string array
	 *  7 - Hot Dog Stand
	 */ 
	private string[] GameElementsObstacles = {"Green Infection","Red Infection","Yellow Infection"};
	/** Infection Types:
	 *  Infection types are organized in alphabetical order of the infection 
	 *  texture file names in the Assets/Resources/Textures/Infections/ directory
	 * 	0 - Green Infection 1
	 *    - Green Infection 2 (for animation purposes)
	 *  1 - Red Infection 1
	 *    - Red Infection 2 (for animation purposes)
	 *  2 - Yellow Infection 1
	 *    - Yellow Infection 2 (for animation purposes)
	 */
	private string[] GameElementsPowerUps = {"Pill Bottle","Water Bottle"};
	
	//private string[] GameElementsSceneElements = {};
	/** Scene Element Types:
	 *  Scene Element  types are organized in alphabetical order of the scene element  
	 *  texture file names in the Assets/Resources/Textures/Scene/ directory
	 * 	
	 */
	
	private List<GameObject> Buildings = new List<GameObject> ();
	private List<GameObject> Obstacles = new List<GameObject> ();
	private List<GameObject> PowerUps = new List<GameObject> ();
	private List<GameObject> SceneElements = new List<GameObject> ();
	private int currentCategory;
	private int currentElement;
	private string elementName;
	private string elementComponent;
	private float lastBuildingXPosition = -2.5f;
	private int RowNum;

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
	
	void OnSelectionChange ()
	{
		Repaint ();
	}
	
	void OnGUI ()
	{
		EditorGUILayout.BeginHorizontal ();{
			EditorGUILayout.PrefixLabel ("Element Category: ");
			currentCategory = EditorGUILayout.Popup (currentCategory, ElementCategories);
		}
		EditorGUILayout.EndHorizontal ();
		switch (currentCategory) {
		case 0: //Building Element Category
			EditorGUILayout.BeginHorizontal ();{
				EditorGUILayout.PrefixLabel ("Element Type: ");
				if(currentElement >= GameElementsBuildings.Length){
					currentElement = 0;
				}
				currentElement = EditorGUILayout.Popup (currentElement, GameElementsBuildings);
			}
			EditorGUILayout.EndHorizontal ();
			elementComponent = "Building";
			elementName = elementComponent + " - " + GameElementsBuildings [currentElement];
			
			break;
			
			
		case 1://Obstacle Element Category
			EditorGUILayout.BeginHorizontal ();{
				EditorGUILayout.PrefixLabel ("Element Type: ");
				if(currentElement >= GameElementsObstacles.Length){
					currentElement = 0;
				}
				currentElement = EditorGUILayout.Popup (currentElement, GameElementsObstacles);
			}
			EditorGUILayout.EndHorizontal ();
			elementComponent = "Obstacle";
			elementName = elementComponent + " - " + GameElementsObstacles [currentElement];
			EditorGUILayout.BeginHorizontal ();{
				EditorGUILayout.PrefixLabel ("Row Number: ");
				RowNum = EditorGUILayout.IntSlider (RowNum, 1, 3);
			}
			EditorGUILayout.EndHorizontal ();
			break;
			
			
		case 2://Power Up Element Category
			EditorGUILayout.BeginHorizontal ();{
				EditorGUILayout.PrefixLabel ("Element Type: ");
				if(currentElement >= GameElementsPowerUps.Length){
					currentElement = 0;
				}
				currentElement = EditorGUILayout.Popup (currentElement, GameElementsPowerUps);
			}
			EditorGUILayout.EndHorizontal ();
			elementComponent = "PowerUp";
			elementName = elementComponent + " - " + GameElementsPowerUps [currentElement];
			EditorGUILayout.BeginHorizontal ();{
				EditorGUILayout.PrefixLabel ("Row Number: ");
				RowNum = EditorGUILayout.IntSlider (RowNum, 1, 3);
			}
			EditorGUILayout.EndHorizontal ();
			break;
		case 3://Scene Element Category
			EditorGUILayout.BeginHorizontal ();{
				EditorGUILayout.PrefixLabel ("Element Type: ");
				if(currentElement >= ElementCategories.Length){
					currentElement = 0;
				}
				currentElement = currentCategory = EditorGUILayout.Popup (currentElement, ElementCategories);
			}
			EditorGUILayout.EndHorizontal ();
			elementName = elementComponent + " - " + GameElementsPowerUps [currentElement];
			break;
		default:
			Debug.Log ("ERROR! Current category is out of bounds!");
			break;
		}
		
	
		if (GUILayout.Button ("Create Object")) {
			CreateGameElement ();
			SceneView.RepaintAll ();
		}
	}
	
	void CreateGameElement ()
	{
		GameObject newObject = GameObject.CreatePrimitive (PrimitiveType.Quad);
		newObject.name = elementName;
		
		//Adds respective components that correspond with the element's category and type and then adds that item to the list:
		switch (elementComponent) {
		case "Building":
			/* IMPORTANT NOTE:
			 * It is important to note that if a building element has been deleted from the scene view, the next time 
			 * "Create Object" is called for a a building element, it will be added to the first empty location in the scene.
			 * 
			 * Standard Building Type Properties:
			 *  ***The first Building object starts at position (x,y,z) = (-2.5,1.5,47), putting it at the top left corner of the screen
			 * Buildings will always be put in the z position of 47
			 * Standard transform scale of building objects is (x,y,z) = (1,1,1)
			 * except in the case of hospitals, then the scale is (x,y,z) = (2,2,1) and the position is (xpos,1.91,47)
			 */ 
			Building objectComponent = newObject.AddComponent (elementComponent) as Building;
			int BuildingType = currentElement;
			if(BuildingType == 7){//aligning types with Building object type handling
				BuildingType = 8;
			}
			objectComponent.ChangeElementType(BuildingType);
			
			if (CheckForEmptyBuildingSpots ()) {
				int index = 0;
				foreach (var Building in Buildings) {
					if (Building == null) {
						break;
					}
					index++;
				}
				Debug.Log ("Building " + index + " was Deleted!");
				if(index == 0){
					lastBuildingXPosition = -2.5f;
				}
				else{
					lastBuildingXPosition = Buildings[index-1].transform.localPosition.x + 
											Buildings[index-1].transform.localScale.x + 
											((BuildingType == 6 && Buildings[index-1].GetComponent<Building>().getType() !=6 )?0.5f:0f) + 
											((BuildingType != 6 && Buildings[index-1].GetComponent<Building>().getType() == 6)?(-0.5f):0f);
				}
				Buildings [index] = newObject;
				newObject.transform.localPosition = new Vector3(lastBuildingXPosition,1.5f,47f);
			} else {
				Buildings.Add (newObject);
				newObject.transform.localPosition = new Vector3(lastBuildingXPosition,1.5f,47f);
				lastBuildingXPosition = Buildings[Buildings.Count -1].transform.localPosition.x + Buildings[Buildings.Count -1].transform.localScale.x;
			}
			objectComponent.Create();
			break;
		case "Obstacle":
			newObject.AddComponent (elementComponent);
			Obstacles.Add (newObject);
			break;
		case "PowerUp":
			newObject.AddComponent (elementComponent);
			PowerUps.Add (newObject);
			break;
		case "Foreground":
			newObject.AddComponent ("ForegroundItem");
			SceneElements.Add (newObject);
			break;
		case "Sky":
			break;
		case "Floor":
			break;
		default:
			Debug.Log ("ERROR! Component not found for element chosen!");
			break;
		}
		
		
	}
	
	private bool CheckForEmptyBuildingSpots ()
	{
		int index = 0;
		foreach (var Building in Buildings) {
			if (Building == null) {
				return true;
			}
			index++;
		}
		return false;
	}
	
	
}
