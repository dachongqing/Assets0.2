using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceBadScript : StoryScript
{

    private List<Condition> winBadditions;

    private bool winResult;

    public RaceBadScript()
    {

        winBadditions = new List<Condition>();

        RaceBadWinCondition raceBadWinCondition = new RaceBadWinCondition();
        winBadditions.Add(raceBadWinCondition);
    }

    public bool checkStatus(Character chara, RoomInterface room)
    {
         bool winc = true;

        for (int i = 0; i < winBadditions.Count; i++)
        {
            bool con = winBadditions[i].getConditionStatus(chara, room);
         
            if (!con)
            {
                winc = false;
                break;
            }
        }

        Debug.Log("检查坏人胜利条件：" + winc);
        winResult = winc;
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
        return this.winBadditions;
    }

    public string getWinEndInfo()
    {
        return "你跑赢了nolan， 知道了他是3级残疾人的身份";
    }

    public void scriptAction(Character chara, RoomContraller roomContraller, EventController eventController, DiceRollCtrl diceRoll, APathManager aPathManager,
        RoundController roundController, BattleController battleController)
    {
        
        if (chara.getName() == "叶成亮") {
            NolanMove( chara,  roomContraller,  eventController,  diceRoll,  aPathManager, roundController, battleController);
        }
       
    }

    private void NolanMove(Character chara, RoomContraller roomContraller, EventController eventController, DiceRollCtrl diceRoll, APathManager aPathManager,
         RoundController roundController, BattleController battleController) {
         Character player =  roundController.getPlayerChara();
        
        if (AutoMoveManager.move(chara, roomContraller, eventController, diceRoll, aPathManager, player.getCurrentRoom()))
        {
                
                //找到房间后， 等待后续细节，：根据设定找下一个房间？ 开启剧本？ 目前直接结束回合
                Debug.Log(chara.getName() + "已经到达目标房间 (" + chara.getCurrentRoom()[0] + "," + chara.getCurrentRoom()[1] + ")");
                Debug.Log("直接开打，开始疯狂攻击玩家");
           
                battleController.fighte(chara, player);
             
                if (typeof(NPC).IsAssignableFrom(chara.GetType()))
                {
                    Debug.Log("该角色是属于NPC");
                    NPC npc = (NPC)chara;
                    //角色行动 找物品
                }
                else
                {
                    Debug.Log("该角色是属于怪物");
                };

          
        }
       
    }
}
