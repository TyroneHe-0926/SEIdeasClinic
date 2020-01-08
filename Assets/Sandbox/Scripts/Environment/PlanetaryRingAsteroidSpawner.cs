using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Meant to be placed on a giant gas planet,
/// this script will create rings of orbitting asteroids.
/// </summary>
    
public class PlanetaryRingAsteroidSpawner : MonoBehaviour
{
    [SerializeField] float SpawnAngle;
    [SerializeField] bool SpawnAsteroidsAtStart;
    [SerializeField] float SpawnAngleUpperBoundAtStart;
    [SerializeField] bool SpawnAsteroidsDuringUpdate;
    [SerializeField] List<PlanetaryRing> PlanetaryRings;
    [System.Serializable]
    public struct PlanetaryRing
    {
        public GameObject AsteroidPrefab;
        public int asteroidCount;
        public float frequency;
        public float minDistance;
        public float maxDistance;
        public float sizeScaleMedian;
        public float sizeScaleVariance;
        public float speedMedian;
        public float speedVariance;
    }
    
    List<GameObject> asteroidTypes = new List<GameObject>();
    List<int> poolCounts = new List<int>();
    float angleLBound;
    float angleUBound;

    private void Start()
    {
        angleLBound = SpawnAngle* Mathf.Deg2Rad;
        angleUBound = SpawnAngleUpperBoundAtStart* Mathf.Deg2Rad;

        foreach (PlanetaryRing ring in PlanetaryRings)
        {
            CheckPlanetaryRing(ring);

            // create an asteroid type if it doesn't already exist in our list
            bool asteroidTypeIsNewAsteroidType = true;
            if(asteroidTypes.Count > 0)
                for (int i = 0; i < asteroidTypes.Count; i++)
                    if (ring.AsteroidPrefab == asteroidTypes[i])
                    {
                        asteroidTypeIsNewAsteroidType = false;
                        poolCounts[i] += ring.asteroidCount;
                    }
            if (asteroidTypeIsNewAsteroidType)
            {
                asteroidTypes.Add(ring.AsteroidPrefab);
                poolCounts.Add(ring.asteroidCount);
            }
        }

        // create pools for all our asteroid types
        for (int i = 0; i < asteroidTypes.Count; i++)
            SimplePool.Preload(asteroidTypes[i], poolCounts[i]);

        if(SpawnAsteroidsAtStart)
            // spawns initial asteroids
            foreach (PlanetaryRing ring in PlanetaryRings)
                for (int i = 0; i < ring.asteroidCount; i++)
                {
                    float angle = Random.Range(angleLBound, angleUBound);
                    SpawnAsteroidInRingAtAngle(ring, angle);
                }
    }
    
    private void FixedUpdate()
    {
        if (SpawnAsteroidsDuringUpdate)
            foreach (PlanetaryRing ring in PlanetaryRings)
            {
                // calculate spawn probability and spawn based on probability
                float spawnProbability = Time.deltaTime * ring.frequency;
                bool spawnAsteroidThisFrame = Random.Range(0, 100) < spawnProbability;

                if (spawnAsteroidThisFrame)
                    SpawnAsteroidInRingAtAngle(ring, angleLBound);
            }

        KeepAsteroidsOnCirclePath();
    }

    void SpawnAsteroidInRingAtAngle(PlanetaryRing ring, float spawnAngleRelToLocalX)
    {
        // where to spawn in the next asteroid
        float nextSize = Random.Range(ring.sizeScaleMedian - ring.sizeScaleVariance, ring.sizeScaleMedian + ring.sizeScaleVariance);
        if (nextSize == 0)
            return;
        float nextDistance = Random.Range(ring.minDistance, ring.maxDistance);
        Vector3 nextAsteroidPosition =  new Vector2(Mathf.Cos(spawnAngleRelToLocalX), Mathf.Sin(spawnAngleRelToLocalX)) * nextDistance;

        // spawn it in
        GameObject nextAsteroid = SimplePool.Spawn(ring.AsteroidPrefab, nextAsteroidPosition, Quaternion.identity);

        // set initial conditions
        nextAsteroid.transform.parent = transform;
        nextAsteroid.transform.localScale = new Vector3(nextSize, nextSize, 1);
        float nextSpeed = Random.Range(ring.speedMedian - ring.speedVariance, ring.speedMedian + ring.speedVariance);
        nextAsteroid.GetComponent<Rigidbody2D>().velocity = transform.up * nextSpeed;
    }

    void KeepAsteroidsOnCirclePath()
    {
        foreach(Transform asteroid in transform)
        {
            // useful variables
            Vector2 vel = asteroid.GetComponent<Rigidbody2D>().velocity;
            Vector3 positionDif = asteroid.position - transform.position;
            float speed = vel.magnitude;
            float distance = positionDif.magnitude;

            // keep the velocity tangent to the circle
            vel = Vector3.Cross(Vector3.forward, positionDif) * speed;
            float turnRate = speed / distance;
            float angle = Mathf.Atan2(vel.y, vel.x);
            angle += turnRate * Time.deltaTime;
            vel = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * speed;
            asteroid.GetComponent<Rigidbody2D>().velocity = vel;
        }
    }

    void CheckPlanetaryRing(PlanetaryRing ring)
    {
        // we don't want non-sense values
        if (ring.asteroidCount < 0)
            ring.asteroidCount = 0;
        if (ring.frequency < 0f)
            ring.frequency = 0f;
        if (ring.minDistance < 10f)
            ring.minDistance = 10f;
        if (ring.maxDistance < 10f)
            ring.maxDistance = 10f;
        if (ring.maxDistance < ring.minDistance)
        {
            float temp = ring.minDistance;
            ring.minDistance = ring.maxDistance;
            ring.maxDistance = temp;
        }
        if (ring.sizeScaleMedian < 0)
            ring.sizeScaleMedian = 0;
        if (ring.sizeScaleMedian - ring.sizeScaleVariance < 0)
            ring.sizeScaleVariance = ring.sizeScaleMedian;
        if (ring.speedMedian - ring.speedVariance < 0)
            ring.speedVariance = ring.speedMedian;
    }
}