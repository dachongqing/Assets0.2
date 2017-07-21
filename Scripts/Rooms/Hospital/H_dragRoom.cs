using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_dragRoom : CommonRoom
{

    public GameObject dragTable;

    public GameObject getDragTable() {
        return dragTable;
    }

    public override List<string> findSomethingNews(Character chara)
    {
        getClickMessage().Clear();
        if (chara.getName() == SystemConstant.P1_NAME)
        {
            getClickMessage().Add("让我找找，好像有能用的药？");

        }
        else if (chara.getName() == SystemConstant.P2_NAME)
        {

            getClickMessage().Add("有没有什么安眠药啊？ 最近一直兴奋的睡不着。");

        }
        else if (chara.getName() == SystemConstant.P3_NAME)
        {
            getClickMessage().Add("信神明的话，是不需要这些西药的，神会治愈一切。");
        }
        else if (chara.getName() == SystemConstant.P4_NAME)
        {
            this.getClickMessage().Add("好好找找有什么止血消炎的药带身上。");
        }
        else if (chara.getName() == SystemConstant.P5_NAME)
        {
            getClickMessage().Add("这医院药还挺多的。");
        }
        else if (chara.getName() == SystemConstant.P6_NAME)
        {
            getClickMessage().Add("这个药好像有点用？");
        }

        return getClickMessage();
    }

    // Use this for initialization
    void Start () {
       // dragTable.GetComponent<DragTable>().setRoom(this.getXYZ());
        dragTable.GetComponent<DragTable>().init(this.getXYZ());
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
