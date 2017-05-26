using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nolan : MonoBehaviour,NPC {

    [SerializeField] private int actionPoint;

    private int[] abilityInfo;

    [SerializeField] private int[] xyz;

    private String playerName;

    private bool roundOver;

    private bool actionPointrolled;

    private APathManager aPathManager = new APathManager();

    private RoomContraller roomContraller;

    private DiceRollCtrl diceRoll;

    private EventController eventController;


    public bool ActionPointrolled()
    {
        return actionPointrolled;
    }

    public void endRound()
    {
        this.roundOver = true;
    }

    public int[] getAbilityInfo()
    {
        return abilityInfo;
    }

    public int getActionPoint()
    {
        return actionPoint;
    }

    public int[] getCurrentRoom()
    {
        return xyz;
    }

    public string getName()
    {
        return playerName;
    }

    public bool isDead()
    {
        return false;
    }

    public bool isPlayer()
    {
        return false;
    }

    public bool isRoundOver()
    {
        return this.roundOver;
    }

    public bool isWaitPlayer()
    {
        return false;
    }


    public void defaultAction()
    {
        /**
         * 伪代码
         * 
         * 根据剧本是否开启，调用默认的行为 或者剧本行为： 等剧本代码设计好后 完善
         * 以下是默认行为
         * 
         * 回合开始 ai 先丢骰子
         * 迭代行动力，目前没有行为树分析， 所以直接奔向主题： 走到某个特殊房间
         * 注意： 如果目标房间是地面 直接调用， 如果目标房间是楼下， 先定位到下楼梯口房间， 如果目标是楼上，先定位到上楼梯口房间
         * 
         *  upc 进入房间， 离开房间 都需要手动触发房间事件， 如果有选择的事件， 细化（根据角色剧情 或者其他属性 判定选项），
         * 迭代行动力 直到行动力为0 结束回合
         * 
         * 等待剧情代码完善后 再细化
         * 
        */
        if (ActionPointrolled())
        {
            //int speed = ply.getAbilityInfo()[1] + ply.getEffectBuff();
            int speed = getAbilityInfo()[1];
            int res = diceRoll.calculateDice(speed, speed, 0);
            updateActionPoint(res);
            setActionPointrolled(false);
            Stack<Node> path = null;

            RoomInterface targetRoom = roomContraller.findRoomByRoomType(RoomConstant.ROOM_TYPE_BOOK_ROOM);

            RoomInterface currentRoom = roomContraller.findRoomByXYZ(getCurrentRoom());
            //如果当前房间不是目标房间
            //开始找路 : 目前直接找书房
            if (getCurrentRoom()[0] != targetRoom.getXYZ()[0] || getCurrentRoom()[1] != targetRoom.getXYZ()[1] || getCurrentRoom()[2] != targetRoom.getXYZ()[2])
            {
                //判定是否同层
                if (getCurrentRoom()[2] != targetRoom.getXYZ()[2])
                {
                    // 如果目标房间是楼下， 先定位到下楼梯口房间， 如果目标是楼上，先定位到上楼梯口房间
                }
                else
                {
                    path = aPathManager.findPath(currentRoom, targetRoom, roomContraller);
                }
                while (getActionPoint() > 0)
                {
                    Node nextRoom = path.Pop();
                    bool opened = false;
                    //判断向什么方向的房间
                    if (getCurrentRoom()[0] == nextRoom.xy[0] && getCurrentRoom()[1] - nextRoom.xy[1] < 0) {
                        //up room
                        //调用AI 专用方法
                        opened =  currentRoom.getNorthDoor().GetComponent<WoodDoor>().openDoor(this);
                            //开门成功
                    
                    } else if(getCurrentRoom()[0] == nextRoom.xy[0] && getCurrentRoom()[1] - nextRoom.xy[1] > 0) {
                        //down room
                        opened = currentRoom.getSouthDoor().GetComponent<WoodDoor>().openDoor(this);
                    } else if (getCurrentRoom()[1] == nextRoom.xy[1] && getCurrentRoom()[0] - nextRoom.xy[0] < 0)
                    {
                        //east room
                        opened = currentRoom.getEastDoor().GetComponent<WoodDoor>().openDoor(this);
                    }
                    else
                    {
                        //west room
                        opened = currentRoom.getWestDoor().GetComponent<WoodDoor>().openDoor(this);
                    }


                    //如果进入房间是目标房间 暂时回合结束
                    if (opened) {

                        bool result = eventController.excuteLeaveRoomEvent(currentRoom,this);

                        //非正式测试用，只考虑行动力足够


                        if (result == true)
                        {
                            //离开门成功
                          

                            //当前人物坐标移动到下一个房间
                            this.setCurrentRoom(nextRoom.xy);

                            //触发进门事件
                            //	eventController.excuteEnterRoomEvent (nextRoom, roundController.getCurrentRoundChar ());  暂时禁用 运行时有异常

                        }
                        else
                        {
                            //离开失败
                            Debug.Log("WoodDoor.cs OnMouseDown 离开房间失败");
                        }
                    }

                }
            }
            else
            {
                //找到房间后， 等待后续细节，：根据设定找下一个房间？ 开启剧本？ 目前直接结束回合
                Debug.Log(this.playerName + "已经到达目标房间 (" + getCurrentRoom()[0]+","+ getCurrentRoom()[1] +")" );
            }
        }
        else
        {
            Debug.Log("你已经丢过行动力骰子");
        }

        endRound();
    }

    public void roundStart()
    {
        roundOver = false;
        if (this.isPlayer())
        {
        }
        else {
            defaultAction();
        }



    }

    public void setActionPointrolled(bool actionPointrolled)
    {
        this.actionPointrolled = actionPointrolled;
    }

    public void setCurrentRoom(int[] nextRoomXYZ)
    {
        this.xyz = nextRoomXYZ;
    }

    public void updateActionPoint(int actionPoint)
    {
        this.actionPoint = actionPoint;
    }

    // Use this for initialization
    void Start () {
        //游戏一开始 所处的房间 默认房间的坐标为 0,0,0
        int[] roomXYZ = { 0, 0, 0 };
        setCurrentRoom(roomXYZ);
        abilityInfo = new int[] { 3, 3, 2, 6 };
        this.actionPointrolled = false;
        Debug.Log("叶成亮 进入默认房间");
        playerName = "叶成亮";
        roomContraller = FindObjectOfType<RoomContraller>();
        diceRoll = FindObjectOfType<DiceRollCtrl>();
        eventController = FindObjectOfType<EventController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
