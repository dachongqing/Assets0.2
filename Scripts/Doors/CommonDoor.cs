using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class CommonDoor : MonoBehaviour ,DoorInterface{

    public Sprite DoorSprite;

    private int[] nextRoomXYZ;
    private RoomInterface ri;
    private bool showFlag;

   

    public GameObject minOperationDoor;

    private GameObject playerGameObject;

    private bool leavlFlag = false;

    public  void doMiniOperation()
    {
        doClickDoor();
    }

    public virtual RoomContraller getRooController()
    {
        return null;
    }

    public virtual RoundController getRoundController()
    {
        return null;
    }

    public virtual EventController getEventController()
    {
        return null;
    }

    public virtual CameraCtrl getCameraCtrl()
    {
        return null;
    }

    public virtual MessageUI getMSGUI() {
        return null;
    }

    private void doClickDoor()
    {
        if (this.getShowFlag())
        {
            RoomInterface nextRoom = this.getRooController().findRoomByXYZ(getNextRoomXYZ());
            if (nextRoom.checkOpen(getRoundController().getPlayerChara()))
            {
                //检查玩家的行动力
                bool opened = openDoor(getRoundController().getCurrentRoundChar());

                //这里有bug，玩家应该是只能点击 所在房间的几个门，其余房间的门都是不能点击的.
                //生成门时，门启用，但加锁；玩家进入房间，门解锁可点击；玩家离开房间，门加锁不可点击

                if (opened)
                {

                    if (getRoundController().getCurrentRoundChar().isPlayer())
                    {
                        getEventController().excuteLeaveRoomEvent(this, getRoom(), getRoundController().getCurrentRoundChar());
                    }
                    else
                    {
                        bool result = getEventController().excuteLeaveRoomEvent(getRoom(), getRoundController().getCurrentRoundChar());

                        if (result == true)
                        {
                            //离开门成功
                            Debug.Log("离开房间成功");
                            //进入下一个房间


                            //摄像机移动到下一个房间坐标
                            getCameraCtrl().setTargetPos(getNextRoomXYZ());

                            //当前人物坐标移动到下一个房间
                            getRoundController().getCurrentRoundChar().setCurrentRoom(getNextRoomXYZ());

                            //触发进门事件
                            getEventController().excuteEnterRoomEvent(nextRoom, getRoundController().getCurrentRoundChar());  //暂时禁用 运行时有异常

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
            else
            {
                getMSGUI().showMessage("房间被锁的。");
            }


        }
    }

    public  void playerOpenDoorResult(bool result) {

        if (result == true)
        {
            //离开门成功
            Debug.Log("离开房间成功");
            //进入下一个房间

            //计算朝什么方向移动
            int[] Nxyz = getNextRoomXYZ();
            int[] Cxyz = this.getRoom().getXYZ();
            string nextDoor = "";
            if (Nxyz[0] - Cxyz[0] == 0)
            {
                if (Nxyz[1] - Cxyz[1] == 1)
                {
                    // up move
                    nextDoor = "D";
                }
                else
                {
                    //donw move
                    nextDoor = "U";
                }
            }
            else
            {
                if (Nxyz[0] - Cxyz[0] == -1)
                {
                    // left move
                    nextDoor = "L";
                }
                else
                {
                    //right move
                    nextDoor = "R";
                }
            }

            RoomInterface nextRoom = getRooController().findRoomByXYZ(getNextRoomXYZ());

            //摄像机移动到下一个房间坐标

            getCameraCtrl().setTargetPos(getNextRoomXYZ());

            //当前人物坐标移动到下一个房间
            getRoundController().getCurrentRoundChar().setCurrentRoom(nextRoom, nextDoor);
            nextRoom.setChara(this.getRoundController().getCurrentRoundChar());
            this.getRooController().setCharaInMiniMap(getNextRoomXYZ(), this.getRoundController().getCurrentRoundChar(), true);
            this.getRoom().removeChara(this.getRoundController().getCurrentRoundChar());
            this.getRooController().setCharaInMiniMap(this.getRoom().getXYZ(), this.getRoundController().getCurrentRoundChar(), false);
            //触发进门事件
            getEventController().excuteEnterRoomEvent(nextRoom, getRoundController().getCurrentRoundChar());  //暂时禁用 运行时有异常

        }
        else
        {
            //离开失败
            Debug.Log("离开房间失败");
            //FindObjectOfType<MessageUI>().ShowMessge("离开房间失败 ", 0);
        }
    }

    public virtual int getOpenCost() {
        return 0;
    }

    public int[] getNextRoomXYZ()
    {
        return nextRoomXYZ;
    }

    public RoomInterface getRoom()
    {
        return ri;
    }

    public bool getShowFlag()
    {
        return showFlag;
    }

    public bool openDoor(Character chara)
    {
        //可以自定义不同的门，消耗不同的行动力
        if (chara.getActionPoint() - this.getOpenCost() >= 0)
        {
            chara.updateActionPoint(chara.getActionPoint() - this.getOpenCost());
            return true;
        }
        else
        {

            getMSGUI().ShowMessge("玩家行动点数不足", 0);
            return false;
        }
    }

   

    public void setNextRoomXYZ(int[] xyz)
    {
        this.nextRoomXYZ = xyz;
    }

    public void setRoom(RoomInterface room)
    {
        this.ri = room;
    }

    public void setShowFlag(bool showFlag)
    {
        //该门状态为显示
        this.showFlag = showFlag;
        //替换门的图片为门
        SpriteRenderer sPrRe = GetComponent<SpriteRenderer>();
        sPrRe.sprite = DoorSprite;
    }

   


    // Use this for initialization
    void Start () {
        
    }

    private void FixedUpdate()
    {
        if (leavlFlag)
        {
            float dis = Vector3.Distance(this.transform.position, this.playerGameObject.transform.position);
            // Debug.Log("we are dis is " + dis);
            if (dis > 1)
            {
                minOperationDoor.SetActive(false);
                leavlFlag = false;
            }
        }
    }



    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log(coll.gameObject.name);
        if (coll.gameObject.name == "Player")
        {
            minOperationDoor.SetActive(true);
            leavlFlag = false;
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        Debug.Log(coll.gameObject.name);
        if (coll.gameObject.name == "Player")
        {
            //minOperationDoor.SetActive(false);
            this.playerGameObject = coll.gameObject;
            leavlFlag = true;
        }

    }
}
