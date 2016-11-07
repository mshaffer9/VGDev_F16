using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//TODO: FIX RIGHT EDGE DETECTION
public class GrandmaGhostMove : Movement {

    private Queue<KeyCode> path;
    private int nSides;
    private int currPI;
    public bool isGhost;
    //private int[] leftEdges;
    private int[] rightEdges;

    void Start () {
        path = new Queue<KeyCode>();
        rightEdges = new int[nSides + 1];
        calcRightEdges();
    }

	void Update () {
        currPI = transform.parent.GetSiblingIndex();
        nSides = LevelManager.instance.numSides;


        //Moves with key presses
        if (isGhost)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (currPI >= nSides)
                {
                    transform.SetParent(transform.parent.parent.GetChild(currPI - nSides));
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                if ((currPI > 0) && (currPI % nSides != 0))
                {
                    transform.SetParent(transform.parent.parent.GetChild(currPI - 1));
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (currPI <= (nSides*nSides - nSides))
                {
                    transform.SetParent(transform.parent.parent.GetChild(currPI + nSides));
                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if ((currPI < (nSides*nSides - 1)) && !onRightEdges(currPI))
                {
                    transform.SetParent(transform.parent.parent.GetChild(currPI + 1));
                }
            }
        } else
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                path.Enqueue(KeyCode.W);

            }
            if (Input.GetKeyDown(KeyCode.A))
            {

                path.Enqueue(KeyCode.A);

            }
            if (Input.GetKeyDown(KeyCode.S))
            {

                path.Enqueue(KeyCode.S);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {

                path.Enqueue(KeyCode.D);

            }
        }

    }

    public override void Move()
    {
        //Debug.Log("GRANNY GOTTA MOVE");
        if (path.Count > 0)
        {
            KeyCode key = path.Dequeue();
            if (key == KeyCode.W)
            {
                transform.SetParent(transform.parent.parent.GetChild(currPI - nSides));
            }
            else if (key == KeyCode.A)
            {
                transform.SetParent(transform.parent.parent.GetChild(currPI - 1));
            }
            else if (key == KeyCode.S)
            {
                transform.SetParent(transform.parent.parent.GetChild(currPI + nSides));
            }
            else if (key == KeyCode.D)
            {
                transform.SetParent(transform.parent.parent.GetChild(currPI + 1));
            }
        }
    }

    private void calcRightEdges()
    {
        //Debug.Log("calcing right edges");
        for (int i = 0; i <= nSides; i++)
        {
            rightEdges[i] = (i * nSides) - 1;
        }
    }

    public bool onRightEdges(int index)
    {
        //Debug.Log("checking right edges");
        bool toReturn = false;
        foreach (int x in rightEdges) {
            toReturn = (index == x);
        }

        return toReturn;
    }

}
