using UnityEngine;
using System.Collections;

/** The GameElement class is a generic parent class used to manage and set up any 
 *  basic game element needed in this game and to avoid code repetion for indiviadual
 *  game element types.
 */ 
public class GameElement : MonoBehaviour {
	protected Character characterComponent;
	protected Animal animalComponent;
	protected PainIndicator painComponent;
	public Material[] materials;
	
	//Gameplay related variables
	protected int elementType; //See end of file to see how the element type system is organized
	protected int RowNum; // The row number in the level that the object is located
	protected bool touched; //if the element has been touched by either the character or the user depending on the object
	
	//Animation related variables
	public bool animateable; // true if the element has animation frames, false otherwise
	protected bool animating; //if the object is currently switching from one sprite to another
	protected float fps; // the frames per second for the element's animation
	protected int currentFrame; // the current animation frame the element is on
	protected bool forward; // whether we are in the forward or reverse segment of the animation sequence
	
	public int getType(){
		return elementType;
	}
	
	public void ChangeElementType(int type){
		elementType = type;
	}
	
	public void ChangeFPS(int rate){
		fps = rate;
	}
	
	public void ChangeObjectPosition(Vector3 position){
		transform.localPosition = position;
	}
	
	public void ChangeObjectXPosition(float xPosition){
		Vector3 currentPos = transform.localPosition;
		currentPos.x = xPosition;
		transform.localPosition = currentPos;
	}
	public void ShiftObjectXPosition(float xdelta){ //shifts the object by xdelta in the x direction
		Vector3 currentPos = transform.localPosition;
		currentPos.x += xdelta;
		transform.localPosition = currentPos;
	}
	
	public void ChangeObjectYPosition(float yPosition){
		Vector3 currentPos = transform.localPosition;
		currentPos.y = yPosition;
		transform.localPosition = currentPos;
	}
	public void ShiftObjectYPosition(float ydelta){ //shifts the object by ydelta in the y direction
		Vector3 currentPos = transform.localPosition;
		currentPos.y += ydelta;
		transform.localPosition = currentPos;
	}
	
	public void ChangeObjectZPosition(float zPosition){
		Vector3 currentPos = transform.localPosition;
		currentPos.z = zPosition;
		transform.localPosition = currentPos;
	}
	public void ShiftObjectZPosition(float zdelta){ //shifts the object by ydelta in the y direction
		Vector3 currentPos = transform.localPosition;
		currentPos.z += zdelta;
		transform.localPosition = currentPos;
	}
	
	public void ChangeObjectSize(Vector3 size){
		transform.localScale = size;
	}
	
	protected void ChangeTexture(Texture2D texture){
		renderer.sharedMaterial.SetTexture ("_MainTex", texture);
	}
	
	public IEnumerator ChangeMaterial (float time)
	{
		int j = 1;
		animating = true;
		renderer.material = materials[j];
		yield return new WaitForSeconds(time);
		j--;
		renderer.material = materials[j];
		yield return new WaitForSeconds(time);
		animating = false;
	}
	
	public void ChangeMaterial (int index)
	{
		renderer.material = materials[index];
	}
	
	public IEnumerator ChangeMaterial (int index, float time)
	{
		animating = true;
		renderer.material = materials[index];
		yield return new WaitForSeconds(time);
		animating = false;
	}
	
	
	public void ResetAlpha ()
	{
		Color originalColour = renderer.material.color;
		renderer.sharedMaterial.color = new Color (originalColour.r, originalColour.g, originalColour.b, 1f);
	}
	
	public IEnumerator itemFlash ()
	{
		float newAlpha = 0.5f;
		float waitTime = 0.07f;
		Color originalColour = renderer.material.color;
		renderer.material.color = new Color (originalColour.r, originalColour.g, originalColour.b, newAlpha);
		yield return new WaitForSeconds(waitTime);
		newAlpha = 1f;
		renderer.material.color = new Color (originalColour.r, originalColour.g, originalColour.b, newAlpha);
		yield return new WaitForSeconds(waitTime);
		newAlpha = 0f;
		renderer.material.color = new Color (originalColour.r, originalColour.g, originalColour.b, newAlpha);
		yield return new WaitForSeconds(waitTime);
		newAlpha = 0.5f;
		renderer.material.color = new Color (originalColour.r, originalColour.g, originalColour.b, newAlpha);
		yield return new WaitForSeconds(waitTime);
		newAlpha = 1f;
		renderer.material.color = new Color (originalColour.r, originalColour.g, originalColour.b, newAlpha);
		yield return new WaitForSeconds(waitTime);
		newAlpha = 0.5f;
		renderer.material.color = new Color (originalColour.r, originalColour.g, originalColour.b, newAlpha);
		yield return new WaitForSeconds(waitTime);
		newAlpha = 0f;
		renderer.material.color = new Color (originalColour.r, originalColour.g, originalColour.b, newAlpha);
		yield return new WaitForSeconds(waitTime);
		renderer.enabled = false;
	}
	
	public void animate(float time){
		if(animateable){
			if(!animating){
				StartCoroutine(ChangeMaterial(time));
			}
		}
	}
}
