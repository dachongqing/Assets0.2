using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpStairOuter : MonoBehaviour, IPointerClickHandler
{


    private RoomContraller roomContraller;
    private RoundController roundController;

    private CameraCtrl camCtrl;

    public void OnPointerClick(PointerEventData eventData)
    {



        // 载入上楼图片...
        RoomInterface upStairBackRoom = roomContraller.findRoomByRoomType(RoomConstant.ROOM_TYPE_UPSTAIR_BACK_ROOM);
        Character chara = roundController.getCurrentRoundChar();
        roomContraller.findRoomByXYZ(chara.getCurrentRoom()).removeChara(chara);
        upStairBackRoom.setChara(chara);
        chara.setCurrentRoom(upStairBackRoom.getXYZ());
        camCtrl.setTargetPos(upStairBackRoom.getXYZ(), RoomConstant.ROOM_Y_GROUND);
        // 载入上楼图片结束。。。
    }

    // Use this for initialization
    void Start()
    {
        roundController = FindObjectOfType<RoundController>();

        roomContraller = FindObjectOfType<RoomContraller>();

        camCtrl = FindObjectOfType<CameraCtrl>();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
