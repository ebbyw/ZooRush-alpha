using UnityEngine;
using System.Collections;

public class CloudGenerator : MonoBehaviour {
	
	private Material[] cloudmeshes = new Material[4];
	public GameObject[] clouds;
	private Vector3 location;
	private bool animating;
	
	void Start () {
		animating = false;
		location = Camera.main.transform.localPosition;
		cloudmeshes[0] = Resources.Load("Materials/StageElements/Cloud1a",typeof(Material)) as Material;
		cloudmeshes[1] = Resources.Load("Materials/StageElements/Cloud1b",typeof(Material)) as Material;
		cloudmeshes[2] = Resources.Load("Materials/StageElements/Cloud2a",typeof(Material)) as Material;
		cloudmeshes[3] = Resources.Load("Materials/StageElements/Cloud2b",typeof(Material)) as Material;
		clouds = new GameObject[Random.Range(3,7)];
		for(int i = 0; i < clouds.Length; i++){
			clouds[i] = GameObject.CreatePrimitive(PrimitiveType.Quad);
			clouds[i].name = "Cloud";
			float xlocation = location.x + Random.Range(-3,4);
			float ylocation = 1f + (Random.Range(90,126)/100f); //Number between 1.9 and 2.25
			float zlocation = 7f;
			location.x = xlocation;
			location.y = ylocation;
			location.z = zlocation;
			clouds[i].transform.localPosition = location;
			clouds[i].renderer.material = cloudmeshes[Random.Range(0,4)];
		}
	}
	
	// Update is called once per frame
	void Update () {
		foreach(GameObject cloud in clouds){
			if(cloud.transform.localPosition.x < Camera.main.transform.localPosition.x - 3.7f){
				Vector3 newPos = cloud.transform.localPosition;
				newPos.x += Random.Range(8,14);
				newPos.y = 1f + (Random.Range(90,126)/100f);
				newPos.z = 7f;
				cloud.transform.localPosition = newPos;
			}
//			if(!animating){
//				StartCoroutine(ChangeMaterial(cloud));
//			}
			
		}
	}
	
	private IEnumerator ChangeMaterial(GameObject cloud){
		animating = true;
		switch(cloud.renderer.material.name){
			case "Cloud1a (Instance)":
				cloud.renderer.material = cloudmeshes[1];
				break;
			case "Cloud1b (Instance)":
				cloud.renderer.material = cloudmeshes[0];
				break;
			case "Cloud2a (Instance)":
				cloud.renderer.material = cloudmeshes[3];
				break;
			case "Cloud2b (Instance)":
				cloud.renderer.material = cloudmeshes[2];
				break;
			default:
				break;
			}
		yield return new WaitForSeconds(0.1f);
		animating = false;
	}
}
