using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Martin : CommonUser {

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
       
       Debug.Log("随便找个房间看看");               
       AutoMoveManager.move(this, roomContraller, eventController, diceRoll, aPathManager, roomContraller.getRandomRoom().getXYZ());
        endRound();
    }

    public override void checkTargetRoomLocked(string roomType)
    {
       
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
                Debug.Log("npc 当前回合状态是: " + isRoundOver());
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
        this.setName(SystemConstant.P5_NAME);
        //游戏一开始 所处的房间 默认房间的坐标为 0,0,0
        int[] roomXYZ = { 0, 0, RoomConstant.ROOM_Z_GROUND };
        setDistance(-1.5f);
        setCurrentRoom(roomXYZ);
        setCrazyFlag(false);

        this.roomContraller.findRoomByXYZ(roomXYZ).setChara(this);
        this.roomContraller.findMiniRoomByXYZ(getCurrentRoom()).setPenable(this.getName(), true);
        setAbilityInfo(new int[] { 6, 4, 8, 4 });

        setMaxAbilityInfo(new int[] { 6, 4, 8, 4 });
        setActionPointrolled(false);
        setIsDead(false);

        setBag(new Bag());
     
        this.waitPlan = false;

        this.setDesc("沉默寡言，不爱说话的黑客.");
        this.waitPlan = false;
       
        this.setClickMessage(new string[] { "谁能给我一台电脑？" });
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
            charaInfoManager.showCharaInfoMenu(this, getClickMessage());
        }
        else
        {
            Debug.Log("click ui");
        }

    }



}
