using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        this.xyz = xyz;

//		Debug.Log ("玩家进入新房间: ");
		Vector3 temPos = new Vector3(xyz [0] * roomH,xyz[1]*roomV,0);
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
                defaultAction();
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
        //游戏一开始 所处的房间 默认房间的坐标为 0,0,0
        int[] roomXYZ = { 0, 0, 0 };
        setCurrentRoom(roomXYZ);
        abilityInfo = new int[] { 5, 2, 6, 8, 20};

        maxAbilityInfo = new int[] { 5, 2, 6, 8,20 };
        this.deadFlag = false;
        this.actionPointrolled = false;
        this.waitFlag = true;
        Debug.Log("赵日天 玩家进入默认房间");
        playerName = "赵日天";
        roomContraller = FindObjectOfType<RoomContraller>();
        diceRoll = FindObjectOfType<DiceRollCtrl>();
        eventController = FindObjectOfType<EventController>();
        battleController = FindObjectOfType<BattleController>();
        roundController = FindObjectOfType<RoundController>();
        this.bag = new Bag();
    }

    // Update is called once per frame
    void Update()
    {
        if (getAbilityInfo()[4] <=0) {
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
}
