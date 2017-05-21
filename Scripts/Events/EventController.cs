using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour {

    private EventConstant constant;



    public bool excuteLeaveRoomEvent(RoomInterface ri, Character chara) {

        EventInterface eventI = ri.getRoomEvent(EventConstant.LEAVE_EVENT);

        if(eventI != null ) { 
            //show ui 
            string selectCode = showMessageUi(eventI.getEventBeginInfo(), eventI.getSelectItem());

            EventResult result = eventI.excute(chara, selectCode);

            showMessageUi(eventI.getEventEndInfo(result.getResultCode()), null);

            return result.getStatus();

         } else
        {
            return true;
        }
    }

    public bool excuteStayRoomEvent(RoomInterface ri, Character chara) {
        return false;
    }

    public bool excuteEnterRoomEvent(RoomInterface ri, Character chara) {
        return false;
    }


   

    public string showMessageUi(string message,Dictionary<string,string> selectItem) {

        return null;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
