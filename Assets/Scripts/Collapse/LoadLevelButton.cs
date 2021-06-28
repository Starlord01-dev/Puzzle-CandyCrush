using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadLevelButton : MonoBehaviour
{
    public string LevelId;
    public void LoadLevel()
    {
        GameManager.instance.LevelId = LevelId;
        SceneManager.LoadScene(sceneName: "LoadedBoard");
    }

    public void BackToWorldMap()
    {
        SceneManager.LoadScene(sceneName: "WorldMap");
    }
}
