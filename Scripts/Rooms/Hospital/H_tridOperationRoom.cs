using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_tridOperationRoom : CommonRoom
{

    public GameObject operatingTable;

    public GameObject getOperatingTable()
    {
        return operatingTable;
    }

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
        setLock(true);
        EventInterface ei = new SanCheckRoomEvent(7, 5, this.getEventBeginInfo(), this.getEventEndInfoMap(),
            EventConstant.ENTER_EVENT, EventConstant.SANCHECK_EVENT, 1, -1, 1);
        this.setRoomEvent(ei);
        operatingTable.GetComponent<OperatingTable>().init(this.getXYZ());
     
    }
    

    public override bool checkOpen(Character chara)
    {
        if (isLock())
        {
            if (typeof(NPC).IsAssignableFrom(chara.GetType()))
            {

                NPC npc = (NPC)chara;

                if (npc.getBag().checkItem(ItemConstant.ITEM_CODE_SPEC_Y0002))
                {
                    setLock(false);
                    return true;
                }
                else if (npc.getName() == SystemConstant.P6_NAME)
                {
                    setLock(false);
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }

        }
        else {
            return true;
        }
    }

    public override string[] getEventBeginInfo()
    {

        return new string[] { "当你打开门一股腐烂的气味马上传入你的鼻子里， 让你感到一阵恶心。。。"
        ,"你注意到手术台上放着一具不知道放了多久的尸体。","你需要投一个神志检测。"};
    }

    public override Dictionary<string, string[]> getEventEndInfoMap()
    {
        Dictionary<string, string[]> map = new Dictionary<string, string[]>();
        string[] good = new string[] { "你遇到太多这样的画面，所以你只是感到一阵恶心。"
        };
        string[] normal = new string[] { "你没忍住胃的翻江倒海，你的神志下降1点。"
        };
        string[] bad = new string[] { "你的眼睛一直停留在尸体上，那个手臂上的脓包让你感觉一阵寒意突然袭来"
            ,"你需要投掷一个1D2的神志扣除。"
        };
        map.Add(EventConstant.SANCHECK_EVENT_GOOD, good);
        map.Add(EventConstant.SANCHECK_EVENT_NORMAL, normal);
        map.Add(EventConstant.SANCHECK_EVENT_BED, bad);
        return map;
    }
    
}
