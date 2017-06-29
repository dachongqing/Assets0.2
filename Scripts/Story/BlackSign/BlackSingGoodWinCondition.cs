using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSingGoodWinCondition : Condition
{
    public string getConditionInfo()
    {
        return "杀死怪物，杀死萝莉侦探或者唤醒侦探";
    }

    public bool getConditionStatus(Character chara, RoomInterface room, RoundController roundController)
    {
        Kate kete = (Kate)roundController.getCharaByName(SystemConstant.P4_NAME);
        BenMonster monster = (BenMonster)roundController.getCharaByName(SystemConstant.MONSTER1_NAME);
        if (monster.isDead() && (kete.isDead() || kete.getAbilityInfo()[3] >= 7))
        {
           
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
