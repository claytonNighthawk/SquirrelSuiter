using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOBehavior : MonoBehaviour {

	public float speed;

	private Transform transform;
	private int randomPos;
	private float randomRate;
	private float randomBehavior;
	private AudioSource sound;
	//private Vector3 position;

	// Use this for initialization
	void Start () {
		transform = GetComponent <Transform> ();
		sound = GetComponent<AudioSource> ();
		randomPos = Random.Range (0, 6);
		randomRate = Random.Range (50, 100) / 100.0f;
		speed *= randomRate;
		sound.pitch = randomRate + 0.25f;
		randomBehavior = Random.Range (10, 1000) / 1000.0f;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position = new Vector3 (250.0f + 100.0f * Mathf.Cos (randomPos + (float)Time.time * speed * randomBehavior), 45.0f + 20.0f * Mathf.Sin (randomPos + (float)Time.time * speed), transform.position.z);
		transform.Rotate (new Vector3(0, 1.0f, 0), randomRate * 1000 * Time.deltaTime);
		transform.position -= (Vector3.down / 5);

	}


}
