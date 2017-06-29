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
        this.setStroryAttrs(conditions, goodStoryScript, badStoryScript);
    }

    public override void initStroy(Character chara)
    {
        string monsteUrl = "";
        GameObject servantObject = Instantiate(Resources.Load(monsteUrl)) as GameObject;
        BenMonster benMonster = servantObject.GetComponent<BenMonster>();
        benMonster.setCurrentRoom(chara.getCurrentRoom());
    }

    public override string getStoryInfo()
    {
        return "萝莉侦探似乎看见了什么，突然变得狂暴起来，见人就攻击，能否活着逃离这个空间成了最大的问题。";
    }
}
