using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyMapVisual : MonoBehaviour
{
    GameCore gameCore;

    public Dictionary<string, GalaxyMapNode> Nodes { get; private set; } = new Dictionary<string, GalaxyMapNode>();
    public GalaxyMapEdge[] Edges { get; private set; }

    public GalaxyMapData GalaxyMapData { get; private set; }
    
    public void Setup(GameCore parentGameCore, int galaxySeed)
    {
        gameCore = parentGameCore;

        GalaxyMapNode[] foundNodes = GetComponentsInChildren<GalaxyMapNode>();
        Edges = GetComponentsInChildren<GalaxyMapEdge>();

        //Inform the nodes about their relevant edges
        foreach (var node in foundNodes)
        {
            Nodes.Add(node.name, node);

            foreach (var edge in Edges)
            {
                if (edge.nodeA == node || edge.nodeB == node)
                {
                    node.edges.Add(edge);
                }
            }
        }

        //Randomize edge weights
        if(gameCore.SimMode == GameCore.SimulationMode.C && galaxySeed != 0)
        {   
            Random.InitState(galaxySeed);
            foreach(var edge in Edges)
            {
                edge.EdgeCost = Random.Range(1, 21);
            }
        }

        GalaxyMapData = GenerateGalaxyMapData();
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    public GalaxyMapNode GetDefaultNode()
    {   
        foreach(var node in Nodes.Values)
        {
            if (node.IsDefaultLocation)
                return node;
        }

        return null;
    }

    //Copy Unity-side hierarchy information over to "student safe" data containers
    public GalaxyMapData GenerateGalaxyMapData()
    {
        //Data copies for GalaxyMapNodes
        var nodeDataToReturn = new List<GalaxyMapNodeData>();
        foreach(var node in Nodes.Values)
        {
            var newNodeData = new GalaxyMapNodeData();
            newNodeData.systemName = node.name;
            newNodeData.galacticPosition = node.transform.position;
            nodeDataToReturn.Add(newNodeData);
        }

        //Data copies for GalaxyMapEdges
        var edgeDataToReturn = new List<GalaxyMapEdgeData>();
        foreach(var edge in Edges)
        {
            var newEdgeData = new GalaxyMapEdgeData();
            newEdgeData.edgeCost = edge.EdgeCost;
            
            foreach(var nodeData in nodeDataToReturn)
            {
                if(nodeData.systemName == edge.nodeA.name)
                {
                    newEdgeData.nodeA = nodeData;
                }

                if (nodeData.systemName == edge.nodeB.name)
                {
                    newEdgeData.nodeB = nodeData;
                }
            }

            edgeDataToReturn.Add(newEdgeData);
        }

        foreach(var node in nodeDataToReturn)
        {
            List<GalaxyMapEdgeData> edges = new List<GalaxyMapEdgeData>();
            foreach(var edge in edgeDataToReturn)
            {
                if (edge.nodeA == node || edge.nodeB == node)
                    edges.Add(edge);
            }
            node.edges = edges.ToArray();
        }

        //Package the data up and return it to the caller
        GalaxyMapData dataToReturn = new GalaxyMapData();
        dataToReturn.nodeData = nodeDataToReturn.ToArray();
        dataToReturn.edgeData = edgeDataToReturn.ToArray();
        return dataToReturn;
    }
    
    public GalaxyMapNode FindDestinationNodeForWarpGate(WarpGate departureGate)
    {
        var localEdges = Nodes[gameCore.CurrentSolarSystemName].edges;
        int gateSiblingIndex = departureGate.transform.GetSiblingIndex();
        if(gateSiblingIndex < localEdges.Count)
        {
            GalaxyMapEdge targetEdge = localEdges[gateSiblingIndex];
            if (targetEdge.nodeA.name == gameCore.CurrentSolarSystemName)
                return targetEdge.nodeB;
            else
                return targetEdge.nodeA;

        } else
        {
            return null;
        }
    }
}
