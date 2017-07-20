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
