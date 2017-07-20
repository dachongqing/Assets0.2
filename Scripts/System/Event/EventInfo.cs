using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventInfo  {

    private int[] roomXyz;

    private List<string> effectedList = new List<string>();

    public int[] RoomXyz
    {
        get
        {
            return roomXyz;
        }

        set
        {
            roomXyz = value;
        }
    }

    public List<string> EffectedList
    {
        get
        {
            return effectedList;
        }

        set
        {
            effectedList = value;
        }
    }
}
