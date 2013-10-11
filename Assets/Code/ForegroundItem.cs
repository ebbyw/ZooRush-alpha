using UnityEngine;
using System.Collections;

public class ForegroundItem : MonoBehaviour {
	
	public int fgType;
	public int numFgItems;
	public Texture2D[] textures;
	private Texture2D spriteSheet;
	private Rect[] uvCoord;
	private Vector2[] uva;
	private Component[] filters;
	private Material fgMaterial;

	void Start () {
		numFgItems = textures.Length;
// <TEXTURE STUFF>
		spriteSheet = new Texture2D (0, 0);
		uvCoord = spriteSheet.PackTextures (textures, 0);
		fgMaterial = new Material (Shader.Find ("Transparent/VertexLit"));
		filters = GetComponentsInChildren (typeof(MeshFilter));
		filters [0].gameObject.renderer.sharedMaterial = fgMaterial;
		uva = (Vector2[])(((MeshFilter)filters [0]).mesh.uv);
		fgType = Random.Range(0,numFgItems);
		ChangeSprite (fgType);
		renderer.sharedMaterial.SetTexture ("_MainTex", spriteSheet);
// </TEXTURE STUFF>
	}

	void Update () {

	}
	
	public void ChangeSprite (int j)
	{
		if(j>1){
			transform.localScale += new Vector3 (30f,17.5f,0f);
			transform.localPosition += new Vector3(0f,0f,-1f);
		}
		else{
			transform.localScale = new Vector3 (10f,2.5f,1f);
		}
		Vector2[] uvb;
		uvb = new Vector2[uva.Length];
		for (int k=0; k < uva.Length; k++) {
			uvb [k] = new Vector2 ((uva [k].x * uvCoord [j].width) + uvCoord [j].x, 
								   (uva [k].y * uvCoord [j].height) + uvCoord [j].y);
		}
		((MeshFilter)filters [0]).mesh.uv = uvb;
	}
}
