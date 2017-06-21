using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceGoodScript : StoryScript {

  

    private List<Condition> winConditions;

    private bool winResult;


    public RaceGoodScript() {
       
        winConditions = new List<Condition>();
        RaceGoodWinCondition raceGoodWinCondition = new RaceGoodWinCondition();
        winConditions.Add(raceGoodWinCondition);
    }

    public bool checkStatus(Character chara, RoomInterface room, RoundController roundController)
    {

        bool winc = true;
        for (int i = 0; i < winConditions.Count; i++)
        {
            if (!winConditions[i].getConditionStatus(chara, room, roundController))
            {
                winc =  false;
                break;
            }
        }
        Debug.Log("检查好人胜利条件：" + winc);
        this.winResult = winc;
        return winc;

      

       
    }

   

    public string getFailureEndInfo()
    {
        return " 你没能跑赢 Nolan， 你被他吊打了，他向你头来鄙视的眼神";
    }

    public bool getResult()
    {
        return winResult;
    }

    public List<Condition> getWinCondition()
    {
      return this.winConditions;
    }

    public string getWinEndInfo()
    {
        return "你跑赢了nolan， 知道了他是3级残疾人的身份";
    }

    public void scriptAction(Character chara, RoomContraller roomContraller , EventController eventController , DiceRollCtrl diceRoll , APathManager aPathManager,
        RoundController roundController,BattleController battleController)
    {

        if (chara.getName() == "本尼")
        {
            BenMove(chara, roomContraller, eventController, diceRoll, aPathManager, roundController,battleController);
        }

      
       
    }

    private void BenMove(Character chara, RoomContraller roomContraller, EventController eventController, DiceRollCtrl diceRoll, APathManager aPathManager,
        RoundController roundController, BattleController battleController)
    {
        AutoMoveManager.move(chara, roomContraller, eventController, diceRoll, aPathManager, new int[] {0,0,0 });
    }
}
