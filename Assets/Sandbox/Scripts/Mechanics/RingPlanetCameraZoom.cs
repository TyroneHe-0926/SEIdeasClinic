using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingPlanetCameraZoom : MonoBehaviour {

    [SerializeField] AnimationCurve positionCurve;
    [SerializeField] AnimationCurve zoomCurve;

    Vector3 startingPosition = new Vector3(-4, 70.4f, -1.5f);
    float startingSize = 28.9f;
    Vector3 endingPosition = new Vector3(127.4f, 89.6f, -1.5f);
    float endingSize = 116.5f;

    GameObject Player;
    Vector3 startingPlayerPos;
    Vector3 currentPlayerPos;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        startingPlayerPos = Player.transform.position;
        transform.position = startingPosition;
        GetComponent<Camera>().orthographicSize = startingSize;
    }

    private void Update()
    {
        float playerTravelDistance = (Player.transform.position - startingPlayerPos).magnitude;
        float curveInput = playerTravelDistance / 300f;

        transform.position = Vector3.Lerp(startingPosition, endingPosition, positionCurve.Evaluate(curveInput));
        GetComponent<Camera>().orthographicSize = Mathf.Lerp(startingSize, endingSize, zoomCurve.Evaluate(curveInput));
    }
}
