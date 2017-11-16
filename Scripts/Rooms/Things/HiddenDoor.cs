using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenDoor : CommonThing
{


    private RoomContraller roomContraller;
    private RoundController roundController;

    private CameraCtrl camCtrl;

    private H_hiddenRoom HiddenRoom01;

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
      
        H_hiddenRoom hiddenRoom = HiddenRoom01.GetComponent<H_hiddenRoom>();
        Character chara = roundController.getCurrentRoundChar();

        chara.setCharaInHiddenRoom(hiddenRoom); 


        camCtrl.setHiddenTargetPos(hiddenRoom.getXYZ(), RoomConstant.ROOM_X_X);
      

        // 载入上楼图片结束。。。
    }



    // Use this for initialization
    void Start()
    {
        roundController = FindObjectOfType<RoundController>();

        roomContraller = FindObjectOfType<RoomContraller>();
        HiddenRoom01 = FindObjectOfType<H_hiddenRoom>();
        camCtrl = FindObjectOfType<CameraCtrl>();
        this.setThingCode(ThingConstant.HIDDEN_DOOR_01_CODE);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
