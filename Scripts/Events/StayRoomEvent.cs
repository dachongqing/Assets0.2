using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayRoomEvent : CommonEvent
{
    public StayRoomEvent(int good, int bad, string[] eventBeginInfo, Dictionary<string, string[]> endInfoMap, string eventType, string subEventType, int goodValue, int normalValue, int badDiceNum) : base(good, bad, eventBeginInfo, endInfoMap, eventType, subEventType, goodValue, normalValue, badDiceNum)
    {
        this.setEffectedList();
    }

    public override EventResult doExcute(Character character, string selectCode, int rollValue)
    {
        EventResult er = new EventResult();
        if(EventConstant.SAN_STAY_EVENT == this.getSubEventType())
        {
           
            this.getEffectedList().Add(character.getName());
            character.getAbilityInfo()[3] = character.getAbilityInfo()[3] + this.getGoodValue();
            character.getMaxAbilityInfo()[3] = character.getMaxAbilityInfo()[3] + this.getGoodValue();
            er.setResultCode(EventConstant.SAN_STAY_EVENT_GOOD);
        }
      
        er.setStatus(true);
        return er;
    }

    public void addEffectList(Character chara)
    {
        this.getEffectedList().Add(chara.getName());
    }

    public void copyEffectList(List<string> list)
    {

    }
}
