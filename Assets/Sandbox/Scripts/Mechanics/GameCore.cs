using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sandbox;

/// <summary>
/// There should be only one game core in the game core scene. Another gameObject tagged "StarContainer" is also required.
/// This script controls level loading, winning and losing, as well as spawning in the Player gameObject.
/// </summary>

public class GameCore : MonoBehaviour
{
    [Header("Tunable")]
    [SerializeField]
    SimulationMode simulationMode = SimulationMode.A;
    [SerializeField]
    int galaxySeed = 0; //Pick any integer value to set the random generation seed. Used for galaxy edge weights.
    [SerializeField]
    float emsInterferenceScale = 0f;
    [SerializeField]
    float gwiInterferenceScale = 0f;
    
    [Header("References")]
    [SerializeField]
    Canvas uiOverlayCanvas;

    [Header("Assets")]
    [SerializeField]
    GameObject shipPrefab;
    [SerializeField]
    GameObject externalCameraPrefab;
    [SerializeField]
    GameObject missionAccomplishedPanelPrefab;
        
    public GalaxyMapVisual GalaxyMapVisual { get; private set; }
    GalaxyMapCamera GalaxyMapCamera;

    //Properties
    public string CurrentSolarSystemName { get { return currentSolarSystemName; } }
    public static Transform DynamicObjectsRoot;
    public SimulationMode SimMode { get { return simulationMode; } }
    public float EMSInterferenceScale { get { return emsInterferenceScale; } }
    public float GWIInterferenceScale { get { return gwiInterferenceScale; } }

    //Internal
    bool isHelpVisible = false;
    bool isGameOver = false;
    Ship ship;
    ExternalCamera externalCamera;
    string currentSolarSystemName;

    public enum SimulationMode
    {
        A,
        B,
        C
    }

    // Use this for initialization
    IEnumerator Start()
    {
        switch (simulationMode)
        {
            case SimulationMode.A:
                yield return LoadGalaxyMapRoutine("GalaxyMapA");
                break;
            case SimulationMode.B:
                yield return LoadGalaxyMapRoutine("GalaxyMapB");
                break;
            case SimulationMode.C:
                yield return LoadGalaxyMapRoutine("GalaxyMapC");
                break;
        }

        GalaxyMapNode defaultNode = GalaxyMapVisual.GetDefaultNode();
        if (defaultNode == null)
            defaultNode = FindObjectOfType<GalaxyMapNode>();
        yield return LoadSolarSystemSceneRoutine(defaultNode.name);

        ship = Instantiate(shipPrefab, transform).GetComponent<Ship>();
        ship.Setup(this);
        
        StartingLocation startingLocation = FindObjectOfType<StartingLocation>();

        externalCamera = Instantiate(externalCameraPrefab, transform).GetComponent<ExternalCamera>();
        externalCamera.Setup(ship.transform);

        DynamicObjectsRoot = new GameObject("DynamicObjectsRoots").transform;
        DynamicObjectsRoot.SetParent(transform);
    }

    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            isHelpVisible = !isHelpVisible;
        }

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            Time.timeScale += 1f;
            Debug.Log("New timescale = " + Time.timeScale);
        }

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            Time.timeScale -= 1f;
            Debug.Log("New timescale = " + Time.timeScale);
        }

        //Turn the galaxy map camera on/off
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GalaxyMapCamera.gameObject.SetActive(!GalaxyMapCamera.gameObject.activeSelf);
        }
    }

    IEnumerator LoadGalaxyMapRoutine(string galaxyMapName)
    {
        yield return SceneManager.LoadSceneAsync("Sandbox/Scenes/GalaxyMaps/" + galaxyMapName, LoadSceneMode.Additive);

        GalaxyMapVisual = FindObjectOfType<GalaxyMapVisual>();
        GalaxyMapVisual.Setup(this, galaxySeed);

        GalaxyMapCamera = FindObjectOfType<GalaxyMapCamera>();
        GalaxyMapCamera.gameObject.SetActive(false);
    }

    IEnumerator UnloadCurrentSolarSystemSceneRoutine()
    {
        yield return SceneManager.UnloadSceneAsync(currentSolarSystemName);
    }

    IEnumerator LoadSolarSystemSceneRoutine(string solarSystemName)
    {
        currentSolarSystemName = solarSystemName;
        yield return SceneManager.LoadSceneAsync(solarSystemName, LoadSceneMode.Additive);
        yield return SceneManager.SetActiveScene(SceneManager.GetSceneByName(solarSystemName));

        WarpGate[] warpGates = FindObjectsOfType<WarpGate>();
        foreach (var gate in warpGates)
        {
            gate.Setup(this);
            gate.OnWarpGateTriggered += HandleWarpGateTriggered;
        }
    }

    bool isTransitioningScenes = false;
    private void HandleWarpGateTriggered(WarpGate triggeredGate, Ship triggeringShip)
    {
        Debug.Log("HandleWarpGateTriggered");
        
        if (!isTransitioningScenes)
        {
            isTransitioningScenes = true;
            StartCoroutine(SolarSystemSwapRoutine(triggeredGate.DestinationGalaxyMapNode.name));
        }
            
    }

    IEnumerator SolarSystemSwapRoutine(string destinationGalaxyMapNodeName)
    {
        string startingSolarSystemName = currentSolarSystemName;
            
        yield return UnloadCurrentSolarSystemSceneRoutine();
        yield return LoadSolarSystemSceneRoutine(destinationGalaxyMapNodeName);

        currentSolarSystemName = destinationGalaxyMapNodeName;

        //Move ship on top of arrival gate
        ship.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        ship.GetComponent<Rigidbody2D>().angularVelocity = 0f;
        foreach (var gate in FindObjectsOfType<WarpGate>())
        {
            GalaxyMapNode sourceNode = GalaxyMapVisual.FindDestinationNodeForWarpGate(gate);
            if (sourceNode != null && sourceNode.name == startingSolarSystemName)
            {
                ship.transform.position = gate.transform.position + Vector3.right * 10f;
                
            }
        }

        isTransitioningScenes = false;
    }
    
    public void TriggerVictory()
    {
        if (isGameOver)
            return;

        Debug.Log("Mission Accomplished!");
        isGameOver = true;
        Time.timeScale = 0f;

        MissionAccomplishedPanel victoryPanel = Instantiate(missionAccomplishedPanelPrefab, uiOverlayCanvas.transform).GetComponent<MissionAccomplishedPanel>();
        victoryPanel.SetTitleText("Mission Accomplished!");
        victoryPanel.SetBodyText("The ship has successfully reached the new interstellar colony.");
    }

    public void TriggerDefeat()
    {
        if (isGameOver)
            return;

        Debug.Log("Mission Failed!");
        isGameOver = true;
        Time.timeScale = 0f;

        MissionAccomplishedPanel victoryPanel = Instantiate(missionAccomplishedPanelPrefab, uiOverlayCanvas.transform).GetComponent<MissionAccomplishedPanel>();
        victoryPanel.SetTitleText("Mission Failed!");
        victoryPanel.SetBodyText("The ship failed to reach the new interstellar colony.");
    }

    private void OnGUI()
    {
        if (!isHelpVisible)
        {
            GUILayout.Label("F1 - Help");
        } else
        {
            GUILayout.Label("Press F1 to hide help");
            GUILayout.Label("Scroll wheel to zoom camera in/out");
            GUILayout.Label("Numpad +/- to speed up or slow down simulation timescale.");
        }
        
    }
}
