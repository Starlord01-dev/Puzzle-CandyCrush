using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collapse : MonoBehaviour
{

    public bool matched = false;
    private CollapseBoard board;

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<CollapseBoard>();
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
