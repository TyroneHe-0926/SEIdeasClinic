using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//put on side thruster gameobjects
public class SideThrusters : MonoBehaviour {

    public float thrust = 0f;
    public Rigidbody2D ship;

    void Start()
    {
        ship = GetComponentInParent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        ship.AddForceAtPosition(transform.right * thrust, transform.position);
    }
}
