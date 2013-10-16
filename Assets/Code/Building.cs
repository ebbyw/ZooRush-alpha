using UnityEngine;
using System.Collections;

public class Building : GameElement {
	public int BuildingType;
	/** Building Types:
	 *  Building types are organized in alphabetical order of the building 
	 *  texture file names in the Assets/Resources/Textures/Buildings/ directory
	 * 
	 * 	0 - Balloon Stand
	 *  1 - Cage 1 (Round Top Cage)
	 *  2 - Cage 2 (Swamp/Wetlands)
	 *  3 - Cage 3 (Lake)
	 *  4 - Cage 4 (Plains)
	 *  5 - Cage 5 (General Purpose)
	 *  6 - Doctor Office (Doors Closed)
	 *  7 - Doctor Office (Doors Open)
	 *  8 - Hot Dog Stand
	 */ 
	
	void Start( ){
		touched = false;
		elementType = BuildingType;
		if(elementType == 7){
			elementType = 6;
		}
		if(elementType == 6){
			transform.localScale = new Vector3(3f,3f,1f);
		}
		renderer.material = materials[0];
	}
	
	void Update(){
		Vector3 defaultPosition = transform.localPosition;
		if(elementType == 6){
			defaultPosition.y = 1.933f;
			defaultPosition.z = 5f;
		}
		else{
			defaultPosition.y = 1.7f;
			defaultPosition.z = 4f;
		}
		transform.localPosition = defaultPosition;	
	}
	
	private void OnTriggerEnter( Collider other ){//detects if the Character has touched the Building
		if(!touched && Character.up){
			Character.flashing = Character.up;
			touched = true;
			if(elementType == 6){
				renderer.material = materials[1];
				//Add Character Animation code somewhere here
				GUIHealthBar.healthValue = 100f;
			}
		}
	}
	
	private void OnTriggerStay(Collider other){
		if(!touched && Character.up){
			Character.flashing = Character.up;
			touched = true;
			if(elementType == 6){
				renderer.material = materials[1];
				//Add Character Animation code somewhere here
				GUIHealthBar.healthValue = 100f;
			}
		}
	}
}
