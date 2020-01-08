using System;
using System.Collections.Generic;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using Sandbox;

public class PropulsionSubsystemController
{
    bool turning = true;

    public void PropulsionUpdate(SubsystemReferences subsystemReferences, ThrusterControls thrusterControls)
    {
        Vector2 direction = subsystemReferences.Navigation.GetDestination() - (Vector2)subsystemReferences.currentShipPositionWithinGalaxyMapNode;
        direction /= direction.magnitude;
        direction *= 10;
        thrusterControls.UFODriveVelocity = direction;















        /*Vector3 target = new Vector3(0, 0, 0);
        float targetAngle = Vector3.SignedAngle(Vector3.right, target, Vector3.forward);
        float angle = Vector3.SignedAngle(Vector3.right, subsystemReferences.forward, Vector3.forward);
        if (turning)
        {

            if ((angle > 25) && (angle < 35))
            {
                turning = false;

            }
            thrusterControls.portAftThrust = 3;
            
        }
        else {
            thrusterControls.portAftThrust = 0;
        }
        */
    }



}