using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_morgueRoom : CommonRoom
{

    public GameObject bed;

    public GameObject getBed()
    {
        return bed;
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
        EventInterface ei = new SanCheckRoomEvent(10,4, this.getEventBeginInfo(), this.getEventEndInfoMap(),
           EventConstant.ENTER_EVENT, EventConstant.SANCHECK_EVENT,2,-1,2);
        this.setRoomEvent(ei);
        bed.GetComponent<Bed>().init(this.getXYZ());
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override string[] getEventBeginInfo()
    {

        return new string[] { "跟你记忆中的停尸房完全不一样，没有大柜子一样的装置来存放尸体，而是一排排的床位直接摆放着尸体。"
        ,"你需要投一个神志检测。"};
    }

    public override Dictionary<string, string[]> getEventEndInfoMap()
    {
        Dictionary<string, string[]> map = new Dictionary<string, string[]>();
        string[] good = new string[] { "尸体都是直接暴露在床上，你虽然感受到了恐惧，但是仍然没有逃跑。"
        };
        string[] normal = new string[] { "尸体都是直接暴露在床上，没有把你吓跑， 你的神志下降1点。"
        };
        string[] bad = new string[] { "庞大的房间里摆满了床位，而尸体就直接放在上面，没有任何遮掩","尸体大多数都是被解剖过的，眼前的景象让你感到恐惧，短暂的瞬间你似乎听到了一阵阵莎莎声。"
            ,"你需要投掷一个1D2的神志扣除。"
        };
        map.Add(EventConstant.SANCHECK_EVENT_GOOD, good);
        map.Add(EventConstant.SANCHECK_EVENT_NORMAL, normal);
        map.Add(EventConstant.SANCHECK_EVENT_BED, bad);
        return map;
    }
}
