using System.Collections;
using UnityEngine;


public class SquirrelController : MonoBehaviour {

	public float terrainWidth;
	public float horizontalSpeed = 10.0f;
    public float mobileSpeedAddition = 10.0f;
	public float forwardSpeed = 90;
	public float tilt;
	public int acornCount = 0;
    public int boostsAvailable = 0;
    private int ctrlDirection = 1;
    private int scoreThreshold = 2000;

    public PlayerManager playerManager;
	public ScoreText scoreScript;
	public GameObject playerExplosion;
	public AudioSource explosionAudio;
	public AudioSource engineSound;
	public AudioSource windSound;
	public AudioClip crunch1, crunch2, crunch3, boost, boostPickup, barrelRollSound;
	public AudioSource soundSource;
    public GameObject soundBarrier;

    private bool rolling = false;
    private bool boosting = false;
    private float mobileSpeedBoost = 0.0f;
	private float coroutineStart;
	private float currentX;
	private float boostStart;
	private float lastscore;
    private Vector3 pos;
    private Vector3 movement;

    private Quaternion calibrationQuaternion;
    private Rigidbody rb;
	private ParticleSystem acornParticles;
	private Vector3 soundBarStart, soundBarEnd;
	private Light engineLight;


	void Start() {
		lastscore = scoreScript.scoreNum;
		rb = GetComponent<Rigidbody>();
		acornParticles = GetComponent<ParticleSystem>();
		engineSound.Play();
		windSound.Play();
		Time.timeScale = 1;
		boosting = false;
        
        if (PlayerPrefs.HasKey("ctrlDirection")) {
            ctrlDirection = PlayerPrefs.GetInt("ctrlDirection");
        } else {
            ctrlDirection = 1;
        }

		soundBarStart = new Vector3 (0, 0.025f, 0);
		soundBarEnd = new Vector3 (1000.0f, 0.025f, 1000.0f);
		soundBarrier.transform.localScale = soundBarStart;
		engineLight = soundBarrier.GetComponent<Light> ();
    #if MOBILE_INPUT
        calibrationQuaternion.Set(PlayerPrefs.GetFloat("calibrationX"), PlayerPrefs.GetFloat("calibrationY"), PlayerPrefs.GetFloat("calibrationZ"), PlayerPrefs.GetFloat("calibrationW"));
        //CalibrateAccelerometer();
        mobileSpeedBoost = mobileSpeedAddition;
    #endif
    }

    void FixedUpdate() {
        int direction;
		if (scoreScript.scoreNum > lastscore + scoreThreshold) {
		    forwardSpeed += 5;
            horizontalSpeed += 1;
            lastscore = scoreScript.scoreNum;
		}

        float moveHorizontal;
        float moveVertical;

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
                //rolling = true;
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
		rb.velocity = (mobileSpeedBoost + horizontalSpeed) * movement;

		if (Mathf.Abs(transform.position.x - 250) > terrainWidth) {
			Collision other = new Collision();
			OnCollisionEnter(other);
		}

	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("acorn")) {
			other.gameObject.SetActive(false);
			scoreScript.IncAcorns();

			acornParticles.Play(true);
			acornParticles.Play(false);

			soundSource.dopplerLevel = 0.0f;
			soundSource.pitch = Random.Range(0.8f, 1.1f);

			int randomSound = Random.Range(0, 2);
			if (randomSound == 0) {
				soundSource.PlayOneShot(crunch1);
			} else if (randomSound == 1) {
				soundSource.PlayOneShot(crunch2);
			} else {
				soundSource.PlayOneShot(crunch3);
			}
		}
		if (other.gameObject.CompareTag("Boost")){
			other.gameObject.SetActive(false);

			boostsAvailable += 1;
			scoreScript.IncBoosts();

			soundSource.dopplerLevel = 0.0f;
			soundSource.pitch = Random.Range (0.8f, 1.1f);
			soundSource.PlayOneShot(boostPickup);
		}
	}

	void OnCollisionEnter(Collision other) {
		Instantiate(playerExplosion, transform.position, transform.rotation);
		engineSound.Stop();
		windSound.Stop();
		explosionAudio.Play();
		playerManager.Die();
		gameObject.SetActive(false);
	}

    public void StartSpeedBoost() {
        if (boostsAvailable > 0 && !boosting) {
            boosting = true;
            StartCoroutine("SpeedBoost");
        }
    }

	IEnumerator SpeedBoost() {
        boostStart = Time.time;
        boostsAvailable -= 1;
        soundSource.volume = 2.0f;
        soundSource.dopplerLevel = 0.0f;
        soundSource.pitch = Random.Range(0.8f, 1.1f);
        soundSource.PlayOneShot(boost);
        scoreScript.DecBoosts();
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
        StartCoroutine("BarrelRoll", dir);
    }

	IEnumerator BarrelRoll(object dirObj) {
        int dir = (int)dirObj;
        coroutineStart = Time.time;
        currentX = transform.position.x;
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