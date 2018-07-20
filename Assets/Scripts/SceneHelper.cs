using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneHelper
{
    public enum SceneName
    {
        Start,
        Main,
    }

    public static void LoadScene(SceneName name)
    {
        SceneManager.LoadScene(name.ToString());
    }

    public static void LoadStartScene()
    {
        LoadScene(SceneName.Start);
    }

    public static void LoadMainScene()
    {
        LoadScene(SceneName.Main);
    }

    public static void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
