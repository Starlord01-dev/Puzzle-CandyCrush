using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TurnOffButton : MonoBehaviour
{
    public Button[] loadButton;
    public string LevelId;
    public LoadableBoard board;


    public void TurnOffButtonText()
    {
        for (int i = 0; i < loadButton.Length; i++)
        {
            loadButton[i].gameObject.SetActive(!loadButton[i].gameObject.activeSelf);
        }
        GameObject map = GameObject.Find("WorldMap");
        map.GetComponent<Renderer>().enabled = false;
        board.LoadBoardId = LevelId;
        board.LoadBoard();
    }

    public void TurnOnButton()
    {
        for (int i = 0; i < board.width; i++)
        {
            for (int j = 0; j < board.height; j++)
            {
                try
                {
                    Destroy(board.Popables[i, j]);
                }
                catch { }
            }
        }
        for (int i = 0; i < loadButton.Length; i++)
        {
            loadButton[i].gameObject.SetActive(!loadButton[i].gameObject.activeSelf);
        }
        GameObject map = GameObject.Find("WorldMap");
        map.GetComponent<Renderer>().enabled = true;
        
    }

}
