using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

abstract public class CommonUser : MonoBehaviour , NPC
{

    [Tooltip("房间宽度")] public float roomH = 13.7f;
    [Tooltip("房间高度")] public float roomV = 11f;

    private float distance;
   

    [SerializeField] private int actionPoint;

    private int[] abilityInfo;

    private int[] maxAbilityInfo;

    [SerializeField] private int[] xyz;

    private String playerName;

    private bool roundOver;

    private bool actionPointrolled;

    private string[] clickMessage;

    private StoryScript ss;

    private bool bossFlag;

    private bool deadFlag;
    
    private Bag bag;
    
    private List<string> targetChara = new List<string>();

    public bool neworLoad;

    public void setClickMessage(string[] clickMessage) {
        this.clickMessage = clickMessage;
    }

    public string[]  getClickMessage() {
        return this.clickMessage;
    }

    public void setDistance(float distance) {
        this.distance = distance;
    }

    public bool ActionPointrolled()
    {
        return actionPointrolled;
    }

    public void startRound()
    {
        this.roundOver = false;
    }

    public void endRound()
    {
        this.roundOver = true;
    }

    public void setAbilityInfo(int[]  abilityInfo)
    {
        this.abilityInfo = abilityInfo;
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

    public void setName(string playerName)
    {
        this.playerName = playerName;
    }

    public string getName()
    {
        return playerName;
    }

    public bool isDead()
    {
        return this.deadFlag;
    }

    public void setIsDead(bool deadFlag)
    {
        this.deadFlag = deadFlag;
    }

    public virtual bool isPlayer()
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
    public void setWaitPlayer(bool waitFlag)
    {
        this.waitFlag = waitFlag;
    }

   

    private GuangBoAction gba;

    public void defaultAction()
    {
       
    }

    public virtual  void roundStart()
    {
        Debug.Log("run common roundStart");
    }

    public void setActionPointrolled(bool actionPointrolled)
    {
        this.actionPointrolled = actionPointrolled;
    }

    public void setCurrentRoom(int[] nextRoomXYZ)
    {
        Vector3 temPos;
        this.xyz = nextRoomXYZ;
       

        if (xyz[2] == RoomConstant.ROOM_Z_UP)
        {
            temPos = new Vector3(xyz[0] * roomH - distance, RoomConstant.ROOM_Y_UP + (xyz[1] * roomV + distance), 0);
        }
        else if (xyz[2] == RoomConstant.ROOM_Z_GROUND)
        {
            temPos = new Vector3(xyz[0] * roomH - distance, RoomConstant.ROOM_Y_GROUND + (xyz[1] * roomV + distance), 0);
        }
        else
        {
            temPos = new Vector3(xyz[0] * roomH - distance, RoomConstant.ROOM_Y_DOWN + (xyz[1] * roomV + distance), 0);
        }


        this.transform.position = temPos;

    }

    public void updateActionPoint(int actionPoint)
    {
        this.actionPoint = actionPoint;
    }

   

    private bool crazyFlag;

    public void setCrazyFlag(bool crazyFlag) {
        this.crazyFlag = crazyFlag;
    }

    public bool isCrazy()
    {
        return crazyFlag;
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

    public void setMaxAbilityInfo(int[] maxAbilityInfo)
    {
        this.maxAbilityInfo = maxAbilityInfo;
    }

    public int[] getMaxAbilityInfo()
    {
        return maxAbilityInfo;
    }

    public void setBag(Bag bag)
    {
         this.bag = bag;
    }

    public Bag getBag()
    {
        return this.bag;
    }

    private int diceNum;

    public void setDiceNumberBuffer(int number)
    {
        this.diceNum = number;
    }
    public int getDiceNumberBuffer()
    {
        int tmp = this.diceNum;
        this.diceNum = 0;
        return tmp;
    }

    private int diceValue;

    public void setDiceValueBuffer(int value)
    {
        this.diceValue = value;
    }

    public int getDiceValueBuffer()
    {

        int tmp = this.diceValue;
        this.diceValue = 0;
        return tmp;
    }

    private int damge = 1;

    public void setDamgeBuffer(int damge)
    {

        this.damge = damge;
    }

    public int getDamgeBuffer()
    {

        int tmp = this.damge;
        this.damge = 1;
        return tmp;
    }

   

    public string getLiHuiURL()
    {
        return null;
    }

    private GuangBoListener listener;

    public void setGuangBoListener(GuangBoListener listener)
    {
        this.listener = listener;
    }

    public void sendMessageToPlayer(string[] message)
    {
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

    public GuangBoAction getGuangBoAction()
    {
       return  this.guangBoAction;
    }

    public bool checkItem(string itemCode)
    {
        return this.getBag().checkItem(itemCode);
    }

    public List<string> getTargetChara()
    {
        return this.targetChara;
    }

    public void setTargetChara(List<string> targetChara)
    {
         this.targetChara = targetChara;
    }

    private string desc;

    public void setDesc(string desc)
    {
        this.desc = desc;
    }

    public string getDesc()
    {
        return this.desc;
    }

    private bool locked;

    public virtual void checkTargetRoomLocked(string roomType)
    {
        locked = false;
    }

    public void setTargetRoomLocked(bool locked)
    {
        this.locked = locked;
    }

    public bool isTargetRoomLocked()
    {
        return this.locked;
    }

    void Start()
    {
       
    }

    public string getDeitalPic()
    {
        return null;
    }

    public string getProfilePic()
    {
        return null;
    }


    public P0 loadInfo(string name)
    {
        string datapath = Application.persistentDataPath + "/Save/SaveData0.sav";

        SaveData data = (SaveData)IOHelper.GetData(datapath, typeof( SaveData));

        if(name == SystemConstant.P1_NAME)
        {
            return data.P1;
        } else if(name == SystemConstant.P2_NAME) 
        {
            return data.P2;
        }
        else if (name == SystemConstant.P3_NAME)
        {
            return data.P3;
        }
        else if (name == SystemConstant.P4_NAME)
        {
            return data.P4;
        }
        else if (name == SystemConstant.P5_NAME)
        {
            return data.P5;
        }
        else if (name == SystemConstant.P6_NAME)
        {
            return data.P6;
        }
        return null;
    }

    public void loadInfo(Character chara, P0 p)
    {
        setCrazyFlag(p.CrazyFlag);       
        setAbilityInfo(p.AbilityInfo);
        setMaxAbilityInfo(p.MaxAbilityInfo);
        setActionPointrolled(p.ActionPointrolled);
        setIsDead(p.IsDead);       
        this.setDesc(p.Desc);
        foreach (ItemInfo i in p.Bag)
        {
            if (i.Type == ItemConstant.ITEM_TYPE_POTION)
            {
                getBag().insertItem(new ItemPotion(i.Code, i.Name, i.Desc));
            }

        }
        updateActionPoint(p.ActionPoint);
    }
}
