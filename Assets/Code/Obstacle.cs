using UnityEngine;
using System.Collections;

public class Obstacle : GameElement
{
	public int RowNumber;
	public int ObstacleType;
	private Vector3 defaultScale = new Vector3(0.5f,0.5f,1f);
	
	void Start(){
		animating = false;
		renderer.material = materials[0];
		elementType = ObstacleType;
		transform.localScale = defaultScale;
		RowNum = RowNumber;
		fps = 0.25f;
	}
	
	void Update ()
	{
		animate(fps);
		Vector3 defaultPosition = transform.localPosition;
		defaultPosition.y = -3f + RowNumber;
		defaultPosition.z = -0.5f + RowNumber;
		transform.localPosition = defaultPosition;
	}
		
	void OnTriggerEnter (Collider other)
	{ //detects if the Character has touched the obstacle
		GUIHealthBar.obstacleType = elementType;
		StartCoroutine (itemFlash ());
		Character.flashing = true;
		GUIHealthBar.subtractFromHealthBar = true;
	}
	
	
}