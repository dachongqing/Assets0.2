using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSignBadWinCOndition : Condition
{
    public string getConditionInfo()
    {
        return "杀死所有人。";
    }

    public bool getConditionStatus(Character chara, RoomInterface room, RoundController roundController)
    {
        Player player = (Player)roundController.getCharaByName(SystemConstant.P6_NAME);
        if (player.isDead()) {
            return true;
        } else
        {
            return false;
        }
    }

    public string getConditionType()
    {
        return StoryConstan.CONDITION_TYPE_WIN;
    }
}
