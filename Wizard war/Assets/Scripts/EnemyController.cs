using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public bool playerDeath;

	GunController gunController;

	void Start () {
		gunController = GetComponent<GunController> ();
		StartCoroutine (AutoShoot());
	}

	IEnumerator AutoShoot(){
		if (!playerDeath){
			yield return new WaitForSeconds (1.5f);
			gunController.Shoot ();
			StartCoroutine (AutoShoot());
		}

	}

}
