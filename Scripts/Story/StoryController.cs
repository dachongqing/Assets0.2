using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryController : MonoBehaviour
{

    private bool isStartStory;

    RoundController roundController;

    RoomContraller roomContraller;

    private StoryInterface story;



    // Use this for initialization
    void Start()
    {
        isStartStory = false;
        roundController = FindObjectOfType<RoundController>();
        roomContraller = FindObjectOfType<RoomContraller>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Character boss = roundController.getCurrentRoundChar();
        RoomInterface room = roomContraller.findRoomByXYZ(boss.getCurrentRoom());
        if (!isStartStory)
        {
            if (room.checkRoomStoryStart(boss))
            {
                isStartStory = true;
                //正式开始剧情模式， 剧情UI介绍
                // showStoryUI();

                Character[] charas = roundController.getAllChara();
                this.story = room.getStartedStory();
                Debug.Log("UI 显示《《剧情模式已经开启： " + story.getStoryInfo());
                boss.setScriptAction(story.getBadManScript());
                boss.setBoss(true);
                for (int i = 0; i < charas.Length; i++)
                {
                    if (charas[i].getName() != boss.getName())
                    {
                        charas[i].setScriptAction(story.getGoodManScript());
                        charas[i].setBoss(false);
                    }
                }


            }
        }
        else
        {
            //剧情已经开始，监听剧本的结束条件
            if (this.story.checkStoryEnd(boss, room))
            {
                //找到玩家所在的角色，检查胜利条件
                Debug.Log("剧情已经开始，监听剧本的结束条件");
                Character player = roundController.getPlayerChara();
                //调用 UI 显示 结局
                if (boss.isScriptWin())
                {
                    Debug.Log("有人胜利了，游戏结束");
                    if (!boss.isPlayer() && boss.isBoss())
                    {
                        Debug.Log("UI 显示《《 日了狗了，输了啊：" + player.getScriptAciont().getFailureEndInfo());

                    }
                    else
                    {
                        if (player.isBoss())
                        {
                            Debug.Log("UI 显示《《 日了狗了，输了啊：" + player.getScriptAciont().getFailureEndInfo());
                        }
                        else {
                            Debug.Log("UI 显示《《 吊炸天，胜利了：" + boss.getScriptAciont().getWinEndInfo());
                        }
                    }



                    //黑屏 显示游戏结束画面，显示感谢 制作人信息
                    Debug.Log("UI 显示《《黑屏 显示游戏结束画面，显示感谢 制作人信息");
                    //扫尾工作，清除游戏的信息
                    Debug.Log("扫尾工作，清除游戏的信息,保存游戏进度信息等");
                    //返回开始页面
                    Debug.Log("返回开始页面");
                }

            }
        }

    }
}
