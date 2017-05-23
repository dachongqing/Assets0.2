using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Node : IComparable<Node>
{



    public int[] xy;

    public Node parent;

    public int G;

    public int F;

    public int H;

    public Node(int x, int y, int z) {
        xy = new int[] {x,y,z };
    }

   

    public int CompareTo(Node other)
    {
        if (this.F > other.F)

        {
            return 1;

        }

        else if (this.F < other.F)

        {

            return -1;

        }

        return 0;
    }
}
