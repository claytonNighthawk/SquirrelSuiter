﻿using System.Collections;
using UnityEngine;


public class SquirrelController : MonoBehaviour {

    public float terrainWidth = 250;
    public float directionalSpeed = 90.0f;
    public float forwardSpeed = 90;
    public float mobileSpeedAddition = 10.0f;
    public float tilt = 70.0f;

    public GameObject playerExplosion;
    public GameObject soundBarrier;
    public AudioClip[] crunchSounds;
    public AudioClip boost, boostPickup, barrelRollSound;
    
    private int ctrlDirection = 1;
    private bool rolling = false;
    private bool boosting = false;
    private float mobileSpeedBoost = 0.0f;
    private float lastscore;

    private PlayerManager playerManager;
    private Score score;

    private AudioSource engineSound;
    private AudioSource windSound;
    private AudioSource soundSource;

    private Vector3 pos;
    private Vector3 movement;
    private Quaternion calibrationQuaternion;
    private Rigidbody rb;
    private ParticleSystem acornParticles;
    private Vector3 soundBarStart, soundBarEnd;
    private Light engineLight;

    void Start() {
        AudioSource[] audioSources = GetComponentsInChildren<AudioSource>();
        engineSound = audioSources[0];
        windSound = audioSources[1];
        soundSource = audioSources[2];

        playerManager = GameObject.Find("Player Manager").GetComponent<PlayerManager>();
        score = GameObject.Find("ScoreCanvas").GetComponent<Score>();
        rb = GetComponent<Rigidbody>();
        acornParticles = GetComponent<ParticleSystem>();

        engineSound.Play();
        windSound.Play();
        boosting = false;
        lastscore = score.scoreNum;

        if (PlayerPrefs.HasKey("ctrlDirection")) {
            ctrlDirection = PlayerPrefs.GetInt("ctrlDirection");
        } 

        soundBarStart = new Vector3 (0, 0.025f, 0);
        soundBarEnd = new Vector3 (1000.0f, 0.025f, 1000.0f);
        soundBarrier.transform.localScale = soundBarStart;
        engineLight = soundBarrier.GetComponent<Light>();

    #if MOBILE_INPUT
        calibrationQuaternion.Set(PlayerPrefs.GetFloat("calibrationX"), PlayerPrefs.GetFloat("calibrationY"), PlayerPrefs.GetFloat("calibrationZ"), PlayerPrefs.GetFloat("calibrationW"));
        mobileSpeedBoost = mobileSpeedAddition;
    #endif
    }

    void FixedUpdate() {
        int direction;
        float moveHorizontal;
        float moveVertical;

        if (score.scoreNum > lastscore + score.scoreThreshold) {
            forwardSpeed += 5;
            directionalSpeed += 1;
            lastscore = score.scoreNum;
        }

    #if MOBILE_INPUT
        Vector3 acceleration = calibrationQuaternion * Input.acceleration;
        moveHorizontal = acceleration.x;
        moveVertical = ctrlDirection * -acceleration.y;
    #else
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = ctrlDirection * Input.GetAxis("Vertical");
    #endif

        pos.Set(transform.position.x, Mathf.Clamp(transform.position.y, 3f, 60f), transform.position.z);
        transform.position = pos;

        if (moveHorizontal != 0) {
            windSound.pitch = 1.0f + Mathf.Abs(moveHorizontal) / 2;
            direction = moveHorizontal > 0 ? 1 : -1;
            if (Input.GetButtonDown("Jump")) {
                StartBarrelRoll(direction);
            }
            if (!rolling) {
                rb.rotation = Quaternion.Euler(80 + direction * (moveHorizontal * -tilt * 0.25f) + (moveVertical * -tilt * 0.25f), (moveHorizontal * -tilt * 0.80f), (moveHorizontal * -tilt * 0.83f));
            }
        } else {
            windSound.pitch = 1.0f - (moveHorizontal) / 3;
            rb.rotation = Quaternion.Euler (80 + (moveVertical * -tilt * 0.25f), 0, 0);
        }
            
        if (!rolling && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.JoystickButton1))) {
            StartSpeedBoost();
        }

        movement.Set(moveHorizontal, moveVertical, forwardSpeed * Time.deltaTime);
        rb.velocity = (mobileSpeedBoost + directionalSpeed) * movement;

        if (Mathf.Abs(transform.position.x - 250) > terrainWidth) {
            Collision other = new Collision();
            OnCollisionEnter(other);
        }

    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("acorn")) {
            other.gameObject.SetActive(false);
            score.IncAcorns();

            acornParticles.Play(true);
            soundSource.dopplerLevel = 0.0f;
            soundSource.pitch = Random.Range(0.8f, 1.1f);

            int randomSound = Random.Range(0, 2);
            soundSource.PlayOneShot(crunchSounds[randomSound]);
        }

        if (other.gameObject.CompareTag("Boost")){
            other.gameObject.SetActive(false);

            score.boostsAvailable += 1;

            soundSource.dopplerLevel = 0.0f;
            soundSource.pitch = Random.Range (0.8f, 1.1f);
            soundSource.PlayOneShot(boostPickup);
        }
    }

    void OnCollisionEnter(Collision other) {
        Instantiate(playerExplosion, transform.position, transform.rotation);
        engineSound.Stop();
        windSound.Stop();
        playerManager.Die();
        gameObject.SetActive(false);
    }

    public void StartSpeedBoost() {
        if (score.boostsAvailable > 0 && !boosting) {
            boosting = true;
            StartCoroutine(SpeedBoost());
        }
    }

    IEnumerator SpeedBoost() {
        float boostStart = Time.time;
        score.boostsAvailable -= 1;
        soundSource.volume = 2.0f;
        soundSource.dopplerLevel = 0.0f;
        soundSource.pitch = Random.Range(0.8f, 1.1f);
        soundSource.PlayOneShot(boost);
        while (boosting) {
            if (Time.time - boostStart < 1.0f) {
                forwardSpeed += 15.0f * Mathf.Cos(Mathf.PI * (Time.time - boostStart));
                Vector3 ScaleFactor = Vector3.Lerp(soundBarStart, soundBarEnd, Time.time - boostStart);
                soundBarrier.transform.localScale = ScaleFactor;
                engineLight.intensity += Mathf.Cos(Mathf.PI * (Time.time - boostStart));
            } else {
                boosting = false;
                soundBarrier.transform.localScale = soundBarStart;
                engineLight.intensity = 0.0f;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    public void StartBarrelRoll(int dir) {
        rolling = true;
        StartCoroutine(BarrelRoll(dir));
    }

    IEnumerator BarrelRoll(object dirObj) {
        int dir = (int)dirObj;
        float coroutineStart = Time.time;
        float currentX = transform.position.x;
        soundSource.dopplerLevel = 0.0f;
        soundSource.pitch = Random.Range(0.8f, 1.1f);
        soundSource.PlayOneShot(barrelRollSound);
        while (rolling) {
            if (Time.time - coroutineStart < 0.5f) {
                float corkscrew = Mathf.Cos((Time.time - coroutineStart) * (1 * Mathf.PI));
                transform.position = new Vector3(currentX + dir * 80 * (Mathf.Sin((Time.time - coroutineStart) * 1 * Mathf.PI)), transform.position.y, transform.position.z);
                transform.RotateAround(transform.position, Vector3.forward, dir * -130.0f * corkscrew / (Mathf.PI));
            } else {
                rolling = false;
            }
            yield return new WaitForFixedUpdate();
        }
    }
}