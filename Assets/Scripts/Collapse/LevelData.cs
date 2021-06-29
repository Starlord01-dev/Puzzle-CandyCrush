using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class LevelData
{
    public string levelId;
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
        levelId = board.CurrentBoardId;

    Databoard = new int[width, height];

        for(int i = 0; i< board.width; i++)
        {
            for(int j = 0; j<board.height; j++)
            {
                switch (board.Popables[i, j].tag) 
                {
                    case "Obstacle":
                        Databoard[i, j] = 0;
                        break;
                    case "Coin":
                        Databoard[i, j] = 1;
                        break;
                    case "Beige":
                        Databoard[i, j] = 2;
                        break;
                    case "Black":
                        Databoard[i, j] = 3;
                        break;
                    case "BlancSalle":
                        Databoard[i, j] = 4;
                        break;
                    case "Blue":
                        Databoard[i, j] = 5;
                        break;
                    case "Bordaux":
                        Databoard[i, j] = 6;
                        break;
                    case "Brown":
                        Databoard[i, j] = 7;
                        break;
                    case "White":
                        Databoard[i, j] = 8;
                        break;
                    case "Turquoise":
                        Databoard[i, j] = 9;
                        break;
                    case "SkyBlue":
                        Databoard[i, j] = 10;
                        break;
                    case "Red":
                        Databoard[i, j] = 11;
                        break;
                    case "Purple":
                        Databoard[i, j] = 12;
                        break;
                    case "Pink":
                        Databoard[i, j] = 13;
                        break;
                    case "Orange":
                        Databoard[i, j] = 14;
                        break;
                    case "Yellow":
                        Databoard[i, j] = 15;
                        break;
                    case "Grass":
                        Databoard[i, j] = 16;
                        break;
                    case "Green":
                        Databoard[i, j] = 17;
                        break;
                    case "Grey":
                        Databoard[i, j] = 18;
                        break;
                    case "Lavander":
                        Databoard[i, j] = 19;
                        break;
                    case "Lime":
                        Databoard[i, j] = 20;
                        break;
                    case "Magenta":
                        Databoard[i, j] = 21;
                        break;
                    case "NavyBlue":
                        Databoard[i, j] = 22;
                        break;
                    case "NeonBlue":
                        Databoard[i, j] = 23;
                        break;
                    default:
                        Debug.LogError("Couldn't match " + board.Popables[i, j].tag + " with an existing tag");
                        break;
                }
            }
        }
    }

}
