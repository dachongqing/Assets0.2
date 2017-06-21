using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface GuangBoAction  {

    void guangBoAction(Character chara, RoomContraller roomContraller, EventController eventController,
        DiceRollCtrl diceRoll, APathManager aPathManager, RoundController roundController, BattleController battleController);

    int getSanLimit();

    void setSanLimit(int sanLimit);

    bool checkOwner(string npc);

    void addWhiteList(string npc);

    List<string> getWhiteList();

    void addBlackList(string npc);

    bool isPlanSuccess();

    string getGuangBoOwnerName();

    string getGuangBoTargetVaule();

    string getGuangBoType();

    void sendGuangBoToOwner(NPC npc, RoomContraller roomContraller, RoundController roundController);

    bool hasVictim();

    Character getVictim();

}
