using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_surgeryRoom : CommonRoom
{
    
    public GameObject bookTable;
    
    public GameObject getBookTable() {
        return bookTable;
    }
    
    public new List<string>  findSomethingNews(string charaName)
    {
        this.getGuangboMessage().Clear();
        if (charaName == SystemConstant.P4_NAME)
        {
            this.getGuangboMessage().Add("这间屋子摆放了不少人体模型，好像是个外科室。");
            this.getGuangboMessage().Add("桌子上有不少的病人就诊资料，我仔细看看。");        
            return this.getGuangboMessage();
        }

        return null;
    }

   
}
