using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class LevelData
{
    int width;
    int height;
    int[,] Databoard;

    LevelData(EditorBoard board)
    {
        width = board.width;
        height = board.height;

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
                    case "RedEyesBox":
                        Databoard[i, j] = 2;
                        break;
                    case "PyramidBlue":
                        Databoard[i, j] = 3;
                        break;
                    case "Coin":
                        Databoard[i, j] = 0;
                        break;
                    case "Obstacle":
                        Databoard[i, j] = -1;
                        break;
                }
            }
        }
    }

}
