﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoomContraller : MonoBehaviour
{

    private Queue<String> groundRoomType = new Queue<String>();

    private Queue<String> upRoomType = new Queue<String>();

    private Queue<String> downRoomType = new Queue<String>();

    private Dictionary<int[], RoomInterface> groundRoomList = new Dictionary<int[], RoomInterface>();

    private Dictionary<int[], RoomInterface> upRoomList = new Dictionary<int[], RoomInterface>();

    private Dictionary<int[], RoomInterface> downRoomList = new Dictionary<int[], RoomInterface>();

    private Dictionary<int[], RoomInterface> allRoomList = new Dictionary<int[], RoomInterface>();

    public Dictionary<int[], MinMapRoom> miniRoomList = new Dictionary<int[], MinMapRoom>();

    List<int[]> keys = new List<int[]>();

    private System.Random random = new System.Random();

    private List<EventInterface> events = new List<EventInterface>();

    private RoomConstant roomConstant;

    //这个队列的长度，限制了房间最大数量
    public RoomContraller()
    {
        genRoomType();
        genRoomEvent();
    }

    private void genRoomType()
    {
        //这个队列的长度，限制了房间最大数量
        groundRoomType.Enqueue("LobbyRoom");
        groundRoomType.Enqueue("UpStairEnterRoom");
        groundRoomType.Enqueue("DownStairEnterRoom");
        groundRoomType.Enqueue("LobbyRoom");
        groundRoomType.Enqueue("LobbyRoom");
        groundRoomType.Enqueue("LobbyRoom");
        groundRoomType.Enqueue("LobbyRoom");
        groundRoomType.Enqueue(RoomConstant.ROOM_TYPE_HOSPITAIL_SURGERY);
        groundRoomType.Enqueue("LobbyRoom");
        groundRoomType.Enqueue("LobbyRoom");
        groundRoomType.Enqueue("LobbyRoom");
        groundRoomType.Enqueue("LobbyRoom");
        groundRoomType.Enqueue(RoomConstant.ROOM_TYPE_HOSPITAIL_SECURITY);
        groundRoomType.Enqueue("LobbyRoom");
        groundRoomType.Enqueue("LobbyRoom");
        groundRoomType.Enqueue("LobbyRoom");
        groundRoomType.Enqueue(RoomConstant.ROOM_TYPE_HOSPITAIL_DRAG);
        groundRoomType.Enqueue("LobbyRoom");
        groundRoomType.Enqueue("LobbyRoom");
        groundRoomType.Enqueue("LobbyRoom");
        groundRoomType.Enqueue("LobbyRoom");

        //这个队列的长度，限制了房间最大数量
        upRoomType.Enqueue("UpStairOuterRoom");
        upRoomType.Enqueue("LobbyRoom");
        upRoomType.Enqueue("LobbyRoom");
        upRoomType.Enqueue("LobbyRoom");
        upRoomType.Enqueue("LobbyRoom");
        upRoomType.Enqueue("LobbyRoom");
        upRoomType.Enqueue("LobbyRoom");
        upRoomType.Enqueue("LobbyRoom");
        upRoomType.Enqueue(RoomConstant.ROOM_TYPE_HOSPITAIL_MINITOR);
        upRoomType.Enqueue("LobbyRoom");
        upRoomType.Enqueue("LobbyRoom");
        upRoomType.Enqueue("LobbyRoom");
        upRoomType.Enqueue("LobbyRoom");
        upRoomType.Enqueue(RoomConstant.ROOM_TYPE_HOSPITAIL_DEAN);
        upRoomType.Enqueue("LobbyRoom");
        upRoomType.Enqueue("LobbyRoom");
        upRoomType.Enqueue("LobbyRoom");
        upRoomType.Enqueue("LobbyRoom");
        upRoomType.Enqueue("LobbyRoom");
        upRoomType.Enqueue("LobbyRoom");
        upRoomType.Enqueue("LobbyRoom");

        //这个队列的长度，限制了房间最大数量
        downRoomType.Enqueue("DownStairOuterRoom");
        downRoomType.Enqueue("LobbyRoom");
        downRoomType.Enqueue("LobbyRoom");
        downRoomType.Enqueue("LobbyRoom");
        downRoomType.Enqueue("LobbyRoom");
        downRoomType.Enqueue(RoomConstant.ROOM_TYPE_HOSPITAIL_TRI_OPERATION);
        downRoomType.Enqueue("LobbyRoom");
        downRoomType.Enqueue("LobbyRoom");
        downRoomType.Enqueue("LobbyRoom");
        downRoomType.Enqueue("LobbyRoom");
        downRoomType.Enqueue(RoomConstant.ROOM_TYPE_HOSPITAIL_MORGUE);
        downRoomType.Enqueue("LobbyRoom");
        downRoomType.Enqueue("LobbyRoom");
        downRoomType.Enqueue("LobbyRoom");
        downRoomType.Enqueue("LobbyRoom");
        downRoomType.Enqueue("LobbyRoom");
        downRoomType.Enqueue(RoomConstant.ROOM_TYPE_HOSPITAIL_STORE);
        downRoomType.Enqueue("LobbyRoom");
        downRoomType.Enqueue("LobbyRoom");
        downRoomType.Enqueue("LobbyRoom");
        downRoomType.Enqueue("BookRoom");
    }

    private void genRoomEvent()
    {
        EventInterface ei = new SpeedLeveaRoomEvent();
        events.Add(ei);

    }

    private EventInterface getRandomEvent(String banEventType)
    {

        EventInterface et = events[random.Next(events.Count)];
        if (et.getEventType() == banEventType)
        {
            return getRandomEvent(banEventType);
        }
        else
        {
            return et;
        }


    }

    private void setRoomEvents(RoomInterface room)
    {

        //只有30%的概率房间会生成事件
        if (6 == random.Next(0, 3))
        {

            //判定房间是处于什么位置 楼上 地面 楼下， 不能出现 有冲突的事件， 比如楼下不能出现掉落事件
            if (room.getXYZ()[2] == RoomConstant.ROOM_Z_GROUND)
            {
                //对于地面事件 所有事件都可以发生
                room.setRoomEvent(getRandomEvent(null));

                //  if () {
                //  }
            }
            else if (room.getXYZ()[2] == RoomConstant.ROOM_Z_UP)
            {
                //对于楼上事件 
            }
            else
            {
                //地下事件
            }
        }

    }

    private void setRoomStory(RoomInterface room)
    {
        if (room.getRoomType() == RoomConstant.ROOM_TYPE_BOOK)
        {
            StoryInterface storyScript = new RaceStory();
            room.setRoomStory(storyScript);
        }
    }

    private string roomType;
    public GameObject genRoom(int[] xyz, int[] door)
    {
        //房间Prefab所在文件夹路径
        if (xyz[2] == RoomConstant.ROOM_Z_GROUND) {
            roomType = groundRoomType.Dequeue();

        } else if (xyz[2] == RoomConstant.ROOM_Z_DOWN) {
            roomType = this.downRoomType.Dequeue();
        }
        else if (xyz[2] == RoomConstant.ROOM_Z_UP)
        {
            roomType = this.upRoomType.Dequeue();
        }
        string url = getPrefabUrlByRoomType(roomType);

        //仅用Resources.Load会永久修改原形Prefab。应该用Instatiate,操作修改原形的克隆体
        GameObject room = Instantiate(Resources.Load(url)) as GameObject;

        if (room == null)
        {
            Debug.Log("cant find room Prefab !!!");
        }
        else
        {
            RoomInterface ri;

            if (roomType == RoomConstant.ROOM_TYPE_UPSTAIR || roomType == RoomConstant.ROOM_TYPE_UPSTAIR_BACK
                || roomType == RoomConstant.ROOM_TYPE_DOWNSTAIR || roomType == RoomConstant.ROOM_TYPE_DOWNSTAIR_BACK)
            {
                ri = room.GetComponent(System.Type.GetType(RoomConstant.ROOM_TYPE_COMMON)) as RoomInterface;
            }
            else {
                ri = room.GetComponent(System.Type.GetType(roomType)) as RoomInterface;
            }
            if (ri == null) {
                Debug.Log("cant find type:  " + roomType);
            }
            ri.setRoomType(roomType);
            ri.setXYZ(xyz);

            //for test
            if (xyz[2] == RoomConstant.ROOM_Z_GROUND)
            {
                ri.setRoomName("地面");

            }
            else if (xyz[2] == RoomConstant.ROOM_Z_DOWN)
            {
                ri.setRoomName("地下");
            }
            else if (xyz[2] == RoomConstant.ROOM_Z_UP)
            {
                ri.setRoomName("楼上");
            }
           

            //随机生成事件

            setRoomEvents(ri);

            setRoomStory(ri);

            setRoomDoor(door,ri,roomType, xyz);


            

            if (xyz[2] == RoomConstant.ROOM_Z_GROUND)
            {
                groundRoomList.Add(ri.getXYZ(), ri);

            }
            else if (xyz[2] == RoomConstant.ROOM_Z_UP)
            {
                upRoomList.Add(ri.getXYZ(), ri);
            }
            else
            {
                downRoomList.Add(ri.getXYZ(), ri);
            }
            allRoomList.Add(ri.getXYZ(), ri);
            keys.Add(ri.getXYZ());
           
        }

        return room;
    }

    private void setRoomDoor(int[] door,RoomInterface ri, string roomType,int[] xyz)
    {
        //根据这房间门的数据，生成对应的门
        if (door[0] == 1)
        {
            //门启用
            ri.northDoorEnable();
            //门属于这个房间
            GameObject doorGo = ri.getNorthDoor();
            doorGo.GetComponent<DoorInterface>().setRoom(ri);
            //门外有相邻房间的坐标为
            //				错误代码int[] nextRoomXYZ = xyz;
            //				错误代码nextRoomXYZ [2] += 1原因：一维数组是引用类型,+1会导致xyz[]的修改;
            //				体现为  房间的map坐标!=房间的getXYZ

            //修正为
            int[] nextRoomXYZ = new int[3];
            nextRoomXYZ[0] = xyz[0];
            nextRoomXYZ[1] = xyz[1];
            nextRoomXYZ[2] = xyz[2];
            nextRoomXYZ[1] += 1;

            doorGo.GetComponent<DoorInterface>().setNextRoomXYZ(nextRoomXYZ);

        }
        if (door[1] == 1)
        {
            //门启用
            ri.southDoorEnable();
            //门属于这个房间
            GameObject doorGo = ri.getSouthDoor();
            doorGo.GetComponent<DoorInterface>().setRoom(ri);
            //门外有相邻房间的坐标为
            int[] nextRoomXYZ = new int[3];
            nextRoomXYZ[0] = xyz[0];
            nextRoomXYZ[1] = xyz[1];
            nextRoomXYZ[2] = xyz[2];
            nextRoomXYZ[1] -= 1;

            doorGo.GetComponent<DoorInterface>().setNextRoomXYZ(nextRoomXYZ);
        }
        if (door[2] == 1)
        {
            //门启用
            ri.westDoorEnable();
            //门属于这个房间
            GameObject doorGo = ri.getWestDoor();
            doorGo.GetComponent<DoorInterface>().setRoom(ri);
            //门外有相邻房间的坐标为
            int[] nextRoomXYZ = new int[3];
            nextRoomXYZ[0] = xyz[0];
            nextRoomXYZ[1] = xyz[1];
            nextRoomXYZ[2] = xyz[2];
            nextRoomXYZ[0] -= 1;

            doorGo.GetComponent<DoorInterface>().setNextRoomXYZ(nextRoomXYZ);
        }
        if (door[3] == 1)
        {
            //门启用
            ri.eastDoorEnable();
            //门属于这个房间
            GameObject doorGo = ri.getEastDoor();
            doorGo.GetComponent<DoorInterface>().setRoom(ri);
            //门外有相邻房间的坐标为
            int[] nextRoomXYZ = new int[3];
            nextRoomXYZ[0] = xyz[0];
            nextRoomXYZ[1] = xyz[1];
            nextRoomXYZ[2] = xyz[2];
            nextRoomXYZ[0] += 1;

            doorGo.GetComponent<DoorInterface>().setNextRoomXYZ(nextRoomXYZ);
        }
    }

    private string getPrefabUrlByRoomType(string roomType)
    {
        if (roomType.StartsWith("H_")) {
            return "Prefabs/SPRoom/Hospital/" + roomType;
        }
        return "Prefabs/" + roomType;
    }

    public RoomInterface findRoomByXYZ(int[] xyz)
    {
        if (xyz[2] == RoomConstant.ROOM_Z_GROUND)
        {

            foreach (int[] key in groundRoomList.Keys)
            {
                if (key[0] == xyz[0] && key[1] == xyz[1] && key[2] == xyz[2])
                {
                    return groundRoomList[key];
                }
            }

        }
        else if (xyz[2] == RoomConstant.ROOM_Z_UP)
        {
            foreach (int[] key in upRoomList.Keys)
            {
                if (key[0] == xyz[0] && key[1] == xyz[1] && key[2] == xyz[2])
                {
                    return upRoomList[key];
                }
            }
        }
        else
        {
            foreach (int[] key in downRoomList.Keys)
            {
                if (key[0] == xyz[0] && key[1] == xyz[1] && key[2] == xyz[2])
                {
                    return downRoomList[key];
                }
            }
        }
        return null;
    }

    public RoomInterface findRoomByRoomType(string roomType)
    {

        foreach (int[] key in allRoomList.Keys)
        {
            if (allRoomList[key].getRoomType() == roomType)
            {
                return allRoomList[key];
            }
        }
        
        return null;
    }

    public Dictionary<int[], RoomInterface> getAllRoom(int z)
    {



        if (z == RoomConstant.ROOM_Z_GROUND)
        {

            return groundRoomList;

        }
        else if (z == RoomConstant.ROOM_Z_UP)
        {

            return upRoomList;

        }
        else
        {

            return downRoomList;

        }
    }

    public Dictionary<int[], RoomInterface> getAllRoom() {
        return this.allRoomList;
    }

    public RoomInterface getRandomRoom() {

        keys = FunctionUnity<int[]>.orderList(keys);
        return this.allRoomList[keys[0]];
    }

    public void addMiniRoomList(int[] xyz,MinMapRoom minR) {
        this.miniRoomList.Add(xyz,minR);
    }

    public MinMapRoom findMiniRoomByXYZ(int[] xyz) {
        foreach (int[] key in miniRoomList.Keys)
        {
            if (key[0] == xyz[0] && key[1] == xyz[1] && key[2] == xyz[2])
            {
           //     Debug.Log("找到小房间:" + xyz[0] + "," + xyz[1] + "," + xyz[2]);
                return miniRoomList[key];
            }
        }
       // Debug.Log("没有找到小房间:" + xyz[0] + "," + xyz[1] + "," + xyz[2]);
        return null;
    }
}