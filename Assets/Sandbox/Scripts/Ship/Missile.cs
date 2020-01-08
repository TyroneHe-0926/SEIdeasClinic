using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script goes on missile Prefab

public class Missile : MonoBehaviour {

    private float deployDistance = 100; //if missed, distance until despawn

    private Rigidbody2D missileRb;
    private Vector2 missileLaunchPos;

    private void Awake()
    {
        missileRb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(8, 13);
    }

    void Start () {
        //Physics2D.IgnoreLayerCollision(8, 13);
        //This prevents missile from colliding with ship collider, need to set this
     
	}
	
	void Update () {
        if (Vector2.Distance(transform.position, missileLaunchPos) > deployDistance)
        {
            Destroy(gameObject);
            //ReturnToPool(); //if too far away
        }
    }

    void ReturnToPool()
    {
        missileRb.velocity = Vector2.zero;
        SimplePool.Despawn(gameObject);
    }

    public void LockOn()
    {
        missileLaunchPos = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //put exolosion effect here or on asteroids
        //ReturnToPool();
        Destroy(gameObject);
    }
}
