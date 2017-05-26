using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceTriggerCondition :  Condition{

    public string getConditionInfo()
    {
        return "NPC到达书房";
    }

    public bool getConditionStatus(Character chara, RoomInterface room)
    {
        if (room.getRoomType() == RoomConstant.ROOM_TYPE_BOOK_ROOM && chara.getName() == "叶成亮")
        {
            Debug.Log("检查剧情开启："  + room.getRoomType() +","+ chara.getName());
            return true;
        }
        else {
            Debug.Log("检查剧情没开启：" + room.getRoomType() + "," + chara.getName());
            return false;
        }
    }

    public string getConditionType()
    {
        return StoryConstan.CONDITION_TYPE_TRIGGER;
    }

    
}
