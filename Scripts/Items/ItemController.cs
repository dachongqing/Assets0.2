using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {

    public void useItem(Item item, NPC chara) {
        if (item.getType() == ItemConstant.ITEM_TYPE_POTION)
        {
            Debug.Log("使用药水道具");
            useItemPotion(item, chara);
        }
        else if(item.getType() == ItemConstant.ITEM_TYPE_TOOL)
        {
            useItemTool(item, chara);
        }
        else if (item.getType() == ItemConstant.ITEM_TYPE_SPEC)
        {
            useItemSpec(item);
        }
    }

    private void useItemSpec(Item item)
    {
      
    }

	private void useItemTool(Item item, NPC chara)
    {
		if (item.getCode() == ItemConstant.ITEM_CODE_TOOL_10001)
		{
			Debug.Log("你使用了一个透明骰子的道具");
			chara.setDiceNumberBuffer(1);
			chara.getBag().updateItem (item);

		}
    }

    private void destroyItemTool() {
    }

    private void useItemPotion(Item item, NPC chara)
    {
        if (item.getCode() == ItemConstant.ITEM_CODE_POTION_00001)
        {
            int str = chara.getAbilityInfo()[0];
            if (str + 2 > chara.getMaxAbilityInfo()[0]) {
                chara.getAbilityInfo()[0] = chara.getMaxAbilityInfo()[0];
            } else {
                chara.getAbilityInfo()[0] = str + 2;
            }
            Debug.Log("你使用了一个回复力量的物品");
        }

        if (item.getCode() == ItemConstant.ITEM_CODE_POTION_10001)
        {
            int speed = chara.getAbilityInfo()[1];
            if (speed + 2 > chara.getMaxAbilityInfo()[1])
            {
                chara.getAbilityInfo()[1] = chara.getMaxAbilityInfo()[1];
            }
            else
            {
                chara.getAbilityInfo()[1] = speed + 2;
            }
            Debug.Log("你使用了一个回复速度的物品");
        }
        Debug.Log("从背包里移除用掉的药水道具 " + item.getName());

        chara.getBag().removeItem(item);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
