using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCollapse : MonoBehaviour
{
    public int column;
    public int row;
    public bool matched = false;
    private LoadableBoard board;


    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<LoadableBoard>();
        column = (int)transform.position.x;
        row = (int)transform.position.y;
    }

    public void isMatch(GameObject popable)
    {
        if (matched == false)
        {
            if (gameObject.CompareTag(popable.tag))
            {
                matched = true;
                board.Match((int)Mathf.Round(transform.position.y), (int)Mathf.Round(transform.position.x));
            }
        }
    }


}
