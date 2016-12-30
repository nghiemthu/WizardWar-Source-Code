using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {

	Rigidbody myRigidbody;

	void Start(){
		myRigidbody = GetComponent<Rigidbody> ();
		Destroy (gameObject, 50f);
	}

	void Update(){
		myRigidbody.MovePosition (myRigidbody.position + new Vector3(1f, 0 ,0) * Time.deltaTime);
	}
}
