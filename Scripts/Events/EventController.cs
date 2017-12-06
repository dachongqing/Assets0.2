using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventController : MonoBehaviour
{

    private bool leaveExecuted = true;

    private bool sanCheckExecuted = true;

    private bool fallRoomExecuted = true;

    public MessageUI messageUI;

   
    public RollDiceUIManager uiManager;

    private EventInterface eventI;

    private Character chara;

    private RoomInterface ri;
       
    private int phase;

    private DoorInterface door;

    private EventResult result;

    private List<EventInfo> stayEventList = new List<EventInfo>();

    private RoomContraller roomContraller;
    private CameraCtrl camCtrl;

    private string selectedOptionCode;

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
                showMessageUi(eventI.getEventBeginInfo(), eventI.getSelectItem());



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
                if (result.getResultCode() == EventConstant.LEAVE_EVENT_SAFE)
                {
                     return true;
                }
                else
                {
                    chara.updateActionPoint(0);               
                    return false;
                }       
        }
        else
        {
            //为空没有事件
          //  Debug.Log("没有离开事件");
            return true;
        }
    }

    public void excuteStayRoomEvent (RoomInterface ri, Character chara)
	{
    
        this.eventI = ri.getRoomEvent(EventConstant.STAY_EVENT);
        if (this.eventI != null)
        {
            EventInfo ei = new EventInfo();
            ei.RoomXyz = ri.getXYZ();
            ei.EffectedList.Add(chara.getName());
            this.stayEventList.Add(ei);
            eventI.excute(chara, null, 0);                         
        }
       
    }

	public bool excuteEnterRoomEvent (RoomInterface ri, Character chara)
	{
        eventI = ri.getRoomEvent(EventConstant.ENTER_EVENT);
       // Debug.LogError(this.eventI);
        if (eventI != null) {
            if(eventI.getSubEventType() == EventConstant.SANCHECK_EVENT)
            {
                return excuteSanCheckEvent(eventI, chara);
            }
            if (eventI.getSubEventType() == EventConstant.FALL_DOWN__EVENT)
            {
                return excuteFallRoomEvent(eventI, chara);
            }
        }
        Debug.Log(" no san check");
        return true;
	}

    public bool excuteFallRoomEvent(EventInterface eventI, Character chara)
    {

        this.eventI = eventI;
        if (this.eventI != null)
        {
            if (chara.isPlayer())
            {
                fallRoomExecuted = false;
                this.chara = chara;
                phase = 1;
                messageUI.getResult().setDone(false);
                showMessageUi(eventI.getEventBeginInfo(), eventI.getSelectItem());
            }
            else
            {
                result = eventI.excute(chara, messageUI.getResult().getResult(), 0);

                if (result.getResultCode() == EventConstant.FALL_DOWN__EVENT_GOOD)
                {
                  
                }
                else if (result.getResultCode() == EventConstant.FALL_DOWN__EVENT_NORMAL)
                {
                    roomContraller.findRoomByXYZ(chara.getCurrentRoom()).removeChara(chara);
                    this.roomContraller.setCharaInMiniMap(chara.getCurrentRoom(), chara, false);
                    RoomInterface ri =  roomContraller.getRandomDownRoom();
                    ri.setChara(chara);                  
                    chara.setCurrentRoom(ri.getXYZ());
                    this.roomContraller.setCharaInMiniMap(ri.getXYZ(), chara, true);
                  //  camCtrl.setTargetPos(upStairRoom.getXYZ(), RoomConstant.ROOM_Y_DOWN, true);

                }
                else
                {
                    RollDiceParam param = new RollDiceParam(this.eventI.getBadDiceNum());
                    rollVaue = uiManager.showRollDiceImmediately(param);
                    chara.getAbilityInfo()[0] = chara.getAbilityInfo()[0] - rollVaue;
                    roomContraller.findRoomByXYZ(chara.getCurrentRoom()).removeChara(chara);
                    this.roomContraller.setCharaInMiniMap(chara.getCurrentRoom(), chara, false);
                    RoomInterface ri = roomContraller.getRandomDownRoom();
                    ri.setChara(chara);
                    chara.setCurrentRoom(ri.getXYZ());
                    this.roomContraller.setCharaInMiniMap(ri.getXYZ(), chara, true);
                }
            }
        }
        return true;
    }

    public bool excuteSanCheckEvent(EventInterface eventI, Character chara)
    {
        this.eventI = eventI;
        if(this.eventI != null)
        {
            if(chara.isPlayer())
            {
                sanCheckExecuted = false;          
                this.chara = chara;
                phase = 1;
                messageUI.getResult().setDone(false);
                showMessageUi(eventI.getEventBeginInfo(), eventI.getSelectItem());
            } else
            {
                result = eventI.excute(chara, messageUI.getResult().getResult(), 0);

                if (result.getResultCode() == EventConstant.SANCHECK_EVENT_GOOD)
                {
                    chara.getMaxAbilityInfo()[3] = chara.getMaxAbilityInfo()[3] + this.eventI.getGoodValue();
                    chara.getAbilityInfo()[3] = chara.getAbilityInfo()[3] + this.eventI.getGoodValue();
                } else if (result.getResultCode() == EventConstant.SANCHECK_EVENT_NORMAL)
                {
                    chara.getAbilityInfo()[3] = chara.getAbilityInfo()[3] + this.eventI.getNormalValue();
                } else {
                    RollDiceParam param = new RollDiceParam(this.eventI.getBadDiceNum());
                    rollVaue = uiManager.showRollDiceImmediately(param);
                    chara.getAbilityInfo()[3] = chara.getAbilityInfo()[3] - rollVaue;
                }
            }
        }
        return true;
    }

    public void showMessageUi(string message)
    {        
     messageUI.showMessges(new string[] { message });     
    }

    public void showMessageUi (string[] message, Dictionary<string, string[]> options)
	{
        if (options == null || options.Count == 0) {
            messageUI.showMessges(message);

        } else {
           
            string[] StringOptions = new string[] { options[EventConstant.OPTION_CODE_1][0], options[EventConstant.OPTION_CODE_2][0], options[EventConstant.OPTION_CODE_3][0] };

            messageUI.showMessges(message[0], StringOptions);
           
        }
    }

    void Start()
    {
        leaveExecuted = true;
        roomContraller = FindObjectOfType<RoomContraller>();
        camCtrl = FindObjectOfType<CameraCtrl>();
    }

      private int rollVaue = 0;
      void Update()
    {
        if (!leaveExecuted) {

		
             if(phase == 1 && messageUI.getResult().getDone())
            {
                Debug.Log("phase =1 and " + messageUI.getResult().getDone());
                phase = 2;
               
            }

            if (phase == 2 && !uiManager.getResult().getDone() && messageUI.isClosed())
            {
                Debug.Log("wait mesui end");
				RollDiceParam param = new RollDiceParam(chara.getAbilityInfo()[1] +chara.getDiceNumberBuffer());            
                rollVaue = uiManager.showRollDiceImmediately(param);
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
                if (result.getResultCode() ==EventConstant.LEAVE_EVENT_SAFE)
                {
                    this.door.playerOpenDoorResult(true);
                } else
                {
                    chara.updateActionPoint(0);
                    this.door.playerOpenDoorResult(false);
                }
                leaveExecuted = true;
            }
        }

        if(!this.sanCheckExecuted)
        {
            if (phase == 1 && messageUI.getResult().getDone())
            {
                Debug.Log("phase =1 and " + messageUI.getResult().getDone());
                phase = 2;

            }

            if (phase == 2 && !uiManager.getResult().getDone() && messageUI.isClosed())
            {
                Debug.Log("wait mesui end");
                RollDiceParam param = new RollDiceParam(chara.getAbilityInfo()[3] + chara.getDiceNumberBuffer());
                rollVaue = uiManager.showRollDiceImmediately(param);
                phase = 3;


            }

            if (phase == 3 && !uiManager.isClosedPlane())
            {
                result = eventI.excute(chara, messageUI.getResult().getResult(), rollVaue);
                Debug.Log("event result is " + result);
                showMessageUi(eventI.getEventEndInfo(result.getResultCode()), null);
                phase = 4;
            }

            else if (phase == 4 && messageUI.getResult().getDone())
            {
                if(result.getResultCode() == EventConstant.SANCHECK_EVENT_BED)
                {
                    RollDiceParam param = new RollDiceParam(eventI.getBadDiceNum());
                    rollVaue = uiManager.showRollDiceImmediately(param);
                    chara.getAbilityInfo()[3] = chara.getAbilityInfo()[3] - rollVaue;
                } else if(result.getResultCode() == EventConstant.SANCHECK_EVENT_NORMAL)
                {
                    chara.getAbilityInfo()[3] = chara.getAbilityInfo()[3] + eventI.getNormalValue();
                } else
                {
                    chara.getAbilityInfo()[3] = chara.getAbilityInfo()[3] + eventI.getGoodValue();
                }
                sanCheckExecuted = true;
            }
        }

        if (!this.fallRoomExecuted)
        {
            // Debug.LogError(eventI);
            if (phase == 1 && messageUI.getResult().getDone())
            {
                Debug.Log("phase =1 and " + messageUI.getResult().getDone());

                if (eventI.getSelectItem() == null || eventI.getSelectItem().Count == 0) {
                    phase = 3;
                } else {
                    phase = 2;
                    this.selectedOptionCode = messageUI.getResult().getResult();
                    showMessageUi(eventI.getSelectItem()[this.selectedOptionCode][1]);
                }

            } else if (phase == 2 &&  messageUI.getResult().getDone())
            {
                phase = 3;
            }

            else if (phase == 3 && !uiManager.getResult().getDone() && messageUI.isClosed())
            {
                Debug.Log("wait mesui end");
                int rollNum;
                if (this.selectedOptionCode == EventConstant.OPTION_CODE_1) {
                    rollNum = chara.getAbilityInfo()[3];
                } else {
                    rollNum = chara.getAbilityInfo()[1];
                }
                RollDiceParam param = new RollDiceParam(rollNum + chara.getDiceNumberBuffer());
                rollVaue = uiManager.showRollDiceImmediately(param);
                phase = 4;


            }
                else if (phase == 4 && !uiManager.isClosedPlane())
            {
               // Debug.LogError(eventI);
               // Debug.LogError(messageUI.getResult());
                result = eventI.excute(chara, this.selectedOptionCode, rollVaue);
                Debug.Log("event result is " + result);
                showMessageUi(eventI.getEventEndInfo(result.getResultCode()), null);
                phase = 5;
            }

            else if (phase == 5 && messageUI.getResult().getDone())
            {
                if (result.getResultCode() == EventConstant.FALL_DOWN__EVENT_GOOD)
                {

                }
                else if (result.getResultCode() == EventConstant.FALL_DOWN__EVENT_NORMAL)
                {
                    roomContraller.findRoomByXYZ(chara.getCurrentRoom()).removeChara(chara);
                    this.roomContraller.setCharaInMiniMap(chara.getCurrentRoom(), chara, false);
                    RoomInterface ri = roomContraller.getRandomDownRoom();
                    ri.setChara(chara);
                    chara.setCurrentRoom(ri.getXYZ());
                    this.roomContraller.setCharaInMiniMap(ri.getXYZ(), chara, true);
                    camCtrl.setTargetPos(ri.getXYZ(), RoomConstant.ROOM_Y_DOWN, true);
                    fallRoomExecuted = true;
                }
                else
                {
                    RollDiceParam param = new RollDiceParam(this.eventI.getBadDiceNum());
                    rollVaue = uiManager.showRollDiceImmediately(param)+1;
                    chara.getAbilityInfo()[0] = chara.getAbilityInfo()[0] - rollVaue;
                    chara.getMaxAbilityInfo()[3] = chara.getMaxAbilityInfo()[3] + rollVaue;
                    chara.getAbilityInfo()[3] = chara.getAbilityInfo()[3] + rollVaue;
                    messageUI.getResult().setDone(false);
                    showMessageUi(new string[] { "你的力量下降：" + rollVaue + "点,神志上升：" + rollVaue + "点" }, null);
                    phase = 6;
                }
            }else if(phase == 6 && messageUI.getResult().getDone())
            {
                roomContraller.findRoomByXYZ(chara.getCurrentRoom()).removeChara(chara);
                this.roomContraller.setCharaInMiniMap(chara.getCurrentRoom(), chara, false);
                RoomInterface ri = roomContraller.getRandomDownRoom();
                ri.setChara(chara);
                chara.setCurrentRoom(ri.getXYZ());
                this.roomContraller.setCharaInMiniMap(ri.getXYZ(), chara, true);
                camCtrl.setTargetPos(ri.getXYZ(), RoomConstant.ROOM_Y_DOWN, true);
                if(ri.isLock())
                {
                    ri.setLock(false);
                }
                fallRoomExecuted = true;

            }
        }
    }

    public List<EventInfo> getStayEventList()
    {
        return this.stayEventList;
    }
}
