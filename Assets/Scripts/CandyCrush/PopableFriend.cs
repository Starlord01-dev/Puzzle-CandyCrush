using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopableFriend : MonoBehaviour
{

    public int column;
    public int row;
    public int prevColumn;
    public int prevRow;
    public int targetx;
    public int targety;
    public bool matched = false;
    private GameObject otherPopable;
    private Board board;
    private Vector2 firstTouchPos;
    private Vector2 lastTouchPos;
    private Vector2 tempPos;
    public float angle = 0;
    public float swipeResist = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
        board = FindObjectOfType<Board>();
        targetx =(int)transform.position.x ;
        targety =(int)transform.position.y ;
        row = targety;
        column = targetx;
        prevColumn = column;
        prevRow = row;
    }

    // Update is called once per frame
    void Update()
    {
        FindMatch();
        if (matched)
        {
            SpriteRenderer thisSprite = GetComponent<SpriteRenderer>();
            thisSprite.color = new Color(1f, 1f, 1f, .2f);
        }

        targetx = column;
        targety = row;
        if(Mathf.Abs(targetx-transform.position.x) > .01)
        {
            //Move Popable
            tempPos = new Vector2(targetx, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPos, .02f);
            if(board.Popables[column, row] != this.gameObject)
            {
                board.Popables[column, row] = this.gameObject;
            }
        }
        else
        {
            //Directly set the pos
            tempPos = new Vector2(targetx, transform.position.y);
            transform.position = tempPos;
        }
        if (Mathf.Abs(targety - transform.position.y) > .01)
        {
            //Move Popable
            tempPos = new Vector2(transform.position.x, targety);
            transform.position = Vector2.Lerp(transform.position, tempPos, .06f);
            if (board.Popables[column, row] != this.gameObject)
            {
                board.Popables[column, row] = this.gameObject;
            }
        }
        else
        {
            //Directly set the pos
            tempPos = new Vector2(transform.position.x, targety);
            transform.position = tempPos;
        }
    }

    public IEnumerator CheckMoveCoroutine()
    {
        yield return new WaitForSeconds(.5f);
        if(otherPopable != null)
        {
            if (!matched && !otherPopable.GetComponent<PopableFriend>().matched)
            {
                otherPopable.GetComponent<PopableFriend>().row = row;
                otherPopable.GetComponent<PopableFriend>().column = column;
                row = prevRow;
                column = prevColumn;
            }
            else
            {
                board.DestroyMatches();
            }
            otherPopable = null;
        }
        
    }

    private void OnMouseDown()
    {
        firstTouchPos =Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        lastTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GetAngle();
    }

    void GetAngle()
    {
        //Check if the angle is bigger than 0 because if you click it registers as 0
        if (Mathf.Abs(lastTouchPos.y - firstTouchPos.y) > swipeResist || Mathf.Abs(lastTouchPos.x - firstTouchPos.x) > swipeResist)
        {
            angle = Mathf.Atan2(lastTouchPos.y - firstTouchPos.y, lastTouchPos.x - firstTouchPos.x) * 180 / Mathf.PI;
            MovePopable();
        }
    }

    void MovePopable()
    {
        

        if((angle > -45 && angle <= 45) && column < board.width-1)
        {
            //RightSwipe
            otherPopable = board.Popables[column + 1, row];
            otherPopable.GetComponent<PopableFriend>().column -= 1;
            column += 1;
        }else if((angle > 45 && angle <= 135) && row < board.height-1)
        {
            //UpSwipe
            otherPopable = board.Popables[column , row+1];
            otherPopable.GetComponent<PopableFriend>().row -= 1;
            row += 1;
        }
        else if ((angle > 135 || angle <= -135) && column>0)
        {
            //LeftSwipe
            otherPopable = board.Popables[column-1, row];
            otherPopable.GetComponent<PopableFriend>().column += 1;
            column -= 1;
        }
        else if ((angle < -45 && angle >= -135) && row >0)
        {
            //DownSwipe
            otherPopable = board.Popables[column, row - 1];
            otherPopable.GetComponent<PopableFriend>().row += 1;
            row -= 1;
        }
        StartCoroutine(CheckMoveCoroutine());
    }

    void FindMatch()
    {
        if (column > 0 && column < board.width - 1)
        {
            GameObject leftPopable1 = board.Popables[column - 1, row];
            GameObject rightPopable1 = board.Popables[column + 1, row];
            if (leftPopable1 != null && rightPopable1 != null) {
                if (leftPopable1.tag == this.gameObject.tag && rightPopable1.tag == this.gameObject.tag)
                {
                    leftPopable1.GetComponent<PopableFriend>().matched = true;
                    rightPopable1.GetComponent<PopableFriend>().matched = true;
                    matched = true;
                }
            }
        }
        if (row > 0 && row < board.height - 1)
        {
            GameObject downPopable1 = board.Popables[column, row-1];
            GameObject upPopable1 = board.Popables[column , row+1];
            if (downPopable1 != null && upPopable1 != null)
            {
                if (downPopable1.tag == this.gameObject.tag && upPopable1.tag == this.gameObject.tag)
                {
                    downPopable1.GetComponent<PopableFriend>().matched = true;
                    upPopable1.GetComponent<PopableFriend>().matched = true;
                    matched = true;
                }
            }
        }
    }

}
