using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSomethingTaskItem : CommonTaskItem
{

    public KillSomethingTaskItem(string desc):base(desc)
    {

    }


    private Character target;

    public void setTarget(Character chara)
    {
        this.target = chara;
    }

    private bool checkSomethingisFinded()
    {
        
        return target.isDead();

    }

    public override void checkItemComplelted()
    {
        if (checkSomethingisFinded())
        {
            setCompleted(true);
        }
        else
        {
            setCompleted(false);
        };
    }

    public override string getItemCode()
    {
        return TaskConstant.TASK_ITEM_CODE_KILL;
    }

}
