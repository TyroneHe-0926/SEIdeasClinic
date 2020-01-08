using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public Vector3 spawnPosition;
    public GameObject asteroidPrefab;
    

	void Start () {
        //spawnPosition = transform.position;
        SimplePool.Preload(asteroidPrefab, 6);
        NextSpawn();
	}
	
    void NextSpawn()
    {
        //Debug.Log("here");
        InvokeRepeating("SpawnAsteroid", 1, 0.6f);
    }

    void SpawnAsteroid()
    {
        GameObject asteroid = SimplePool.Spawn(asteroidPrefab, Vector2.one, Quaternion.identity);
        Asteroid asteroidScript = asteroid.GetComponent<Asteroid>();
        asteroidScript.Launch();

    }

	// Update is called once per frame
	void Update () {
        spawnPosition = transform.position;
    }
}
