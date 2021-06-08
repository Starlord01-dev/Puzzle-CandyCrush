using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject TilePrefab;
    public GameObject[] PopablesPrefab;
    private BackgroundTile[,] board;
    public GameObject[,] Popables;
    // Start is called before the first frame update
    void Start()
    {
        board = new BackgroundTile[width, height];
        Popables = new GameObject[width, height];
        Create_Board();
    }

    private void Create_Board()
    {
        for(int i = 0; i < width; i++)
        {
            for(int j=0; j<height; j++)
            {
                Vector3 Pos = new Vector3(i, j,0);
                GameObject backgroundTile = Instantiate(TilePrefab, Pos, Quaternion.identity) as GameObject;
                backgroundTile.name = "( " + i +", "+ j + " )";
                int pickRand = Random.Range(0, PopablesPrefab.Length);
                GameObject popable = Instantiate(PopablesPrefab[pickRand], Pos, Quaternion.identity) as GameObject;
                popable.name = "( " + i + ", " + j + " )";
                Popables[i, j] = popable;
            }
        }
    }
}
