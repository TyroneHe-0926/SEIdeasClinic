using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
public class GalaxyMapEdge : MonoBehaviour
{
    [Header("Tunables")]
    [SerializeField]
    float edgeCost = 0f;

    [Header("References")]
    public GalaxyMapNode nodeA;
    //public int gateIndexA;

    public GalaxyMapNode nodeB;
    //public int gateIndexB;

    //Properties
    public float EdgeCost { get { return edgeCost; } set { edgeCost = value; } }

    //Internal
    SpriteRenderer spriteRenderer;


	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        if(nodeA != null && nodeB != null && nodeA != nodeB)
        {
            spriteRenderer.enabled = true;

            Vector3 delta = nodeB.transform.position - nodeA.transform.position;

            transform.position = Vector3.Lerp(nodeA.transform.position, nodeB.transform.position, 0.5f);

            float edgeThickness = Mathf.Lerp(0.1f, 1f, (edgeCost / 20));
            transform.localScale = new Vector3(delta.magnitude, edgeThickness, 1f);

            transform.rotation = Quaternion.FromToRotation(Vector3.right, delta.normalized);
            //transform.rotation = Quaternion.AngleAxis(Vector3.SignedAngle(Vector3.right, delta.normalized, Vector3.forward), Vector3.forward);
        } else
        {
            spriteRenderer.enabled = false;
        }
    }

    private void OnDrawGizmos()
    {
        Handles.Label(transform.position + Vector3.up, EdgeCost.ToString());
    }
}
