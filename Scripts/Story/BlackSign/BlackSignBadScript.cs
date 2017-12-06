using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSignBadScript : StoryScript
{
    private List<Condition> winConditions = new List<Condition>();

    private bool winResult;

    private Queue<string> targetList = new Queue<string>();

    public BlackSignBadScript()
    {

        BlackSignBadWinCOndition badWinCondition = new BlackSignBadWinCOndition();
        winConditions.Add(badWinCondition);
        targetList.Enqueue(SystemConstant.P1_NAME);
        targetList.Enqueue(SystemConstant.P6_NAME);


    }

    public Queue<string> loadCheck()
    {
        return targetList;
    }

    public bool checkStatus(Character chara, RoomInterface room, RoundController roundController)
    {
        bool winc = true;
        for (int i = 0; i < winConditions.Count; i++)
        {
            if (!winConditions[i].getConditionStatus(chara, room, roundController))
            {
                winc = false;
                break;
            }
        }
       
        Debug.Log("检查坏人胜利条件：" + winc);
        this.winResult = winc;
        return winc;
    }

    public string getFailureEndInfo()
    {
        return "慢慢地你觉得回复了理智，你感觉到能控制身体了，仿佛刚刚做了一场噩梦。";
    }

    public bool getResult()
    {
        return this.winResult;
    }

    public List<Condition> getWinCondition()
    {
        return winConditions;
    }

    public string getWinEndInfo()
    {
        return "你感觉到你的主人在呼唤你，就在地下。你控制着怪物疯狂地挖掘地下。";
    }

    public void scriptAction(Character chara, RoomContraller roomContraller, EventController eventController, DiceRollCtrl diceRoll, APathManager aPathManager, RoundController roundController, BattleController battleController)
    {
        Character target = roundController.getCharaByName(targetList.Peek());
        Debug.Log("target name is " + target.getName());
        if (AutoMoveManager.move(chara, roomContraller, eventController, diceRoll, aPathManager, target.getCurrentRoom()))
        {
            if (!target.isPlayer()) {
                battleController.fighte(chara, target);
            } else
            {
               
                battleController.showBattleUI(target, chara , false);
            }
            if (target.isDead())
            {
                if(target.getName() == SystemConstant.P1_NAME)
                {
                    NPC npc = (NPC)chara;
                    npc.sendMessageToPlayer(new string[] { "阻止我寻找真相的黑心医生已经死了。。。","你们这群怪物，都必须死！"});

                }
                targetList.Dequeue();
            }
        } else
        {
            Debug.Log("return false... finding path.");
        }
    }
}
