using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obticles : MonoBehaviour {

    public GameObject[] ObsticalPrefabs;
    public int amountObsOnScreen = 13;

    private Transform playerTransform;
    private float spawnZ = 000.0f;
    private float ObsLength = 195.0f;
    private float SafeZone = 325.0f;
    private int lastPrefabIndex = 0;
    private int numObs;
    private List<GameObject> activeObs;
    private List<GameObject> activeResetObs;

    // Use this for initialization
    private void Start () {
        activeObs = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        //SpawnObs(0); // Starting Terrain;
        for (int i=0; i<amountObsOnScreen-1; i++) {
			SpawnObs();
			if (i < 8) {
				activeObs[i].SetActive(false);
			}
        }
    }
	
    private void Update () {
        if (playerTransform.position.z - SafeZone > (spawnZ - (amountObsOnScreen * ObsLength))) {
            SpawnObs();
            DeleteObs();
        }
	}

    private void SpawnObs(int prefabIndex = -1) {
        GameObject go;
        if (prefabIndex == -1) {
            go = Instantiate(ObsticalPrefabs[RandomPrefabIndex()]) as GameObject;
        } else {
            go = Instantiate(ObsticalPrefabs[prefabIndex]) as GameObject;
        }
        
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        numObs++;
        spawnZ += ObsLength;
        activeObs.Add(go);
    }

    private void DeleteObs() {
        Destroy(activeObs[0]);
        activeObs.RemoveAt(0);
        numObs--;
    }

    private int RandomPrefabIndex() {
        if (ObsticalPrefabs.Length <= 1) {
            return 0;
        }
        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex) {
            randomIndex = Random.Range(0, ObsticalPrefabs.Length);
        }

        lastPrefabIndex = randomIndex;
        return randomIndex;
    }

    public void ResetLocation() {
        spawnZ = 0.0f;
        for (int i=0; i< amountObsOnScreen-1; i++) {
			activeObs[i].transform.position = new Vector3(activeObs[i].transform.position.x, activeObs[i].transform.position.y, activeObs[i].transform.position.z-2970.0f);
            spawnZ += ObsLength;
        }
    }
}
