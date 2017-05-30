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

    public bool checkStatus(Character chara, RoomInterface room)
    {

        bool winc = true;
        for (int i = 0; i < winConditions.Count; i++)
        {
            if (!winConditions[i].getConditionStatus(chara, room))
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

    public void scriptAction(Character chara, RoomContraller roomContraller , EventController eventController , DiceRollCtrl diceRoll , APathManager aPathManager)
    {

        if (chara.getName() == "叶成亮")
        {
            NolanMove(chara, roomContraller, eventController, diceRoll, aPathManager);
        }

      
        chara.endRound();
    }

    private void NolanMove(Character chara, RoomContraller roomContraller, EventController eventController, DiceRollCtrl diceRoll, APathManager aPathManager)
    {
        if (chara.ActionPointrolled())
        {
            //int speed = ply.getAbilityInfo()[1] + ply.getEffectBuff();
            int speed = chara.getAbilityInfo()[1];
            int res = diceRoll.calculateDice(speed);
            chara.updateActionPoint(res);
            chara.setActionPointrolled(false);
            Stack<Node> path = null;

            RoomInterface targetRoom = roomContraller.findRoomByXYZ(new int[] { 0, 0, 0 });

            RoomInterface currentRoom = roomContraller.findRoomByXYZ(chara.getCurrentRoom());
            //如果当前房间不是目标房间
            //开始找路 : 目前直接找书房
            if (chara.getCurrentRoom()[0] != targetRoom.getXYZ()[0] || chara.getCurrentRoom()[1] != targetRoom.getXYZ()[1] || chara.getCurrentRoom()[2] != targetRoom.getXYZ()[2])
            {
                //判定是否同层
                if (chara.getCurrentRoom()[2] != targetRoom.getXYZ()[2])
                {
                    // 如果目标房间是楼下， 先定位到下楼梯口房间， 如果目标是楼上，先定位到上楼梯口房间
                }
                else
                {
                    path = aPathManager.findPath(currentRoom, targetRoom, roomContraller);
                }
                while (chara.getActionPoint() > 0 && path.Count > 0)
                {
                    Node nextRoom = path.Peek();
                    bool opened = false;
                    //判断向什么方向的房间
                    if (chara.getCurrentRoom()[0] == nextRoom.xy[0] && chara.getCurrentRoom()[1] - nextRoom.xy[1] < 0)
                    {
                        //up room
                        //调用AI 专用方法
                        opened = currentRoom.getNorthDoor().GetComponent<WoodDoor>().openDoor(chara);
                        //开门成功

                    }
                    else if (chara.getCurrentRoom()[0] == nextRoom.xy[0] && chara.getCurrentRoom()[1] - nextRoom.xy[1] > 0)
                    {
                        //down room
                        opened = currentRoom.getSouthDoor().GetComponent<WoodDoor>().openDoor(chara);
                    }
                    else if (chara.getCurrentRoom()[1] == nextRoom.xy[1] && chara.getCurrentRoom()[0] - nextRoom.xy[0] < 0)
                    {
                        //east room
                        opened = currentRoom.getEastDoor().GetComponent<WoodDoor>().openDoor(chara);
                    }
                    else
                    {
                        //west room
                        opened = currentRoom.getWestDoor().GetComponent<WoodDoor>().openDoor(chara);
                    }


                    //如果进入房间是目标房间 暂时回合结束
                    if (opened)
                    {

                   //     bool result = eventController.excuteLeaveRoomEvent(currentRoom, chara);

                        //非正式测试用，只考虑行动力足够


                     //   if (result == true)
                       // {
                            //离开门成功

                         //   path.Pop();
                            //当前人物坐标移动到下一个房间
                           // chara.setCurrentRoom(nextRoom.xy);

                            //触发进门事件
                            //	eventController.excuteEnterRoomEvent (nextRoom, roundController.getCurrentRoundChar ());  暂时禁用 运行时有异常

                        //}
                       // else
                       // {
                            //离开失败
                         //   Debug.Log("WoodDoor.cs OnMouseDown 离开房间失败");
                        //}
                    }

                }
            }
            else
            {
                //找到房间后， 等待后续细节，：根据设定找下一个房间？ 开启剧本？ 目前直接结束回合
                Debug.Log(chara.getName() + "已经到达目标房间 (" + chara.getCurrentRoom()[0] + "," + chara.getCurrentRoom()[1] + ")");
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
        else
        {
            Debug.Log("你已经丢过行动力骰子");
        }
    }
}
