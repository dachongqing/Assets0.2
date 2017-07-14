using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P0  {

    private bool waitPlan;
    private bool scriptEnd;
    private bool isPlayer;
    private string liHuiURL;
    private string profilePic;
    private int[] abilityInfo;

    private int[] maxAbilityInfo;

    private int actionPoint;

    private int[] xyz;

    private string playerName;

    private bool roundOver;

    private bool actionPointrolled;

    private string[] clickMessage;

    private bool bossFlag;

    private bool deadFlag;

    private bool waitFlag;

    private bool crazyFlag;

    private int diceNum;

    private int diceValue;

    private int damge = 1;

    private string desc;

    private bool locked;

    private bool isDead;

    private List<ItemInfo> bag = new List<ItemInfo>();

    private List<string> targetRoomlist = new List<string>();
  
    public bool Locked
    {
        get
        {
            return locked;
        }

        set
        {
            locked = value;
        }
    }

    public string Desc
    {
        get
        {
            return desc;
        }

        set
        {
            desc = value;
        }
    }

    public int Damge
    {
        get
        {
            return damge;
        }

        set
        {
            damge = value;
        }
    }

    public int DiceValue
    {
        get
        {
            return diceValue;
        }

        set
        {
            diceValue = value;
        }
    }

    public int DiceNum
    {
        get
        {
            return diceNum;
        }

        set
        {
            diceNum = value;
        }
    }

    public bool CrazyFlag
    {
        get
        {
            return crazyFlag;
        }

        set
        {
            crazyFlag = value;
        }
    }

    public bool WaitFlag
    {
        get
        {
            return waitFlag;
        }

        set
        {
            waitFlag = value;
        }
    }

    public bool DeadFlag
    {
        get
        {
            return deadFlag;
        }

        set
        {
            deadFlag = value;
        }
    }

    public bool BossFlag
    {
        get
        {
            return bossFlag;
        }

        set
        {
            bossFlag = value;
        }
    }

    public string[] ClickMessage
    {
        get
        {
            return clickMessage;
        }

        set
        {
            clickMessage = value;
        }
    }

    public bool ActionPointrolled
    {
        get
        {
            return actionPointrolled;
        }

        set
        {
            actionPointrolled = value;
        }
    }

    public bool RoundOver
    {
        get
        {
            return roundOver;
        }

        set
        {
            roundOver = value;
        }
    }

    public string PlayerName
    {
        get
        {
            return playerName;
        }

        set
        {
            playerName = value;
        }
    }

    public int[] Xyz
    {
        get
        {
            return xyz;
        }

        set
        {
            xyz = value;
        }
    }

    public int[] MaxAbilityInfo
    {
        get
        {
            return maxAbilityInfo;
        }

        set
        {
            maxAbilityInfo = value;
        }
    }

    public int[] AbilityInfo
    {
        get
        {
            return abilityInfo;
        }

        set
        {
            abilityInfo = value;
        }
    }

    public string ProfilePic
    {
        get
        {
            return profilePic;
        }

        set
        {
            profilePic = value;
        }
    }

    public string LiHuiURL
    {
        get
        {
            return liHuiURL;
        }

        set
        {
            liHuiURL = value;
        }
    }

    public bool IsPlayer
    {
        get
        {
            return isPlayer;
        }

        set
        {
            isPlayer = value;
        }
    }

    public bool ScriptEnd
    {
        get
        {
            return scriptEnd;
        }

        set
        {
            scriptEnd = value;
        }
    }

    public bool WaitPlan
    {
        get
        {
            return waitPlan;
        }

        set
        {
            waitPlan = value;
        }
    }

    public bool IsDead
    {
        get
        {
            return isDead;
        }

        set
        {
            isDead = value;
        }
    }

    public int ActionPoint
    {
        get
        {
            return actionPoint;
        }

        set
        {
            actionPoint = value;
        }
    }

    public List<ItemInfo> Bag
    {
        get
        {
            return bag;
        }

        set
        {
            bag = value;
        }
    }

    public List<string> TargetRoomlist
    {
        get
        {
            return targetRoomlist;
        }

        set
        {
            targetRoomlist = value;
        }
    }

  
}
