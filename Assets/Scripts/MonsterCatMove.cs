using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterCatMove : Movement
{
    private int nSides;
    private int currPI;
    private int moveCount = 0;

    public int dist; //how many squares ya move son????
    public bool upDown = true; //where ya goin my dude???
    public bool movePosi = true; //ya starting right and up???

    void Start()
    {

    }

    void Update()
    {
        currPI = transform.parent.GetSiblingIndex();
        nSides = LevelManager.instance.numSides;
    }

    public override void Move()
    {
        //Debug.Log("GOTTA GO");
        if (upDown)
        {
            if (movePosi) //goin up
            {
                if (moveCount < dist)
                {
                    if (currPI >= nSides)
                    {
                        transform.SetParent(transform.parent.parent.GetChild(currPI - nSides));
                        moveCount++;
                    } else
                    {
                        movePosi = !movePosi;
                        moveCount = 0;
                    }
                } else if (moveCount == dist)
                {
                    movePosi = !movePosi;
                    moveCount = 0;
                }
            } else //goin down
            {
                if (moveCount < dist)
                {
                    if (currPI <= (nSides * nSides - nSides))
                    {
                        transform.SetParent(transform.parent.parent.GetChild(currPI + nSides));
                        moveCount++;
                    }
                } else if (moveCount == dist)
                {
                    movePosi = !movePosi;
                    moveCount = 0;
                }
            }

        } else //going leftRight
        {
            if (movePosi) //goin right
            {
                if (moveCount < dist)
                {
                    transform.SetParent(transform.parent.parent.GetChild(currPI + 1));
                    moveCount++;
                } else if (moveCount == dist)
                {
                    movePosi = !movePosi;
                    moveCount = 0;
                }
            } else //goin left
            {
                if (moveCount < dist)
                {
                    transform.SetParent(transform.parent.parent.GetChild(currPI - 1));
                    moveCount++;
                } else if (moveCount == dist)
                {
                    movePosi = !movePosi;
                    moveCount = 0;
                }
            }
        }
    }

}
