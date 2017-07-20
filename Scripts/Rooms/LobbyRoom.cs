using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LobbyRoom : CommonRoom
{
    public GameObject Barrel;

    public GameObject getBarrel()
    {
        return Barrel;
    }

    public override List<string> findSomethingNews(Character chara)
    {
        getClickMessage().Clear();
        if (chara.getName() == SystemConstant.P1_NAME)
        {
            getClickMessage().Add("现在几点了？");
            getClickMessage().Add("我好像还有一个手术要做？");
        } else if (chara.getName() == SystemConstant.P2_NAME) {
           
              getClickMessage().Add("管它是什么地方？");
            getClickMessage().Add("这难道不是一个冒险的绝佳场所吗？");
        }
        else if (chara.getName() == SystemConstant.P3_NAME)
        {
            getClickMessage().Add("不管这里是那里，神都会保佑我的。");
        }
        else if (chara.getName() == SystemConstant.P4_NAME)
        {
            
                getClickMessage().Add("这里是哪里？");
                getClickMessage().Add("我不是在调查医院弃尸的黑幕吗？");
           
        }
        else if (chara.getName() == SystemConstant.P5_NAME)
        {
             getClickMessage().Add("这里很奇怪？这么多的门？");
            getClickMessage().Add("好像黑客帝国的场景？");
        }
        else if (chara.getName() == SystemConstant.P6_NAME)
        {
            getClickMessage().Add("这是什么地方？我为什么会在这里？"); 
        }

        return getClickMessage();
    }

    void Start()
    {
        Debug.Log("房间内物品初始化");
        Barrel.GetComponent<Barrel>().init(getXYZ());
      
    }


}

