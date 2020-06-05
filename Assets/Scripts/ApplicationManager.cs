using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ApplicationManager
{
    public static void LoadScene(int sceneIndex ) {
        SceneManager.LoadScene ( sceneIndex );
    }
    public static void LoadScene(string sceneName ) {
        SceneManager.LoadScene ( sceneName );
    }
}
