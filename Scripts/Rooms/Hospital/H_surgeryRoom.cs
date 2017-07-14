using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_surgeryRoom : CommonRoom
{
    
    public GameObject bookTable;
    
    public GameObject getBookTable() {
        return bookTable;
    }
    
    public new List<string>  findSomethingNews(Character chara)
    {
        this.getClickMessage().Clear();
        if (chara.getName() == SystemConstant.P4_NAME)
        {
            this.getClickMessage().Add("这间屋子摆放了不少人体模型，好像是个外科室。");
            this.getClickMessage().Add("桌子上有不少的病人就诊资料，我仔细看看。");        
            return this.getClickMessage();
        }

        return null;
    }

    void Start()
    {
        bookTable.GetComponent<BookTable>().init(this.getXYZ());
      
    }


}
