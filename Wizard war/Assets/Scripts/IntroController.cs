using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (wait());
	}

	IEnumerator wait(){
		yield return new WaitForSeconds (3f);
		SceneManager.LoadScene ("MainScene");
	}
}
