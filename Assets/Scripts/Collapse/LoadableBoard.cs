using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LoadableBoard : MonoBehaviour
{
    public string LoadBoardId;
    public string path;
    public int width;
    public int height;
    public int Score;
    private int numbOfBlocksPoped;
    public int coinsCollected;
    private int selectedObject;

    public GameObject TilePrefab;
    private BackgroundTile[,] board;
    public GameObject[,] Popables;
    public GameObject[] SelectablesPrefabs;

    private Vector2 mousePos;


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
                    }
                }
                catch { Debug.Log("Error at trying to match"); }

                DestroyMatches();
                if (numbOfBlocksPoped > 2 && numbOfBlocksPoped < 5)
                {
                    Score += 10 * numbOfBlocksPoped;
                }
                else if (numbOfBlocksPoped >= 5 && numbOfBlocksPoped < 8)
                {
                    Score += 20 * numbOfBlocksPoped;
                }
                else if (numbOfBlocksPoped <= 2 && numbOfBlocksPoped > 0)
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

    public void Match(int row, int column)
    {

        Popables[column, row].GetComponent<EditorCollaps>().matched = true;
        try
        {
            Popables[column, row - 1].GetComponent<EditorCollaps>().isMatch(Popables[column, row]);
        }
        catch { }

        try
        {
            Popables[column, row + 1].GetComponent<EditorCollaps>().isMatch(Popables[column, row]);
        }
        catch { }
        try
        {
            Popables[column - 1, row].GetComponent<EditorCollaps>().isMatch(Popables[column, row]);
        }
        catch { }

        try
        {
            Popables[column + 1, row].GetComponent<EditorCollaps>().isMatch(Popables[column, row]);
        }
        catch { }


    }

    private void DestroyMatchesAt(int column, int row)
    {
        if (Popables[column, row].GetComponent<EditorCollaps>().matched)
        {
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
                    if (Popables[i, j].GetComponent<EditorCollaps>().matched)
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
                    Popables[i, j].GetComponent<EditorCollaps>().row -= nullCount;
                    Popables[i, j - nullCount] = Popables[i, j];
                    Popables[i, j - nullCount].transform.position = new Vector2(Popables[i, j].GetComponent<EditorCollaps>().column, Popables[i, j].GetComponent<EditorCollaps>().row);
                    Popables[i, j - nullCount].tag = Popables[i, j].tag;
                    Popables[i, j] = null;
                }
            }
        }
        yield return new WaitForSeconds(.4f);

    }

    private IEnumerator DecreaseColumnCoroutine()
    {
        for (int k = 0; k < width; k++)
        {
            try
            {
                if (Popables[k, 0].CompareTag("Coin"))
                {
                    Popables[k, 0].GetComponent<EditorCollaps>().matched = true;
                    DestroyMatches();
                    coinsCollected++;
                }
                if (Popables[k, 1].CompareTag("Coin") && Popables[k, 0].CompareTag("Obstacle"))
                {
                    Popables[k, 1].GetComponent<EditorCollaps>().matched = true;
                    DestroyMatches();
                    coinsCollected++;
                }
            }
            catch { }
            if (Popables[k, 0] == null)
            {
                for (int i = k - 1; i > -1; i--)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (Popables[i, j] != null)
                        {
                            Popables[i, j].GetComponent<EditorCollaps>().column += 1;
                            Popables[i + 1, j] = Popables[i, j];
                            Popables[i + 1, j].transform.position = new Vector2(Popables[i, j].GetComponent<EditorCollaps>().column, Popables[i, j].GetComponent<EditorCollaps>().row);
                            Popables[i + 1, j].tag = Popables[i, j].tag;
                            Popables[i, j] = null;
                        }
                    }
                }
            }
        }
        yield return new WaitForSeconds(.6f);
    }


    public void LoadBoard()
    {
        LevelData data = SaveSystem.LoadLevel(this);

        GameObject map = GameObject.Find("WorldMap");
        map.GetComponent<Renderer>().enabled = false;

        width = data.width;
        height = data.height;
        Popables = new GameObject[width, height];
        Score = data.score;
        coinsCollected = data.coinsCollected;


        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                for (int k = 0; k < SelectablesPrefabs.Length; k++)
                {
                    switch (data.Databoard[i, j])
                    {
                        case 1:
                            try
                            {
                                Destroy(Popables[i, j]);
                            }
                            catch { }
                            Popables[i, j] = Instantiate(SelectablesPrefabs[0], new Vector2(i, j), Quaternion.identity);
                            break;
                        case 2:
                            try
                            {
                                Destroy(Popables[i, j]);
                            }
                            catch { }
                            Popables[i, j] = Instantiate(SelectablesPrefabs[1], new Vector2(i, j), Quaternion.identity);
                            break;
                        case 3:
                            try
                            {
                                Destroy(Popables[i, j]);
                            }
                            catch { }
                            Popables[i, j] = Instantiate(SelectablesPrefabs[2], new Vector2(i, j), Quaternion.identity);
                            break;
                        case -1:
                            try
                            {
                                Destroy(Popables[i, j]);
                            }
                            catch { }
                            Popables[i, j] = Instantiate(SelectablesPrefabs[3], new Vector2(i, j), Quaternion.identity);
                            break;
                        case 0:
                            try
                            {
                                Destroy(Popables[i, j]);
                            }
                            catch { }
                            Popables[i, j] = Instantiate(SelectablesPrefabs[4], new Vector2(i, j), Quaternion.identity);
                            break;
                        default:
                            Debug.LogError("Couldn't instatiate object.");
                            break;
                    }
                }
            }
        }
    }

    public void SetLoadBoardId(string id)
    {
        LoadBoardId = id;
    }

        
}