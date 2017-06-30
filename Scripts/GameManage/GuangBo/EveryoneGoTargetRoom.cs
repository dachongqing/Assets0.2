using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EveryoneGoTargetRoom : GuangBoAction
{

    private string targetRoomType;

    private List<string> blackList;

    private List<string> whiteList;

    private int sanLimit;

    private List<string> targetNameList;

    private string ownerName;

    private string targetRoomName;

    private bool actionEnd;

    public EveryoneGoTargetRoom(string ownerName, string targetRoomType,List<string> targetNameList, int sanLimit) {
        this.targetRoomType = targetRoomType;
        this.sanLimit = sanLimit;
        this.targetNameList = targetNameList;
        this.ownerName = ownerName;
        this.blackList = new List<string>();
        this.whiteList = new List<string>();

    }

    public void addBlackList(string npc)
    {
        blackList.Add(npc);
    }

    public void addWhiteList(string npc)
    {
        whiteList.Add(npc);

    }

       
    public bool checkOwner(string npcName)
    {
        if (this.ownerName == npcName)
        {
            return true;
        }
        else {
            return false;
        }
    }

    public string getGuangBoOwnerName()
    {
        return this.ownerName;
    }
      
    public string getGuangBoTargetVaule()
    {
        return this.targetRoomType;
    }

    public string getGuangBoType()
    {
        return GuangBoConstant.GUANGBO_TYPE_MOVE_ROOM;
    }

    public int getSanLimit()
    {
        return this.sanLimit;
    }

    public Character getVictim()
    {
        return null;
    }

    public List<string> getWhiteList()
    {
        return this.whiteList;
    }

    public void guangBoAction(Character chara, RoomContraller roomContraller, EventController eventController, DiceRollCtrl diceRoll, APathManager aPathManager, RoundController roundController, BattleController battleController)
    {
        actionEnd = false;
        Debug.Log("执行广播任务，目标是 " + this.targetRoomType);
        if (AutoMoveManager.move(chara, roomContraller, eventController, diceRoll, aPathManager, this.targetRoomType)) {
            NPC npc = (NPC)chara;
            npc.setFollowGuangBoAction(false);
            if (targetNameList.Contains(chara.getName())) {

                actionEnd = true;
            }
        };
    }

    public bool hasVictim()
    {
        return false;
    }

    public bool isGuangBoActionEnd()
    {
        return actionEnd;
    }

    public bool isPlanSuccess()
    {
        foreach (string name in targetNameList) {
            if (!whiteList.Contains(name)) {
                return false;
            }
        }
        return true;
    }

    public void sendGuangBoToOwner(NPC npc, RoomContraller roomContraller, RoundController roundController)
    {
        string targetRoomName = roomContraller.findRoomByRoomType(this.getGuangBoTargetVaule()).getRoomName();
        npc.sendMessageToPlayer(new string[] { npc.getName() + " :我准备听从" + this.getGuangBoOwnerName() + "的意见去" + targetRoomName + "看看" });
    }

    public void setSanLimit(int sanLimit)
    {
        this.sanLimit = sanLimit;
    }
}
