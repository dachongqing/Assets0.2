using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BookRoom : MonoBehaviour, RoomInterface
{

    private String roomName;

    [SerializeField] private int[] xyz;



    public GameObject northDoor;
    public GameObject southDoor;
    public GameObject westDoor;
    public GameObject eastDoor;

	public GameObject box;

    private List<Character> charas = new List<Character>();
  

    private StoryInterface si;


    private Dictionary<String, EventInterface> eventsList = new Dictionary<string, EventInterface>();




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
        return RoomConstant.ROOM_TYPE_BOOK_ROOM;
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

	public GameObject getBox() {
		return box;	
	}


    public EventInterface getRoomEvent(string eventType)
    {
        if (eventsList.Count != 0)
        {
            return eventsList[eventType];

        }
        else
        {
            return null;
        }
    }

    public void setRoomEvent(EventInterface ei)
    {
        eventsList.Add(ei.getEventType(), ei);

    }

    public List<Character> getCharas()
    {
        return this.charas;
    }

    public void setChara(Character chara)
    {
        this.charas.Add(chara);
       

    }

    public void removeChara(Character chara)
    {
        this.charas.Remove(chara);
       
    }

    public bool checkRoomStoryStart(Character chara)
    {
        return si.checkStoryStart(chara, this);
    }

    public void setRoomStory(StoryInterface si)
    {
        this.si = si;
    }

    public StoryInterface getStartedStory()
    {
        return this.si;
    }

    List<string> guangboMessage = new List<string>();


    public List<string> findSomethingNews(string charaName)
    {
        guangboMessage.Clear();
        if (charaName == "叶成亮")
        {
            guangboMessage.Add("哟，这里还有个书店");
            guangboMessage.Add("我要好好调查下这个书店");
            guangboMessage.Add("一本黑色的新书成功引起了我的注意");
            return guangboMessage;
            
        }

        return null;
    }
}
