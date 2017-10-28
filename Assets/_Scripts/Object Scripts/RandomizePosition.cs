using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizePosition : MonoBehaviour {

	public GameObject Parent;
	public float height;

	// Use this for initialization
	void Start () {
		transform.position = Parent.transform.position + new Vector3 (Random.Range (150.0f, 350.0f), height, Random.Range (150.0f, 350.0f));
		transform.Rotate(Vector3.up, Random.Range(0, 360.0f));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
