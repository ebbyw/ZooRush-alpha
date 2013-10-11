using UnityEngine;
using System;
using System.Collections;

	/** Sets up all necessary game resources at the start of the application
	 *  - Sets up all texture atlases and rect coordinates for all textues
	 * 	  used in the game.
	 * 
	 * 	- Sets up static values that will be used in every level
	 * 
	 * 
	 */
	public class GameSetUp : MonoBehaviour
	{
		static public float gridSize = 65f;
		
		static String textureDir = "Textures/";
		static String[] characterDirs = {"Characters/BoyZoo"};
		static String[] animalDirs = {"Animals/Crocodile","Animals/Tortoise"};
		static String obstacleDir = "Infections";
		static String powerUpDir = "PowerUps";
		static String buildingDir = "Buildings";
		
		static public Texture2D[] characterAtlases;
		static public Texture2D[] animalAtlases;
		static public Texture2D obstacleAtlas;
		static public Texture2D powerUpAtlas;
		static public Texture2D buildingAtlas;
		
		static public Rect[][] characterRects;
		static public Rect[][] animalRects;
		static public Rect[] obstacleRects;
		static public Rect[] powerUpRects;
		static public Rect[] buildingRects;
		
		//Sets up the index values and names we will use in order to access characters easily
		//enum Characters {cBBoy, cBGirl, cHGirl, cDoctorGirl, MAX_CHARACTERS};
		//enum Animals {aCrocodile, aFlamingo, aGorilla, aRhino, aTortoise, MAX_ANIMALS};
		//enum Obstacles {oGreenInfection, oRedInfection, oYelowInfection, MAX_OBSTACLES};
		//enum PowerUps {pOxygenTank, pPillBottle, pWaterBottle, MAX_POWERUPS};
		//enum Buildings {bDoctorOffice, bCage, bConcessionStand, MAX_BUILDINGS };
		
		
		void Awake ()
		{
//Initialize the texture atlas arrays with the size of the number of members they will contain
			initializeTexture2DArray(ref characterAtlases, characterDirs.Length);
			initializeTexture2DArray(ref animalAtlases, animalDirs.Length);
			obstacleAtlas = new Texture2D(0,0);
			powerUpAtlas = new Texture2D(0,0);
			buildingAtlas = new Texture2D(0,0);
			
//Initialize the size of the Rect[][] Arrays
			characterRects = new Rect[characterDirs.Length][];
			animalRects = new Rect[animalDirs.Length][];
			
//Start packing textures into atlas(es)
			//for characters:
			for(int i = 0 ; i < characterDirs.Length ; i++){
				TextureSetUp(textureDir+characterDirs[i],ref characterAtlases[i], ref characterRects[i]);
			}
			//for animals
			for(int i = 0; i < animalDirs.Length ; i++){
				TextureSetUp(textureDir+animalDirs[i],ref animalAtlases[i], ref animalRects[i]);
			}
			//for obstacles
			TextureSetUp(textureDir+obstacleDir,ref obstacleAtlas, ref obstacleRects);
			//for powerups
			TextureSetUp(textureDir+powerUpDir, ref powerUpAtlas, ref powerUpRects);
			//for buildings
			TextureSetUp(textureDir+buildingDir, ref buildingAtlas, ref buildingRects);
		}
		
		private void initializeTexture2DArray(ref Texture2D[] array, int size){
			array = new Texture2D[size];
			for(int i = 0; i < array.Length; i++){
				array[i] = new Texture2D (0,0);
			}
		}
		
		private void TextureSetUp(String directory, ref Texture2D spriteSheet, ref Rect[] uvCoord){
//Step 1: Load all textures for this element into a Texture2D array
			UnityEngine.Object[] obj = Resources.LoadAll (directory, typeof(Texture2D));
			Texture2D[] textures = new Texture2D[obj.Length];
			for (int i = 0; i < textures.Length; i++) {
				textures [i] = (Texture2D)obj [i];
			}
//Step 2: Pack all the textures into the spriteSheet Atlas and store the Rects that 
//        point to the coordinates of each image
			uvCoord = spriteSheet.PackTextures (textures, 2);
		}
	}

