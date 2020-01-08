using System;
using System.Collections.Generic;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
//using UnityEngine;
using Sandbox;
using System.Linq;

public class SensorSubsystemController
{
    public class DetectedFarObject
    {
        public Vector2 position;
        public GravitySignature type;
        public string destinationName;
        public bool isWarpGate()
        {
            return type == GravitySignature.WarpGate;
        }
        public string ToString()
        {
            string ret = "<FarObject position=" + position + ", type=" + type.ToString();
            if (isWarpGate()) ret += ", dest=" + destinationName;
            return ret + ">";
        }
    }
    public class DetectedCloseObject
    {
        public Vector2 position;
        public Vector2 velocity;
        public float radius;
        public List<SpaceMaterial> materials = new List<SpaceMaterial>();
        public int materialSignature;
        public string ToString()
        {
            return "<CloseObject position=" + position +", velocity="+velocity+ ", materials="
                + string.Join(";", materials.Select(o => o.ToString())) +">";
        }
    }

    // The list of all far objects
    public List<DetectedFarObject> detectedFarObjects { private set; get; }
    // The list of all close objects
    public List<DetectedCloseObject> detectedCloseObjects { private set; get; }
    // The list of all warp gates
    public List<DetectedFarObject> getWarpGates()
    {
        return detectedFarObjects.Where(o => o.isWarpGate()).ToList();
    }
    // The list of all far objects except warp gates.
    public List<DetectedFarObject> getNotWarpGatesFarObjects()
    {
        return detectedFarObjects.Where(o => !o.isWarpGate()).ToList();
    }

    private Vector2 AngleToPosition(Vector2 shipPos, float angle, float distance, bool isDegree)
    {
        //Debug.Log("x=" + shipPos.x + ", y=" + shipPos.y + ", angle=" + angle + "dis=" + distance);
        if (isDegree) angle = angle / 180.0f * (float)Math.PI;
        shipPos.x += (float)Math.Cos(angle)* distance;
        shipPos.y += (float)Math.Sin(angle)* distance;
        //Debug.Log("(final) x=" + shipPos.x + ", y=" + shipPos.y);
        return shipPos;
    }

    public void SensorsUpdate(SubsystemReferences subsysRef, ShipSensors Data)
    {
        detectedFarObjects = new List<DetectedFarObject>();
        detectedCloseObjects = new List<DetectedCloseObject>();

        foreach (GWI_Detection detection in Data.GWInterferometer)
        {
            DetectedFarObject obj = new DetectedFarObject();
            obj.type = detection.signature;
            obj.position = AngleToPosition(subsysRef.currentShipPositionWithinGalaxyMapNode,
                detection.angle, ShipSensors.GConstant / detection.waveAmplitude, true);
            obj.destinationName = detection.warpGateDestination;
            detectedFarObjects.Add(obj);
        }

        foreach (EMS_Detection detection in Data.EMSensor)
        {
            DetectedCloseObject obj = new DetectedCloseObject();
            obj.velocity = detection.velocity;
            obj.radius = detection.radius;
            obj.position = AngleToPosition(subsysRef.currentShipPositionWithinGalaxyMapNode,
                detection.angle, ShipSensors.EMConstant / detection.signalStrength, false);
            obj.materialSignature = detection.materialSignature;
            foreach (SpaceMaterial material in Enum.GetValues(typeof(SpaceMaterial)))
            {
                if (Data.CheckSignatureForSpaceMaterial(detection.materialSignature, material))
                    obj.materials.Add(material);
            }
            detectedCloseObjects.Add(obj);
        }
    }
}