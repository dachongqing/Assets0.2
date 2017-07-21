using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_restRoom : CommonRoom {

    public override List<string> findSomethingNews(Character chara)
    {
        getClickMessage().Clear();
        if (chara.getName() == SystemConstant.P1_NAME)
        {
            getClickMessage().Add("平常我都是来这里休息的，躺一会马上感觉身体充满了力量。");
           
        }
        else if (chara.getName() == SystemConstant.P2_NAME)
        {

            getClickMessage().Add("不错，沙发软软的，我要休息会了。");
          
        }
        else if (chara.getName() == SystemConstant.P3_NAME)
        {
            getClickMessage().Add("偶尔在这里躺一会也是不错的。");
        }
        else if (chara.getName() == SystemConstant.P4_NAME)
        {
            this.getClickMessage().Add("好困啊，只能眯会了。");
           
        }
        else if (chara.getName() == SystemConstant.P5_NAME)
        {
            getClickMessage().Add("要是再配个电脑，我都不想走出这个房间了。");       

        }
        else if (chara.getName() == SystemConstant.P6_NAME)
        {
            getClickMessage().Add("安静的房间，适合休息一下。");
        }

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
