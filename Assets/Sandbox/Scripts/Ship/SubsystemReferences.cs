using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox
{
    public class SubsystemReferences
    {   
        // Subsystems
        public SensorSubsystemController Sensors { get; private set; }
        public DefenceSubsystemController Defence { get; private set; }
        public NavigationSubsystemController Navigation { get; private set; }
        public PropulsionSubsystemController Propulsion { get; private set; }

        public event Action<string, string> OnWarpJumpCompleted; //Old system name, new current system name

        public SubsystemReferences(Ship parentShip)
        {   
            parentShip.OnWarpJumpCompleted += HandleWarpJumpCompleted;

            Sensors = new SensorSubsystemController();
            Defence = new DefenceSubsystemController();
            Navigation = new NavigationSubsystemController();
            Propulsion = new PropulsionSubsystemController();
        }

        private void HandleWarpJumpCompleted(string oldSystemName, string currentSystemName)
        {
            OnWarpJumpCompleted(oldSystemName, currentSystemName);
        }

        // Ship information
        public Vector3 currentShipPositionWithinGalaxyMapNode = new Vector3();
        public float shipCollisionRadius;
        public Vector3 velocity;
        public Vector3 forward;
        public Vector3 back;
        public Vector3 right;
        public Vector3 left;
        public Vector3 inwards;
        public Vector3 outwards;
        public float fixedDeltaTime;
        public string currentGalaxyMapNodeName;
    }
}