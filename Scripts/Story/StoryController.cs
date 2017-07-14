using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryController : MonoBehaviour
{

    private bool isStartStory;

    RoundController roundController;

    RoomContraller roomContraller;

    private StoryInterface story;

    private DuiHuaUImanager duiHuaUImanager;

    public bool neworLoad;

    public StoryInterface getStory()
    {
        return story;
    }

    public bool getIsStartStory()
    {
        return isStartStory;
    }


    public bool checkStoryStartBySPEvnet(StoryInterface story,Character boss, RoundController roundController, RoomInterface room) {
         if (story.checkStoryStart(boss, room, roundController)) {
            story.initStroy(boss, roundController);
            isStartStory = true;
            this.story = story;
            return true;
        } else {         
            return false;
        }
    }

    // Use this for initialization
    void Start()
    {
        isStartStory = false;
        roundController = FindObjectOfType<RoundController>();
        roomContraller = FindObjectOfType<RoomContraller>();
        if (!this.neworLoad)
        {
            string datapath = Application.persistentDataPath + "/Save/SaveData0.sav";
            SaveData data = (SaveData)IOHelper.GetData(datapath, typeof(SaveData));
            if(data.StoryInfo.IsStoryStart)
            {
                isStartStory = true;
                if(data.StoryInfo.StoryCode == StoryConstan.STORY_CODE_02)
                {
                    this.story = new BlackSignStory();
                    string monsteUrl = "Prefabs/Monsters/BenMonster";
                    GameObject servantObject = Instantiate(Resources.Load(monsteUrl)) as GameObject;
                    BenMonster benMonster = servantObject.GetComponent<BenMonster>();
                    benMonster.init();
                    benMonster.init(data.BenMonster);
                    benMonster.setInitRoom(data.BenMonster.Xyz);
                    roundController.setEndRound(benMonster);


                }
                Character[] charas = roundController.getAllChara();
                foreach(Character chara in charas)
                {
                    if(chara.isBoss())
                    {
                        chara.setScriptAction(this.story.getBadManScript());
                    } else
                    {
                        chara.setScriptAction(this.story.getGoodManScript());
                    }
                }
            }

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Character boss = roundController.getCurrentRoundChar();
        RoomInterface room = roomContraller.findRoomByXYZ(boss.getCurrentRoom());
        if (!isStartStory)
        {
            if (room.checkRoomStoryStart(boss, roundController))
            {
                isStartStory = true;
                //正式开始剧情模式， 剧情UI介绍
                // showStoryUI();

                Character[] charas = roundController.getAllChara();
                this.story = room.getStartedStory();
                story.initStroy(boss, roundController);
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
            if (this.story.checkStoryEnd(boss, room, roundController))
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
                        if (duiHuaUImanager.isDuiHuaEnd())
                        {
                           
                             Debug.Log("UI 显示《《 日了狗了，输了啊：" + player.getScriptAciont().getFailureEndInfo());
                        }

                    }
                    else
                    {
                        if (player.isBoss())
                        {
                            if (duiHuaUImanager.isDuiHuaEnd())
                            {
                                Debug.Log("UI 显示《《 日了狗了，输了啊：" + player.getScriptAciont().getFailureEndInfo());
                            }
                        }
                        else
                        {
                            if(duiHuaUImanager.isDuiHuaEnd())
                            {
                                 Debug.Log("UI 显示《《 吊炸天，胜利了：" + boss.getScriptAciont().getWinEndInfo());

                            }
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
