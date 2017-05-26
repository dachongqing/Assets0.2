using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface RoomInterface 
{


    //获取当前房名
    string getRoomName();

    //获取当前房间坐标
    int[] getXYZ();

    void setXYZ(int[] xyz);

    //获取当前房间类型
    string getRoomType();
       
    void northDoorEnable();

    void southDoorEnable();

    void westDoorEnable();

    void eastDoorEnable();

    //获取当前房间人物列表

    //获取当前房间的事件列表：支持一个房间拥有多个事件
    
    EventInterface getRoomEvent(string eventType);

    void setRoomEvent(EventInterface ei);

    //获取当前房间的隐藏物品列表

    //检查房间是否开启剧本

    //触发房间事件

    //


    GameObject getNorthDoor();
	GameObject getSouthDoor();
	GameObject getEastDoor();
	GameObject getWestDoor();

}
