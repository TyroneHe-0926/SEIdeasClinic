using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox
{
    public class ThrusterControls
    {
        public float mainThrust;

        public float starboardAftThrust;
        public float portAftThrust;
        public float starboardBowThrust;
        public float portBowThrust;

        public Vector2 UFODriveVelocity; //Can be used for early iterations, just to get the ship moving.

        public event Action OnWarpJumpTriggered; 
        public void TriggerWarpJump() //Call this method in order to attempt to use a WarpGate. Ship must be overlapping with a gate at the time.
        {
            OnWarpJumpTriggered();
        }
    }
}
