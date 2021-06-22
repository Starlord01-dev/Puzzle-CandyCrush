using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorCollaps : MonoBehaviour
{
    public int column;
    public int row;
    public bool matched = false;
    private EditorBoard board;

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<EditorBoard>();
        column = (int)transform.position.x;
        row = (int)transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (matched)
        {
            SpriteRenderer thisSprite = GetComponent<SpriteRenderer>();
            thisSprite.color = new Color(1f, 1f, 1f, .2f);
        }
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
