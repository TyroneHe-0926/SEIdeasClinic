using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidField : MonoBehaviour {
    public GameObject asteroidPrefab;
    [SerializeField] float spawnerWidth = 8f;
    [SerializeField] float minSize = 0.2f;
    [SerializeField] float maxSize = 2f;
    [SerializeField] float minVelocity = 3f;
    [SerializeField] float maxVelocity = 6f;
    [SerializeField] float spawnRate = 1f;

    private float speed;

    private void Start()
    {
        SimplePool.Preload(asteroidPrefab, 20);
        StartSpawning();
    }

    public void StartSpawning()
    {
        InvokeRepeating("Spawn", 1, spawnRate);
    }

    private void Spawn()
    {
        GameObject asteroid = SimplePool.Spawn(asteroidPrefab, Vector2.one, Quaternion.identity);
        asteroid.transform.localScale = Vector2.one * (0.2f + Random.Range(minSize, maxSize));
        speed = Random.Range(minVelocity, maxVelocity);
        float spawnXPosition = transform.position.x + Random.Range(-spawnerWidth/2, spawnerWidth/2);
        asteroid.transform.position = new Vector2(spawnXPosition, transform.position.y);
        asteroid.transform.GetComponent<Rigidbody2D>().velocity = transform.up * speed;

    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SmallBodies"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            SimplePool.Despawn(collision.gameObject);
        }
    }



}
