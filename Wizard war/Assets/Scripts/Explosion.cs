﻿using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	public float lifetime = 2f;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, lifetime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
