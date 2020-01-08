using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generates sensor data for the Sensors Subsystem to use.
/// </summary>

namespace Sandbox
{
    public class ShipSensors : MonoBehaviour
    {
        GameCore gameCore;
        GalaxyMapVisual galaxyMap;

        public const float GConstant = 1f;
        public const float EMConstant = 1f;
        public const float EMSRange = 30f;

        public LayerMask EMSMask;

        private GameObject WarpGatesParent;
        private GameObject LBodiesParent;

        private void Start()
        {
            gameCore = GetComponentInParent<GameCore>();
            galaxyMap = FindObjectOfType<GalaxyMapVisual>();    
        }

        #region Generating Sensor Data

        public List<GWI_Detection> GWInterferometer = new List<GWI_Detection>();

        public void GenerateGWIData()
        {
            WarpGatesParent = GameObject.FindGameObjectWithTag("WarpGates");
            LBodiesParent = GameObject.FindGameObjectWithTag("LargeBodies");

            if (WarpGatesParent != null)
            {
                foreach (Transform warpGate in WarpGatesParent.transform)
                {
                    Vector3 noiseOffset = Random.insideUnitCircle * gameCore.GWIInterferenceScale;
                    Vector3 positionDiff = (warpGate.transform.position + noiseOffset) - transform.position;
                    //float angle = Mathf.Atan2(positionDiff.y, positionDiff.x);
                    float angle = Vector3.SignedAngle(Vector3.right, positionDiff, Vector3.forward);
                    float waveAmplitude = ShipSensors.GConstant / positionDiff.magnitude;

                    WarpGate gate = warpGate.GetComponent<WarpGate>();
                    GalaxyMapNode destinationNode = galaxyMap.FindDestinationNodeForWarpGate(gate);
                    string destinationNodeName = "Unknown";
                    if(destinationNode != null)
                    {
                        destinationNodeName = destinationNode.name;
                    }
                    GWInterferometer.Add(new GWI_Detection(angle, waveAmplitude, GravitySignature.WarpGate, destinationNodeName));
                }
            }

            if (LBodiesParent != null)
            {
                foreach (Transform largeBody in LBodiesParent.transform)
                {
                    Vector3 noiseOffset = Random.insideUnitCircle * gameCore.GWIInterferenceScale;
                    Vector3 positionDiff = (largeBody.transform.position + noiseOffset) - transform.position;
                    //float angle = Mathf.Atan2(positionDiff.y, positionDiff.x);
                    float angle = Vector3.SignedAngle(Vector3.right, positionDiff, Vector3.forward);
                    float waveAmplitude = ShipSensors.GConstant / positionDiff.magnitude;
                    GravitySignature signature = largeBody.GetComponent<SpaceObject>().gravitySignature;
                    GWInterferometer.Add(new GWI_Detection(angle, waveAmplitude, signature));
                }
            }
        }

        public bool CheckSignatureForSpaceMaterial(int objectSignature, SpaceMaterial material)
        {
            int knownMaterialSignature = 0x1 << (int)material;
            return (objectSignature & knownMaterialSignature) != 0x0;
        }

        public List<EMS_Detection> EMSensor = new List<EMS_Detection>();

        public void GenerateEMSData()
        {
            Collider2D[] hazardColliders = Physics2D.OverlapCircleAll(transform.position, EMSRange, EMSMask);
            foreach (Collider2D col in hazardColliders)
            {
                var spaceObject = col.GetComponent<SpaceObject>();
                if (spaceObject != null && !col.gameObject.CompareTag("Player") && !col.gameObject.CompareTag("Missile"))
                {
                    Vector3 noiseOffset = Random.insideUnitCircle * gameCore.EMSInterferenceScale;
                    Vector3 positionDiff = (col.transform.position + noiseOffset) - transform.position;
                    float angle = Mathf.Atan2(positionDiff.y, positionDiff.x);
                    float waveAmplitude = 1f / positionDiff.magnitude;
                    int signature = spaceObject.MaterialSignature;
                    Rigidbody2D rb2D = col.GetComponent<Rigidbody2D>();
                    Vector2 velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y);
                    //Radius
                    float radius = -1f;
                    if(col is CircleCollider2D)
                    {
                        CircleCollider2D circleCollider = col as CircleCollider2D;
                        radius = circleCollider.radius;
                    } else if(col is BoxCollider2D)
                    {
                        BoxCollider2D boxCollider = col as BoxCollider2D;
                        radius = boxCollider.size.magnitude;
                    }

                    EMS_Detection detection = new EMS_Detection(angle, waveAmplitude, velocity, radius, signature);
                    EMSensor.Add(detection);
                }
            }
        }
        #endregion

        public void NewSensorDataForThisFrame()
        {
            EMSensor.Clear();
            GWInterferometer.Clear();
            GenerateEMSData();
            GenerateGWIData();
        }
    }

    // The GWI outputs a list of these
    public struct GWI_Detection
    {
        public float angle { get; private set; }
        public float waveAmplitude { get; private set; }
        public GravitySignature signature { get; private set; }
        public string warpGateDestination { get; private set; }

        public GWI_Detection(float angle0, float waveAmplitude0, GravitySignature signature0, string destination0 = "")
        {
            angle = angle0;
            waveAmplitude = waveAmplitude0;
            signature = signature0;
            warpGateDestination = destination0;
        }
    }

    // The EMS outputs a list of these
    public struct EMS_Detection
    {
        public float angle { get; private set; }
        public float signalStrength { get; private set; }
        public Vector2 velocity { get; private set; }
        public float radius { get; private set; }
        public int materialSignature { get; private set; }

        public EMS_Detection(float angle0, float signalStrength0, Vector2 velocity0, float radius, int signature0)
        {
            angle = angle0;
            signalStrength = signalStrength0;
            velocity = velocity0;
            this.radius = radius;
            materialSignature = signature0;
        }
    }
}

