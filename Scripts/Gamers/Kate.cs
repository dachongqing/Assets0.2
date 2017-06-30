using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Kate : CommonUser
{

    private RoomContraller roomContraller;
    private DiceRollCtrl diceRoll;
    private EventController eventController;
    private RoundController roundController;
    private BattleController battleController;
    private CharaInfoManager charaInfoManager;
    private GuangBoController guangBoController;
    private StoryController storyController;
    private Queue<RoomInterface> targetRoomList = new Queue<RoomInterface>();
    private APathManager aPathManager = new APathManager();
    private bool waitPlan;
    public GameObject servant;
    private BlackSignStory bss;

    private bool scriptEnd;

    public new bool isPlayer()
    {
        return false;
    }

    public new string getLiHuiURL()
    {
        return "lihui/ren_wu_2";
    }



    public new void defaultAction()
    {
      //  Debug.Log("run default aciont");
        if (waitPlan)
        {

            if (this.getGuangBoAction().isPlanSuccess())
            {
                this.sendMessageToPlayer(new string[] { "我这里有新线索发现，都来听我分析。" });
                if (this.getGuangBoAction().isGuangBoActionEnd()) {
                    waitPlan = false;
                }
            }
            else
            {
                this.sendMessageToPlayer(new string[] { "你们这群呆逼，都不来算了。" });
                waitPlan = false;
            }

        }
        else
        {
            if (this.targetRoomList.Count <= 0)
            {
                NPC ben = (NPC)FindObjectOfType<Ben>();
                if (ben.isDead())
                {
                    Debug.Log("随便找个房间看看");
                    this.targetRoomList.Enqueue(roomContraller.getRandomRoom());

                }
                else
                {
                    if (AutoMoveManager.move(this, roomContraller, eventController, diceRoll, aPathManager, ben.getCurrentRoom()))
                    {
                        Debug.Log("kate 开始跟踪冒险家");
                        if (this.getAbilityInfo()[3] <= 1)
                        {
                             Debug.Log("kate 疯了， 准备杀人了");
                            ben.sendMessageToPlayer(new string[] { "侦探疯了，他要杀死所有人！", " 大家快跑。。。", "啊。。。" });
                            ben.getAbilityInfo()[0] = 0;
                            this.getAbilityInfo()[3] = 1;
                            
                            if (storyController.checkStoryStartBySPEvnet(bss, this, roundController, roomContraller.findRoomByXYZ(this.getCurrentRoom())))
                            {
                                Debug.Log(" 黑色征兆 剧本开启");
                                this.sendMessageToPlayer(new string[] { "啊！啊！啊！。。。", "冒险家被我杀死了。。。" });
                                this.getAbilityInfo()[3] = 3;
                            }
                            else {
                                Debug.Log(" 剧本开启失败");
                            }
                               


                        }
                        else {
                            Debug.Log("kate 没疯，暗中观察");
                            this.setClickMessage(new string[] { SystemConstant.P2_NAME + ", 让我看一下你当年手术的地方吧？" });
                        }
                    }
                }
            }
            else { 

                RoomInterface target = this.targetRoomList.Peek();
                if (target == null ) {
                    Debug.Log("target is null !!!!error");
                }
                if (target.getRoomType() == RoomConstant.ROOM_TYPE_HOSPITAIL_TRI_OPERATION && isTargetRoomLocked())
                {

                    Debug.Log("NPC发现手术室被锁着 去保安室找钥匙");

                    if (AutoMoveManager.move(this, roomContraller, eventController, diceRoll, aPathManager, RoomConstant.ROOM_TYPE_HOSPITAIL_SECURITY))
                    {
                        Debug.Log("达到保安室");
                        H_securityRoom surgeryRoom = (H_securityRoom)roomContraller.findRoomByXYZ(this.getCurrentRoom());
                        Item item = surgeryRoom.getKeysCabinet().GetComponent<KeysCabinet>().getItem(this);
                        if (item != null)
                        {
                            this.getBag().insertItem(item);
                            this.setTargetRoomLocked(false);
                        }
                        else
                        {
                            //没有找到钥匙， 直接判断手术室有没有被打开
                            H_tridOperationRoom tridOperationRoom = (H_tridOperationRoom)roomContraller.findRoomByRoomType(RoomConstant.ROOM_TYPE_HOSPITAIL_TRI_OPERATION);
                            if (tridOperationRoom.isLock())
                            {
                                Debug.Log("第3手术室锁着， 我又没钥匙,暂定瞎逛");
                                AutoMoveManager.move(this, roomContraller, eventController, diceRoll, aPathManager, roomContraller.getRandomRoom().getRoomType());
                            }
                            else
                            {
                                this.setTargetRoomLocked(false);
                            }
                        }
                    }
                }
                else
                {
                   // Debug.Log("targetRoom is " + target.getXYZ()[0] + "," + target.getXYZ()[1] + "," + target.getXYZ()[2]);
                    if (AutoMoveManager.move(this, roomContraller, eventController, diceRoll, aPathManager, target.getXYZ()))
                    {
                      //  Debug.Log("reached the targetRoom is " + target.getXYZ()[0] + "," + target.getXYZ()[1] + "," + target.getXYZ()[2]);
                        this.targetRoomList.Dequeue();
                        if (target.getRoomType() == RoomConstant.ROOM_TYPE_HOSPITAIL_SURGERY)
                        {
                            Debug.Log("达到外科室");
                            H_surgeryRoom surgeryRoom = (H_surgeryRoom)roomContraller.findRoomByXYZ(this.getCurrentRoom());
                            Item item = surgeryRoom.getBookTable().GetComponent<BookTable>().getItem(this);
                            if (item != null)
                            {

                                getTargetChara().Add(SystemConstant.P2_NAME);
                                setGuangBoAction(new EveryoneGoTargetRoom(this.getName(), RoomConstant.ROOM_TYPE_HOSPITAIL_SURGERY, getTargetChara(), 8));
                                guangBoController.insertGuangBo(this.getGuangBoAction());
                                waitPlan = true;
                                this.sendMessageToPlayer(new string[] { SystemConstant.P2_NAME + ", 这个病例上有你的名字", " 你最好来看看" });
                                this.getBag().insertItem(item);
                            }
                        }
                        else if (target.getRoomType() == RoomConstant.ROOM_TYPE_HOSPITAIL_TRI_OPERATION)
                        {
                            Debug.Log("到达第3手术室");
                            H_tridOperationRoom tridOperationRoom = (H_tridOperationRoom)roomContraller.findRoomByXYZ(this.getCurrentRoom());
                            Item item = tridOperationRoom.getOperatingTable().GetComponent<OperatingTable>().getItem(this);
                            if (item != null)
                            {
                                this.getBag().insertItem(item);
                                this.sendMessageToPlayer(new string[] { SystemConstant.P1_NAME + ", 你最好解释下这个解剖样本，这个是什么病？" });
                            }
                            else
                            {
                                Debug.Log("这个解剖手术太奇怪了，我一定错过了什么。我要好好检查下。");
                                this.sendMessageToPlayer(new string[] { "这个解剖手术太奇怪了，我一定错过了什么。", "我要好好检查下。" });
                            }

                        }
                        else if (target.getRoomType() == RoomConstant.ROOM_TYPE_HOSPITAIL_MORGUE)
                        {
                            Debug.Log("到达停尸房");
                            this.sendMessageToPlayer(new string[] { "这里有好多尸体都标明了实验失败的标签", "而且实验人全是写的你医生的名字", "大家把医生抓住啊，他有重大嫌疑！" });
                        }
                        else if (target.getRoomType() == RoomConstant.ROOM_TYPE_HOSPITAIL_STORE)
                        {
                            Debug.Log("到达储藏室");
                            H_storeRoom storeRoom = (H_storeRoom)roomContraller.findRoomByXYZ(this.getCurrentRoom());
                            Item item = storeRoom.getSafeCabinet().GetComponent<SafeCabinet>().getItem(this);
                            if (item != null)
                            {
                                this.getBag().insertItem(item);
                                this.getAbilityInfo()[3] = 1;
                            }
                            else
                            {
                                if (!isCrazy())
                                {
                                    //this.sendMessageToPlayer(target.findSomethingNews(this.getName()).ToArray());
                                    this.setClickMessage(target.findSomethingNews(this.getName()).ToArray());
                                }
                            }

                        }
                        else
                        {
                            Debug.Log("找路失败了");

                        }
                    }
                }
            }
        }
       endRound();
    }

    public override void checkTargetRoomLocked(string roomType)
    {
        if (roomType == RoomConstant.ROOM_TYPE_HOSPITAIL_TRI_OPERATION)
        {
            Debug.Log("是我的目标房间锁了");
            setTargetRoomLocked(true);

        }
        else
        {
            Debug.Log("不是我的目标房间锁了");
            setTargetRoomLocked(false);
        }
    }

    public override void roundStart()
    {
     //   Debug.Log("roundStart round this game");
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
              //  Debug.Log("npc 当前回合状态是: " + isRoundOver());
                scriptEnd = true;
            }
            else
            {
                if (this.isFollowGuangBoAction())
                {
                    getGuangBoAction().guangBoAction(this, roomContraller, eventController, diceRoll, aPathManager, roundController, battleController);
                    scriptEnd = true;
                }
                else
                {
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
        setGuangBoListener(FindObjectOfType<GuangBoListener>());
        guangBoController = FindObjectOfType<GuangBoController>();
        storyController = FindObjectOfType<StoryController>();
        bss = new BlackSignStory();
        this.setName(SystemConstant.P4_NAME);
        //游戏一开始 所处的房间 默认房间的坐标为 0,0,0
        int[] roomXYZ = { 0, 0, RoomConstant.ROOM_Z_GROUND };
        setDistance(-0.5f);
        setCurrentRoom(roomXYZ);
        setCrazyFlag(false);

        this.roomContraller.findRoomByXYZ(roomXYZ).setChara(this);
        this.roomContraller.findMiniRoomByXYZ(getCurrentRoom()).setPenable(this.getName(), true);
        setAbilityInfo(new int[] { 8, 1, 6, 7 });

        setMaxAbilityInfo(new int[] { 8, 3, 6, 7 });
        setActionPointrolled(false);
        setIsDead(false);

        setBag(new Bag());

        //targetRoomList.Enqueue(roomContraller.getRandomRoom());
        //targetRoomList.Enqueue(roomContraller.getRandomRoom());
        targetRoomList.Enqueue(roomContraller.findRoomByRoomType(RoomConstant.ROOM_TYPE_HOSPITAIL_SURGERY));
        //targetRoomList.Enqueue(roomContraller.getRandomRoom());
        targetRoomList.Enqueue(roomContraller.findRoomByRoomType(RoomConstant.ROOM_TYPE_HOSPITAIL_TRI_OPERATION));
       // targetRoomList.Enqueue(roomContraller.getRandomRoom());
        targetRoomList.Enqueue(roomContraller.findRoomByRoomType(RoomConstant.ROOM_TYPE_HOSPITAIL_MORGUE));
        //targetRoomList.Enqueue(roomContraller.getRandomRoom());
       // targetRoomList.Enqueue(roomContraller.getRandomRoom());
        targetRoomList.Enqueue(roomContraller.findRoomByRoomType(RoomConstant.ROOM_TYPE_HOSPITAIL_STORE));
        this.waitPlan = false;

        this.setDesc("看似小萝莉，其实是一个出名的侦探。");
        this.waitPlan = false;
        setTargetChara(new List<string>());
        getTargetChara().Add(SystemConstant.P2_NAME);
        this.setClickMessage(new string[] { "真相只有一个。", "你就是犯人。" });
    }

    // Update is called once per frame
    void Update()
    {
        if (getAbilityInfo()[0] <= 0 || getAbilityInfo()[1] <= 0 ||
            getAbilityInfo()[2] <= 0 || getAbilityInfo()[3] <= 0
        )
        {
            Debug.Log(this.getName() + " 已经死亡。。。");
            setIsDead(true);
        }
        if (getAbilityInfo()[3] <= 3)
        {
            //发疯后行动力加强
            this.getAbilityInfo()[1] = this.getAbilityInfo()[1] + 3;
            setCrazyFlag(true);
        }
        if (!isRoundOver())
        {

            if (scriptEnd && !isWaitPlayer())
            {
                this.endRound();
            }
        }
    }
    
    void OnMouseDown()
    {

        if (!SystemUtil.IsTouchedUI())
        {


            // duiHuaUImanager.showDuiHua(getLiHuiURL(), co);
            if (this.isCrazy() || this.isBoss()) {
                battleController.fighte(this,roundController.getPlayerChara());
            } else
            {
                charaInfoManager.showCharaInfoMenu(this, getClickMessage());
            }
        }
        else
        {
            Debug.Log("click ui");
        }

    }

   

    
}
