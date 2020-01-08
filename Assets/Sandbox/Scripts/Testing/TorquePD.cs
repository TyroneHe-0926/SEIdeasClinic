using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorquePD : MonoBehaviour {

    Quaternion desiredRotation = Quaternion.Euler(0, 0, 45f);
    public float kp = 0f;
    public float kd = 0f;
    Rigidbody rb;

	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	

	void FixedUpdate () {
        float dt = Time.fixedDeltaTime;
        float g = 1 / (1 + kd * dt + kp * dt * dt);
        float ksg = kp * g;
        float kdg = (kd + kp + dt) * g;
        Vector3 x;
        float xMag;
        Quaternion q = desiredRotation * Quaternion.Inverse(transform.rotation);
        q.ToAngleAxis(out xMag, out x);
        x.Normalize();
        x *= Mathf.Deg2Rad;
        Vector3 pidv = kp * x * xMag - kd * rb.angularVelocity;
        Quaternion rotInertia2World = rb.inertiaTensorRotation * transform.rotation;
        pidv = Quaternion.Inverse(rotInertia2World) * pidv;
        pidv.Scale(rb.inertiaTensor);
        pidv = rotInertia2World * pidv;
        rb.AddTorque(pidv);


    }
}
