using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyMapEdgeData {

    public float edgeCost;
    public GalaxyMapNodeData nodeA;
    public GalaxyMapNodeData nodeB;

    /// <summary>
    /// Convenience function for returning the other node given one of either nodeA or nodeB for this edge.
    /// Returns null if passed a node that doesn't belong to this edge.
    /// </summary>
    /// <param name="startNode"></param>
    /// <returns></returns>
    public GalaxyMapNodeData GetOtherNode(GalaxyMapNodeData startNode)
    {
        if (startNode == nodeA)
            return nodeB;
        else if (startNode == nodeB)
            return nodeA;
        else
            return null;
    }
}
