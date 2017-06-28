using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenMonster : CommonMonster
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
       
    }

   

    public override void roundStart()
    {
        Debug.Log("roundStart round this game");
        startRound();
        defaultAction();
        
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
        this.setName(SystemConstant.P4_NAME);
        //游戏一开始 所处的房间 默认房间的坐标为 0,0,0
        int[] roomXYZ = { 0, 0, RoomConstant.ROOM_Z_GROUND };
        setDistance(1.5f);
        setCurrentRoom(roomXYZ);
        

        this.roomContraller.findRoomByXYZ(roomXYZ).setChara(this);
        this.roomContraller.findMiniRoomByXYZ(getCurrentRoom()).setPenable(this.getName(), true);
        setAbilityInfo(new int[] { 8, 3, 6, 7 });

        setMaxAbilityInfo(new int[] { 8, 3, 6, 7 });
        setActionPointrolled(false);
        setIsDead(false);
        
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
        
        if (!isRoundOver())
        {

            if (!isWaitPlayer())
            {
                this.endRound();
            }
        }
    }

    void OnMouseDown()
    {

        if (!SystemUtil.IsTouchedUI())
        {           
            battleController.fighte(roundController.getPlayerChara(), this);
        }
        else
        {
            Debug.Log("click ui");
        }

    }


}
