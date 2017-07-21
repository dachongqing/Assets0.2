using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_minitorRoom : CommonRoom

{
    public GameObject computer;

    public GameObject getComputer() {
        return computer;
    }

    public override List<string> findSomethingNews(Character chara)
    {
        getClickMessage().Clear();
        if (chara.getName() == SystemConstant.P1_NAME)
        {
            getClickMessage().Add("以前一旦上班偷懒马上就被发现了，我恨摄像头。");
       
        }
        else if (chara.getName() == SystemConstant.P2_NAME)
        {
            getClickMessage().Add("怎么多的屏幕，怎么都是黑的啊？");
        }
        else if (chara.getName() == SystemConstant.P3_NAME)
        {
            getClickMessage().Add("哼，我们的所作所为都在神明的注视下。");
        }
        else if (chara.getName() == SystemConstant.P4_NAME)
        {
            this.getClickMessage().Add("我估计这里一定有什么秘密，好好找找。");
        }
        else if (chara.getName() == SystemConstant.P5_NAME)
        {
            getClickMessage().Add("啊哈！让我看看里面有什么好东西？");
        }
        else if (chara.getName() == SystemConstant.P6_NAME)
        {
            getClickMessage().Add("这些摄像头能监控所有房间吗？");
        }

        return getClickMessage();
    }

    // Use this for initialization
    void Start () {
        computer.GetComponent<Computer>().init(this.getXYZ());
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
