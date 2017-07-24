using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jessie : CommonUser {

    private RoomContraller roomContraller;
    private DiceRollCtrl diceRoll;
    private EventController eventController;
    private RoundController roundController;
    private BattleController battleController;
    private CharaInfoManager charaInfoManager;
    private GuangBoController guangBoController;
    private StoryController storyController;
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

    public new string getDeitalPic()
    {
        return "detail/kate_detail01";
    }

    public new string getProfilePic()
    {
        return "detail/9";
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
      //  Debug.Log("roundStart round this game");
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
            //    Debug.Log("npc 当前回合状态是: " + isRoundOver());
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
        this.setName(SystemConstant.P3_NAME);
        setDistance(2.5f);
        setBag(new Bag());
        //游戏一开始 所处的房间 默认房间的坐标为 0,0,0
        int[] roomXYZ;
        if (this.neworLoad)
        {
            roomXYZ = new int[] { 0, 0, RoomConstant.ROOM_Z_GROUND };
            setCrazyFlag(false);
            setAbilityInfo(new int[] { 5, 2, 7, 10 });
            setMaxAbilityInfo(new int[] { 5, 2, 7, 10 });
            setActionPointrolled(false);
            setIsDead(false);
           

            this.setDesc("一脸严肃的样子，不爱说笑的神父。");
            this.waitPlan = false;
          //  this.setClickMessage(new string[] {  });
            getTargetRoomList().Enqueue(roomContraller.getRandomRoom());
            getTargetRoomList().Enqueue(roomContraller.findRoomByRoomType(RoomConstant.ROOM_TYPE_BOOK));
        }
        else
        {
            P0 p = loadInfo(this.getName());
            roomXYZ = p.Xyz;
            this.waitPlan = p.WaitPlan;
            loadInfo(this, p, roomContraller);
        }
        setCurrentRoom(roomXYZ);
        this.roomContraller.findRoomByXYZ(roomXYZ).setChara(this);
        this.roomContraller.setCharaInMiniMap(roomXYZ, this, true);
        this.setClickMessage(this.roomContraller.findRoomByXYZ(roomXYZ).findSomethingNews(this).ToArray());
    }

    // Update is called once per frame
    void Update()
    {
        if (getAbilityInfo()[0] <= 0 || getAbilityInfo()[1] <= 0 ||
            getAbilityInfo()[2] <= 0 || getAbilityInfo()[3] <= 0
        )
        {
          //  Debug.Log(this.getName() + " 已经死亡。。。");
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
