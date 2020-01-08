using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox
{
    public class TurretControls
    {
        private Turret parentTurret;
        public TurretControls(Turret parentTurret)
        {
            this.parentTurret = parentTurret;
        }

        public Vector2 aimTo = new Vector2();
        
        public float Tube0Cooldown { get { return parentTurret.Tube0Cooldown; } }
        public float Tube1Cooldown { get { return parentTurret.Tube1Cooldown; } }
        public float Tube2Cooldown { get { return parentTurret.Tube2Cooldown; } }
        public float Tube3Cooldown { get { return parentTurret.Tube3Cooldown; } }

        public event Action OnTube0Fired;
        public event Action OnTube1Fired;
        public event Action OnTube2Fired;
        public event Action OnTube3Fired;

        public void FireTube0()
        {
            OnTube0Fired();
        }

        public void FireTube1()
        {
            OnTube1Fired();
        }

        public void FireTube2()
        {
            OnTube2Fired();
        }

        public void FireTube3()
        {
            OnTube3Fired();
        }
    }
}