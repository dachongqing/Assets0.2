using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {

    private MessageUI msg;

    private DuiHuaUImanager duiHuaUImanager;

    public void useItem(Item item, NPC chara, Character forNPCchara, BattleMenuUI battleMenuUI)
    {
        Debug.Log("战斗中使用物品");
        if (item == null)
        {
            msg.showMessage("请选择一个物品。");
        }
        else
        {
            if (item.getType() == ItemConstant.ITEM_TYPE_POTION
                || item.getType() == ItemConstant.ITEM_TYPE_TOOL)
            {
                Debug.Log("对自己使用");
                this.useItem(item, chara);
            }        
            else 
            {
                    Debug.Log("对NPC使用任务道具");
                if (useItemSpec(forNPCchara, item, battleMenuUI))
                {
                    chara.getBag().removeItem(item);
                };
            }
        }
    }

    public void useItem(Item item, NPC chara, Character forNPCchara)
    {

        if (item == null)
        {
            msg.showMessage("请选择一个物品。");
        }
        else
        {
            if (item.getType() == ItemConstant.ITEM_TYPE_POTION)
            {
                Debug.Log("使用药水道具");
                if (typeof(NPC).IsAssignableFrom(forNPCchara.GetType())) {
                    useItemPotion(item, (NPC)forNPCchara);
                    chara.getBag().removeItem(item);
                } else {
                    msg.showMessage("无法对怪物使用药水道具。");
                    Debug.Log("无法对怪物使用药水道具");
                }
            }
            else if (item.getType() == ItemConstant.ITEM_TYPE_TOOL)
            {
                //useItemTool(item, chara);
                Debug.Log("无法使用道具");
                msg.showMessage("无法对"+ chara.getName()+ "使用功能道具。");
            }
            else if (item.getType() == ItemConstant.ITEM_TYPE_SPEC)
            {
                if (useItemSpec(forNPCchara,item,null))
                {
                    chara.getBag().removeItem(item);
                };
            }
        }
    }

    public void useItem(Item item, NPC chara) {

        if (item == null)
        {
            msg.showMessage("请选择一个物品。");
        } else {
            if (item.getType() == ItemConstant.ITEM_TYPE_POTION)
            {
                Debug.Log("使用药水道具");
                useItemPotion(item, chara);
                chara.getBag().removeItem(item);
            }
            else if(item.getType() == ItemConstant.ITEM_TYPE_TOOL)
            {
                useItemTool(item, chara);
                chara.getBag().updateItem(item);
            }
            else if (item.getType() == ItemConstant.ITEM_TYPE_SPEC)
            {
                if (useItemSpec(null, item,null)) {
                    chara.getBag().removeItem(item);
                };
            }
        }
    }

    private bool useItemSpec(Character forNPCchara,Item item,BattleMenuUI battleMenuUI)
    {
        if (forNPCchara!=null ) {
            if (forNPCchara.getName() == SystemConstant.P4_NAME)
            {
                return spItemforP4((NPC)forNPCchara, item, battleMenuUI);
            } else if(forNPCchara.getName() == SystemConstant.MONSTER1_NAME) {
                return spItemforM1(forNPCchara, item);
            }
            else
            {
                msg.showMessage(forNPCchara.getName() + "似乎对这个没什么兴趣。");
                return false;
            }
        }else
        {
            msg.showMessage("什么事情都没有发生。");
            return false;
        }
    }

	private void useItemTool(Item item, NPC chara)
    {
		if (item.getCode() == ItemConstant.ITEM_CODE_TOOL_10001)
		{
			Debug.Log("你使用了一个透明骰子的道具");
			chara.setDiceNumberBuffer(1);
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

        
    }

    // Use this for initialization
    void Start () {
        msg = FindObjectOfType<MessageUI>();
        duiHuaUImanager = FindObjectOfType<DuiHuaUImanager>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private bool spItemforP4(NPC forNPCchara, Item item, BattleMenuUI battleMenuUI) {
        Debug.Log("使用任务道具");
        if (forNPCchara.isBoss() && item.getCode() == ItemConstant.ITEM_CODE_SPEC_Y0006)
        {
            int san = forNPCchara.getAbilityInfo()[3];
            if (san + 4 > forNPCchara.getMaxAbilityInfo()[3])
            {
                forNPCchara.getAbilityInfo()[3] = forNPCchara.getMaxAbilityInfo()[3];
            }
            else
            {
                forNPCchara.getAbilityInfo()[3] = san + 2;
            }
            Debug.Log("使用任务道具成功");
            battleMenuUI.hidenUI(false);
            duiHuaUImanager.showDuiHua(forNPCchara.getLiHuiURL(), new string[] { "父亲。。父亲的照片。","为什么他会在这里?" }, 1);
         
            return true;
        }
        else if (forNPCchara.isBoss()
          && item.getCode() == ItemConstant.ITEM_CODE_SPEC_Y0007)
        {
            int san = forNPCchara.getAbilityInfo()[3];
            if (san + 3 > forNPCchara.getMaxAbilityInfo()[3])
            {
                forNPCchara.getAbilityInfo()[3] = forNPCchara.getMaxAbilityInfo()[3];
            }
            else
            {
                forNPCchara.getAbilityInfo()[3] = san + 3;
            }
            Debug.Log("使用任务道具成功");
            battleMenuUI.hidenUI(false);
            duiHuaUImanager.showDuiHua(forNPCchara.getLiHuiURL(), new string[] { "医院保密文件？", "难道医生是好人？" }, 1);
            return true;
        }
        else
        {
            Debug.Log("使用任务道具失败");
            return false;
        }
    }

    private bool spItemforM1(Character forNPCchara, Item item)
    {
        if (item.getCode() == ItemConstant.ITEM_CODE_SPEC_Y0005)
        {
            int str = forNPCchara.getAbilityInfo()[0];            
            forNPCchara.getAbilityInfo()[3] = str - 2;
            return true;
        }
        else
        {
            return false;
        }
    }
}
