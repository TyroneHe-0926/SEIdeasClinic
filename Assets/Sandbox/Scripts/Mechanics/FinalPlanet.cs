using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sandbox;

public class FinalPlanet : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            GameObject.FindObjectOfType<GameCore>().TriggerVictory();
    }
}
