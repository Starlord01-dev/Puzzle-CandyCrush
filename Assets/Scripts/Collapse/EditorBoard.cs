using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EditorBoard : MonoBehaviour
{
    public int width;
    public int height;
    public int Score;
    private int numbOfBlocksPoped;
    public int coinsCollected;
    private int selectedObject;

    public GameObject TilePrefab;
    public GameObject[] SelectablesPrefabs;
    private BackgroundTile[,] board;
    public GameObject[,] Popables;

    private Vector2 mousePos;

    public bool editMode = true;
    public bool destroy = false;
    

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
            if (mousePos.y < 0)
            {
                RaycastHit2D selectHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (selectHit.collider != null)
                {
                    for (int i = 0; i < SelectablesPrefabs.Length; i++)
                    {
                        if (selectHit.collider.gameObject.CompareTag(SelectablesPrefabs[i].tag))
                        {
                            selectedObject = i;
                            Debug.Log("Selected " + SelectablesPrefabs[selectedObject].tag);
                        }
                    }
                }
            }else if (editMode)
                {
                if (!destroy)
                {
                    if (((int)Mathf.Round(mousePos.x) < width + 1 && (int)Mathf.Round(mousePos.y) < height + 1))
                    {
                        if (Popables[(int)Mathf.Round(mousePos.x), (int)Mathf.Round(mousePos.y)] == null)
                        {
                            Popables[(int)Mathf.Round(mousePos.x), (int)Mathf.Round(mousePos.y)] = Instantiate(SelectablesPrefabs[selectedObject], new Vector2((int)Mathf.Round(mousePos.x), (int)Mathf.Round(mousePos.y)), Quaternion.identity);
                            Popables[(int)Mathf.Round(mousePos.x), (int)Mathf.Round(mousePos.y)].name = "( " + (int)Mathf.Round(mousePos.x) + " " + (int)Mathf.Round(mousePos.y) + " )";
                        }
                    }
                }else if (destroy)
                {
                    Destroy(Popables[(int)Mathf.Round(mousePos.x), (int)Mathf.Round(mousePos.y)]);
                    Popables[(int)Mathf.Round(mousePos.x), (int)Mathf.Round(mousePos.y)] = null;
                }
                }
                else
                {
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
    }

    private void Create_Board()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector3 Pos = new Vector3(i, j, 0);
                GameObject backgroundTile = Instantiate(TilePrefab, Pos, Quaternion.identity) as GameObject;
                backgroundTile.name = "( " + i + ", " + j + " )";
            }
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

}
