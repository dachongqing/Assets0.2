using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpStairEnterRoom : CommonRoom {

    public override List<string> findSomethingNews(Character chara)
    {
        getClickMessage().Clear();
        if (chara.getName() == SystemConstant.P1_NAME)
        {
            getClickMessage().Add("楼上的空气很好");

        }
        else if (chara.getName() == SystemConstant.P2_NAME)
        {

            getClickMessage().Add("貌似上面的房间 光线不错嘛？我要上去看看");

        }
        else if (chara.getName() == SystemConstant.P3_NAME)
        {
            getClickMessage().Add("楼上的教堂在等着我");
        }
        else if (chara.getName() == SystemConstant.P4_NAME)
        {

            getClickMessage().Add("上面会有什么秘密呢？");


        }
        else if (chara.getName() == SystemConstant.P5_NAME)
        {
            getClickMessage().Add("要是楼上有一台电脑就好了");

        }
        else if (chara.getName() == SystemConstant.P6_NAME)
        {
            getClickMessage().Add("上面的楼层似乎很安静");
        }

        return getClickMessage();
    }
}
