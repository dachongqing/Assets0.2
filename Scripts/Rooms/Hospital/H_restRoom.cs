using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_restRoom : CommonRoom {

    public override List<string> findSomethingNews(Character chara)
    {
        getClickMessage().Clear();      
        getClickMessage().Add("这里写的是休息室，我想待在这里休息一下。");  
        return getClickMessage();

    }

    public GameObject newsBoard;

    public GameObject getNewsBoard()
    {
        return newsBoard;
    }


    void Start()
    {
    
        newsBoard.GetComponent<NewsBoard>().setClickMessage(new string[] {"干净舒适的房间，心灵让你得到救赎。" });
        newsBoard.GetComponent<NewsBoard>().init(this.getXYZ());
        StayRoomEvent ei = new StayRoomEvent(0, 0, null, null,
        EventConstant.STAY_EVENT, EventConstant.SAN_STAY_EVENT, 1, 0, 0);
        this.setRoomEvent(ei);
        
        if (!FindObjectOfType<RoundController>().newOrLoad)
        {
            Debug.Log("load event");
            SaveData data = (SaveData)IOHelper.GetData(Application.persistentDataPath + "/Save/SaveData0.sav", typeof(SaveData));
            foreach(EventInfo eventInfo in data.EffectedList)
            {
                Debug.Log(" ckeck load event " + data.EffectedList.Count);
                if (eventInfo.RoomXyz[0] == this.getXYZ()[0]
                 && eventInfo.RoomXyz[1] == this.getXYZ()[1] && eventInfo.RoomXyz[2] == this.getXYZ()[2])
                {
                    Debug.Log(" do load event :" + eventInfo.EffectedList.Count);
                    ei.setEffectedList(eventInfo.EffectedList);
                }
            }
        }
      

    }
}
