using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData  {

    private P0 p1;
    private P0 p2;
    private P0 p3;
    private P0 p4;
    private P0 p5;
    private P0 p6;
    private P0 benMonster;
    private int roundCount;
    private List<string> charaNames = new List<string>();

    private List<ThingInfo> things = new List<ThingInfo>();

    private List<EventInfo> effectedList = new List<EventInfo>();

    private StoryInfo storyInfo  = new StoryInfo();

    public P0 P1
    {
        get
        {
            return p1;
        }

        set
        {
            p1 = value;
        }
    }

    public P0 P2
    {
        get
        {
            return p2;
        }

        set
        {
            p2 = value;
        }
    }

    public P0 P3
    {
        get
        {
            return p3;
        }

        set
        {
            p3 = value;
        }
    }

    public P0 P4
    {
        get
        {
            return p4;
        }

        set
        {
            p4 = value;
        }
    }

    public P0 P5
    {
        get
        {
            return p5;
        }

        set
        {
            p5 = value;
        }
    }

    public P0 P6
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

    public StoryInfo StoryInfo
    {
        get
        {
            return storyInfo;
        }

        set
        {
            storyInfo = value;
        }
    }

    public P0 BenMonster
    {
        get
        {
            return benMonster;
        }

        set
        {
            benMonster = value;
        }
    }

    public int RoundCount
    {
        get
        {
            return roundCount;
        }

        set
        {
            roundCount = value;
        }
    }

    public List<string> CharaNames
    {
        get
        {
            return charaNames;
        }

        set
        {
            charaNames = value;
        }
    }

    public List<ThingInfo> Things
    {
        get
        {
            return things;
        }

        set
        {
            things = value;
        }
    }

    public List<EventInfo> EffectedList
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
