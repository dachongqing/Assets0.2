using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_commonRoom : CommonRoom
{

	
    public GameObject bed1;

    public GameObject bed2;

    public GameObject light1;

    public GameObject light2;

    public GameObject getBed1()
    {
        return bed1;
    }

    public GameObject getBed2()
    {
        return bed2;
    }

    public GameObject getLight1()
    {
        return light1;
    }

    public GameObject getLight2()
    {
        return light2;
    }

    public override List<string> findSomethingNews(Character chara)
    {
        getClickMessage().Clear();
        if (chara.getName() == SystemConstant.P1_NAME)
        {
            getClickMessage().Add("普通的病房");
        }
        else if (chara.getName() == SystemConstant.P2_NAME)
        {
            getClickMessage().Add("有人吗？");
        }
        else if (chara.getName() == SystemConstant.P3_NAME)
        {
            getClickMessage().Add("躺在床上，能安静地睡去");
        }
        else if (chara.getName() == SystemConstant.P4_NAME)
        {
            this.getClickMessage().Add("好想睡觉");
        }
        else if (chara.getName() == SystemConstant.P5_NAME)
        {
            getClickMessage().Add("这床脏兮兮的");
        }
        else if (chara.getName() == SystemConstant.P6_NAME)
        {
            getClickMessage().Add("这些房间很老旧了");
        }

        return getClickMessage();
    }
    // Use this for initialization
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {

    }
   
}