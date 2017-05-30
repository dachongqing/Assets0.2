using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class EventController : MonoBehaviour
{

	private EventConstant constant;

	private DoorInterface door;

	private RoomInterface ri;

	private Character chara;

	private EventInterface eventI;

	public void excuteLeaveRoomEvent (DoorInterface door, RoomInterface ri, Character chara)
	{
		//这个房间有没有离开事件
		this.door = door;
		this.chara = chara;
		this.ri = ri;

		 eventI = ri.getRoomEvent (EventConstant.LEAVE_EVENT);

		//不为空有事件
		if (eventI != null) {
			if (chara.isPlayer ()) {
				Debug.Log ("有离开事件");
				//show ui 
				 showMessageUi (eventI.getEventBeginInfo(), eventI.getSelectItem());


				Debug.Log("wait ui end");
				//showMessageUi (eventI.getEventEndInfo (result.getResultCode ()), null);

				//return result.getStatus ();
			} else {
				EventResult result = eventI.excute (chara, null);
				this.door.callback(result.getStatus());
				//return result.getStatus ();
			}

		} else {
			//为空没有事件
			Debug.Log ("没有离开事件");
			//return true;
			this.door.callback(true);
		}
	}

	public void uiCallBack(int selectCode) {
		EventResult result = eventI.excute(chara, "");

		this.door.callback (result.getStatus());

	}

	public void rollCallBack() {
		uiManager.showRollTest(this);
	}

	public bool excuteStayRoomEvent (RoomInterface ri, Character chara)
	{
		return false;
	}

	public bool excuteEnterRoomEvent (RoomInterface ri, Character chara)
	{
		return false;
	}

	public RollDiceUIManager uiManager;

	public MessageUI messageUI;


	public string showMessageUi (string message, Dictionary<string,string> selectItem)
	{
		Debug.Log (message);
		//弹出roll点界面
		//		UIrollPlane.SetActive (true);
		messageUI.ShowMessge(message,1,this);

		//eventNotice.WaitOne ();
		//关闭按钮
	//	rollCloseGO.SetActive(true);
		//roll点按钮启用
	//	rollStartGO.SetActive(true);

		return null;
	}

	void Start() {
		uiManager = FindObjectOfType<RollDiceUIManager>();
	}
}
