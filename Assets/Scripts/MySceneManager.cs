using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{

    [SerializeField]
    private string preLevelName;
    public void GoToLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void GoToPreloadLevel()
    {
        SceneManager.LoadScene(preLevelName);
    }

    public void SetLevelName(string levelName)
    {
        preLevelName = levelName;
    }
}
