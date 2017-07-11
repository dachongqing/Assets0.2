using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo  {

    private int durability;

    private string code;

    private string type;

    private string name;

    private string desc;

    public string Desc
    {
        get
        {
            return desc;
        }

        set
        {
            desc = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public string Type
    {
        get
        {
            return type;
        }

        set
        {
            type = value;
        }
    }

    public string Code
    {
        get
        {
            return code;
        }

        set
        {
            code = value;
        }
    }

    public int Durability
    {
        get
        {
            return durability;
        }

        set
        {
            durability = value;
        }
    }
}
