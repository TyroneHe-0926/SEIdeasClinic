using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternalCamera : MonoBehaviour {

    [Header("Tunable")]
    public Vector2 velocity;
    public float smoothX;
    public float smoothY;

    [Header("References")]
    public Transform targetTransform;

    //Internal
    Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();        
    }

    public void Setup(Transform newTargetTransform)
    {
        targetTransform = newTargetTransform;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        cam.orthographicSize -= Input.mouseScrollDelta.y;
	}

    private void FixedUpdate()
    {
        if(targetTransform != null)
        {   
            float posX = Mathf.SmoothDamp(transform.position.x, targetTransform.position.x, ref velocity.x, smoothX);
            float posY = Mathf.SmoothDamp(transform.position.y, targetTransform.position.y, ref velocity.y, smoothY);

            transform.position = new Vector3(posX, posY, transform.position.z);
        }
    }
}
