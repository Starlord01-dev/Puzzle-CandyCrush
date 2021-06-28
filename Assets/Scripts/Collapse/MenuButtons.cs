using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void GoToWorldMap()
    {
        SceneManager.LoadScene(sceneName: "WorldMap");
    }

    public void GoToSandBox()
    {
        SceneManager.LoadScene(sceneName: "Sandbox");
    }

    public void GoToCasual()
    {
        SceneManager.LoadScene(sceneName: "Casual");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(sceneName: "MainMenu");
    }
}
