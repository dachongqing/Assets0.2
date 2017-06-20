using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Ben : MonoBehaviour, NPC
{
    [Tooltip("房间宽度")] public float roomH = 13.7f;
    [Tooltip("房间高度")] public float roomV = 11f;

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

    //private DuiHuaUImanager duiHuaUImanager;

    private CharaInfoManager charaInfoManager;

    private Queue<RoomInterface> TargetRoomList = new Queue<RoomInterface>();

    private GuangBoController guangBoController;

    List<string> targetChara;

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
    public void setWaitPlayer(bool waitFlag)
    {
        this.waitFlag = waitFlag;
    }

    private bool waitPlan;

    private GuangBoAction gba;

    public void defaultAction()
    {
        if (waitPlan)
        {
            
        }
        else
        {

            if (this.TargetRoomList.Count <= 0)
            {
                this.TargetRoomList.Enqueue(roomContraller.getRandomRoom());
            }
            RoomInterface target = this.TargetRoomList.Peek();
            if (AutoMoveManager.move(this, roomContraller, eventController, diceRoll, aPathManager, target.getXYZ()))
            {
              
                this.sendMessageToPlayer(new string[] { "我已经到达目标房间 :" + target.getRoomName(), "没有什么中意的地方，我准备去其他房间看看" });
              

            }
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
                Debug.Log("npc 当前回合状态是: " + roundOver);
                scriptEnd = true;
            }
            else
            {
                if (this.isFollowGuangBoAction())
                {
                    Debug.Log("开始执行广播任务。。。。");

                    this.guangBoAction.guangBoAction(this, roomContraller, eventController, diceRoll, aPathManager, roundController, battleController);
                   
                    scriptEnd = true;
                }
                else
                {
                    Debug.Log("开始执行默认任务。。。。");
                    defaultAction();

                }
            }
        }

    }

    public void setActionPointrolled(bool actionPointrolled)
    {
        this.actionPointrolled = actionPointrolled;
    }

    public void setCurrentRoom(int[] nextRoomXYZ)
    {
        Vector3 temPos;
        this.xyz = nextRoomXYZ;
        //		Debug.Log ("玩家进入新房间: ");

        if (xyz[2] == RoomConstant.ROOM_Z_UP)
        {
            temPos = new Vector3(xyz[0] * roomH - 0.5f, RoomConstant.ROOM_Y_UP + (xyz[1] * roomV + 0.5f), 0);
        }
        else if (xyz[2] == RoomConstant.ROOM_Z_GROUND)
        {
            temPos = new Vector3(xyz[0] * roomH - 0.5f, RoomConstant.ROOM_Y_GROUND + (xyz[1] * roomV + 0.5f), 0);
        }
        else
        {
            temPos = new Vector3(xyz[0] * roomH - 0.5f, RoomConstant.ROOM_Y_DOWN + (xyz[1] * roomV + 0.5f), 0);
        }


        this.transform.position = temPos;

    }

    public void updateActionPoint(int actionPoint)
    {
        this.actionPoint = actionPoint;
    }

    // Use this for initialization
    void Start()
    {
        roomContraller = FindObjectOfType<RoomContraller>();
        diceRoll = FindObjectOfType<DiceRollCtrl>();
        eventController = FindObjectOfType<EventController>();
        roundController = FindObjectOfType<RoundController>();
        battleController = FindObjectOfType<BattleController>();
        // duiHuaUImanager = FindObjectOfType<DuiHuaUImanager>();
        charaInfoManager = FindObjectOfType<CharaInfoManager>();
         guangBoController = FindObjectOfType<GuangBoController>();
        listener = FindObjectOfType<GuangBoListener>();

        playerName = SystemConstant.P2_NAME;
        //游戏一开始 所处的房间 默认房间的坐标为 0,0,0
        int[] roomXYZ = { 0, 0, RoomConstant.ROOM_Z_GROUND };
        setCurrentRoom(roomXYZ);
        this.roomContraller.findRoomByXYZ(roomXYZ).setChara(this);
        this.roomContraller.findMiniRoomByXYZ(this.xyz).setPenable(this.getName(), true);
        abilityInfo = new int[] { 3, 3, 2, 6, 15 };
        
        maxAbilityInfo = new int[] { 3, 3, 2, 6, 15 };
        this.actionPointrolled = false;
        this.deadFlag = false;
       
        this.bag = new Bag();
        TargetRoomList.Enqueue(roomContraller.getRandomRoom());

        this.waitPlan = false;
        targetChara = new List<string>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (getAbilityInfo()[0] <= 0 || getAbilityInfo()[1] <= 0 ||
            getAbilityInfo()[2] <= 0 || getAbilityInfo()[3] <= 0 ||
            getAbilityInfo()[4] <= 0
        )
        {
            this.deadFlag = true;
        }
        if (!roundOver)
        {

            if (scriptEnd && !waitFlag)
            {
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

    private int diceNum;

    public void setDiceNumberBuffer(int number)
    {
        this.diceNum = number;
    }
    public int getDiceNumberBuffer()
    {
        int tmp = this.diceNum;
        this.diceNum = 0;
        return tmp;
    }

    private int diceValue;

    public void setDiceValueBuffer(int value)
    {
        this.diceValue = value;
    }

    public int getDiceValueBuffer()
    {

        int tmp = this.diceValue;
        this.diceValue = 0;
        return tmp;
    }

    private int damge = 1;

    public void setDamgeBuffer(int damge)
    {

        this.damge = damge;
    }

    public int getDamgeBuffer()
    {

        int tmp = this.damge;
        this.damge = 1;
        return tmp;
    }

    void OnMouseDown()
    {

        if (!SystemUtil.IsTouchedUI())
        {

            string[] co = new string[] { "导演让我干什么就干什么", "盒饭什么时候发？" };
            //duiHuaUImanager.showDuiHua(getLiHuiURL(), co);
            charaInfoManager.showCharaInfoMenu(this,co);
}
        else
        {
            Debug.Log("click ui");
        }

    }

    public string getLiHuiURL()
    {
        return "lihui/ren_wu_2";
    }

    private GuangBoListener listener;


    public void sendMessageToPlayer(string[] message)
    {


        listener.insert(this, message);

    }

    private bool isFollowGuangBoActionFlag;

    public bool isFollowGuangBoAction()
    {
        return isFollowGuangBoActionFlag;
    }

    public void setFollowGuangBoAction(bool flag)
    {
        this.isFollowGuangBoActionFlag = flag;
    }

    private GuangBoAction guangBoAction;

    public void setGuangBoAction(GuangBoAction gb)
    {
        this.guangBoAction = gb;
    }

    public bool checkItem(string itemCode)
    {
        return this.getBag().checkItem(itemCode);
    }

    public List<string> getTargetChara()
    {
        return this.targetChara;
    }

    private string desc;

    public void setDesc(string desc)
    {
        this.desc = desc;
    }

    public string getDesc() {
        return this.desc;
    }
}
