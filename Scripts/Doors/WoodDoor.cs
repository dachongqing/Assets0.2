using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WoodDoor : MonoBehaviour, DoorInterface
{
	public Sprite DoorSprite;

	[Tooltip ("开门所需消耗点数")][Range (0, 10)]public int OpenCost = 1;

	[SerializeField]private int[] nextRoomXYZ;

	[SerializeField]bool showFlag;

	private RoomInterface ri;

	EventController eventController;

	RoundController roundController;

	RoomContraller roomContraller;

	CameraCtrl camCtrl;

	public void setNextRoomXYZ (int[] xyz)
	{
		this.nextRoomXYZ = xyz;
	}

	public void setRoom (RoomInterface room)
	{
		this.ri = room;
	}

	public void setShowFlag (bool showFlag)
	{
		//该门状态为显示
		this.showFlag = showFlag;
		//替换门的图片为门
		SpriteRenderer sPrRe = GetComponent<SpriteRenderer> ();
		sPrRe.sprite = DoorSprite;
	}

	public int[] getNextRoomXYZ ()
	{
		return nextRoomXYZ;
	}

	public RoomInterface getRoom ()
	{
		return ri;
	}

	bool DoorInterface.getShowFlag ()
	{
		return showFlag;
	}

	public bool openDoor (Character chara)
	{
		//可以自定义不同的门，消耗不同的行动力
		if (chara.getActionPoint () - OpenCost >= 0) {
			chara.updateActionPoint (chara.getActionPoint () - OpenCost);
			return true;
		} else {
			MessageUI msgUI = FindObjectOfType<MessageUI> ();
			msgUI.ShowMessge ("玩家行动点数不足",0);
			return false;
		}

	}

	// Use this for initialization
	void Start ()
	{
		eventController = FindObjectOfType<EventController> ();

		roundController = FindObjectOfType<RoundController> ();

		roomContraller = FindObjectOfType<RoomContraller> ();

		camCtrl = FindObjectOfType<CameraCtrl> ();


	}

	//鼠标进入门区域
	void OnMouseEnter ()
	{
		//仅对启用的门有效
		if (showFlag) {
			//放大效果
			//this.transform.localScale = new Vector3 (1.2f, 1.2f, 1.2f);
		}
	}

    private void doClickDoor() {
        if (showFlag)
        {

            //			Debug.Log ("WoodDoor.cs OnMouseDown");
            //检查玩家的行动力
            bool opened = openDoor(roundController.getCurrentRoundChar());

            //这里有bug，玩家应该是只能点击 所在房间的几个门，其余房间的门都是不能点击的.
            //生成门时，门启用，但加锁；玩家进入房间，门解锁可点击；玩家离开房间，门加锁不可点击

            if (opened)
            {

                if (roundController.getCurrentRoundChar().isPlayer())
                {
                    eventController.excuteLeaveRoomEvent(this, getRoom(), roundController.getCurrentRoundChar());
                }
                else
                {
                    bool result = eventController.excuteLeaveRoomEvent(getRoom(), roundController.getCurrentRoundChar());

                    if (result == true)
                    {
                        //离开门成功
                        Debug.Log("离开房间成功");
                        //进入下一个房间
                        RoomInterface nextRoom = roomContraller.findRoomByXYZ(getNextRoomXYZ());

                        //摄像机移动到下一个房间坐标
                        camCtrl.setTargetPos(getNextRoomXYZ());

                        //当前人物坐标移动到下一个房间
                        roundController.getCurrentRoundChar().setCurrentRoom(getNextRoomXYZ());

                        //触发进门事件
                        //eventController.excuteEnterRoomEvent (nextRoom, roundController.getCurrentRoundChar ());  暂时禁用 运行时有异常

                    }
                    else
                    {
                        //离开失败
                        Debug.Log("离开房间失败");
                        //  FindObjectOfType<MessageUI> ().ShowMessge ("离开房间失败 ",0);
                    }
                }

            }

        }
    }

    

    void OnMouseDown ()
	{
        if (!SystemUtil.IsTouchedUI())
        {

            doClickDoor();
        }
        else
        {
            Debug.Log("click ui");
        }
    }

    public void playerOpenDoorResult(bool result) {
        if (result == true)
        {
            //离开门成功
            Debug.Log("离开房间成功");
            //进入下一个房间
            RoomInterface nextRoom = roomContraller.findRoomByXYZ(getNextRoomXYZ());

            //摄像机移动到下一个房间坐标
            camCtrl.setTargetPos(getNextRoomXYZ());

            //当前人物坐标移动到下一个房间
            roundController.getCurrentRoundChar().setCurrentRoom(getNextRoomXYZ());
            nextRoom.setChara(this.roundController.getCurrentRoundChar());
            this.roomContraller.findMiniRoomByXYZ(getNextRoomXYZ()).setPenable(this.roundController.getCurrentRoundChar().getName(), true);
            this.getRoom().removeChara(this.roundController.getCurrentRoundChar());
            this.roomContraller.findMiniRoomByXYZ(this.getRoom().getXYZ()).setPenable(this.roundController.getCurrentRoundChar().getName(), false);
            //触发进门事件
            //eventController.excuteEnterRoomEvent (nextRoom, roundController.getCurrentRoundChar ());  暂时禁用 运行时有异常

        }
        else
        {
            //离开失败
            Debug.Log("离开房间失败");
            //FindObjectOfType<MessageUI>().ShowMessge("离开房间失败 ", 0);
        }
    }

	//鼠标离开门区域
	void OnMouseExit ()
	{
		//缩小效果
		//this.transform.localScale = new Vector3 (1f, 1f, 1f);
	}
		
}
