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

    public new string getDeitalPic()
    {
        return "detail/kate_detail01";
    }

    public new string getProfilePic()
    {
        return "detail/9";
    }

    public override void findthisRoomNews(int[] nextRoomXYZ)
    {
        this.setClickMessage(this.roomContraller.findRoomByXYZ(nextRoomXYZ).findSomethingNews(this).ToArray());
    }

    public new void defaultAction()
    {
       // Debug.Log("run default aciont");
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

            if (this.getTargetRoomList().Count<= 0 ) {
                Debug.Log("随便找个房间看看");
                this.getTargetRoomList().Enqueue(roomContraller.getRandomRoom());
            }
            RoomInterface target = this.getTargetRoomList().Peek();

            if (AutoMoveManager.move(this, roomContraller, eventController, diceRoll, aPathManager, target.getXYZ()))
            {

            }



            }

        endRound();
    }

    public override void roundStart()
    {
       // Debug.Log("roundStart round this game");
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
             //   Debug.Log("npc 当前回合状态是: "+ isRoundOver());
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
        setGuangBoListener(FindObjectOfType<GuangBoListener>());
        guangBoController = FindObjectOfType<GuangBoController>();
        this.setName(SystemConstant.P1_NAME);
        //游戏一开始 所处的房间 默认房间的坐标为 0,0,0
        int[] roomXYZ;
        setBag(new Bag());
        if (this.neworLoad)
        {
            roomXYZ = new int[] { 0, 0, RoomConstant.ROOM_Z_GROUND };
            setCrazyFlag(false);
            setAbilityInfo(new int[] { 8, 3, 6, 7 });
            setMaxAbilityInfo(new int[] { 8, 3, 6, 7 });
            setActionPointrolled(false);
            setIsDead(false);
           
        
            this.waitPlan = false;

            this.setDesc("一身脏兮兮的白大褂，第一感觉是个跳大神的庸医。");
            //this.setClickMessage(new string[] { "", "你就是犯人。" });
            getTargetRoomList().Enqueue(roomContraller.getRandomRoom());
            getTargetRoomList().Enqueue(roomContraller.findRoomByRoomType(RoomConstant.ROOM_TYPE_BOOK));
        }
        else
        {
            P0 p = loadInfo(this.getName());
            roomXYZ = p.Xyz;
            this.waitPlan = p.WaitPlan;
            loadInfo(this, p, roomContraller);
            Debug.Log(" idont kone? nolan is dead ?" + this.isDead());
        }
        setDistance(0.5f);
        setCurrentRoom(roomXYZ);             
        this.roomContraller.findRoomByXYZ(roomXYZ).setChara(this);
        this.roomContraller.setCharaInMiniMap(roomXYZ, this, true);  
        this.setClickMessage(this.roomContraller.findRoomByXYZ(roomXYZ).findSomethingNews(this).ToArray());

    }

    // Update is called once per frame
    void Update()
    {
		if (getAbilityInfo()[0] <=0 || getAbilityInfo()[1] <=0  ||
			getAbilityInfo()[2] <=0 || getAbilityInfo()[3] <=0  			
		) {
       //     Debug.Log(this.getName() + " 已经死亡。。。");
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








    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log(coll.gameObject.name);
        charaInfoManager.showCharaInfoMenu(this, getClickMessage());

    }

    /**

    void OnMouseDown ()
	{

        if (!SystemUtil.IsTouchedUI())
        {

            //string[] co = new string[] { "你感觉到绝望了吗", "老实讲，我要带你飞了" };
           // duiHuaUImanager.showDuiHua(getLiHuiURL(), co);
            charaInfoManager.showCharaInfoMenu(this, getClickMessage());
        }
        else {
            Debug.Log("click ui");
        }

	}**/

   

  

   

   

   

   

   

  
}
