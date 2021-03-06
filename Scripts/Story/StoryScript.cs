﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface StoryScript  {


    List<Condition> getWinCondition();
      
    void scriptAction(Character chara, RoomContraller roomContraller, EventController eventController,
        DiceRollCtrl diceRoll, APathManager aPathManager, RoundController roundController, BattleController battleController);

    bool getResult();

    string getWinEndInfo();

    string getFailureEndInfo();

    bool checkStatus(Character chara, RoomInterface room, RoundController roundController);

}
