using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingInfo  {

    private int[] roomXyz;

    private string thingCode;

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

    public string ThingCode
    {
        get
        {
            return thingCode;
        }

        set
        {
            thingCode = value;
        }
    }
}
