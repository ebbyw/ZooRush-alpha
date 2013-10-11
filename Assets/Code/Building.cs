using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {
	
	public int buildingType;
	public int numBuildings;
	private Material buildingMaterial;
	private Component[] filters;
	private Vector2[] uva;
	private bool touched = false;
	
	/** Building Spritesheet:
	 *  Building textures are stored in the GameSetUp.buildingAtlas texture,
	 *  the GameSetUp.buildingRects stores the coordinates of each individual
	 *  building in the building atlas. 
	 * 
	 *  Building Types:
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
	 *  7 - Doctor Office (Doors Open)
	 *  8 - Hot Dog Stand
	 *  9 - Sign 1 (Safari Trail)
	 *  10 - Sign 2 (Butterfly Garden)
	 */ 
	
	void Start () {
		numBuildings = GameSetUp.buildingRects.Length;
		buildingType = Random.Range(0,numBuildings);
		if(buildingType == 7){
			buildingType = 6;
		}
		buildingMaterial = new Material (Shader.Find ("Transparent/VertexLit"));
		filters = GetComponentsInChildren (typeof(MeshFilter));
		filters [0].gameObject.renderer.sharedMaterial = buildingMaterial;
		uva = (Vector2[])(((MeshFilter)filters [0]).mesh.uv);
		ChangeSprite (buildingType);//Specific to current test, must create generic form
		renderer.sharedMaterial.SetTexture ("_MainTex", GameSetUp.buildingAtlas);
	}
	
	void Update () {
		//Add Building Animation for character interaction
	}
	
	private void OnTriggerEnter( Collider other ){//detects if the Character has touched the Building
		if(!touched && Character.up){
			Character.flashing = Character.up;
			touched = true;
			if(buildingType == 6){
				ChangeSprite(buildingType+1);
				GUIHealthBar.healthValue = 100f;
			}
		}
	}
	
	private void OnTriggerStay(Collider other){
		if(!touched && Character.up){
			Character.flashing = Character.up;
			touched = true;
			if(buildingType == 6){
				ChangeSprite(buildingType+1);
			}
		}
	}
	
	public void ChangeSprite (int j)
	{
		Vector2[] uvb;
		uvb = new Vector2[uva.Length];
		for (int k=0; k < uva.Length; k++) {
			uvb [k] = new Vector2 ((uva [k].x * GameSetUp.buildingRects[j].width) + GameSetUp.buildingRects[j].x, 
								   (uva [k].y * GameSetUp.buildingRects[j].height) + GameSetUp.buildingRects[j].y);
		}
		((MeshFilter)filters [0]).mesh.uv = uvb;
	}
}
