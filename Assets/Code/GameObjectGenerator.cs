using UnityEngine;
using System.Collections;

public class GameObjectGenerator : MonoBehaviour {
	
	public GameObject element;
	public GameObject characterPointer;
	public string elementType;
	public int numOfObjects;
	public int objectsInField;
	public float destroyOffset;
	public int destroyCount;
	public Vector3 position;
	
	
	public void OnDestroy(){
		for(int count = 1; count <= numOfObjects; count++){
			GameObject temp = GameObject.FindGameObjectWithTag(elementType);
			Destroy (temp);
		}
	}
	
	public void CreateSceneElement(float offset){
		if(objectsInField <= numOfObjects){
			position.x += offset;
			GameObject Elementpointer = Instantiate(element,position,Quaternion.identity) as GameObject;
			Elementpointer.tag = elementType;
			objectsInField++;
		}
		else{
			DestroyAllOutOfRange();
		}
	}
	
	public void DestroyAllOutOfRange(){
		//looks for all objects with tag "element", checks if they are out of range:
		//(GameObject.FindWithTag("element").transform.localPosition.x + recycleOffset < Character.x);
		GameObject[] allObjects = GameObject.FindGameObjectsWithTag(elementType);
		destroyCount = 0;
		
		for(int i = 0; i < allObjects.Length; i++){
			if(allObjects[i].transform.localPosition.x + destroyOffset < Character.xPosition){
				Destroy(allObjects[i]);
				destroyCount++;
			}
		}
		objectsInField -= destroyCount;
	}
}
