using System;
using UnityEngine;
using System.Collections.Generic;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using Sandbox;

public class NavigationSubsystemController {
    private bool firstTime = false;
    private List<string> galaxyPath = new List<string>();
    private Vector2 destinationPosition;
    private string nextGalaxyName;

    private void UpdatePath(SubsystemReferences SystemReferences, GalaxyMapData galaxyMapData) {
        string start = SystemReferences.currentGalaxyMapNodeName;
        // TODO Dijkstra
        galaxyPath.Add("Sol");
        galaxyPath.Add("Alpha Centauri");
        galaxyPath.Add("Kepler 438");
    }

    public void NavigationUpdate(SubsystemReferences SystemReferences, GalaxyMapData galaxyMapData) {
        if (!firstTime) {
            // If havn't, run Dijkstra and find the shortest path
            UpdatePath(SystemReferences, galaxyMapData);
            firstTime = true;
        }
        // Update the destination within the current galaxy 
        // (Kepler 438b if in Kepler 438, the next wrap gate along the path if otherwise)
        // Traverse all galaxy
        for (int i = 0; i < galaxyPath.Count; i++) {
            // Find the current galaxy
            if (galaxyPath[i] == SystemReferences.currentGalaxyMapNodeName) {
                if (i == galaxyPath.Count - 1) {
                    // This is the final galaxy, return the planet position
                    // Check if the planet is close
                    foreach (SensorSubsystemController.DetectedCloseObject closeObject
                    in SystemReferences.Sensors.detectedCloseObjects) {
                        if (closeObject.materials.Contains(SpaceMaterial.Common) &&
                            closeObject.materials.Contains(SpaceMaterial.Metal) &&
                            closeObject.materials.Contains(SpaceMaterial.Water)) {
                            // Planet found
                            destinationPosition = closeObject.position;
                            return;
                        }
                    }
                    // If the planet is not close, go to the planetoid
                    foreach (SensorSubsystemController.DetectedFarObject farObject
                    in SystemReferences.Sensors.detectedFarObjects) {
                        if (farObject.type == GravitySignature.Planetoid) {
                            destinationPosition = farObject.position;
                            return;
                        }
                    }
                }
                else {
                    // This is NOT the final galaxy, return the wrap gate position
                    nextGalaxyName = galaxyPath[i + 1];
                    foreach (SensorSubsystemController.DetectedFarObject wrapgate
                    in SystemReferences.Sensors.getWarpGates()) {
                        if (wrapgate.destinationName == nextGalaxyName) {
                            //Debug.Log("(" + wrapgate.position.x + ",(" + wrapgate.position.y + ")");
                            destinationPosition = wrapgate.position;
                            return;
                        }
                    }
                }
            }
        }
    }

    /// Returns a Vector2, the destination within the current system:
    /// if at Kepler-438 -> Kepler-438
    /// else             -> the correct wrap gate
    public Vector2 GetDestination() { return destinationPosition; }
}
