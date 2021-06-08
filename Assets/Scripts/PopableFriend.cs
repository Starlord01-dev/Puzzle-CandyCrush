using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopableFriend : MonoBehaviour
{

    public int column;
    public int row;
    public int targetx;
    public int targety;
    private GameObject otherPopable;
    private Board board;
    private Vector2 firstTouchPos;
    private Vector2 lastTouchPos;
    private Vector2 tempPos;
    public float angle = 0;

    // Start is called before the first frame update
    void Start()
    {
        
        board = FindObjectOfType<Board>();
        targetx =(int)transform.position.x ;
        targety =(int)transform.position.y ;
        row = targety;
        column = targetx;
    }

    // Update is called once per frame
    void Update()
    {
        targetx = column;
        targety = row;
        if(Mathf.Abs(targetx-transform.position.x) > .01)
        {
            //Move Popable
            tempPos = new Vector2(targetx, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPos, .06f);
        }
        else
        {
            //Directly set the pos
            tempPos = new Vector2(targetx, transform.position.y);
            transform.position = tempPos;
            board.Popables[column, row] = this.gameObject;
        }
        if (Mathf.Abs(targety - transform.position.y) > .01)
        {
            //Move Popable
            tempPos = new Vector2(transform.position.x, targety);
            transform.position = Vector2.Lerp(transform.position, tempPos, .06f);
        }
        else
        {
            //Directly set the pos
            tempPos = new Vector2(transform.position.x, targety);
            transform.position = tempPos;
            board.Popables[column, row] = this.gameObject;
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
        angle = Mathf.Atan2(lastTouchPos.y - firstTouchPos.y, lastTouchPos.x - firstTouchPos.x)*180/Mathf.PI;
        MovePopable();
    }

    void MovePopable()
    {
        if((angle > -45 && angle <= 45) && column < board.width)
        {
            //RightSwipe
            otherPopable = board.Popables[column + 1, row];
            otherPopable.GetComponent<PopableFriend>().column -= 1;
            column += 1;
        }else if((angle > 45 && angle <= 135) && row < board.height)
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
    }

}
