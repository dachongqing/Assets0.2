using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class AbsStrory : MonoBehaviour,StoryInterface
{

    private List<Condition> triggerConditions;

    private StoryScript badStoryScript;

    private StoryScript goodStoryScript;

    public void setStroryAttrs(List<Condition> triggerConditions, StoryScript goodStoryScript, StoryScript badStoryScript )
    {
       this.triggerConditions = triggerConditions;       
       this.goodStoryScript= goodStoryScript;
       this.badStoryScript = badStoryScript ;
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
        for (int i = 0; i < triggerConditions.Count; i++)
        {
            if (!triggerConditions[i].getConditionStatus(chara, room, roundController))
            {
                return false;
            }
        }
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

    public virtual void initStroy(Character chara)
    {
        
    }
}
