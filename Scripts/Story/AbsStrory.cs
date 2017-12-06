using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class AbsStrory : MonoBehaviour,StoryInterface
{

    private List<Condition> triggerConditions;

    private StoryScript badStoryScript;

    private StoryScript goodStoryScript;

    private string storyCode;

    public void setStroryAttrs(List<Condition> triggerConditions, StoryScript goodStoryScript, StoryScript badStoryScript,string storyCode)
    {
       this.triggerConditions = triggerConditions;       
       this.goodStoryScript= goodStoryScript;
       this.badStoryScript = badStoryScript ;
        this.storyCode = storyCode;
    }

    public bool checkStoryEnd(Character chara, RoomInterface room, RoundController roundController)
    {
        if (chara.isBoss())
        {
            if (badStoryScript.checkStatus(chara, room, roundController))
            {
                return true;
            }
        }
        else
        {
            if (goodStoryScript.checkStatus(chara, room, roundController))
            {
                return true;
            }
        }

        return false;
    }

    public bool checkStoryStart(Character chara, RoomInterface room, RoundController roundController)
    {
        Debug.Log("开始检查剧本开启条件");
        for (int i = 0; i < triggerConditions.Count; i++)
        {
            if (!triggerConditions[i].getConditionStatus(chara, room, roundController))
            {
                Debug.Log("不满足条件");
                return false;
            }
        }
        Debug.Log("满足条件");
        return true;
    }

    public StoryScript getBadManScript()
    {
        return badStoryScript;
    }

    public StoryScript getGoodManScript()
    {
        return goodStoryScript;
    }

    public virtual string getStoryInfo()
    {
        return null;
    }

    public virtual void initStroy(Character chara, RoundController roundController, TaskMananger taskMananger)
    {
        
    }

    public string getStoryCode()
    {
        return storyCode;
    }
}
