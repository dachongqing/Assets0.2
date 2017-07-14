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
        if (chara.getName() == SystemConstant.P4_NAME)
        {
            if(chara.getAbilityInfo()[3] <=6)
            {
                getClickMessage().Add("侦探空洞的望着那具尸体，全身在发抖。");
            } else
            {
                getClickMessage().Add("看样子好像是手术没做完，出现了什么意外。");
                getClickMessage().Add("而且照这个尸体腐烂程度，应该是有1个月了。");         
            }
            return getClickMessage();

        }

        return null;
    }

    void Start()
    {
        setLock(true);
        EventInterface ei = new SanCheckRoomEvent(7,7);
        ei.setEventBeginInfo(this.getEventBeginInfo());
        ei.setEventEndInfo(this.getEventEndInfoMap());
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
        string[] bad = new string[] { "你的眼睛一直停留在尸体上，那个手臂上的脓包让你感觉一阵寒意突然袭来"
            ,"你需要投掷一个1D2的神志扣除。"
        };
        map.Add(EventConstant.SANCHECK_EVENT_SAFE, good);
        map.Add(EventConstant.SANCHECK_EVENT_BAD, bad);
        return map;
    }
    
}
