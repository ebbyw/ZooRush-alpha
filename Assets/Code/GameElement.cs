using UnityEngine;
using System.Collections;

/** The GameElement class is a generic parent class used to manage and set up any 
 *  basic game element needed in this game and to avoid code repetion for indiviadual
 *  game element types.
 */ 
public class GameElement : MonoBehaviour {
	
	//Texture related variables
	private Material material; //The game element's mesh material
	private Component[] filters; // used to access the uv coordinates of the texture atlas
	private Vector2[] uva; // stores the uv coordinates of the entire texture itself
	private Rect[][] atlasRects;
	
	//Gameplay related variables
	private int elementType; //See end of file to see how the element type system is organized
	private int RowNum; // The row number in the level that the object is located
	private bool animating; //if the object is currently switching from one sprite to another
	private float fps; // the frames per second for the element's animation
	
	private IEnumerator ChangeSprite (int j, float time)
	{
		animating = true;
		Vector2[] uvb;
		uvb = new Vector2[uva.Length];
		for (int k=0; k < uva.Length; k++) {
			uvb [k] = new Vector2 ((uva [k].x * atlasRects[elementType] [j].width) + atlasRects[elementType] [j].x, (uva [k].y * atlasRects[elementType] [j].height) + atlasRects[elementType] [j].y);
		}
		yield return new WaitForSeconds(time);
		((MeshFilter)filters [0]).mesh.uv = uvb;
		animating = false;
	}
	
	private void ChangeSprite (int j)
	{
		Vector2[] uvb;
		uvb = new Vector2[uva.Length];
		for (int k=0; k < uva.Length; k++) {
			uvb [k] = new Vector2 ((uva [k].x * atlasRects[elementType] [j].width) + atlasRects[elementType] [j].x, 
								   (uva [k].y * atlasRects[elementType] [j].height) + atlasRects[elementType] [j].y);
		}
		((MeshFilter)filters [0]).mesh.uv = uvb;
	}
}
