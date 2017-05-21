using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour
{

    private Queue<Character> roundList = new Queue<Character>();

    private bool isRoundEnd;



    private Character playChara;



    private Player player;

    //private Hazard ai;

    public Character getNextCharecter()
    {
        Debug.Log(" Queue number is " + roundList.Count);
        return roundList.Dequeue();
    }

    public void setEndRound(Character chara)
    {
        roundList.Enqueue(chara);
    }

    public void endRound() {
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

    // Use this for initialization
    void Start()
    {

        //目前是写死。。后面需要改为程序控制添加 游戏人数
        player = FindObjectOfType<Player>();
        //  ai = FindObjectOfType<Hazard>();
        setEndRound(player);
        //  setEndRound(ai);
        isRoundEnd = false;
        playChara = this.getNextCharecter();
        playChara.setActionPointrolled(true);
        Debug.Log(playChara.getName() + " round this game");
    }

    public Character getCurrentRoundChar() {
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
            
            Debug.Log(playChara.getName() + " round this game");
            isRoundEnd = false;
            if (playChara.isPlayer())
            {
                mState = OperatorState.Player;
                //解除黑屏
                //解锁roll点
            }
            else
            {
            }
                StartCoroutine("charaMove");
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
                        Debug.Log("wait ai move");
                        //开始黑屏
                    }

                    break;
                //NPC 怪物回合
                case OperatorState.EnemyAI:
                    if (playChara.isWaitPlayer())
                    {
                        // mState = OperatorState.Player;
                        //解除黑屏
                        Debug.Log("wait player Action");

                    }
                    else
                    {
                        //开始黑屏
                    }
                    if (playChara.isRoundOver())
                    {

                        this.setEndRound(playChara);
                        isRoundEnd = true;
                        Debug.Log("ai done,wait next ai move");
                    }
                    break;
            }

        }
        else
        {
            if (playChara.isPlayer())
            {
                //game over 
            }
            else
            {
                isRoundEnd = true;
            }

        }
    }
}
