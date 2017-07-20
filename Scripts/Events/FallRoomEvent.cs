using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallRoomEvent : CommonEvent
{
    public FallRoomEvent(int good, int bad, string[] eventBeginInfo, Dictionary<string, string[]> endInfoMap, string eventType, string subEventType, int goodValue, int normalValue, int badDiceNum) : 
        base(good, bad, eventBeginInfo, endInfoMap, eventType, subEventType, goodValue, normalValue, badDiceNum)
    {
    }
}
