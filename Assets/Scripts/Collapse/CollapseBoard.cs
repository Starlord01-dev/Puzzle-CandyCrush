using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapseBoard : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject TilePrefab;
    public GameObject[] PopablesPrefab;
    private BackgroundTile[,] board;
    public GameObject[,] Popables;
    private Vector2 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        board = new BackgroundTile[width, height];
        Popables = new GameObject[width, height];
        Create_Board();

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Match((int)Mathf.Round(mousePos.y), (int)Mathf.Round(mousePos.x));
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
                int pickRand = Random.Range(0, PopablesPrefab.Length);

                GameObject popable = Instantiate(PopablesPrefab[pickRand], Pos, Quaternion.identity) as GameObject;
                popable.name = "( " + i + ", " + j + " )";
                Popables[i, j] = popable;
            }
        }
    }

    public void Match(int row, int column)
    {
        if (Popables[column, row] != null)
        {
            Popables[column, row].GetComponent<Collapse>().matched = true;
            if ((column < width - 1 && row < height - 1) && (column > 0 && row > 0))
            {
                Popables[column, row - 1].GetComponent<Collapse>().isMatch(Popables[column, row]);
                Popables[column - 1, row].GetComponent<Collapse>().isMatch(Popables[column, row]);
                Popables[column, row + 1].GetComponent<Collapse>().isMatch(Popables[column, row]);
                Popables[column + 1, row].GetComponent<Collapse>().isMatch(Popables[column, row]);
            }
            else if (column == width - 1)
            {
                if (row == height - 1)
                {
                    Popables[column, row - 1].GetComponent<Collapse>().isMatch(Popables[column, row]);
                    Popables[column - 1, row].GetComponent<Collapse>().isMatch(Popables[column, row]);
                }
                else
                {
                    if (row == 0)
                    {
                        Popables[column - 1, row].GetComponent<Collapse>().isMatch(Popables[column, row]);
                        Popables[column, row + 1].GetComponent<Collapse>().isMatch(Popables[column, row]);
                    }
                    else
                    {
                        Popables[column, row - 1].GetComponent<Collapse>().isMatch(Popables[column, row]);
                        Popables[column - 1, row].GetComponent<Collapse>().isMatch(Popables[column, row]);
                        Popables[column, row + 1].GetComponent<Collapse>().isMatch(Popables[column, row]);
                    }
                }
            }
            else if (column == 0)
            {
                if (row == height - 1)
                {
                    Popables[column + 1, row].GetComponent<Collapse>().isMatch(Popables[column, row]);
                    Popables[column, row - 1].GetComponent<Collapse>().isMatch(Popables[column, row]);
                }
                else
                {
                    if (row == 0)
                    {
                        Popables[column, row + 1].GetComponent<Collapse>().isMatch(Popables[column, row]);
                        Popables[column + 1, row].GetComponent<Collapse>().isMatch(Popables[column, row]);
                    }
                    else
                    {
                        Popables[column, row + 1].GetComponent<Collapse>().isMatch(Popables[column, row]);
                        Popables[column + 1, row].GetComponent<Collapse>().isMatch(Popables[column, row]);
                        Popables[column, row - 1].GetComponent<Collapse>().isMatch(Popables[column, row]);
                    }
                }
            }
            else if (column < width - 1)
            {
                if (row == 0)
                {
                    Popables[column, row + 1].GetComponent<Collapse>().isMatch(Popables[column, row]);
                    Popables[column + 1, row].GetComponent<Collapse>().isMatch(Popables[column, row]);
                    Popables[column - 1, row].GetComponent<Collapse>().isMatch(Popables[column, row]);
                }
                else if (row == height - 1)
                {
                    Popables[column + 1, row].GetComponent<Collapse>().isMatch(Popables[column, row]);
                    Popables[column - 1, row].GetComponent<Collapse>().isMatch(Popables[column, row]);
                    Popables[column, row - 1].GetComponent<Collapse>().isMatch(Popables[column, row]);
                }
            }
        }
    }
}
