using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_storeRoom : CommonRoom
{
    public GameObject safeCabinet;

    public GameObject getSafeCabinet()
    {
        return safeCabinet;
    }

    public override List<string> findSomethingNews(Character chara)
    {
        getClickMessage().Clear();
        if (chara.getName() == SystemConstant.P1_NAME)
        {
            getClickMessage().Add("竟然还有这种房间？这些器官都是从实验体解剖出来的？");
        }
        else if (chara.getName() == SystemConstant.P2_NAME)
        {
            getClickMessage().Add("啊。。那个器官还在动！");
        }
        else if (chara.getName() == SystemConstant.P3_NAME)
        {
            getClickMessage().Add("这么多的收藏品啊，神明一定会喜欢的。");
        }
        else if (chara.getName() == SystemConstant.P4_NAME)
        {
            this.getClickMessage().Add("。。。怪物！。。怪物！");
        }
        else if (chara.getName() == SystemConstant.P5_NAME)
        {
            getClickMessage().Add("这就是医院一直在做的地下研究吗？");
        }
        else if (chara.getName() == SystemConstant.P6_NAME)
        {
            getClickMessage().Add("医院到底再研究什么？");
        }

        return getClickMessage();
    }
    // Use this for initialization
    void Start () {
        EventInterface ei = new SanCheckRoomEvent(8, 6, this.getEventBeginInfo(), this.getEventEndInfoMap(),
           EventConstant.ENTER_EVENT, EventConstant.SANCHECK_EVENT, 1, -2, 2);
        this.setRoomEvent(ei);
        safeCabinet.GetComponent<SafeCabinet>().init(this.getXYZ());
      
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public override Dictionary<string, string[]> getEventEndInfoMap()
    {
        Dictionary<string, string[]> map = new Dictionary<string, string[]>();
        string[] good = new string[] { "映入你眼睛的是大量的玻璃瓶子里，装满了某种器官的样本","一些不知名的器官似乎还活着。","你虽然被眼前的景象震惊了，但是仍然保持着镇静。"
        };
        string[] normal = new string[] { "密密麻麻的瓶子让你感到恐惧","你的神志下降2点"
        };
        string[] bad = new string[] { "器官在瓶子里似乎有生命一样的","一股未知恐惧马上涌上你的头脑"
            ,"你需要投掷一个1D2的神志扣除。"
        };
        map.Add(EventConstant.SANCHECK_EVENT_GOOD, good);
        map.Add(EventConstant.SANCHECK_EVENT_NORMAL, normal);
        map.Add(EventConstant.SANCHECK_EVENT_BED, bad);
        return map;
    }
}
