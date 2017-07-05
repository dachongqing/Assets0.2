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
        EventInterface ei = new SanCheckRoomEvent(7, 7);
        ei.setEventBeginInfo(this.getEventBeginInfo());
        ei.setEventEndInfo(this.getEventEndInfoMap());
        this.setRoomEvent(ei);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override Dictionary<string, string[]> getEventEndInfoMap()
    {
        Dictionary<string, string[]> map = new Dictionary<string, string[]>();
        string[] good = new string[] { "让你感到惊讶的是停尸房并不是一个大柜子，而是直接放了很多床，每个床上都放了一具尸体。","尸体都是直接暴露在床上，你虽然感受到了恐惧，但是仍然没有逃跑。"
        };
        string[] bad = new string[] { "庞大的房间里摆满了床位，而尸体就直接放在上面，没有任何遮掩","尸体大多数都是被解剖过的，眼前的景象让你感到恐惧，短暂的瞬间你似乎听到了一阵阵莎莎声。"
            ,"你需要投掷一个1D2的神志扣除。"
        };
        map.Add(EventConstant.SANCHECK_EVENT_SAFE, good);
        map.Add(EventConstant.SANCHECK_EVENT_BAD, bad);
        return map;
    }
}
