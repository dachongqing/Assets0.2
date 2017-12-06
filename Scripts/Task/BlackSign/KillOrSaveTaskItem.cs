using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOrSaveTaskItem : CommonTaskItem
{

    public KillOrSaveTaskItem(string desc):base(desc)
    {

    }


    private Character target;

    public void setTarget(Character chara)
    {
        this.target = chara;
    }

    private bool checkSomethingisFinded()
    {
        Debug.Log("this.target " + this.target);
        return (this.target.isDead() || this.target.getAbilityInfo()[3] >= 7);

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
        return TaskConstant.TASK_ITEM_CODE_SAVEORKILL;
    }
}
