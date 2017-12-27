using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WoodDoor : CommonDoor
{

    EventController eventController;

    RoundController roundController;

    RoomContraller roomContraller;

    CameraCtrl camCtrl;

    MessageUI msgUI;

    private int OpenCost = 1;

    public override int getOpenCost() {
        return this.OpenCost;
    }

    public override MessageUI getMSGUI()
    {
       return  msgUI;
    }

  

    public override RoomContraller getRooController()
    {
        return roomContraller;
    }

    public override EventController getEventController()
    {
        return eventController;
    }

    public override RoundController getRoundController()
    {
        return roundController;
    }

    public override CameraCtrl getCameraCtrl()
    {
        return camCtrl;
    }
    
    // Use this for initialization
    void Start ()
	{
        eventController = FindObjectOfType<EventController>();

        roundController = FindObjectOfType<RoundController>();

        roomContraller = FindObjectOfType<RoomContraller>();

        camCtrl = FindObjectOfType<CameraCtrl>();

        msgUI = FindObjectOfType<MessageUI>();

    }

	//鼠标进入门区域
	void OnMouseEnter ()
	{
		
			//放大效果
			//this.transform.localScale = new Vector3 (1.2f, 1.2f, 1.2f);
		
	}
    
   
	//鼠标离开门区域
	void OnMouseExit ()
	{
		//缩小效果
		//this.transform.localScale = new Vector3 (1f, 1f, 1f);
	}
		
}
