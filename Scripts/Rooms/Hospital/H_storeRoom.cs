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
            getClickMessage().Add("这里好像是我的办公室，哈哈， 咦？");
            getClickMessage().Add("没错，那个冒险家的病 被我轻松治好了。");

        }
        else if (chara.getName() == SystemConstant.P2_NAME)
        {

            getClickMessage().Add("这个应该是巧合吧？我在一次地下溶洞探险的时候被 什么东西咬了一下");
            getClickMessage().Add("咬伤而已没什么大不了。哈哈，对吧医生？");
            getClickMessage().Add("后面医院还派了其他医生来跟踪回访呢。");
        }
        else if (chara.getName() == SystemConstant.P3_NAME)
        {
            getClickMessage().Add("早知道不来这个房间了，我还有更重要的祷告要做呢。");
        }
        else if (chara.getName() == SystemConstant.P4_NAME)
        {
            this.getClickMessage().Add("这间屋子摆放了不少人体模型，好像是个外科室。");
            this.getClickMessage().Add("桌子上有不少的病人就诊资料，我仔细看看。");
        }
        else if (chara.getName() == SystemConstant.P5_NAME)
        {
            getClickMessage().Add("这间房间没有什么特别值得注意的");
            getClickMessage().Add("看看那边的报栏上有什么报纸新闻？");

        }
        else if (chara.getName() == SystemConstant.P6_NAME)
        {
            getClickMessage().Add("不明虫子咬伤？");
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
