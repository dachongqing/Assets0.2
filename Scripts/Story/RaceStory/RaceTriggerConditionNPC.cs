using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceTriggerConditionNPC : Condition
{

    // Use this for initialization
    private  List<string> roomNames = new List<string>();

    public string getConditionInfo()
    {
        return "本尼和叶乘凉都在书房";
    }

    public bool getConditionStatus(Character chara, RoomInterface room)
    {
       roomNames.Clear();
       NPC npc = (NPC)chara;
       List<string> targetNames = npc.getTargetChara();
       List<Character>  chars = room.getCharas();
       foreach (Character c in chars)
       {
         roomNames.Add(c.getName());
       }
        foreach (string name in targetNames) {
        Debug.Log("目标角色 ：" + name);
            if (!roomNames.Contains(name)) {
                Debug.Log("目标角色 ben 不在书房");
                return false;
            }
        }

        Debug.Log("目标角色 ben 也在书房了");
        return true;
    }

    public string getConditionType()
    {
        return StoryConstan.CONDITION_TYPE_TRIGGER;
    }
}
