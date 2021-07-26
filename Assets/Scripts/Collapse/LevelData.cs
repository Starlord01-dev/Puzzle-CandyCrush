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
                if (board.Popables[i, j] != null)
                {
                    switch (board.Popables[i, j].tag)
                    {
                        case "Obstacle":
                            Databoard[i, j] = 0;
                            break;
                        case "Coin":
                            Databoard[i, j] = 1;
                            break;
                        case "White":
                            Databoard[i, j] = 2;
                            break;
                        case "Red":
                            Databoard[i, j] = 3;
                            break;
                        case "LightRed":
                            Databoard[i, j] = 4;
                            break;
                        case "DarkRed":
                            Databoard[i, j] = 5;
                            break;
                        case "Blue":
                            Databoard[i, j] = 6;
                            break;
                        case "LightBlue":
                            Databoard[i, j] = 7;
                            break;
                        case "DarkBlue":
                            Databoard[i, j] = 8;
                            break;
                        case "Green":
                            Databoard[i, j] = 9;
                            break;
                        case "LightGreen":
                            Databoard[i, j] = 10;
                            break;
                        case "DarkGreen":
                            Databoard[i, j] = 11;
                            break;
                        case "Yellow":
                            Databoard[i, j] = 12;
                            break;
                        case "LightYellow":
                            Databoard[i, j] = 13;
                            break;
                        case "DarkYellow":
                            Databoard[i, j] = 14;
                            break;
                        case "Orange":
                            Databoard[i, j] = 15;
                            break;
                        case "LightOrange":
                            Databoard[i, j] = 16;
                            break;
                        case "DarkOrange":
                            Databoard[i, j] = 17;
                            break;
                        case "Brown":
                            Databoard[i, j] = 18;
                            break;
                        case "LightBrown":
                            Databoard[i, j] = 19;
                            break;
                        case "DarkBrown":
                            Databoard[i, j] = 20;
                            break;
                        case "Purple":
                            Databoard[i, j] = 21;
                            break;
                        case "LightPurple":
                            Databoard[i, j] = 22;
                            break;
                        case "DarkPurple":
                            Databoard[i, j] = 23;
                            break;
                        case "Pink":
                            Databoard[i, j] = 24;
                            break;
                        case "LightPink":
                            Databoard[i, j] = 25;
                            break;
                        case "DarkPink":
                            Databoard[i, j] = 26;
                            break;
                        case "Grey":
                            Databoard[i, j] = 27;
                            break;
                        case "Black":
                            Databoard[i, j] = 28;
                            break;
                        default:
                            Debug.LogError("Couldn't match " + board.Popables[i, j].tag + " with an existing tag");
                            break;
                    }
                }
                else
                {
                    Databoard[i, j] = -1;
                }
            }
        }
    }

}
