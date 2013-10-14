using UnityEngine;
using System.Collections;

/** The GameElement class is a generic parent class used to manage and set up any 
 *  basic game element needed in this game and to avoid code repetion for indiviadual
 *  game element types.
 */ 
public class GameElement : MonoBehaviour {
	
	//Texture related variables
	protected Material material; //The game element's mesh material
	protected Component[] filters; // used to access the uv coordinates of the texture atlas
	protected Vector2[] uva; // stores the uv coordinates of the entire texture itself
	protected Rect[] atlasRects;
	
	//Gameplay related variables
	protected int elementType; //See end of file to see how the element type system is organized
	protected int RowNum; // The row number in the level that the object is located
	protected bool touched; //if the element has been touched by either the character or the user depending on the object
	
	//Animation related variables
	public bool animateable; // true if the element has animation frames, false otherwise
	private bool animating; //if the object is currently switching from one sprite to another
	private float fps; // the frames per second for the element's animation
	protected int currentFrame; // the current animation frame the element is on
	protected bool forward; // whether we are in the forward or reverse segment of the animation sequence
	
	public int getType(){
		return elementType;
	}
	
	public void SetRects(Rect[] rects){
		atlasRects = rects;
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
	
	protected void MaterialSetUp(){
		material = new Material (Shader.Find ("Transparent/VertexLit"));
		filters = GetComponentsInChildren (typeof(MeshFilter));
		filters [0].gameObject.renderer.sharedMaterial = material;
		uva = (Vector2[])(((MeshFilter)filters [0]).mesh.uv);
	}
	
	protected void animate(int startFrame, int endFrame){
		if (!animating) {
				StartCoroutine (ChangeSprite (currentFrame, fps));
				if (currentFrame == endFrame && forward) {
					currentFrame--;
					forward = false;
				} else {
					if (currentFrame == startFrame && !forward) {
						currentFrame++;
						forward = true;
					} else {
						if (forward) {
							currentFrame++;
						}
						if (!forward) {
							currentFrame--;
						}
					}
				}
			}
	}
	
	protected IEnumerator ChangeSprite (int j, float time)
	{
		animating = true;
		Vector2[] uvb;
		uvb = new Vector2[uva.Length];
		for (int k=0; k < uva.Length; k++) {
			uvb [k] = new Vector2 ((uva [k].x * atlasRects [j].width) + atlasRects [j].x, (uva [k].y * atlasRects[j].height) + atlasRects [j].y);
		}
		yield return new WaitForSeconds(time);
		((MeshFilter)filters [0]).mesh.uv = uvb;
		animating = false;
	}
	
	protected void ChangeSprite (int j)
	{
		Vector2[] uvb;
		uvb = new Vector2[uva.Length];
		for (int k=0; k < uva.Length; k++) {
			uvb [k] = new Vector2 ((uva [k].x * atlasRects [j].width) + atlasRects [j].x, 
								   (uva [k].y * atlasRects [j].height) + atlasRects [j].y);
		}
		((MeshFilter)filters [0]).mesh.uv = uvb;
	}
}
