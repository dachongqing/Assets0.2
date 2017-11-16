using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : CommonUser
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

    public override bool isPlayer()
    {
        return true;
    }

    public new string getLiHuiURL()
    {
        return "lihui/ren_wu_1";
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

    }

    public override void findthisRoomNews(int[] nextRoomXYZ)
    {
        this.setClickMessage(this.roomContraller.findRoomByXYZ(nextRoomXYZ).findSomethingNews(this).ToArray());
    }

    public override void roundStart()
    {

         Debug.Log("player Start round this game");
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
                //   Debug.Log("npc 当前回合状态是: " + isRoundOver());
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
        this.setName(SystemConstant.P6_NAME);
        int[] roomXYZ;
        setBag(new Bag());
        if (this.neworLoad)
        {
           roomXYZ = new int[]{ 0, 0, RoomConstant.ROOM_Z_GROUND };
           setCrazyFlag(false);
            //for test 4-->9
            setAbilityInfo(new int[] { 7, 9, 6, 7 });
            setMaxAbilityInfo(new int[] { 7, 9, 6, 7 });
            setActionPointrolled(false);
            setIsDead(false);          
            this.setDesc("外乡人.");
            this.waitPlan = false;
            setDistance(0);
            setCurrentRoom(roomXYZ);
            // this.setClickMessage(new string[] { "我就是来旅游的。" });
        } else
        {
            P0 p = loadInfo(this.getName());
            roomXYZ = p.Xyz;
            this.waitPlan = p.WaitPlan;
            setDistance(0);
            setCurrentRoom(roomXYZ);
            loadInfo(this, p, roomContraller);
        }
            //游戏一开始 所处的房间 默认房间的坐标为 0,0,0
       

        this.roomContraller.findRoomByXYZ(roomXYZ).setChara(this);
        this.roomContraller.setCharaInMiniMap(roomXYZ, this, true);
        this.setClickMessage(this.roomContraller.findRoomByXYZ(roomXYZ).findSomethingNews(this).ToArray());

        /*
        Item item1 = new ItemTask(ItemConstant.ITEM_CODE_SPEC_Y0006
           , ItemDesConstant.ITEM_CODE_SPEC_Y0006_NAME, ItemDesConstant.ITEM_CODE_SPEC_Y0006_NAME);
        this.getBag().insertItem(item1);
        Item item2 = new ItemTask(ItemConstant.ITEM_CODE_SPEC_Y0007
            , ItemDesConstant.ITEM_CODE_SPEC_Y0007_NAME, ItemDesConstant.ITEM_CODE_SPEC_Y0007_DES);
        this.getBag().insertItem(item2);
        */
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
          //  Debug.Log("click 1");
            charaInfoManager.showCharaInfoMenu(this, getClickMessage());
        }
        else
        {
            Debug.Log("click ui");
        }

    }


}
