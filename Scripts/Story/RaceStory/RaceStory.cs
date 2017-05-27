﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceStory :  StoryInterface {

    private List<Condition> triggerConditions;

    private StoryScript badStoryScript;

    private StoryScript goodStoryScript;

    public RaceStory() {
        triggerConditions = new List<Condition>();
        Condition trigger = new RaceTriggerCondition();
        triggerConditions.Add(trigger);
        goodStoryScript = new RaceGoodScript();
        badStoryScript = new RaceBadScript();
    }

    public bool checkStoryEnd(Character chara, RoomInterface room)
    {
        if (chara.isBoss())
        {
            if (badStoryScript.checkStatus(chara, room))
            {
                return true;
            }
        }
        else {
            if (goodStoryScript.checkStatus(chara, room)) {
                return true;
            }
        }

        return false;
    }

    public bool checkStoryStart(Character chara, RoomInterface room)
    {
        for (int i=0; i< triggerConditions.Count; i++) {
            if (!triggerConditions[i].getConditionStatus(chara, room)) {
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

    public string getStoryInfo()
    {
        return "这是一个比赛剧情，看谁速度高，跑得快。";
    }

   

    
}