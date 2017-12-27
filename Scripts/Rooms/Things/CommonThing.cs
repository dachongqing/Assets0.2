using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class CommonThing : MonoBehaviour ,Thing{

    private string thingCode;

    private bool isEmpty;

    private int[] xyz;

    private string[] clickMessage;

    private GameObject playerGameObject;

    private bool leavlFlag = false;

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
	void FixedUpdate () {
        if (leavlFlag)
        {
            float dis = Vector3.Distance(this.transform.position, this.playerGameObject.transform.position);
           // Debug.Log("we are dis is " + dis);
            if (dis > 1)
            {
                offCharaInfoMenuItem();
                if (getOperationItem() != null)
                {
                        getOperationItem().SetActive(false);
                }
                leavlFlag = false;
            }
        }
    }

    public virtual void doMiniOperation()
    {

    }

    public virtual void showCharaInfoMenuItem()
    {

    }

    public virtual void offCharaInfoMenuItem()
    {

    }

    public virtual GameObject getOperationItem()
    {
        return null;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log(coll.gameObject.name);
        if (coll.gameObject.name == "Player")
        {
            showCharaInfoMenuItem();
            if (getOperationItem() != null) {
                getOperationItem().SetActive(true);
            }
            leavlFlag = false;
            
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        Debug.Log(coll.gameObject.name);
        if (coll.gameObject.name == "Player")
        {
            //offCharaInfoMenuItem();
            //  if (getOperationItem() != null)
            //{
            //     getOperationItem().SetActive(false);
            // }
            this.playerGameObject = coll.gameObject;
            leavlFlag = true;
        }

    }



}
