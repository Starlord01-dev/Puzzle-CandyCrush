using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class LevelData
{
    public int width;
    public int height;
    public int score;
    public int coinsCollected;
    public int[,] Databoard;
    public bool editMode;
    public bool destroy;
    /*public GameObject TilePrefab;
    public GameObject[] SelectablesPrefabs;
    private BackgroundTile[,] board;*/

    public LevelData(EditorBoard board)
    {
        width = board.width;
        height = board.height;
        score = board.Score;
        coinsCollected = board.coinsCollected;
        editMode = board.editMode;
        destroy = board.destroy;

    Databoard = new int[width, height];

        for(int i = 0; i< board.width; i++)
        {
            for(int j = 0; j<board.height; j++)
            {
                switch (board.Popables[i, j].tag) 
                {
                    case "BlueEyesBall":
                        Databoard[i, j] = 1;
                        break;
                    case "BluePyramid":
                        Databoard[i, j] = 2;
                        break;
                    case "RedEyesBox":
                        Databoard[i, j] = 3;
                        break;
                    case "Coin":
                        Databoard[i, j] = 0;
                        break;
                    case "Obstacle":
                        Databoard[i, j] = -1;
                        break;
                    default:
                        Debug.LogError("Couldn't match " + board.Popables[i, j].tag + " with an existing tag");
                        break;
                }
            }
        }
    }

}
