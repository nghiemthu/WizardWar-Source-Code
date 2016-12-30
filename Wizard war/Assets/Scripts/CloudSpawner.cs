using UnityEngine;
using System.Collections;

public class CloudSpawner : MonoBehaviour {

	public Cloud cloud;

	void Start(){
		StartCoroutine (SpawnCloud());
	}

	IEnumerator SpawnCloud(){
		Instantiate (cloud, new Vector3 (-20, Random.Range(5, 12), Random.Range(-18, 18)), Quaternion.identity);
		yield return new WaitForSeconds (10f);
		StartCoroutine (SpawnCloud());
	}
}
