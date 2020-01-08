using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionAccomplishedPanel : MonoBehaviour {

    [Header("References")]
    [SerializeField]
    Text titleText;
    [SerializeField]
    Text bodyText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetTitleText(string msg)
    {
        titleText.text = msg;
    }

    public void SetBodyText(string msg)
    {
        bodyText.text = msg;
    }
}
