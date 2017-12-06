using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EventInterface  {

    string[] getEventBeginInfo();
      
    string getEventType();

    string getSubEventType();

    EventResult excute(Character character,string selectCode, int rollValue);

    Dictionary<string,string[]> getSelectItem();

    string[] getEventEndInfo(string resultCode);

    int getGoodValue();

    int getNormalValue();

    int getBadDiceNum();

 


}
