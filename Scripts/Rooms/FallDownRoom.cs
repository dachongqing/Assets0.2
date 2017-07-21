using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDownRoom : CommonRoom {

    public override List<string> findSomethingNews(Character chara)
    {

        getClickMessage().Clear();
        if (chara.getName() == SystemConstant.P1_NAME)
        {

            getClickMessage().Add("我只负责冒险家这一个病人，他现在不是好好的吗！  ");


        }
        else if (chara.getName() == SystemConstant.P2_NAME)
        {

            getClickMessage().Add("得这个病的人最后会成这个样子？不可能的！");
            getClickMessage().Add("那个病我明明已经好了，再也没有做过那样的噩梦了，我不要再想起那个恐怖声音了。");

        }
        else if (chara.getName() == SystemConstant.P3_NAME)
        {
            getClickMessage().Add("医生！你们就是这样对待病人的吗？");
            getClickMessage().Add("冒险家，想要得到神的眷顾，就必须通过神的考验， 你必须再次面对神明！");
            getClickMessage().Add("告诉我，你梦到了什么？是不是有什么东西在对你说话？");
        }
        else if (chara.getName() == SystemConstant.P4_NAME)
        {
            if (chara.getAbilityInfo()[3] <= 6)
            {
                getClickMessage().Add("侦探空洞的望着那具尸体，全身在发抖。");
            }
            else
            {
                getClickMessage().Add("看样子好像是手术没做完，出现了什么意外。");
                getClickMessage().Add("而且照这个尸体腐烂程度，应该是有1个月了。");
            }
        }
        else if (chara.getName() == SystemConstant.P5_NAME)
        {
            getClickMessage().Add("这间房间没有什么特别值得注意的");
            getClickMessage().Add("看看那边的报栏上有什么报纸新闻？");

        }
        else if (chara.getName() == SystemConstant.P6_NAME)
        {
            getClickMessage().Add("医生你是不是有什么瞒着我们？");
        }

        return getClickMessage();
    }

    void Start()
    {
       
        EventInterface ei = new FallRoomEvent(18, 11, this.getEventBeginInfo(), this.getEventEndInfoMap(),
            EventConstant.ENTER_EVENT, EventConstant.FALL_DOWN__EVENT, 0, 0, 1);
        this.setRoomEvent(ei);
       

    }

    

    public override string[] getEventBeginInfo()
    {

        return new string[] { "哇！。摇晃的地板让你重心不稳，你需要速度判定来躲避掉下去"};
    }

    public override Dictionary<string, string[]> getEventEndInfoMap()
    {
        Dictionary<string, string[]> map = new Dictionary<string, string[]>();
        string[] good = new string[] { "你的反应很好，成功了跳过了碎裂的木板。"
        };
        string[] normal = new string[] { "你一下踩空了木板， 但是你的反应能力让你马上调整了平衡，没有收到伤害"
        };
        string[] bad = new string[] { "你的注意完全不在脚下， 随着一声哇，你重重地摔倒了楼下房间"
            ,"你需要投掷一个1D2的力量扣除。"
        };
        map.Add(EventConstant.FALL_DOWN__EVENT_GOOD, good);
        map.Add(EventConstant.FALL_DOWN__EVENT_NORMAL, normal);
        map.Add(EventConstant.FALL_DOWN__EVENT_BAD, bad);
        return map;
    }
}
