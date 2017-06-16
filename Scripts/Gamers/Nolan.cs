using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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

	private DuiHuaUImanager duiHuaUImanager;

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
    public void setWaitPlayer(bool waitFlag) {
        this.waitFlag = waitFlag;
    }

    private bool waitPlan;

    private GuangBoAction gba;

    public void defaultAction()
    {
        if (waitPlan) {
            
            if (this.gba.isPlanSuccess())
            {
                this.sendMessageToPlayer(new string[] { "快点来，我的大刀已经饥渴难耐了。" });
            }
            else {
                this.sendMessageToPlayer(new string[] { "你们都不来吗？ 那我等会再来问问" });
                waitPlan = false;
            }

        } else {

            if (this.TargetRoomList.Count<= 0 ) {
                this.TargetRoomList.Enqueue(roomContraller.getRandomRoom());
            }
            RoomInterface target = this.TargetRoomList.Peek();
            if (AutoMoveManager.move(this, roomContraller, eventController, diceRoll, aPathManager, target.getXYZ()))
            {
                this.TargetRoomList.Dequeue();
                if (target.getRoomType() == RoomConstant.ROOM_TYPE_BOOK_ROOM)
                {
                    BookRoom bookRoom = (BookRoom)roomContraller.findRoomByXYZ(this.getCurrentRoom()); ;
                    Item item = bookRoom.getBox().GetComponent<Box>().getItem(this);
                    if (item == null && !this.checkItem(ItemConstant.ITEM_CODE_SPEC_00001))
                    {
                        // Debug.Log("我的任务物品，已经没有了，已经是咸鱼了");
                        this.sendMessageToPlayer(new string[] { "我的任务物品，已经没有了，已经是咸鱼了.." });
                    }
                    else
                    {
                       // Debug.Log("我的任务物品，拿到手了，我已经无敌了");
                        this.sendMessageToPlayer(new string[] { "哈哈。。我的任务物品，拿到手了，我已经无敌了！", "所有人都得死！" });
                        this.bag.insertItem(item);
                       
                    }

                    if (this.checkItem(ItemConstant.ITEM_CODE_SPEC_00001)) {
                        this.gba = new EveryoneGoTargetRoom(this.getName(), RoomConstant.ROOM_TYPE_BOOK_ROOM, targetChara, 60);
                        guangBoController.insertGuangBo(gba);
                        waitPlan = true;
                        this.sendMessageToPlayer(new string[] { "书房有一个好东西，大家都来看看啊"," 你一定要来啊"});

                    }
                }
                else {
                    this.sendMessageToPlayer(new string[] { "我已经到达目标房间 :" + target.getRoomName(), "没有什么中意的地方，我准备去其他房间看看" });
                }
            
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
                Debug.Log("npc 当前回合状态是: "+ roundOver);
                scriptEnd = true;
            }
            else
            {
                if (this.isFollowGuangBoAction())
                {
                    this.guangBoAction.guangBoAction(this, roomContraller, eventController, diceRoll, aPathManager, roundController, battleController);
                    scriptEnd = true;
                }
                else {
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
        this.xyz = nextRoomXYZ;
        

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
        roomContraller = FindObjectOfType<RoomContraller>();
        diceRoll = FindObjectOfType<DiceRollCtrl>();
        eventController = FindObjectOfType<EventController>();
        roundController = FindObjectOfType<RoundController>();
        battleController = FindObjectOfType<BattleController>();
		duiHuaUImanager = FindObjectOfType<DuiHuaUImanager>();
        listener = FindObjectOfType<GuangBoListener>();
        guangBoController = FindObjectOfType<GuangBoController>();
        playerName = SystemConstant.P1_NAME;
        //游戏一开始 所处的房间 默认房间的坐标为 0,0,0
        int[] roomXYZ = { 0, 0, RoomConstant.ROOM_Z_GROUND };
        setCurrentRoom(roomXYZ);
        if (roomContraller == null ) {
        }
            Debug.Log("@@@@@@@@@@@@@@@@ roomContraller " + roomContraller);
        if (this.roomContraller.findRoomByXYZ(roomXYZ) == null)
        {
            Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }
        this.roomContraller.findRoomByXYZ(roomXYZ).setChara(this);
        this.roomContraller.findMiniRoomByXYZ(this.xyz).setPenable(this.getName(), true);
        abilityInfo = new int[] { 3, 3, 2, 6,15 };

        maxAbilityInfo = new int[] { 3, 3, 2, 6, 15 };
        this.actionPointrolled = false;
        this.deadFlag = false;
      
        this.bag = new Bag();
        TargetRoomList.Enqueue(roomContraller.getRandomRoom());
        TargetRoomList.Enqueue(roomContraller.findRoomByRoomType(RoomConstant.ROOM_TYPE_BOOK_ROOM));
        this.waitPlan = false;
        targetChara = new List<string>();
        targetChara.Add(SystemConstant.P2_NAME);
        
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

        if (!SystemUtil.IsTouchedUI())
        {

            string[] co = new string[] { "你感觉到绝望了吗", "老实讲，我要带你飞了" };
            duiHuaUImanager.showDuiHua(getLiHuiURL(), co);
        }
        else {
            Debug.Log("click ui");
        }

	}

    public string getLiHuiURL()
    {
        return "lihui/ren_wu_2";
    }

    private GuangBoListener listener;


    public void sendMessageToPlayer(string[] message) {
        
           
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

    public bool checkItem(string itemCode) {
        return this.getBag().checkTaskItem(itemCode);
    }

    public List<string> getTargetChara() {
        return this.targetChara;
    }


}
