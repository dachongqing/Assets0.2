using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSignStory : AbsStrory
{

    public BlackSignStory() {

        List<Condition> conditions = new List<Condition>();
        Condition trigger = new BlackSingTriggerConditionItem();

        conditions.Add(trigger);

        BlackSignGoodScript  goodStoryScript = new BlackSignGoodScript();
        BlackSignBadScript badStoryScript = new BlackSignBadScript();
        this.setStroryAttrs(conditions, goodStoryScript, badStoryScript,StoryConstan.STORY_CODE_02);
    }

    public override void initStroy(Character chara, RoundController roundController, TaskMananger taskMananger)
    {
        string monsteUrl = "Prefabs/Monsters/BenMonster";
        GameObject servantObject = Instantiate(Resources.Load(monsteUrl)) as GameObject;
        BenMonster benMonster = servantObject.GetComponent<BenMonster>();
        benMonster.setCurrentRoom(chara.getCurrentRoom());
        benMonster.init();
        roundController.setEndRound(benMonster);

        Character[] charas = roundController.getAllChara();
       
        Debug.Log("UI 显示《《剧情模式已经开启： " + this.getStoryInfo());
        chara.setScriptAction(this.getBadManScript());
        chara.setBoss(true);
        for (int i = 0; i < charas.Length; i++)
        {
            if (charas[i].getName() != chara.getName())
            {
                charas[i].setScriptAction(this.getGoodManScript());
                charas[i].setBoss(false);
            }
        }

        // init story task..
        initTask(taskMananger,chara, benMonster);


    }

    private void initTask(TaskMananger taskMananger,Character chara, BenMonster benMonster)
    {
        TaskAward award = new TaskAward();
        award.setAttriAwards(0, 0, 0, 0);
        string taskDesc = "萝莉侦探似乎看见了什么，突然变得狂暴起来，见人就攻击，能否活着逃离这个空间成了最大的问题。";
        string taskName = "逃出侦探的追杀";
        List<TaskItemInterface> taskItems = new List<TaskItemInterface>();
        KillSomethingTaskItem cti = new KillSomethingTaskItem("杀死复活的怪物。");
        cti.setTarget(benMonster);
        KillOrSaveTaskItem cti2 = new KillOrSaveTaskItem("杀死萝莉侦探或者唤醒侦探");
        cti2.setTarget(chara);
        taskItems.Add(cti);
        taskItems.Add(cti2);
        BlackSignTask task = new BlackSignTask(TaskConstant.TASK_STORY_CODE_01, TaskConstant.TASK_TYPE_02, award, null, taskDesc, TaskConstant.TASK_NPC_CODE_STORY, null, taskItems,
            taskName, TaskConstant.STATUS_INIT);
        taskMananger.regisisterStoryTask(task);
    }

    public override string getStoryInfo()
    {
        return "萝莉侦探似乎看见了什么，突然变得狂暴起来，见人就攻击，能否活着逃离这个空间成了最大的问题。";
    }
}
