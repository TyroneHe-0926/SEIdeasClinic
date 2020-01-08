using UnityEngine;

public class Asteroid : MonoBehaviour {

    float speed;
    float maxSpeed = 6;
    float minSpeed = 3;
    Rigidbody2D rb;

    Spawner spawn;
    Vector2 tl;
    Vector2 tr;
    Vector2 bl;
    Vector2 br;
    Vector2 pos;
    Vector2 dest;
    float deployDistance;
    public GameObject explosionEffect;


	void Awake () {
        rb = GetComponent<Rigidbody2D>();
        pos = new Vector2(0, 0);
        dest = new Vector2(0, 0);

        rb.angularVelocity = Random.Range(-35, 35);
    }
	
    public void Launch()
    {
        //bl = new Vector2(spawn.spawnPosition.x - 10, spawn.spawnPosition.y - 10);
        //br = new Vector2(spawn.spawnPosition.x + 10, spawn.spawnPosition.y - 10);
        //tl = new Vector2(spawn.spawnPosition.x - 10, spawn.spawnPosition.y + 10);
        //tr = new Vector2(spawn.spawnPosition.x + 10, spawn.spawnPosition.y + 10);
        bl = Camera.main.ScreenToWorldPoint(new Vector2(10, 0));
        br = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width - 20, 0));
        tl = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));
        tr = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        transform.localScale = Vector2.one * (0.2f + Random.Range(0.2f, 2f));
        speed = Random.Range(minSpeed, maxSpeed);
        pos.x = Random.Range(tl.x, tr.x);
        pos.y = tr.y + 1;
        //pos.y = Random.Range(tr.y, bl.y + 1);
        //dest.y = bl.y;
        dest.y = Random.Range(tl.y, bl.y);
        dest.x = Random.Range(bl.x, br.x);
        Vector2 velocity = speed * ((dest - pos).normalized);
        transform.position = pos;
        rb.velocity = velocity;

        

        deployDistance = Vector3.Distance(pos, dest);
    }

    bool ReturnToPool()
    {
        rb.velocity = Vector2.zero;
        return SimplePool.Despawn(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Missile"))
        {
            GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(explosion, 0.6f);
            bool success = ReturnToPool();
            if (!success)
            {
                Destroy(gameObject);
            }
        }
    }

}
