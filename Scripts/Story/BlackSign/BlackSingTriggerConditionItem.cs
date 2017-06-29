using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSingTriggerConditionItem : Condition
{
    public string getConditionInfo()
    {
        return "拿到解剖标本，拿到半孵化的蛛卵";
    }

    public bool getConditionStatus(Character chara, RoomInterface room, RoundController roundController)
    {
        NPC npc = (NPC)chara;
        List<Item> its = npc.getBag().getAllItems();
        bool c1 = false;
        bool c2 = false;

        for (int i = 0; i < its.Count; i++)
        {
            if (its[i].getCode() == ItemConstant.ITEM_CODE_SPEC_Y0003　)
            {
                Debug.Log("检查剧情开启：找到任务道具" + its[i].getName());
                c1 =  true;

            }
            if (its[i].getCode() == ItemConstant.ITEM_CODE_SPEC_Y0004)
            {
                Debug.Log("检查剧情开启：找到任务道具" + its[i].getName());
                c2 = true;

            }

        }
        if (c1 && c2) {
            return true;
        } else
        {
          return false;

        }


    }

    public string getConditionType()
    {
        return StoryConstan.CONDITION_TYPE_TRIGGER;
    }
}
