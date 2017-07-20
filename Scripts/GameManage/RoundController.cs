using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour
{

    private Queue<Character> roundList = new Queue<Character>();

    private Dictionary<string,Character> charaMap = new Dictionary<string, Character>();

    private List<Character> charaList = new List<Character>();

    private bool isRoundEnd;

    private int roundCount;

    private Character playChara;

    private Player player;

    private Nolan nolan;

    private Ben ben;

    private Kate kate;

    private Martin martin;

    private Jessie jessie;

    private Character playerChara; // 玩家所控制的角色

    private RoomContraller roomContraller;

    private EventController eventController;

    public bool newOrLoad;

    public Character getNextCharecter()
    {
        //Debug.Log(" Queue number is " + roundList.Count);
        return roundList.Dequeue();
    }

    public void setEndRound(Character chara)
    {
        roundList.Enqueue(chara);
        if (!charaMap.ContainsKey(chara.getName())) {
            charaMap.Add(chara.getName(), chara);
        }
    }

    public void endRound()
    {
        this.getCurrentRoundChar().updateActionPoint(0);
        eventController.excuteStayRoomEvent(roomContraller.findRoomByXYZ(this.getCurrentRoundChar().getCurrentRoom()), this.getCurrentRoundChar());
        this.getCurrentRoundChar().endRound();
    }

    //默认操作状态为玩家操作  
    private OperatorState mState = OperatorState.Player;

    //定义操作状态枚举  
    public enum OperatorState
    {
        Quit,
        EnemyAI,
        Player
    }

    public int getRoundCount()
    {
        return this.roundCount;
    }

   

    // Use this for initialization
    void Start()
    {
            roomContraller = FindObjectOfType<RoomContraller>();
            eventController = FindObjectOfType<EventController>();
            player = FindObjectOfType<Player>();
            nolan = FindObjectOfType<Nolan>();
            ben = FindObjectOfType<Ben>();
            kate = FindObjectOfType<Kate>();
            martin = FindObjectOfType<Martin>();
            jessie = FindObjectOfType<Jessie>();
        if (newOrLoad)
        {
            //目前是写死。。后面需要改为程序控制添加 游戏人数

            setEndRound(nolan);
            setEndRound(ben);
            setEndRound(jessie);
            setEndRound(kate);
            setEndRound(martin);
            setEndRound(player);
            roundCount = 1;
        }
        else
        {
            string datapath = Application.persistentDataPath + "/Save/SaveData0.sav";
            SaveData data = (SaveData)IOHelper.GetData(datapath, typeof(SaveData));
            foreach (string name in data.CharaNames)
            {
                if(name == SystemConstant.P1_NAME)
                {
                    setEndRound(nolan);
                } else if(name == SystemConstant.P2_NAME)
                {
                    setEndRound(ben);
                } else if (name == SystemConstant.P3_NAME)
                {
                    setEndRound(jessie);
                }
                else if (name == SystemConstant.P4_NAME)
                {
                    setEndRound(kate);
                }
                else if (name == SystemConstant.P5_NAME)
                {
                    setEndRound(martin);
                }
                else if (name == SystemConstant.P6_NAME)
                {
                    setEndRound(player);
                }
            }
            roundCount = data.RoundCount;
        }
       // Debug.Log(playChara.getName() + " round this game");
        if (player.isPlayer())
        {
            this.playerChara = player;
        }
        else if (nolan.isPlayer())
        {
            this.playerChara = nolan;
        }
        else if (ben.isPlayer())
        {
            this.playerChara = ben;
        }
        else if (kate.isPlayer())
        {
            this.playerChara = kate;
        }
        else if (martin.isPlayer())
        {
            this.playerChara = martin;
        }
        else if (jessie.isPlayer())
        {
            this.playerChara = jessie;
        }

        isRoundEnd = false;
        playChara = this.getPlayerChara();
        playChara.setActionPointrolled(true);
    }

    public Character getCharaByName(string name)
    {
        return charaMap[name];

    }

    public List<Character> getAllCharaFromMap() {
        charaList.Clear();
        foreach(string name in charaMap.Keys)
        {
            charaList.Add(charaMap[name]);
        }
        return charaList;
    }

    public Character getCurrentRoundChar()
    {
        return this.playChara;
    }

    public IEnumerator charaMove()
    {

        playChara.roundStart();
        yield return new WaitForSeconds(2);
    }

    // Update is called once per frame
    void Update()
    {


        /***
         * 监听到回合结束
         * 
        *从队列里获取下一个角色
        * 设置回合开始
        * 判定是否是用户回合
        * 如果是用户回合
        * 黑屏取消
        * 如果是npc回合
        * 设置黑屏
        */

        if (isRoundEnd)
        {


            playChara = this.getNextCharecter();
            playChara.setActionPointrolled(true);

           // Debug.Log(playChara.getName() + " round this game");
            isRoundEnd = false;
            if (playChara.isPlayer())
            {
              
                if (this.roundList.Count == 0) {
             //    Debug.Log("世界安静了。。。你是唯一的幸存者。。游戏结束");
                }

                mState = OperatorState.Player;
                //解除黑屏
                //解锁roll点
                roundCount++;
                StartCoroutine("charaMove");
            }
            else
            {
               
                if (playChara.isDead())
                {
                    Debug.Log("npc 死了");
                    isRoundEnd = true;

                }
                else {
                   
                    StartCoroutine("charaMove");

                }
               
            }
        }



        if (!playChara.isDead())
        {

            switch (mState)
            {

                //玩家回合
                case OperatorState.Player:
                    if (playChara.isRoundOver())
                    {
                        this.setEndRound(playChara);
                        isRoundEnd = true;
                        mState = OperatorState.EnemyAI;
                      //  Debug.Log("wait ai move");
                        //开始黑屏
                    }

                    break;
                //NPC 怪物回合
                case OperatorState.EnemyAI:
                    if (playChara.isWaitPlayer())
                    {
                        // mState = OperatorState.Player;
                        //解除黑屏
                       // Debug.Log("wait player Action");

                    }
                    else
                    {
                        //开始黑屏
                    }
                    if (playChara.isRoundOver())
                    {
                      //  Debug.Log("NPC 行动完毕了，");
                        this.setEndRound(playChara);
                        isRoundEnd = true;
                      //  Debug.Log("ai done,wait next ai move");
                    }
                    break;
            }

        }
        else
        {
            if (playChara.isPlayer())
            {
                //game over 
                Debug.Log("你已经死了， 不要怕这只是一个梦");
            }
            else
            {
                Debug.Log("npc 死了");
                isRoundEnd = true;
            }

        }
    }

    public Character[] getAllChara()
    {
        return this.roundList.ToArray();
    }

    public Character getPlayerChara()
    {
        return this.playerChara;
    }
}
