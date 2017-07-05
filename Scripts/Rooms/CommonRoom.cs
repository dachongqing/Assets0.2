using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CommonRoom : MonoBehaviour, RoomInterface
{

    private String roomName;

    [SerializeField] private int[] xyz;
    
    public GameObject northDoor;
    public GameObject southDoor;
    public GameObject westDoor;
    public GameObject eastDoor;

    private List<Character> charas = new List<Character>();
    private StoryInterface si;

    private Dictionary<String, EventInterface> eventsList = new Dictionary<string, EventInterface>();

    private string roomType;

    public void setRoomType(string roomType)
    {

        this.roomType = roomType;
    }

    string RoomInterface.getRoomName()
    {
        return roomName;
    }

    public void setRoomName(string name)
    {
         this.roomName = name;
    }

    string RoomInterface.getRoomType()
    {
        return this.roomType;
    }
    
    int[] RoomInterface.getXYZ()
    {
        return xyz;
    }

    void RoomInterface.setXYZ(int[] xyz)
    {
        this.xyz = xyz;
    }

    public void northDoorEnable()
    {
        //		northDoor.GetComponent<DoorInterface>().enabled = true;
        northDoor.GetComponent<DoorInterface>().setShowFlag(true);//门的图片要替换
    }

    public void southDoorEnable()
    {
        //		southDoor.GetComponent<DoorInterface>().enabled = true;
        southDoor.GetComponent<DoorInterface>().setShowFlag(true);//门的图片要替换

    }

    public void westDoorEnable()
    {
        //           westDoor.GetComponent<MonoBehaviour>().enabled = true;

        westDoor.GetComponent<DoorInterface>().setShowFlag(true);//门的图片要替换
    }

    public void eastDoorEnable()
    {
        //           eastDoor.GetComponent<MonoBehaviour>().enabled = true;
        eastDoor.GetComponent<DoorInterface>().setShowFlag(true);//门的图片要替换
    }

    GameObject RoomInterface.getNorthDoor()
    {
        return northDoor;
    }
    GameObject RoomInterface.getSouthDoor()
    {
        return southDoor;
    }
    GameObject RoomInterface.getEastDoor()
    {
        return eastDoor;
    }
    GameObject RoomInterface.getWestDoor()
    {
        return westDoor;
    }


    public EventInterface getRoomEvent(string eventType)
    {

        if (eventsList.Count != 0)
        {
            Debug.Log(eventsList.Keys);
            if (eventsList.ContainsKey(eventType))
            {
                return eventsList[eventType];

            } else
            {
                return null;
            }

        }
        else
        {
            return null;
        }
    }

    public void setRoomEvent(EventInterface ei)
    {
        Debug.Log("set event " + ei.getEventType());
        eventsList.Add(ei.getEventType(), ei);

    }

    public List<Character> getCharas()
    {
        return charas;
    }

    public void setChara(Character chara)
    {
       
        charas.Add(chara);
    }

    public void removeChara(Character chara)
    {
        charas.Remove(chara);
       
    }

    public void setRoomStory(StoryInterface si)
    {
        this.si = si;
    }

    public StoryInterface getStartedStory()
    {
        return this.si;
    }

    public bool checkRoomStoryStart(Character chara, RoundController roundController)
    {
        return false;
    }

    List<string> guangboMessage = new List<string>();

    public List<string> getGuangboMessage() {
        return guangboMessage;
    }

    public virtual List<string> findSomethingNews(string charaName)
    {
     
        return null;
    }

    private bool locked;

    public void setLock(bool locked)
    {
        this.locked = locked;
    }

    public bool isLock()
    {
        return locked;
    }

    public virtual bool checkOpen(Character chara)
    {
        return true;
    }

    public virtual string[] getEventBeginInfo()
    {
       return null;
    }

    public virtual Dictionary<string, string[]> getEventEndInfoMap()
    {
        return null;
    }
}

