using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BookRoom : CommonRoom
{

    public GameObject Box;

    public GameObject getBox() {
        return Box;
    }


    public new List<string> findSomethingNews(string charaName)
    {
        this.getGuangboMessage().Clear();
        if (charaName == "叶成亮")
        {
            this.getGuangboMessage().Add("哟，这里还有个书店");
            this.getGuangboMessage().Add("我要好好调查下这个书店");
            this.getGuangboMessage().Add("一本黑色的新书成功引起了我的注意");
            return this.getGuangboMessage();
            
        }

        return null;
    }

   
}
