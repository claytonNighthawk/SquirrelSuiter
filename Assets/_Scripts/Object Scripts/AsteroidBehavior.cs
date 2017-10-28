using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour {


    public GameObject squirrel;
    public ParticleSystem smokeLanding;
    public ParticleSystem fireTrail;
    private Transform squirrelTransform;
    public AudioSource falling;
    public AudioSource crash;

    private Vector3 destination;
    private Vector3 startPos;
    private float startTime;
    private ParticleSystem particles;
    private float randoX, randoY, randoZ;
    private bool rotating;
    private float squirrelSpeed;

    void Start() {
        squirrelTransform = squirrel.GetComponent<Transform>();
        squirrelSpeed = squirrel.GetComponent<SquirrelController>().forwardSpeed;

        startTime = Time.time;
        randoX = Random.Range(0, 15.0f);
        randoY = Random.Range(0, 15.0f);
        randoZ = Random.Range(0, 15.0f);
        rotating = true;
        if (squirrelTransform.position.z < 200) {
            startPos = new Vector3(0, 5000.0f, 0);
            destination = startPos;
        } else {
            startPos = new Vector3(Random.Range(-50.0f, 550.0f), 350.0f, squirrelTransform.position.z + 700.0f + squirrelSpeed * 1.75f);
            destination = squirrelTransform.position + new Vector3(randoX - randoY, -(squirrelTransform.position.y + 15), squirrelSpeed * 3.0f);
        }
        falling.Play();
    }

    void FixedUpdate() {
        if (rotating) {
            transform.Rotate(Time.time / randoX, Time.time / randoY, Time.time / randoZ);
            transform.position = Vector3.Lerp(startPos, destination, (Time.time - startTime) / 1.7f);
        }
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag != "Obstacle") {
            fireTrail.Stop();
            smokeLanding.Play();
            rotating = false;
            if (falling.isPlaying) {
                falling.Stop();
            }
            crash.Play();
        }
    }
}
