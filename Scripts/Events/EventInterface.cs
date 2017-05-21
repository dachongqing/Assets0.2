using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EventInterface  {

    string getEventBeginInfo();
       
    string getEventType();

    EventResult excute(List<Character> characters);

    EventResult excute(Character character,string selectCode);

    Dictionary<string,string> getSelectItem();

    string getEventEndInfo(string resultCode);


}
