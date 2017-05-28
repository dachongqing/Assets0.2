using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{

	private EventConstant constant;

	public bool excuteLeaveRoomEvent (RoomInterface ri, Character chara)
	{
		//这个房间有没有离开事件
		EventInterface eventI = ri.getRoomEvent (EventConstant.LEAVE_EVENT);

		//不为空有事件
		if (eventI != null) {
			if (chara.isPlayer ()) {
				Debug.Log ("有离开事件");
				//show ui 
				string selectCode = showMessageUi (eventI.getEventBeginInfo (), eventI.getSelectItem ());

				EventResult result = eventI.excute (chara, selectCode);

				showMessageUi (eventI.getEventEndInfo (result.getResultCode ()), null);

				return result.getStatus ();
			} else {
				EventResult result = eventI.excute (chara, null);
                 
				return result.getStatus ();
			}

		} else {
			//为空没有事件
			Debug.Log ("没有离开事件");
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

	public string showMessageUi (string message, Dictionary<string,string> selectItem)
	{
		Debug.Log (message);
		return null;
	}
}
