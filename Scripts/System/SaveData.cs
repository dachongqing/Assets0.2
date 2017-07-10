using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData  {

    private P6 p6;

    private Dictionary<int[], int[]> map;

    public P6 P6
    {
        get
        {
            return p6;
        }

        set
        {
            p6 = value;
        }
    }

    public Dictionary<int[], int[]> Map
    {
        get
        {
            return map;
        }

        set
        {
            map = value;
        }
    }
}
