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
