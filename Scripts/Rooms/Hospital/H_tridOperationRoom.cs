using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_tridOperationRoom : CommonRoom
{

    public GameObject operatingTable;

    public GameObject getOperatingTable()
    {
        return operatingTable;
    }

    public override List<string> findSomethingNews(string charaName)
    {
        getGuangboMessage().Clear();
        if (charaName == SystemConstant.P1_NAME)
        {
            getGuangboMessage().Add("啊！这里有未做完手术的尸体。");
            getGuangboMessage().Add("尸体手臂的脓包应该还没处理干净。");         
            return getGuangboMessage();

        }

        return null;
    }

    void Start()
    {
        setLock(true);
    }
    

    public override bool checkOpen(Character chara)
    {
        if (isLock())
        {
            if (typeof(NPC).IsAssignableFrom(chara.GetType()))
            {

                NPC npc = (NPC)chara;

                if (npc.getBag().checkItem(ItemConstant.ITEM_CODE_SPEC_Y0002))
                {
                    setLock(false);
                    return true;
                }
                else if (npc.getName() == SystemConstant.P1_NAME)
                {
                    setLock(false);
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }

        }
        else {
            return true;
        }
    }
}
