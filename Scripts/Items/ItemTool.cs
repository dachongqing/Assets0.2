using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTool : Item {

	private int durability;

	private string code;

	private string type;

	private string name;

    private string desc;

    public string getDesc()
    {
        return this.desc;
    }


    public ItemTool(string code , string name, int durability, string desc) {
		this.code = code;
		this.type = ItemConstant.ITEM_TYPE_TOOL;
		this.durability = durability;
		this.name = name;
        this.desc = desc;
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

	public void used(){
		this.durability = this.durability - 1;
	}
}
