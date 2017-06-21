using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Condition  {

    string getConditionInfo();

    bool getConditionStatus(Character chara, RoomInterface room,RoundController roundController);

    string getConditionType();
}
