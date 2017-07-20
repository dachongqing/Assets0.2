using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownStairOuterRoom : CommonRoom
{

    public override List<string> findSomethingNews(Character chara)
    {
        getClickMessage().Clear();
        if (chara.getName() == SystemConstant.P1_NAME)
        {
            getClickMessage().Add("我这里已经疗伤的药了，我想回去一趟。");

        }
        else if (chara.getName() == SystemConstant.P2_NAME)
        {

            getClickMessage().Add("差点没命了啊，我想上去缓缓，太吓人了。");
           
        }
        else if (chara.getName() == SystemConstant.P3_NAME)
        {
            getClickMessage().Add("这真是魔鬼的地盘，神明的力量太弱了，我想上去。");
        }
        else if (chara.getName() == SystemConstant.P4_NAME)
        {

            getClickMessage().Add("没想到这里的房间藏了这么多秘密。我想上去好好捋捋。");


        }
        else if (chara.getName() == SystemConstant.P5_NAME)
        {
            getClickMessage().Add("这下面没法待了，我想回到上面的安全房间。");

        }
        else if (chara.getName() == SystemConstant.P6_NAME)
        {
            getClickMessage().Add("回去会不会好一点？");
        }

        return getClickMessage();
    }
}
