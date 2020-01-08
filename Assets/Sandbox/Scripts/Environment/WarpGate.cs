using Sandbox;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WarpGate : MonoBehaviour
{
    [Header("Tunable")]
    public int gateIndex = -1;
    public GalaxyMapNode DestinationGalaxyMapNode { get; private set; }

    public event Action<WarpGate, Ship> OnWarpGateTriggered; //Triggering gate, triggering ship

    //Internal
    GameCore gameCore;
    
    public void Setup(GameCore parentGameCore)
    {
        gameCore = parentGameCore;
        DestinationGalaxyMapNode = gameCore.GalaxyMapVisual.FindDestinationNodeForWarpGate(this);
    }

    public void TriggerWarpGate(Ship triggeringShip)
    {
        if (triggeringShip != null)
        {
            OnWarpGateTriggered?.Invoke(this, triggeringShip);
        }
    }
    
    /*
    [ContextMenu("Manually trigger gate (find Ship)")] //You can right-click on a WarpGate component to manually trigger the jump
    private void DebugTrigger()
    {      
        Ship ship = FindObjectOfType<Ship>();
        GalaxyMapVisual galaxyMap = FindObjectOfType<GalaxyMapVisual>();
        GalaxyMapNode node = galaxyMap.FindNodeNamed(destination);

        if(node != null)
        {
            OnWarpGateTriggered?.Invoke(this, ship, destination);
        }        
    }
    */
}