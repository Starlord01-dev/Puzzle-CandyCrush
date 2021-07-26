using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapseBoard : MonoBehaviour
{
    public int width;
    public int height;
    public int Score;
    private int numbOfBlocksPoped;

    public GameObject Explode;

    public GameObject CoinPrefab;
    public GameObject TilePrefab;
    public GameObject ObstaclePrefab;
    public GameObject[] PopablesPrefab;
    private BackgroundTile[,] board;
    public GameObject[,] Popables;
    private Vector2 mousePos;
    public int coinsCollected;
    private bool withDiagonal = false;

    void Start()
    {
        board = new BackgroundTile[width, height];
        Popables = new GameObject[width, height];
        numbOfBlocksPoped = 0;
        Score = 0;
        coinsCollected = 0;
        Create_Board();

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            try
            {
                if (!Popables[(int)Mathf.Round(mousePos.x), (int)Mathf.Round(mousePos.y)].CompareTag("Obstacle") && !Popables[(int)Mathf.Round(mousePos.x), (int)Mathf.Round(mousePos.y)].CompareTag("Coin"))
                    {
                        Match((int)Mathf.Round(mousePos.y), (int)Mathf.Round(mousePos.x));
                    DestroyMatches();
                }
            }
            catch { }
            
            if(numbOfBlocksPoped > 2 && numbOfBlocksPoped < 5)
            {
                Score += 10 * numbOfBlocksPoped;
            }else if(numbOfBlocksPoped >= 5 && numbOfBlocksPoped < 8)
            {
                Score += 20 * numbOfBlocksPoped;
            }else if(numbOfBlocksPoped <= 2 && numbOfBlocksPoped > 0)
            {
                Score -= 50;
            }
            else
            {
                Score += 30 * numbOfBlocksPoped;
            }
            numbOfBlocksPoped = 0;
        }
    }

    private void Create_Board()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                int coinpick = Random.Range(0,100);
                if (coinpick < 5)
                {
                    Vector3 Pos = new Vector3(i, j, 0);
                    GameObject backgroundTile = Instantiate(TilePrefab, Pos, Quaternion.identity) as GameObject;
                    backgroundTile.name = "( " + i + ", " + j + " )";

                    GameObject popable = Instantiate(CoinPrefab, Pos, Quaternion.identity) as GameObject;
                    popable.name = "( " + i + ", " + j + " )";
                    Popables[i, j] = popable;
                }else if( coinpick>5 && coinpick < 7)
                {
                    Vector3 Pos = new Vector3(i, j, 0);
                    GameObject backgroundTile = Instantiate(TilePrefab, Pos, Quaternion.identity) as GameObject;
                    backgroundTile.name = "( " + i + ", " + j + " )";

                    GameObject popable = Instantiate(ObstaclePrefab, Pos, Quaternion.identity) as GameObject;
                    popable.name = "( " + i + ", " + j + " )";
                    Popables[i, j] = popable;
                }
                else
                {
                    Vector3 Pos = new Vector3(i, j, 0);
                    GameObject backgroundTile = Instantiate(TilePrefab, Pos, Quaternion.identity) as GameObject;
                    backgroundTile.name = "( " + i + ", " + j + " )";
                    int pickRand = Random.Range(0, PopablesPrefab.Length);

                    GameObject popable = Instantiate(PopablesPrefab[pickRand], Pos, Quaternion.identity) as GameObject;
                    popable.name = "( " + i + ", " + j + " )";
                    Popables[i, j] = popable;
                }
            }
        }
    }

    public void Match(int row, int column)
    {

        Popables[column, row].GetComponent<Collapse>().matched = true;

        try
        {
            Popables[column, row - 1].GetComponent<Collapse>().isMatch(Popables[column, row]);
        }
        catch { }

        try
        {
            Popables[column, row + 1].GetComponent<Collapse>().isMatch(Popables[column, row]);
        }
        catch { }

        try
        {
            Popables[column - 1, row].GetComponent<Collapse>().isMatch(Popables[column, row]);
        }
        catch { }

        try
        {
            Popables[column + 1, row].GetComponent<Collapse>().isMatch(Popables[column, row]);
        }
        catch { }


        if (withDiagonal)
        {
            try
            {
                Popables[column + 1, row + 1].GetComponent<Collapse>().isMatch(Popables[column, row]);
            }
            catch { }

            try
            {
                Popables[column + 1, row - 1].GetComponent<Collapse>().isMatch(Popables[column, row]);
            }
            catch { }

            try
            {
                Popables[column - 1, row + 1].GetComponent<Collapse>().isMatch(Popables[column, row]);
            }
            catch { }

            try
            {
                Popables[column - 1, row - 1].GetComponent<Collapse>().isMatch(Popables[column, row]);
            }
            catch { }
        }
    }

    private void DestroyMatchesAt(int column, int row)
    {
        Color tempColor = Popables[column, row].GetComponent<SpriteRenderer>().color;
        if (Popables[column, row].GetComponent<Collapse>().matched)
        {
            StartCoroutine(StartExplosion(column, row, tempColor));
            Destroy(Popables[column, row]);
            Popables[column, row] = null;
            numbOfBlocksPoped++;
        }
    }

    public void DestroyMatches()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (Popables[i, j] != null)
                {
                    if (Popables[i, j].GetComponent<Collapse>().matched)
                    {
                        DestroyMatchesAt(i, j);
                    }
                }
            }
        }
        StartCoroutine(DecreaseRowCoroutine());
        StartCoroutine(DecreaseColumnCoroutine());
    }

    private IEnumerator DecreaseRowCoroutine()
    {
        for (int i = 0; i < width; i++)
        {
            int nullCount = 0;
            for (int j = 0; j < height; j++)
            {
                if (Popables[i, j] == null)
                {
                    nullCount++;
                }
                else if (nullCount > 0)
                {
                    Popables[i, j].GetComponent<Collapse>().row -= nullCount;
                    Popables[i, j - nullCount] = Popables[i, j];
                    Popables[i, j - nullCount].transform.position = new Vector2(Popables[i, j].GetComponent<Collapse>().column, Popables[i, j].GetComponent<Collapse>().row);
                    Popables[i, j - nullCount].tag = Popables[i, j].tag;
                    Popables[i, j] = null;
                }
            }
        }
        yield return new WaitForSeconds(.4f);
        
    }

    private IEnumerator DecreaseColumnCoroutine()
    {
        for(int k = 0; k < width; k++)
        {
            try
            {
                if (Popables[k, 0].CompareTag("Coin"))
                {
                    Popables[k, 0].GetComponent<Collapse>().matched = true;
                    DestroyMatches();
                    coinsCollected++;
                }
                if(Popables[k, 1].CompareTag("Coin") && Popables[k, 0].CompareTag("Obstacle"))
                {
                    Popables[k, 1].GetComponent<Collapse>().matched = true;
                    DestroyMatches();
                    coinsCollected++;
                }
            }
            catch { }
            if (Popables[k,0] == null)
            {
                for(int i=k-1;i>-1;i--)
                {
                    for(int j=0; j < height; j++)
                    {
                        if(Popables[i, j] != null)
                        {
                            Popables[i, j].GetComponent<Collapse>().column += 1;
                            Popables[i + 1 , j] = Popables[i, j];
                            Popables[i + 1 , j].transform.position = new Vector2(Popables[i, j].GetComponent<Collapse>().column, Popables[i, j].GetComponent<Collapse>().row);
                            Popables[i + 1, j].tag = Popables[i, j].tag;
                            Popables[i, j] = null;
                        }
                    }
                }
            }
        }
        yield return new WaitForSeconds(.6f);
    }

    IEnumerator StartExplosion(float x, float y, Color tempColor)
    {
        GameObject tempExplosion = Instantiate(Explode, new Vector2(x, y), Quaternion.identity);
        ParticleSystem.MainModule mainMod = tempExplosion.GetComponent<ParticleSystem>().main;
        mainMod.startColor = tempColor;
        tempExplosion.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(3);
        Destroy(tempExplosion);
    }

}
