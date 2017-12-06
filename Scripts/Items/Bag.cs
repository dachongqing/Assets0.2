using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag  {

   // private Dictionary<string, List<Item>> bag;

    public System.Random random = new System.Random();
    
    private List<Item> bag = new List<Item>();

    public Bag() {
      
    }

    public void removeItem(Item item)
    {
        bag.Remove(item);
    }

	public void updateItem(Item item) {
        bag[bag.IndexOf (item)].used ();	
	}

	

    public int getItemTotalCount() {
        return bag.Count ;
    }

    public Item getRandomItem() {
       return  bag[random.Next(bag.Count)];
    }

    public void insertItem(Item item) {
            bag.Add(item);
       
    }

    public bool checkItem(string code ) {
       // Debug.Log("check code is " + code);
        foreach (Item item in bag) {
           // Debug.Log("item code is " + item.getCode());
            if (item.getCode() == code) {
                return true;
            }
        }
        return false;
    }

    public List<Item> getAllItems() {
       return  bag;
    }

    public  Item findItemByCode(string itemCode)
    {
        foreach (Item item in bag)
        {
            if (item.getCode() == itemCode)
            {
                return item;
            }
        }
        return null;
    }
}
