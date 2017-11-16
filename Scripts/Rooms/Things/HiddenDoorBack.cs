using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenDoorBack : CommonThing
{
    private RoomContraller roomContraller;
    private RoundController roundController;

    private CameraCtrl camCtrl;

  

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log(coll.gameObject.name);
        doClick();

    }

    /**
    void OnMouseDown()

    {

        if (!SystemUtil.IsTouchedUI())
        {

            doClick();

        }


    }**/

    public override void doClick()
    {



        // 载入上楼图片...

       
        Character chara = roundController.getCurrentRoundChar();


        chara.setCurrentRoom(chara.getCurrentRoom());

        if (chara.getCurrentRoom()[2] == RoomConstant.ROOM_Z_GROUND)
        {        
            camCtrl.setTargetPos(chara.getCurrentRoom(), RoomConstant.ROOM_Y_GROUND,true);
        }
        else if (chara.getCurrentRoom()[2] == RoomConstant.ROOM_Z_UP)
        {
            
            camCtrl.setTargetPos(chara.getCurrentRoom(), RoomConstant.ROOM_Y_UP, true);
        }
        else
        {
            
            camCtrl.setTargetPos(chara.getCurrentRoom(), RoomConstant.ROOM_Y_DOWN, true);
        }
       


        // 载入上楼图片结束。。。
    }



    // Use this for initialization
    void Start()
    {
        roundController = FindObjectOfType<RoundController>();

        roomContraller = FindObjectOfType<RoomContraller>();
        
        camCtrl = FindObjectOfType<CameraCtrl>();
        this.setThingCode(ThingConstant.HIDDEN_DOOR_BACK_01_CODE);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
