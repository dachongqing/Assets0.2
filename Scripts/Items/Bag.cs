using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag  {

    private Dictionary<string, List<Item>> bag;

    public System.Random random = new System.Random();

    public Bag() {
        List<Item> plist = new List<Item>();
        List<Item> tlist = new List<Item>();
        List<Item> slist = new List<Item>();
        bag = new Dictionary<string, List<Item>>();
        bag.Add(ItemConstant.ITEM_TYPE_POTION, plist);
        bag.Add(ItemConstant.ITEM_TYPE_TOOL, tlist);
        bag.Add(ItemConstant.ITEM_TYPE_SPEC, slist);
    }

    public void removeItem(Item item)
    {
        List<Item> list;
        if (item.getCode().StartsWith(ItemConstant.ITEM_TYPE_POTION))
        {
            list = bag[ItemConstant.ITEM_TYPE_POTION];
            list.Remove(item);
        }
        else if (item.getCode().StartsWith(ItemConstant.ITEM_TYPE_TOOL)) {
            list = bag[ItemConstant.ITEM_TYPE_TOOL];
            list.Remove(item);
        }
        else if (item.getCode().StartsWith(ItemConstant.ITEM_TYPE_SPEC)) {
            list = bag[ItemConstant.ITEM_TYPE_SPEC];
            list.Remove(item);
        }
       
    }

	public void updateItem(Item item) {
		List<Item> list;
		if (item.getCode().StartsWith(ItemConstant.ITEM_TYPE_POTION))
		{
			list = bag[ItemConstant.ITEM_TYPE_POTION];
		
			list [list.IndexOf (item)].used ();

		}
		else if (item.getCode().StartsWith(ItemConstant.ITEM_TYPE_TOOL)) {
			list = bag[ItemConstant.ITEM_TYPE_TOOL];
			list [list.IndexOf (item)].used ();
		}
		else if (item.getCode().StartsWith(ItemConstant.ITEM_TYPE_SPEC)) {
			list = bag[ItemConstant.ITEM_TYPE_SPEC];
			list [list.IndexOf (item)].used ();
		}
	}

	public List<Item> getTaskItems() {
		return bag [ItemConstant.ITEM_TYPE_SPEC];
	}

    public int getItemTotalCount() {
        return bag[ItemConstant.ITEM_TYPE_POTION].Count + bag[ItemConstant.ITEM_TYPE_TOOL].Count + bag[ItemConstant.ITEM_TYPE_SPEC].Count;
    }

    public Item getRandomPotionItem() {
       return  bag[ItemConstant.ITEM_TYPE_POTION][random.Next(bag[ItemConstant.ITEM_TYPE_POTION].Count)];
    }

    public void insertItem(Item item) {
        List<Item> list;
        if (item.getCode().StartsWith(ItemConstant.ITEM_TYPE_POTION))
        {
            list = bag[ItemConstant.ITEM_TYPE_POTION];
            list.Add(item);
            Debug.Log("你放入了一个药水物品");
        }
        else if (item.getCode().StartsWith(ItemConstant.ITEM_TYPE_TOOL))
        {
            list = bag[ItemConstant.ITEM_TYPE_TOOL];
            list.Add(item);
            Debug.Log("你放入了一个道具物品");
        }
        else if (item.getCode().StartsWith(ItemConstant.ITEM_TYPE_SPEC))
        {
            list = bag[ItemConstant.ITEM_TYPE_SPEC];
            list.Add(item);
            Debug.Log("你放入了一个人物物品");
        }
    }
    
}
