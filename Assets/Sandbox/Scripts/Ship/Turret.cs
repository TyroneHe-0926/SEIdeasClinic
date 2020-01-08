using UnityEngine;
using Sandbox;

/// <summary>
/// This turret fires an instance of the missilePrefab every second from a pool. 
/// Adjust the fire rate in InvokeRepeating() method.
/// </summary>

public class Turret : MonoBehaviour
{
    //Properties
    public TurretControls TurretControlInputs { get; private set; }

    [Header("References")]
    public GameObject TurretObj;
    public GameObject missilePrefab;
    public float turretAngle;

    //Internal
    float cooldownDuration = 3f;
    public float Tube0Cooldown { get; private set; }
    public float Tube1Cooldown { get; private set; }
    public float Tube2Cooldown { get; private set; }
    public float Tube3Cooldown { get; private set; }


    void Start()
    {
        SimplePool.Preload(missilePrefab, 4);

        TurretControlInputs = new TurretControls(this);

        TurretControlInputs.OnTube0Fired += HandleTube0Fired;
        TurretControlInputs.OnTube1Fired += HandleTube1Fired;
        TurretControlInputs.OnTube2Fired += HandleTube2Fired;
        TurretControlInputs.OnTube3Fired += HandleTube3Fired;
    }

    private void Update()
    {
        //Cooldowns
        Tube0Cooldown = Mathf.Max(0f, Tube0Cooldown - Time.deltaTime);
        Tube1Cooldown = Mathf.Max(0f, Tube1Cooldown - Time.deltaTime);
        Tube2Cooldown = Mathf.Max(0f, Tube2Cooldown - Time.deltaTime);
        Tube3Cooldown = Mathf.Max(0f, Tube3Cooldown - Time.deltaTime);
    }

    void FixedUpdate()
    {
        if (TurretControlInputs.aimTo != null)
        {
            turretAngle = Mathf.Atan2(TurretControlInputs.aimTo.y - TurretObj.transform.position.y, TurretControlInputs.aimTo.x - TurretObj.transform.position.x) * Mathf.Rad2Deg;
            turretAngle -= 90;
            TurretObj.transform.localRotation = Quaternion.Euler(0, 0, turretAngle + 90);
        }
        else
        {
            return;
        }
    }

    void FireMissile(Vector3 deployPos, float turretAngle)
    {
        GameObject firedMissile = Instantiate(missilePrefab, TurretObj.transform.position, Quaternion.AngleAxis(turretAngle, Vector3.forward));
        firedMissile.transform.SetParent(GameCore.DynamicObjectsRoot); //Keeps the hierarchy tidy        

        Missile missileScript = firedMissile.GetComponent<Missile>();
        missileScript.LockOn();

        Rigidbody2D missileRb = firedMissile.GetComponent<Rigidbody2D>();
        missileRb.velocity = Ship.missileSpeed * firedMissile.transform.right;
    }

    private void HandleTube0Fired()
    {
        if (Tube0Cooldown <= 0)
        {
            FireMissile(TurretObj.transform.position, Vector2.SignedAngle(Vector2.right, TurretControlInputs.aimTo));
            Tube0Cooldown = cooldownDuration;
        }
    }

    private void HandleTube1Fired()
    {
        if (Tube1Cooldown <= 0)
        {
            FireMissile(TurretObj.transform.position, Vector2.SignedAngle(Vector2.right, TurretControlInputs.aimTo));
            Tube1Cooldown = cooldownDuration;
        }
    }

    private void HandleTube2Fired()
    {
        if (Tube2Cooldown <= 0)
        {
            FireMissile(TurretObj.transform.position, Vector2.SignedAngle(Vector2.right, TurretControlInputs.aimTo));
            Tube2Cooldown = cooldownDuration;
        }
    }

    private void HandleTube3Fired()
    {
        if (Tube3Cooldown <= 0)
        {
            FireMissile(TurretObj.transform.position, Vector2.SignedAngle(Vector2.right, TurretControlInputs.aimTo));
            Tube3Cooldown = cooldownDuration;
        }
    }
}