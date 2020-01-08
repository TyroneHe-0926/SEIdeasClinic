using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializeOnLoad
{
    [RuntimeInitializeOnLoadMethod]
    public static void Setup()
    {
        SceneManager.LoadScene("GameCore");
    }
}
