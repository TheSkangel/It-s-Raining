using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour {

    public GameObject platformPrefab;

    public float spawnRate = 10f;
    float spawnTimer;

    public float xSpawnOffset = -10;

    void Start() {

        spawnTimer = Time.time + spawnRate;

    }

	void Update () {
		
        if(Time.time > spawnTimer) {

            SpawnPlatform();

            spawnTimer = Time.time + spawnRate;

        }

	}

    void SpawnPlatform() {

        float offSet = Random.Range(-xSpawnOffset, xSpawnOffset);

        GameObject newPlatform = Instantiate(platformPrefab, transform.position + new Vector3(offSet, 0, 0), Quaternion.identity) as GameObject;

    }

    void ResetValues() {

        spawnTimer = Time.time + spawnRate;

    }

}
