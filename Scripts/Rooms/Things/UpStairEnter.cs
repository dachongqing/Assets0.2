using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpStairEnter : MonoBehaviour, Thing


{


    private RoomContraller roomContraller;
    private RoundController roundController;

    private CameraCtrl camCtrl;

    void OnMouseDown()

    {

        if (!SystemUtil.IsTouchedUI())
        {

            doClick();

        }


    }

    public void doClick()
    {



        // 载入上楼图片...
        RoomInterface upStairRoom = roomContraller.findRoomByRoomType(RoomConstant.ROOM_TYPE_UPSTAIR_BACK);
        Character chara =  roundController.getCurrentRoundChar();
        roomContraller.findRoomByXYZ(chara.getCurrentRoom()).removeChara(chara);
        upStairRoom.setChara(chara);
        Debug.Log("upStairRoom.getXYZ() " + upStairRoom.getXYZ()[0]);
        chara.setCurrentRoom(upStairRoom.getXYZ());
        camCtrl.setTargetPos(upStairRoom.getXYZ(), RoomConstant.ROOM_Y_UP,true);
        // 载入上楼图片结束。。。
    }

    // Use this for initialization
    void Start () {
        roundController = FindObjectOfType<RoundController>();

        roomContraller = FindObjectOfType<RoomContraller>();

        camCtrl = FindObjectOfType<CameraCtrl>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
