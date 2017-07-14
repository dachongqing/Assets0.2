using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownStairOuter : CommonThing
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

    public override void doClick()
    {



        // 载入上楼图片...
        RoomInterface upStairBackRoom = roomContraller.findRoomByRoomType(RoomConstant.ROOM_TYPE_DOWNSTAIR);
        Character chara = roundController.getCurrentRoundChar();
        roomContraller.findRoomByXYZ(chara.getCurrentRoom()).removeChara(chara);
        this.roomContraller.setCharaInMiniMap(chara.getCurrentRoom(),this.roundController.getCurrentRoundChar(), false);
        upStairBackRoom.setChara(chara);
        chara.setCurrentRoom(upStairBackRoom.getXYZ());
        camCtrl.setTargetPos(upStairBackRoom.getXYZ(), RoomConstant.ROOM_Y_GROUND, true);
        this.roomContraller.setCharaInMiniMap(upStairBackRoom.getXYZ(),this.roundController.getCurrentRoundChar(), true);
        
        // 载入上楼图片结束。。。
    }

    // Use this for initialization
    void Start()
    {
        roundController = FindObjectOfType<RoundController>();

        roomContraller = FindObjectOfType<RoomContraller>();

        camCtrl = FindObjectOfType<CameraCtrl>();
        this.setThingCode(ThingConstant.DOWNSTAIR_OUTER_CODE);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
