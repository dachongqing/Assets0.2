using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {

    private MessageUI msg;

    public void useItem(Item item, NPC chara) {

        if (item == null)
        {
            msg.showMessage("请选择一个物品。");
        } else {
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


    }

    private void useItemSpec(Item item)
    {
        msg.showMessage("任务物品无法使用。");
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

    public  void destroyItemTool(Item item, NPC chara) {
        if (item == null)
        {
            msg.showMessage("请选择一个物品。");
        }
        else
        {
            Bag bag = chara.getBag();
            bag.removeItem(item);
            msg.showMessage("你丢弃了" + item.getName() + "。");
        }
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
            msg.showMessage("你使用了一个回复2点力量的药水");
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
            msg.showMessage("你使用了一个回复2点速度的药水");
        }
        Debug.Log("从背包里移除用掉的药水道具 " + item.getName());

        chara.getBag().removeItem(item);
    }

    // Use this for initialization
    void Start () {
        msg = FindObjectOfType<MessageUI>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
