using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpStairOuterRoom : CommonRoom
{

    public override List<string> findSomethingNews(Character chara)
    {
        getClickMessage().Clear();
        if (chara.getName() == SystemConstant.P1_NAME)
        {
            getClickMessage().Add("我才发现 有点恐高啊，不行得回去了");

        }
        else if (chara.getName() == SystemConstant.P2_NAME)
        {

            getClickMessage().Add("没啥意思啊？还是地下探险好。");

        }
        else if (chara.getName() == SystemConstant.P3_NAME)
        {
            getClickMessage().Add("是时候净化那些地下怪物了");
        }
        else if (chara.getName() == SystemConstant.P4_NAME)
        {

            getClickMessage().Add("上面的秘密我已经都知道了，是时候回去了");


        }
        else if (chara.getName() == SystemConstant.P5_NAME)
        {
            getClickMessage().Add("难道说电脑在地下？");

        }
        else if (chara.getName() == SystemConstant.P6_NAME)
        {
            getClickMessage().Add(" 回去会不会好一点？");
        }

        return getClickMessage();
    }
}
