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

    private void DestroyMatchesAt(int column, int row)
    {
        if(Popables[column, row].GetComponent<PopableFriend>().matched)
        {
            Destroy(Popables[column, row]);
            Popables[column, row] = null;
        }
    }

    public void DestroyMatches()
    {
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j< height; j++)
            {
                if (Popables[i, j] != null)
                {
                    DestroyMatchesAt(i, j);
                }
            }
        }
        StartCoroutine(DecreaseRowCoroutine());
    }

    private IEnumerator DecreaseRowCoroutine()
    {
        int nullCount = 0;
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                if (Popables[i, j] == null)
                {
                    nullCount++;
                }else if(nullCount > 0)
                {
                    Popables[i, j].GetComponent<PopableFriend>().row -= nullCount;
                    Popables[i, j] = null;
                }
            }
            nullCount = 0;
        }
        yield return new WaitForSeconds(.4f);
        StartCoroutine(FillBoardCoroutine());
    }

    private void RefillBoard()
    {
        for(int i = 0; i< width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                if(Popables[i,j] == null)
                {
                    Vector2 tempPos = new Vector2(i, j);
                    int randomPick = Random.Range(0, PopablesPrefab.Length);
                    GameObject popable = Instantiate(PopablesPrefab[randomPick], tempPos, Quaternion.identity);
                    Popables[i, j] = popable;
                }
            }
        }
    }

    private bool MatchesOnBoard()
    {
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                if(Popables[i, j] != null)
                {
                    if(Popables[i, j].GetComponent<PopableFriend>().matched)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private IEnumerator FillBoardCoroutine()
    {
        RefillBoard();
        yield return new WaitForSeconds(.5f);

        while(MatchesOnBoard()){
            yield return new WaitForSeconds(.5f);
            DestroyMatches();
        }
    }

}
