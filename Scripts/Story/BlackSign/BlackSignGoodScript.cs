using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSignGoodScript : StoryScript
{
    private List<Condition> winConditions = new List<Condition>();

    private bool winResult;

    private int endFlag;

    private Queue<string> P1TargetRooms = new Queue<string>();
    private Queue<string[]> P1Message = new Queue<string[]>();

    private Queue<string> P5TargetRooms = new Queue<string>();
    private Queue<string[]> P5Message = new Queue<string[]>();

    private Queue<string> P3TargetRooms = new Queue<string>();
    private Queue<string[]> P3Message = new Queue<string[]>();

    public BlackSignGoodScript()
    {
        
        BlackSingGoodWinCondition goodWinCondition = new BlackSingGoodWinCondition();
        winConditions.Add(goodWinCondition);
        P1TargetRooms.Enqueue(RoomConstant.ROOM_TYPE_HOSPITAIL_DEAN);
        P1Message.Enqueue(new string[] { "我真的不知道有这种手术啊。。。", "院长室！找到院长室，那里一定有事实的真相！" });
        P5TargetRooms.Enqueue(RoomConstant.ROOM_TYPE_HOSPITAIL_MINITOR);
        P5Message.Enqueue(new string[] { "冒险家死了？。。。", "一定先要把那个怪物杀死。","大家走一起啊。。。" });
        P3Message.Enqueue(new string[] { "别开玩笑了！这个萝莉侦探已经控制了怪物", "这个萝莉侦探已经死魔鬼了，她必须死！" });

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
        if (winc) {
            Kate kete = (Kate)roundController.getCharaByName(SystemConstant.P4_NAME);
            if (kete.isDead())
            {
                this.endFlag = 0;
            }
            else {
                this.endFlag = 1;
            }
        }
        Debug.Log("检查好人胜利条件：" + winc);
        this.winResult = winc;
        return winc;
    }

    public string getFailureEndInfo()
    {
        return "萝莉 发疯一样杀死所有人物。 开始指导怪物往地下挖掘";
    }

    public bool getResult()
    {
        return winResult;
    }

    public List<Condition> getWinCondition()
    {
        return winConditions;
    }

    public string getWinEndInfo()
    {
        if (this.endFlag == 0) {
            return " 你们终于安全了，幸存者聚集在一起， 发现萝莉的身体并没有被感染， 注意到 萝莉手里有一个黑色的东西 玩家伸手碰到黑色物品的时候，进入疯狂状态。。看见所有画面均变异， 最后一个怪物开枪射杀了你。 ";
        } else {
            return "萝莉告诉大家，他看见了恐怖的画面就在地下，然后说明 为什么要杀害你们， 你们在她的眼里全是怪物，并且指出那个地下实验室 有重大的嫌疑，一定要找到那个地下实验室 。正要说下去的时候， 萝莉突然张大的嘴 看着你的后面，， 你正要转个头去的时候。。。你发现自己已经中弹，倒了下去。 只看见了，一双黑色皮鞋。。。";
        }
    }

    public void scriptAction(Character chara, RoomContraller roomContraller, EventController eventController, DiceRollCtrl diceRoll, APathManager aPathManager, RoundController roundController, BattleController battleController)
    {
        if (chara.getName() == SystemConstant.P1_NAME) {
            P1Move(chara, roomContraller, eventController, diceRoll, aPathManager, roundController, battleController);
        } else if(chara.getName() == SystemConstant.P3_NAME)
        {
            P3Move(chara, roomContraller, eventController, diceRoll, aPathManager, roundController, battleController);
        }
        else if (chara.getName() == SystemConstant.P5_NAME)
        {
            P5Move(chara, roomContraller, eventController, diceRoll, aPathManager, roundController, battleController);
        }
    }

    private void P1Move(Character chara, RoomContraller roomContraller, EventController eventController, DiceRollCtrl diceRoll, APathManager aPathManager,
       RoundController roundController, BattleController battleController)
    {
        Character  monster= roundController.getCharaByName(SystemConstant.MONSTER1_NAME);

        Debug.Log("error: this.P1TargetRooms " + this.P1TargetRooms);
        Debug.Log("error: Count " + this.P1TargetRooms.Count);
        string roomType;
        NPC npc = (NPC)chara;
        if (this.P1TargetRooms.Count >= 1)
        {
            roomType = this.P1TargetRooms.Peek();
            if (P1Message.Count > 0 ) {
                 npc.sendMessageToPlayer(P1Message.Dequeue());
            }
        }
        else {
            if (P1Message.Count > 0)
            {
                npc.sendMessageToPlayer(P1Message.Dequeue());
            }

            if (monster.isDead())
            {
                if (P1Message.Count > 0)
                {
                    npc.sendMessageToPlayer(P1Message.Dequeue());
                }
                roomType = roomContraller.getRandomRoom().getRoomType();

            }
            else {
                roomType = roomContraller.findRoomByXYZ(monster.getCurrentRoom()).getRoomType();

            }
            this.P1TargetRooms.Enqueue(roomType);
        }
        if (AutoMoveManager.move(chara, roomContraller,
            eventController, diceRoll, aPathManager, roomContraller.findRoomByRoomType(roomType).getXYZ())) {
            this.P1TargetRooms.Dequeue();

            if (roomType == RoomConstant.ROOM_TYPE_HOSPITAIL_DEAN)
            {
                npc.sendMessageToPlayer(new string[] { "我在院长室了，这里有个保险箱，我打不开，快来帮忙" });
                P1Message.Enqueue(new string[] { "我们得杀死那个蜘蛛怪物。", "那个萝莉侦探疯了，我们不用管她。" });
            }
            else {

                if (!monster.isDead())
                {
                    battleController.fighte(chara, monster);
                    if (monster.isDead())
                    {
                        P1Message.Enqueue(new string[] { "那个蜘蛛怪物死了?。", "我们得想办法救救那个萝莉侦探。" });
                    }
                 }
                
            }
        }
    }

    private void P3Move(Character chara, RoomContraller roomContraller, EventController eventController, DiceRollCtrl diceRoll, APathManager aPathManager,
       RoundController roundController, BattleController battleController)
    {
        NPC npc = (NPC)chara;
        if (P3Message.Count > 0)
        {
            npc.sendMessageToPlayer(P3Message.Dequeue());
        }
        Character kate = roundController.getCharaByName(SystemConstant.P4_NAME);
        if (!kate.isDead()) {
            if (AutoMoveManager.move(chara, roomContraller, eventController, diceRoll, aPathManager, kate.getCurrentRoom())) {
                battleController.fighte(chara, kate);
                if (kate.isDead()) {
                    P3Message.Enqueue(new string[] { "侦探死了？", "感谢神灵保佑，我永远是您忠实的仆从。","哼，那个蜘蛛怪怕除虫剂的，随随便便就收拾了。" });
                }
            }

        } else
        {
            AutoMoveManager.move(chara, roomContraller, eventController, diceRoll, aPathManager, roomContraller.getRandomRoom().getXYZ());
            
        }
    }

    private void P5Move(Character chara, RoomContraller roomContraller, EventController eventController, DiceRollCtrl diceRoll, APathManager aPathManager,
      RoundController roundController, BattleController battleController)
    {
        Character monster = roundController.getCharaByName(SystemConstant.MONSTER1_NAME);
        string roomType;
        NPC npc = (NPC)chara;
        if (this.P5TargetRooms.Count >= 1)
        {
            roomType = this.P5TargetRooms.Peek();
            if (P5Message.Count > 0)
            {
                npc.sendMessageToPlayer(P5Message.Dequeue());
            }
        }
        else
        {
            if (P5Message.Count > 0)
            {
                npc.sendMessageToPlayer(P5Message.Dequeue());
            }

            if (monster.isDead())
            {
                if (P5Message.Count > 0)
                {
                    npc.sendMessageToPlayer(P5Message.Dequeue());
                }
                roomType = roomContraller.getRandomRoom().getRoomType();

            }
            else
            {
                roomType = roomContraller.findRoomByXYZ(monster.getCurrentRoom()).getRoomType();

            }
            this.P5TargetRooms.Enqueue(roomType);
        }
        if (AutoMoveManager.move(chara, roomContraller,
            eventController, diceRoll, aPathManager, roomContraller.findRoomByRoomType(roomType).getXYZ()))
        {
            this.P5TargetRooms.Dequeue();

            if (roomType == RoomConstant.ROOM_TYPE_HOSPITAIL_MINITOR)
            {
                npc.sendMessageToPlayer(new string[] { "我在监控室了，这个有台电脑。","我破解了文件夹，里面有很多照片,有一张很奇怪，大家来看看。" });               
            }
            else
            {

                if (!monster.isDead())
                {
                    battleController.fighte(chara, monster);
                    if (monster.isDead())
                    {
                        P1Message.Enqueue(new string[] { "那个蜘蛛怪已经死了！。", "那个萝莉侦探还管不管了啊？" });
                    }
                }

            }
        }
    }
}
