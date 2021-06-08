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

                int maxLoops = 0;
                while (MatchesAt(i, j, PopablesPrefab[pickRand]) && maxLoops < 100)
                {
                    pickRand = Random.Range(0, PopablesPrefab.Length);
                    maxLoops++;
                }
                maxLoops = 0;

                GameObject popable = Instantiate(PopablesPrefab[pickRand], Pos, Quaternion.identity) as GameObject;
                popable.name = "( " + i + ", " + j + " )";
                Popables[i, j] = popable;
            }
        }
    }

    private bool MatchesAt(int column, int row, GameObject popable)
    {
        if(column > 1 && row > 1)
        {
            if(popable.CompareTag(Popables[column - 1, row].tag) && popable.CompareTag(Popables[column - 2, row].tag))
            {
                return true;
            }
            if (popable.CompareTag(Popables[column, row - 1].tag) && popable.CompareTag(Popables[column, row - 2].tag))
            {
                return true;
            }
        }else if(column <= 1 || row <= 1)
        {
            if(row > 1)
            {
                if(popable.CompareTag(Popables[column,row-1].tag) && popable.CompareTag(Popables[column, row - 2].tag))
                {
                    return true;
                }
            }
            if(column > 1)
            {
                if (popable.CompareTag(Popables[column - 1, row].tag) && popable.CompareTag(Popables[column - 2, row].tag))
                {
                    return true;
                }
            }
        }

        return false;
    }

}
