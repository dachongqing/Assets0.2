using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, NPC
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

    private bool deadFlag;

    private APathManager aPathManager = new APathManager();

    private RoomContraller roomContraller;

    private EventController eventController;

    private RoundController roundController;

    private BattleController battleController;

    private DiceRollCtrl diceRoll;

    private StoryScript ss;

    private bool bossFlag;

    private Bag bag;

    //private DuiHuaUImanager duiHuaUImanager;
    private CharaInfoManager charaInfoManager;

    private GuangBoListener guangBoListener;

    List<string> targetChara;

    public int getActionPoint()
    {
        return actionPoint;
    }

    public int[] getAbilityInfo()
    {
        return abilityInfo;
    }

    public int[] getCurrentRoom()
    {
        return xyz;
    }

    public void setCurrentRoom(int[] xyz)
    {
        Vector3 temPos;
        this.xyz = xyz;
        //		Debug.Log ("玩家进入新房间: ");

        if (xyz[2] == RoomConstant.ROOM_Z_UP)
        {
            temPos = new Vector3(xyz[0] * roomH, RoomConstant.ROOM_Y_UP + (xyz[1] * roomV), 0);
        }
        else if(xyz[2] == RoomConstant.ROOM_Z_GROUND)
        {
             temPos = new Vector3(xyz[0] * roomH, RoomConstant.ROOM_Y_GROUND + (xyz[1] * roomV), 0);
        } else 
        {
            temPos = new Vector3(xyz[0] * roomH, RoomConstant.ROOM_Y_DOWN + (xyz[1] * roomV), 0);
        }
		

		this.transform.position = temPos;
    }

    public string getName()
    {
        return playerName;
    }

    public bool isDead()
    {
        return deadFlag;
    }

    public bool isPlayer()
    {
        return true;
    }

    public bool isRoundOver()
    {
        return roundOver;
    }

    public void endRound()
    {
        this.roundOver = true;
        guangBoListener.cleanQuere();
    }

    public bool isWaitPlayer()
    {
        return waitFlag;
    }

    private bool waitFlag;
    public void setWaitPlayer(bool waitFlag)
    {
        this.waitFlag = waitFlag;
    }

    public void roundStart()
    {

        roundOver = false;
        if (this.isPlayer())
        {
        }
        else
        {
            if (ss != null)
            {
                ss.scriptAction(this, roomContraller, eventController, diceRoll, aPathManager, roundController,battleController);
            }
            else
            {
                if (this.isFollowGuangBoAction())
                {
                    this.guangBoAction.guangBoAction(this, roomContraller, eventController, diceRoll, aPathManager, roundController, battleController);
                }
                else
                {
                    defaultAction();

                }
            }
        }

    }

    public void updateActionPoint(int actionPoint)
    {
        this.actionPoint = actionPoint;
    }

    public bool ActionPointrolled()
    {
        return actionPointrolled;
    }

    public void setActionPointrolled(bool actionPointrolled)
    {
        this.actionPointrolled = actionPointrolled;
    }


    // Use this for initialization
    void Start()
    {
        roomContraller = FindObjectOfType<RoomContraller>();
        diceRoll = FindObjectOfType<DiceRollCtrl>();
        eventController = FindObjectOfType<EventController>();
        battleController = FindObjectOfType<BattleController>();
        roundController = FindObjectOfType<RoundController>();
        //	duiHuaUImanager = FindObjectOfType<DuiHuaUImanager>();
        charaInfoManager = FindObjectOfType<CharaInfoManager>();
            guangBoListener = FindObjectOfType<GuangBoListener>();
        //游戏一开始 所处的房间 默认房间的坐标为 0,0,0
        playerName = SystemConstant.P6_NAME;
        int[] roomXYZ = { 0, 0, RoomConstant.ROOM_Z_GROUND };
        setCurrentRoom(roomXYZ);
        this.roomContraller.findRoomByXYZ(roomXYZ).setChara(this);
        this.roomContraller.findMiniRoomByXYZ(roomXYZ).setPenable(this.getName(), true);
        abilityInfo = new int[] { 5, 4, 6, 8, 20};

        maxAbilityInfo = new int[] { 5, 4, 6, 8,20 };
        this.deadFlag = false;
        this.actionPointrolled = false;
        this.waitFlag = true;
       
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

    }

    public void defaultAction()
    {

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

	public void setDiceNumberBuffer(int number){
		this.diceNum = number;
	}

	public int getDiceNumberBuffer() {
		int tmp = this.diceNum;
		this.diceNum = 0;
		return tmp;
	}

	private int diceValue;

	public void setDiceValueBuffer(int value) {
		this.diceValue = value;
	}

	public int getDiceValueBuffer(){

		int tmp = this.diceValue;
		this.diceValue = 0;
		return tmp;
	}

	private int damge =1;

	public void setDamgeBuffer (int damge) {
	
		this.damge = damge;
	}

	public int getDamgeBuffer () {

		int tmp = this.damge;
		this.damge = 1;
		return tmp;
	}

	void OnMouseDown ()
	{

		if(!SystemUtil.IsTouchedUI()) {

			string[] co = new string[] { "我是谁", "我从哪里来","我要到哪里去" };
            // duiHuaUImanager.showDuiHua(getLiHuiURL(), co);
            charaInfoManager.showCharaInfoMenu(this, co);

        }

	}

    public string getLiHuiURL() {
        return "lihui/ren_wu_1";
    }

   
    public void sendMessageToPlayer(string[] message)
    {

        
        guangBoListener.insert(this, message);

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

    public List<string> getTargetChara()
    {
        return this.targetChara;
    }

    public bool checkItem(string itemCode)
    {
       return this.bag.checkTaskItem(itemCode);
    }

    private string desc;

    public void setDesc(string desc)
    {
        this.desc = desc;
    }

    public string getDesc()
    {
        return this.desc;
    }
}
