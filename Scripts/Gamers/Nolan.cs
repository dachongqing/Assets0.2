using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nolan : MonoBehaviour, NPC
{
	[Tooltip("房间宽度")]public float roomH=13.7f;
	[Tooltip("房间高度")]public float roomV=11f;

    [SerializeField] private int actionPoint;

    private int[] abilityInfo;

    private int[] maxAbilityInfo;

    [SerializeField] private int[] xyz;

    private String playerName;

    private bool roundOver;

    private bool actionPointrolled;

    private APathManager aPathManager = new APathManager();

    private RoomContraller roomContraller;

    private DiceRollCtrl diceRoll;

    private EventController eventController;

    private StoryScript ss;

    private bool bossFlag;

    private bool deadFlag;

    private bool scriptEnd;

    private Bag bag;

    private RoundController roundController;

    private BattleController battleController;

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
        return this.deadFlag;
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
        return this.waitFlag;
    }
    private bool waitFlag;
    public void setWaitPlayer(bool waitFlag) {
        this.waitFlag = waitFlag;
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
            int res = diceRoll.calculateDice(speed);
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
                while (getActionPoint() > 0 && path.Count > 0)
                {
                    Node nextRoom = path.Peek();
                    bool opened = false;
                    //判断向什么方向的房间
                    if (getCurrentRoom()[0] == nextRoom.xy[0] && getCurrentRoom()[1] - nextRoom.xy[1] < 0)
                    {
                        //up room
                        //调用AI 专用方法
                        opened = currentRoom.getNorthDoor().GetComponent<WoodDoor>().openDoor(this);
                        //开门成功

                    }
                    else if (getCurrentRoom()[0] == nextRoom.xy[0] && getCurrentRoom()[1] - nextRoom.xy[1] > 0)
                    {
                        //down room
                        opened = currentRoom.getSouthDoor().GetComponent<WoodDoor>().openDoor(this);
                    }
                    else if (getCurrentRoom()[1] == nextRoom.xy[1] && getCurrentRoom()[0] - nextRoom.xy[0] < 0)
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
                    if (opened)
                    {
                        bool result = eventController.excuteLeaveRoomEvent(currentRoom, this);

                        //非正式测试用，只考虑行动力足够


                        if (result == true)
                        {
                            //离开门成功
                            path.Pop();


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
                Debug.Log(this.playerName + "已经到达目标房间 (" + getCurrentRoom()[0] + "," + getCurrentRoom()[1] + ")");
				if(RoomConstant.ROOM_TYPE_BOOK_ROOM == currentRoom.getRoomType()) {
					BookRoom bookRoom = (BookRoom)currentRoom;
					Item item = bookRoom.getBox ().GetComponent<Box> ().getItem (this);
					if(item == null) {
						Debug.Log ("我的任务物品，已经没有了，已经是咸鱼了");
					} else {
						Debug.Log ("我的任务物品，拿到手了，我已经无敌了");
						this.bag.insertItem (item);
					}
				}
				//开始尝试寻找剧情道具
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
        scriptEnd = false;
        if (this.isPlayer())
        {
        }
        else
        {
            if (ss != null)
            {
                ss.scriptAction(this, roomContraller, eventController, diceRoll, aPathManager, roundController, battleController);
                Debug.Log("npc 当前回合状态是: "+ roundOver);
                scriptEnd = true;
            }
            else
            {
                defaultAction();
            }
        }

    }

    public void setActionPointrolled(bool actionPointrolled)
    {
        this.actionPointrolled = actionPointrolled;
    }

    public void setCurrentRoom(int[] nextRoomXYZ)
    {
        this.xyz = nextRoomXYZ;

		Debug.Log ("叶成亮进入房间");

		Vector3 temPos = new Vector3(xyz [0] * roomH+0.5f,xyz[1]*roomV+0.5f,0);
		this.transform.position = temPos;

    }

    public void updateActionPoint(int actionPoint)
    {
        this.actionPoint = actionPoint;
    }

    // Use this for initialization
    void Start()
    {
        //游戏一开始 所处的房间 默认房间的坐标为 0,0,0
        int[] roomXYZ = { 0, 0, 0 };
        setCurrentRoom(roomXYZ);
        abilityInfo = new int[] { 3, 3, 2, 6,15 };

        maxAbilityInfo = new int[] { 3, 3, 2, 6, 15 };
        this.actionPointrolled = false;
        this.deadFlag = false;
        Debug.Log("叶成亮 进入默认房间");
        playerName = "叶成亮";
        roomContraller = FindObjectOfType<RoomContraller>();
        diceRoll = FindObjectOfType<DiceRollCtrl>();
        eventController = FindObjectOfType<EventController>();
        roundController = FindObjectOfType<RoundController>();
        battleController = FindObjectOfType<BattleController>();
        this.bag = new Bag();
    }

    // Update is called once per frame
    void Update()
    {
		if (getAbilityInfo()[0] <=0 || getAbilityInfo()[1] <=0  ||
			getAbilityInfo()[2] <=0 || getAbilityInfo()[3] <=0  ||
			getAbilityInfo()[4] <=0
		) {
			this.deadFlag = true;
		}
        if (!roundOver) {

            if (scriptEnd && !waitFlag) {
                this.endRound();
            }
        }
    }

    public void setScriptAction(StoryScript ss)
    {
        this.ss = ss;
    }

    public bool isScriptWin()
    {
        return this.ss.getResult();
    }

    public StoryScript getScriptAciont()
    {
        return this.ss;
    }

    public bool isBoss()
    {
        return bossFlag;
    }

    public void setBoss(bool bossFlag)
    {
        this.bossFlag = bossFlag;
    }

    public int[] getMaxAbilityInfo()
    {
        return maxAbilityInfo;
    }

    public Bag getBag()
    {
        return this.bag;
    }
}
