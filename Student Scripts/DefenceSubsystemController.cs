using System;
using System.Collections.Generic;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using Sandbox;
using static SensorSubsystemController;
using System.Linq;

public class DefenceSubsystemController 
{
    bool shouldShot(DetectedCloseObject obj)
    {
        return obj.materials.Count !=0 && !obj.materials.Contains(SpaceMaterial.Fisable);
    }
    Vector2 getAimTo(DetectedCloseObject obj, Vector2 ship)
    {
        return obj.position + obj.velocity * (obj.position - ship).magnitude / Ship.missileSpeed - ship;
    }

    public void DefenceUpdate(SubsystemReferences subsystemReferences, TurretControls turretControls)
    {

        Vector2 ship = subsystemReferences.currentShipPositionWithinGalaxyMapNode;

        List<DetectedCloseObject> objs = subsystemReferences.Sensors.detectedCloseObjects;
        objs.Sort((a,b) => (int)((a.position-ship).sqrMagnitude - (b.position - ship).sqrMagnitude)*1000);
        objs = objs.Where(a => shouldShot(a)).ToList();

        if (objs.Count < 1) return;
        turretControls.aimTo = getAimTo(objs[0], ship);
        turretControls.FireTube0();

        if (objs.Count < 2) return;
        turretControls.aimTo = getAimTo(objs[1], ship);
        turretControls.FireTube1();

        if (objs.Count < 3) return;
        turretControls.aimTo = getAimTo(objs[2], ship);
        turretControls.FireTube2();

        if (objs.Count < 4) return;
        turretControls.aimTo = getAimTo(objs[3], ship);
        turretControls.FireTube3();

    }
}
 