using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownStairEnterRoom : CommonRoom {

    public override List<string> findSomethingNews(Character chara)
    {
        getClickMessage().Clear();
        if (chara.getName() == SystemConstant.P1_NAME)
        {
            getClickMessage().Add("下面的温度异常的低啊，得多穿点衣服才行。");
          
        }
        else if (chara.getName() == SystemConstant.P2_NAME)
        {

            getClickMessage().Add("地下传了什么声音，你听见了吗？");
            getClickMessage().Add("我要下去看看。");
        }
        else if (chara.getName() == SystemConstant.P3_NAME)
        {
            getClickMessage().Add("地下传来一阵污秽之气，你想跟我一起下去净化它们吗？");
        }
        else if (chara.getName() == SystemConstant.P4_NAME)
        {

            getClickMessage().Add("地下室一定什么有不可告人的密码，你要跟我下去吗？");
           

        }
        else if (chara.getName() == SystemConstant.P5_NAME)
        {
            getClickMessage().Add("下面太黑了， 我可不敢一个人下去。");
          
        }
        else if (chara.getName() == SystemConstant.P6_NAME)
        {
            getClickMessage().Add("黑乎乎的楼梯，让人觉得头皮发麻。");
        }

        return getClickMessage();
    }
}
