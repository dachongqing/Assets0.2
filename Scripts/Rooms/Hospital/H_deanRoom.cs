using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_deanRoom : CommonRoom
{
    public GameObject safeBox;

    public GameObject getSafeBox()
    {
        return safeBox;

    }

    public override List<string> findSomethingNews(Character chara)
    {
        getClickMessage().Clear();
        if (chara.getName() == SystemConstant.P1_NAME)
        {
            getClickMessage().Add("院长办公室一定有什么重要资料，让我好好找找");

        }
        else if (chara.getName() == SystemConstant.P2_NAME)
        {

            getClickMessage().Add("这里的装饰很华丽嘛，这个金马我要拿走。");
  
        }
        else if (chara.getName() == SystemConstant.P3_NAME)
        {
            getClickMessage().Add("院长这个人，果然是心怀鬼胎。");
        }
        else if (chara.getName() == SystemConstant.P4_NAME)
        {
            this.getClickMessage().Add("好好调查下，看看有没有什么暗门？");
        }
        else if (chara.getName() == SystemConstant.P5_NAME)
        {
            getClickMessage().Add("我调查过院长的账号，有大量不明的交易，这里一定有问题！");
           
        }
        else if (chara.getName() == SystemConstant.P6_NAME)
        {
            getClickMessage().Add("我想这里一定有秘密。");
        }

        return getClickMessage();
    }
    // Use this for initialization
    void Start () {
        safeBox.GetComponent<SafeBox>().init(this.getXYZ());
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
