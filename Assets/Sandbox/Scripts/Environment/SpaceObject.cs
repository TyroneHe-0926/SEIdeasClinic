using System.Collections.Generic;
using UnityEngine;
using Sandbox;

namespace Sandbox
{
    public enum SpaceMaterial { None, Unknown, Common, Metal, Water, Fisable, Antimatter }
    public enum GravitySignature { None, Unknown, WarpGate, BlackHole, Star, GasGiant, Planetoid }
}

public class SpaceObject : MonoBehaviour
{
    public string Description;
    public List<SpaceMaterial> Materials;
    public int MaterialSignature = 0;
    public GravitySignature gravitySignature = 0;

    public bool isDestructible = false;

    private void Start()
    {
        CalculateMaterialSignature();
    }

    public int CalculateMaterialSignature()
    {
        MaterialSignature = 0;
        if (Materials.Count != 0)
            foreach (SpaceMaterial mat in Materials)
                MaterialSignature += (0x1 << (int)mat);
        return MaterialSignature;
    }
}