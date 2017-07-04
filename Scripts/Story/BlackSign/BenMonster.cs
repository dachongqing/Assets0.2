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

    private Queue<Character> targetList = new Queue<Character>();

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
        Character target = targetList.Peek();

        if (AutoMoveManager.move(this, roomContraller, eventController, diceRoll, aPathManager, target.getCurrentRoom()))
        {
            battleController.fighte(this,target);
            if (target.isDead()) {
                targetList.Dequeue();
            }
        }
    }

   

    public override void roundStart()
    {
        Debug.Log("Monster round this game");
        startRound();
        defaultAction();
        
    }



    public void setInitRoom(int[] xyz) {
        setCurrentRoom(xyz);
        this.roomContraller.findRoomByXYZ(xyz).setChara(this);
        this.roomContraller.findMiniRoomByXYZ(getCurrentRoom()).setPenable(this.getName(), true);
    }



    // Use this for initialization
    public override void init()
    {
        Debug.Log("monster init function start begin...");
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
        this.setName(SystemConstant.MONSTER1_NAME);
        //游戏一开始 所处的房间 默认房间的坐标为 0,0,0
        //int[] roomXYZ = { 0, 0, RoomConstant.ROOM_Z_GROUND };
        setDistance(-1.5f);
        
        

       
        setAbilityInfo(new int[] { 7, 7, 1, 1 });

        setMaxAbilityInfo(new int[] { 7, 7, 1, 1 });
        setActionPointrolled(false);
        setIsDead(false);

        Character martin = roundController.getCharaByName(SystemConstant.P5_NAME);
        Character nolan = roundController.getCharaByName(SystemConstant.P1_NAME);
        Character jessie = roundController.getCharaByName(SystemConstant.P3_NAME);
        Character player = roundController.getCharaByName(SystemConstant.P6_NAME);
        if (!martin.isDead()) {
         targetList.Enqueue(martin);
        }

        if (!nolan.isDead())
        {
            targetList.Enqueue(martin);
        }

        if (!jessie.isDead())
        {
            targetList.Enqueue(jessie);
        }
        if (!player.isDead())
        {
            targetList.Enqueue(player);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("monster update function  begin...");
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
