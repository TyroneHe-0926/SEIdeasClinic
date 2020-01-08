using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[SelectionBase]
public class GalaxyMapNode : MonoBehaviour
{
    [Header("Tunables")]
    [SerializeField]
    bool isDefaultLocation;

    public bool IsDefaultLocation { get { return isDefaultLocation; } }

    public List<GalaxyMapEdge> edges = new List<GalaxyMapEdge>();

    private void OnDrawGizmos()
    {   
        Handles.Label(transform.position + Vector3.up, this.gameObject.name);
    }
}
