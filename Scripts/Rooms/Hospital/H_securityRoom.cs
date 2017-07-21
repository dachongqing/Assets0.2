using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_securityRoom : CommonRoom
{

    public GameObject keysCabinet;

    public GameObject getKeysCabinet() {
        return keysCabinet;
    }

    public override List<string> findSomethingNews(Character chara)
    {
        getClickMessage().Clear();
        if (chara.getName() == SystemConstant.P1_NAME)
        {
            getClickMessage().Add("这里医院的保安一点都不靠谱，办公室老丢东西，一个小偷都没抓住。");
 

        }
        else if (chara.getName() == SystemConstant.P2_NAME)
        {

            getClickMessage().Add("咦？这里有好多钥匙额？");
        }
        else if (chara.getName() == SystemConstant.P3_NAME)
        {
            getClickMessage().Add("我倒是有个想去的地方，不知道这里有没有医院的钥匙？");
        }
        else if (chara.getName() == SystemConstant.P4_NAME)
        {
            this.getClickMessage().Add("我要找找有没有手术室的钥匙？");
        }
        else if (chara.getName() == SystemConstant.P5_NAME)
        {
            getClickMessage().Add("我建议把这里的钥匙都拿走，万一有用呢？");
        }
        else if (chara.getName() == SystemConstant.P6_NAME)
        {
            getClickMessage().Add("保安室有没有防身的道具呢？");
        }

        return getClickMessage();
    }

    // Use this for initialization
    void Start () {
        keysCabinet.GetComponent<KeysCabinet>().init(this.getXYZ());
      
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
