using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sandbox;

/// <summary>
/// Controls the five ship thrusters.
/// </summary>

public class Thrusters : MonoBehaviour
{
    [Header("Tunable")]
    [SerializeField]
    float mainThrustLimit = 100f;
    [SerializeField]
    float maneuverThrustLimit = 50f;
    [SerializeField]
    float maxUFODriveSpeed = 50f;

    public ThrusterControls thrusterControlInputs = new ThrusterControls();

    [Header("References")]
    [SerializeField]
    SideThrusters starboardAftThruster;
    [SerializeField]
    SideThrusters portAftThruster;
    [SerializeField]
    SideThrusters starboardBowThruster;
    [SerializeField]
    SideThrusters portBowThruster;

    //Internal
    GameCore gameCore;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameCore = FindObjectOfType<GameCore>();
    }

    private void FixedUpdate()
    {
        //Check if we're in the Easy Universe, if we are, the UFO drive works
        if(gameCore != null && gameCore.SimMode == GameCore.SimulationMode.A)
        {
            //Check if there's any UFO Drive input
            if(thrusterControlInputs.UFODriveVelocity.sqrMagnitude != 0)
            {
                rb.velocity = Vector3.ClampMagnitude(thrusterControlInputs.UFODriveVelocity, maxUFODriveSpeed);
                return;
            }
        }
            
        //Otherwise, fall through to using normal thrusters
        //Making sure to clamp maximum possible output thrust to avoid students "cheesing" the simulation forces (e.g. negative thrust, infinite forces)

        //Main thruster
        rb.AddForce(transform.up * Mathf.Clamp(thrusterControlInputs.mainThrust, 0f, mainThrustLimit));
        
        //Maneuvering thrusters
        starboardAftThruster.thrust = Mathf.Clamp(thrusterControlInputs.starboardAftThrust, 0f, maneuverThrustLimit);
        portAftThruster.thrust = Mathf.Clamp(thrusterControlInputs.portAftThrust, 0f, maneuverThrustLimit);
        starboardBowThruster.thrust = Mathf.Clamp(thrusterControlInputs.starboardBowThrust, 0f, maneuverThrustLimit);
        portBowThruster.thrust = Mathf.Clamp(thrusterControlInputs.portBowThrust, 0f, maneuverThrustLimit);
    }

       
}
