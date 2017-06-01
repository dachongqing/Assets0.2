using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPotion : Item {

    private int durability;

    private string code;

    private string type;

    private string name;


    public ItemPotion(string code , string type, int durability, string name) {
        this.code = code;
        this.type = type;
        this.durability = durability;
        this.name = name;
    }

    public string getCode()
    {
      return this.code;
    }

    public int getDurability()
    {
        return this.durability;
    }

    public string getName()
    {
        return this.name;
    }

    public string getType()
    {
        return this.type;
    }

    
    
}
