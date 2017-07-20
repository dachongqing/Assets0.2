using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class CommonThing : MonoBehaviour ,Thing{

    private string thingCode;

    private bool isEmpty;

    private int[] xyz;

    private string[] clickMessage;

    private void saveEmptyThing(int[] roomXYZ, string code)
    {
        FindObjectOfType<ThingController>().emptyThing(roomXYZ,code);
    } 

    public void setIsEmpty(bool empty)
    {
        if(empty)
        {
            this.saveEmptyThing(this.xyz, this.getThingCode());
        }
        this.isEmpty = empty;
    }

    public bool getIsEmpty()
    {
        return this.isEmpty;
    }

    public virtual void doClick()
    {
       
    }

    public void setThingCode(string code)
    {
        this.thingCode = code;
    }

    public string getThingCode()
    {
        return this.thingCode;
    }

    public void setRoom(int[] xyz)
    {
        this.xyz = xyz;
    }

    public virtual void init(int[] xyz)
    {
       
    }

    public void loadInfo()
    {
        SaveData data = (SaveData)IOHelper.GetData(Application.persistentDataPath + "/Save/SaveData0.sav", typeof(SaveData));
        foreach(ThingInfo ti in data.Things)
        {         
            if (ti.ThingCode == this.thingCode && ti.RoomXyz[0] == this.xyz[0]
                && ti.RoomXyz[1] == this.xyz[1] && ti.RoomXyz[2] == this.xyz[2])
            {         
               this.isEmpty = true;
            }
        }
    }

    public void setClickMessage(string[] msg)
    {
        this.clickMessage = msg;
    }

    public string[] getClickMessage()
    {
        return this.clickMessage;
    }

    // Use this for initialization
    void Start () {
       

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
