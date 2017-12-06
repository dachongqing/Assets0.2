using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindSomethingTaskItem : CommonTaskItem
{

    public FindSomethingTaskItem(string desc ):base(desc)
    {

    }
       

    private string targetItemCode;

    public void setTargetItem(string targetItem) {
        this.targetItemCode = targetItem;
    } 

    private bool checkSomethingisFinded() {
        NPC player = this.getPlayer();
        return  player.getBag().checkItem(targetItemCode);
     
    }

    public override void checkItemComplelted()
    {
        if (checkSomethingisFinded())
        {
            setCompleted(true);
        }
        else {
            setCompleted(false);
        };
    }

    public override string getItemCode()
    {
        return TaskConstant.TASK_ITEM_CODE_FIND;
    }

    // Use this for initialization

}
