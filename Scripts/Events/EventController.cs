using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventController : MonoBehaviour
{
    
    private bool leaveExecuted;

    public MessageUI messageUI;

    public RollDiceUIManager uiManager;

    private EventInterface eventI;

    private Character chara;

    private RoomInterface ri;
       
    private int phase;

    private DoorInterface door;

    private EventResult result;

    public void excuteLeaveRoomEvent (DoorInterface door, RoomInterface ri, Character chara)
	{
		//这个房间有没有离开事件
		 eventI = ri.getRoomEvent (EventConstant.LEAVE_EVENT);

		//不为空有事件
		if (eventI != null) {
			
				Debug.Log ("有离开事件");
                leaveExecuted = false;
                this.ri = ri;
                this.chara = chara;
                this.door = door;
                phase = 1;
                messageUI.getResult().setDone(false);



        } else {
			//为空没有事件
			Debug.Log ("没有离开事件");
            door.playerOpenDoorResult(true);

            }
	}

    public bool excuteLeaveRoomEvent(RoomInterface ri, Character chara)
    {
        //这个房间有没有离开事件
        eventI = ri.getRoomEvent(EventConstant.LEAVE_EVENT);

        //不为空有事件
        if (eventI != null)
        {
           
                EventResult result = eventI.excute(chara, null, 0);

                return result.getStatus();
          

        }
        else
        {
            //为空没有事件
            Debug.Log("没有离开事件");
            return true;
        }
    }

    public bool excuteStayRoomEvent (RoomInterface ri, Character chara)
	{
		return false;
	}

	public bool excuteEnterRoomEvent (RoomInterface ri, Character chara)
	{
		return false;
	}

	public void showMessageUi (string message, Dictionary<string,string> selectItem)
	{
        messageUI.ShowMessge(message,1);

    }

    void Start()
    {
        leaveExecuted = true;

      
    }

      private int rollVaue = 0;
      void Update()
    {
        if (!leaveExecuted) {

            if (phase == 1 && !messageUI.getResult().getDone())
            {
                Debug.Log("phase =1 and " + messageUI.getResult().getDone());
              
                showMessageUi(eventI.getEventBeginInfo(), eventI.getSelectItem());
            } else if(phase == 1 && messageUI.getResult().getDone())
            {
                Debug.Log("phase =1 and " + messageUI.getResult().getDone());
                phase = 2;
               
            }

            if (phase == 2 && !uiManager.getResult().getDone() && messageUI.isClosed())
            {
                Debug.Log("wait mesui end");
                RollDiceParam param = new RollDiceParam(chara.getAbilityInfo()[1]);
                uiManager.setRollDiceParam(param);
                uiManager.showRollDice();
               
            } else if (phase == 2 && uiManager.getResult().getDone()) {
                rollVaue = uiManager.getResult().getResult();
               // uiManager.closeRollPlane();
                phase = 3;
            }
           
            if (phase == 3 && !uiManager.isClosedPlane()) {
                result = eventI.excute(chara, messageUI.getResult().getResult(), rollVaue);
				Debug.Log ("event result is " + result);
                showMessageUi(eventI.getEventEndInfo(result.getResultCode()), null);
                phase = 4;
            }
            
            else if (phase == 4 && messageUI.getResult().getDone())
            {
                this.door.playerOpenDoorResult(result.getStatus());
                leaveExecuted = true;
            }
        }
    }
}
