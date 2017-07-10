using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownStairEnter : MonoBehaviour {

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
        RoomInterface upStairRoom = roomContraller.findRoomByRoomType(RoomConstant.ROOM_TYPE_DOWNSTAIR_BACK);
        Character chara = roundController.getCurrentRoundChar();
        roomContraller.findRoomByXYZ(chara.getCurrentRoom()).removeChara(chara);
        this.roomContraller.setCharaInMiniMap(chara.getCurrentRoom(),this.roundController.getCurrentRoundChar(), false);
        upStairRoom.setChara(chara);
        Debug.Log("upStairRoom.getXYZ() " + upStairRoom.getXYZ()[0]);
        chara.setCurrentRoom(upStairRoom.getXYZ());
        camCtrl.setTargetPos(upStairRoom.getXYZ(), RoomConstant.ROOM_Y_DOWN, true);

        this.roomContraller.setCharaInMiniMap(upStairRoom.getXYZ(),this.roundController.getCurrentRoundChar(), true);
     
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
    void Update()
    {

    }
}
