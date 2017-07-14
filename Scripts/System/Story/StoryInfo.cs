using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryInfo  {

    private bool isStoryStart;

    private string storyCode;

    public bool IsStoryStart
    {
        get
        {
            return isStoryStart;
        }

        set
        {
            isStoryStart = value;
        }
    }

    public string StoryCode
    {
        get
        {
            return storyCode;
        }

        set
        {
            storyCode = value;
        }
    }
}
