using UnityEngine;
using System.Collections;

public class BurnFire : MonoBehaviour {

	public void SetLifeTime(float time){
		Destroy (gameObject, time);
	}
}
