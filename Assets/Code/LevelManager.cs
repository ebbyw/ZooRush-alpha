using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	/** Essentials of Every Level:  
	 *  A Character Element
	 *  An Animal Element
	 *  Pain Indicator Bar
	 *  Buildings
	 *  Obstacles
	 *  Power Ups
	 *  Ground
	 *  Sky
	 *  Foreground
	 */ 
	
	GameObject PlayerCharacter;
	GameObject LevelAnimal;
	GameObject PainBar;
	GameObject[] Buildings;
	GameObject[] Foreground;
	/** The Level Manager handles the following tasks:
	 *   - The time related information for in level events such as:
	 *        * The wait time when the animal "escapes"
	 *        * The time elapsed since the "chase"/level has started
	 *   - Player Score
	 *   - Transitions to the next scene
	 */ 
	
	
	
	void Awake(){
		PlayerCharacter = GameObject.FindGameObjectWithTag("character");
		LevelAnimal =GameObject.FindGameObjectWithTag("animal");
		PainBar = GameObject.FindGameObjectWithTag("pain");
		Buildings = GameObject.FindGameObjectsWithTag("building");
		Foreground = GameObject.FindGameObjectsWithTag("foreground");
		
	}
	
}
