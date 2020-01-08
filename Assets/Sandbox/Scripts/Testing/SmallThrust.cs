using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallThrust : MonoBehaviour {


    public float thrust = 0f;
    public Rigidbody2D ship;

	void Start () {
        ship = GetComponentInParent<Rigidbody2D>();
	}
	

	void FixedUpdate () {
        ship.AddForceAtPosition(transform.right * thrust, transform.position);
	}
}
