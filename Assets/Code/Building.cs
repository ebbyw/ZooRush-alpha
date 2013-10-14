using UnityEngine;
using System.Collections;

public class Building : GameElement {
	
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
	 */ 
	
	public void Create(){
		touched = false;
		atlasRects = GameSetUp.buildingRects;
		if(elementType == 7){
			elementType = 6;
		}
		if(elementType == 6){
			transform.localScale = new Vector3(2f,2f,1f);
			transform.localPosition += new Vector3(0f,1.91f-1.5f,0f);
		}
		MaterialSetUp();
		ChangeSprite(elementType);
		renderer.sharedMaterial.SetTexture ("_MainTex", GameSetUp.buildingAtlas);
	}
	
	private void OnTriggerEnter( Collider other ){//detects if the Character has touched the Building
		if(!touched && Character.up){
			Character.flashing = Character.up;
			touched = true;
			if(elementType == 6){
				ChangeSprite(elementType+1);
				GUIHealthBar.healthValue = 100f;
			}
		}
	}
	
	private void OnTriggerStay(Collider other){
		if(!touched && Character.up){
			Character.flashing = Character.up;
			touched = true;
			if(elementType == 6){
				ChangeSprite(elementType+1);
			}
		}
	}
}
