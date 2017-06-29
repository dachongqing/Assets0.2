using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceBadWinCondition : Condition {

    public string getConditionInfo()
    {
        return "跑到起始房间";
    }

    public bool getConditionStatus(Character chara, RoomInterface room, RoundController roundController )
    {

        if (chara.getCurrentRoom()[0] == 0 && chara.getCurrentRoom()[1] == 0 &&  roundController.getPlayerChara().isDead())
        {
          Debug.Log("坏人当前房间 " + chara.getCurrentRoom()[0] + ","+chara.getCurrentRoom()[1]);
            return true;
        }   
        else
        {
            return false;
        }
    }

    public string getConditionType()
    {
        return StoryConstan.CONDITION_TYPE_WIN;

    }
}
