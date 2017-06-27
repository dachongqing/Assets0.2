using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Nolan :  CommonUser
{

    private RoomContraller roomContraller;
    private DiceRollCtrl diceRoll;
    private EventController eventController;
    private RoundController roundController;
    private BattleController battleController;
    private CharaInfoManager charaInfoManager; 
    private GuangBoController guangBoController;
    private Queue<RoomInterface> targetRoomList = new Queue<RoomInterface>();
    private APathManager aPathManager = new APathManager();
    private bool waitPlan;
    
    private bool scriptEnd;

    public new bool isPlayer()
    {
        return false;
    }

    public new string getLiHuiURL()
    {
        return "lihui/ren_wu_1";
    }



    public new void defaultAction()
    {
        Debug.Log("run default aciont");
        if (waitPlan) {
            
            if (this.getGuangBoAction().isPlanSuccess())
            {
                this.sendMessageToPlayer(new string[] { "快点来，我的大刀已经饥渴难耐了。" });
            }
            else {
                this.sendMessageToPlayer(new string[] { "你们都不来吗？ 那我等会再来问问" });
                waitPlan = false;
            }

        } else {

            if (this.targetRoomList.Count<= 0 ) {
                Debug.Log("随便找个房间看看");
                this.targetRoomList.Enqueue(roomContraller.getRandomRoom());
            }
            RoomInterface target = this.targetRoomList.Peek();
            Debug.Log("targetRoom is " + target.getXYZ()[0] +","+ target.getXYZ()[1] + "," + target.getXYZ()[2]);
            if (AutoMoveManager.move(this, roomContraller, eventController, diceRoll, aPathManager, target.getXYZ()))
            {
                Debug.Log("reached the targetRoom is " + target.getXYZ()[0] + "," + target.getXYZ()[1] + "," + target.getXYZ()[2]);
                this.targetRoomList.Dequeue();
                if (target.getRoomType() == RoomConstant.ROOM_TYPE_BOOK)
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
                        this.getBag().insertItem(item);
                       
                    }

                    if (this.checkItem(ItemConstant.ITEM_CODE_SPEC_00001)) {
                        
                        guangBoController.insertGuangBo(new EveryoneGoTargetRoom(this.getName(), RoomConstant.ROOM_TYPE_BOOK, getTargetChara(), 60));
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

    public override void roundStart()
    {
        Debug.Log("roundStart round this game");
        startRound();
        scriptEnd = false;
        if (this.isPlayer())
        {
        }
        else
        {
            if (getScriptAciont() != null)
            {
                getScriptAciont().scriptAction(this, roomContraller, eventController, diceRoll, aPathManager, roundController, battleController);
                Debug.Log("npc 当前回合状态是: "+ isRoundOver());
                scriptEnd = true;
            }
            else
            {
                if (this.isFollowGuangBoAction())
                {
                    getGuangBoAction().guangBoAction(this, roomContraller, eventController, diceRoll, aPathManager, roundController, battleController);
                    scriptEnd = true;
                }
                else {
                    defaultAction();

                }
            }
        }

    }

   

   

   

    // Use this for initialization
    void Start()
    {
        roomContraller = FindObjectOfType<RoomContraller>();
        diceRoll = FindObjectOfType<DiceRollCtrl>();
        eventController = FindObjectOfType<EventController>();
        roundController = FindObjectOfType<RoundController>();
        battleController = FindObjectOfType<BattleController>();
        //duiHuaUImanager = FindObjectOfType<DuiHuaUImanager>();
        charaInfoManager = FindObjectOfType<CharaInfoManager>();
       
        guangBoController = FindObjectOfType<GuangBoController>();
        this.setName(SystemConstant.P1_NAME);
        //游戏一开始 所处的房间 默认房间的坐标为 0,0,0
        int[] roomXYZ = { 0, 0, RoomConstant.ROOM_Z_GROUND };
        setDistance(2.5f);
        setCurrentRoom(roomXYZ);
        setCrazyFlag(false);
        
        this.roomContraller.findRoomByXYZ(roomXYZ).setChara(this);
        this.roomContraller.findMiniRoomByXYZ(getCurrentRoom()).setPenable(this.getName(), true);
        setAbilityInfo(new int[] { 8, 3, 6, 7 });

        setMaxAbilityInfo(new int[] { 8, 3, 6, 7 });
        setActionPointrolled(false);
        setIsDead(false);

        setBag(new Bag());

        targetRoomList.Enqueue(roomContraller.getRandomRoom());
        targetRoomList.Enqueue(roomContraller.findRoomByRoomType(RoomConstant.ROOM_TYPE_BOOK));
        this.waitPlan = false;
        setTargetChara(new List<string>());
        getTargetChara().Add(SystemConstant.P1_NAME);
        this.setDesc("一身脏兮兮的白大褂，第一感觉是个跳大神的庸医。");

      
        
    }

    // Update is called once per frame
    void Update()
    {
		if (getAbilityInfo()[0] <=0 || getAbilityInfo()[1] <=0  ||
			getAbilityInfo()[2] <=0 || getAbilityInfo()[3] <=0  			
		) {
            Debug.Log(this.getName() + " 已经死亡。。。");
            setIsDead(true);
		}
        if (getAbilityInfo()[3] <= 3) {
            setCrazyFlag(true);
        }
        if (!isRoundOver()) {

            if (scriptEnd && !isWaitPlayer()) {
                this.endRound();
            }
        }
    }

   
   
   



	
	

	

	void OnMouseDown ()
	{

        if (!SystemUtil.IsTouchedUI())
        {

            string[] co = new string[] { "你感觉到绝望了吗", "老实讲，我要带你飞了" };
           // duiHuaUImanager.showDuiHua(getLiHuiURL(), co);
            charaInfoManager.showCharaInfoMenu(this, co);
        }
        else {
            Debug.Log("click ui");
        }

	}

   

  

   

   

   

   

   

  
}
