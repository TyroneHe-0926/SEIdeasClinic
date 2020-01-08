using UnityEngine;
using UnityEngine.UI;
using Sandbox;
using System;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(ShipSensors))]
[RequireComponent(typeof(Turret))]
[RequireComponent(typeof(Thrusters))]

/// <summary>\
/// This script goes on the ship gameObject tagged "Player".
/// It calls all of the student code and handles the ship's properties.
/// </summary>

public class Ship : MonoBehaviour
{
    //Properties
    public float HealthRatio { get{ return (shipHealth / maxHealth); } }
    public float TotalWarpFuelConsumed { get; private set; }

    //Events
    public event Action<string, string> OnWarpJumpCompleted; //Old system name, new system name

    //Internal
    GameCore parentGameCore;
    Rigidbody2D rigidBody2D;
    CircleCollider2D circleCollider2D;
    ShipSensors shipSensors;
    Turret turret;
    Thrusters thrusters;

    // Create references to student code
    private SubsystemReferences subsystems;
    
    public static float missileSpeed = 14f;
    float shipHealth;
    float maxHealth = 100;
    float damage = 2;
    Slider healthbar;

    public void Setup(GameCore parentGameCore)
    {
        this.parentGameCore = parentGameCore;

         subsystems = new SubsystemReferences(this);
    }

    void Start()
    {
        // Get Ship Components
        rigidBody2D = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        shipSensors = GetComponent<ShipSensors>();
        turret = GetComponent<Turret>();
        thrusters = GetComponent<Thrusters>();
        thrusters.thrusterControlInputs.OnWarpJumpTriggered += HandleWarpJumpTriggered;
            
        UpdateSystemReference();

        // Health Bar
        healthbar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Slider>();
        shipHealth = 0f;
        healthbar.value = HealthRatio;

    }

    private void UpdateSystemReference()
    {
        subsystems.currentShipPositionWithinGalaxyMapNode = transform.position;
        subsystems.shipCollisionRadius = circleCollider2D.radius;
        subsystems.velocity = rigidBody2D.velocity;
        subsystems.forward = transform.up;
        subsystems.back = -subsystems.forward;
        subsystems.right = transform.right;
        subsystems.left = -subsystems.right;
        subsystems.inwards = transform.forward;
        subsystems.outwards = -subsystems.inwards;
        subsystems.fixedDeltaTime = Time.fixedDeltaTime;
        subsystems.currentGalaxyMapNodeName = parentGameCore.CurrentSolarSystemName;
    }    

    private void FixedUpdate()
    {
        // Pre-SubsystemUpdate
        UpdateSystemReference();
        shipSensors.NewSensorDataForThisFrame();

        // SubsystemUpdate
        // The student code runs. One method call per subsystem
        subsystems.Sensors.SensorsUpdate(subsystems, shipSensors);
        subsystems.Defence.DefenceUpdate(subsystems, turret.TurretControlInputs);
        subsystems.Navigation.NavigationUpdate(subsystems, parentGameCore.GalaxyMapVisual.GalaxyMapData);
        subsystems.Propulsion.PropulsionUpdate(subsystems, thrusters.thrusterControlInputs);

        // Post-SubsystemUpdate
        UpdateSystemReference();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("SmallBodies"))
        {
            shipHealth += damage;
            healthbar.value = HealthRatio;
            Debug.Log("Ship health: " + shipHealth);
        }
    }

    private void HandleWarpJumpTriggered()
    {
        Collider2D[] overlappingColliders = Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius);
        foreach(var collider in overlappingColliders)
        {
            WarpGate gate = collider.GetComponentInParent<WarpGate>();
            if (gate != null)
            {
                gate.TriggerWarpGate(this);
                TotalWarpFuelConsumed += rigidBody2D.velocity.magnitude;
                Debug.Log("Total warp fuel consumed: " + TotalWarpFuelConsumed);
            }
        }
    }

    public void SignalWarpJumpCompleted() { 
}

    //for student's debug purposes
    public static void Log(object message)
    {
        Debug.Log(message); 
    }
}