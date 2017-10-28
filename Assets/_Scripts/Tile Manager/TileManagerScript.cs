using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManagerScript : MonoBehaviour {

    public GameObject[] tilePrefabs;
    public GameObject ObsManag;
    private Transform mycamera;
    private Transform playerTransform;
    private float spawnZ = 0.0f;
    private float tileLength = 495.0f;
    private float SafeZone = 525.0f;
    private int amountTilesOnScreen = 6;
    private int lastPrefabIndex = 0;

    private List<GameObject> activeTiles;
    private List<GameObject> activeResetTiles;

    private void Start () {
        activeTiles = new List<GameObject>();
        mycamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        SpawnTile(0); // Starting Terrain;
        for (int i=0; i < amountTilesOnScreen - 1; i++) {
            SpawnTile();
        }
    }
	
    private void Update () {
        if (playerTransform.position.z >= 3000.0f) {
            ResetLocation();
            if (ObsManag != null) {
                ObsManag.GetComponent<Obticles>().ResetLocation();
            }
        }
        if (playerTransform.position.z - SafeZone > (spawnZ - (amountTilesOnScreen * tileLength))) {
            SpawnTile();
            DeleteTile();
        }
	}

    private void SpawnTile(int prefabIndex = -1) {
        GameObject go;
        if (prefabIndex == -1) {
            go = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
        } else {
            go = Instantiate(tilePrefabs[prefabIndex]) as GameObject;
        }
        
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        
        spawnZ += tileLength;
        activeTiles.Add(go);
        
    }

    private void DeleteTile() {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private int RandomPrefabIndex() {
        if(tilePrefabs.Length <= 1) {
            return 0;
        }
        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex) {
            randomIndex = Random.Range(0, tilePrefabs.Length);
        }

        lastPrefabIndex = randomIndex;
        return randomIndex;
    }

    private void ResetLocation() {
        spawnZ = 0.0f;
        for( int i=1; i< amountTilesOnScreen; i++) {
            activeTiles[i].transform.position = new Vector3(0, 0, spawnZ); ;
            spawnZ += tileLength;
        }
        playerTransform.transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z - 2970.0f);
        mycamera.transform.position = new Vector3(mycamera.position.x, mycamera.position.y, mycamera.position.z - 2970.0f);
    }
}
