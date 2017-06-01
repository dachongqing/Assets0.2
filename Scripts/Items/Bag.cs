using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag  {

    private Dictionary<string, List<Item>> bag;

    public Bag() {
        bag = new Dictionary<string, List<Item>>();
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

    public void insertItem(Item item) {
        List<Item> list;
        if (item.getCode().StartsWith(ItemConstant.ITEM_TYPE_POTION))
        {
            list = bag[ItemConstant.ITEM_TYPE_POTION];
            list.Add(item);
        }
        else if (item.getCode().StartsWith(ItemConstant.ITEM_TYPE_TOOL))
        {
            list = bag[ItemConstant.ITEM_TYPE_TOOL];
            list.Add(item);
        }
        else if (item.getCode().StartsWith(ItemConstant.ITEM_TYPE_SPEC))
        {
            list = bag[ItemConstant.ITEM_TYPE_SPEC];
            list.Add(item);
        }
    }
    
}
