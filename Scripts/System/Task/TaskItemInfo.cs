using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskItemInfo  {

    private string itemDesc;

    private bool completed;

    private string itemCode;

    public string ItemDesc
    {
        get
        {
            return itemDesc;
        }

        set
        {
            itemDesc = value;
        }
    }

    public bool Completed
    {
        get
        {
            return completed;
        }

        set
        {
            completed = value;
        }
    }

    public string ItemCode
    {
        get
        {
            return itemCode;
        }

        set
        {
            itemCode = value;
        }
    }
}
