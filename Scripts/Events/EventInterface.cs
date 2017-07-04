using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EventInterface  {

    string[] getEventBeginInfo();

    void setEventBeginInfo(string[] infos);
       
    string getEventType();

    EventResult excute(List<Character> characters);

    EventResult excute(Character character,string selectCode, int rollValue);

    Dictionary<string,string> getSelectItem();

    string[] getEventEndInfo(string resultCode);

    void setEventEndInfo(Dictionary<string, string[]> endMap);


}
