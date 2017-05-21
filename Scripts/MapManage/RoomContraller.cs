using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoomContraller : MonoBehaviour
{

    private Queue<String> groundRoomType = new Queue<String>();

    private Queue<String> upRoomType = new Queue<String>();

    private Queue<String> downRoomType = new Queue<String>();

    private Dictionary<int[], RoomInterface> roomList = new Dictionary<int[], RoomInterface>();

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
        groundRoomType.Enqueue("BookRoom");
        groundRoomType.Enqueue("LobbyRoom");
        groundRoomType.Enqueue("BookRoom");
        groundRoomType.Enqueue("LobbyRoom");
        groundRoomType.Enqueue("BookRoom");
        groundRoomType.Enqueue("LobbyRoom");
        groundRoomType.Enqueue("BookRoom");
        groundRoomType.Enqueue("LobbyRoom");
        groundRoomType.Enqueue("BookRoom");
        groundRoomType.Enqueue("LobbyRoom");
        groundRoomType.Enqueue("BookRoom");
        groundRoomType.Enqueue("LobbyRoom");
        groundRoomType.Enqueue("BookRoom");
        groundRoomType.Enqueue("LobbyRoom");
        groundRoomType.Enqueue("BookRoom");
    }

    private void genRoomEvent()
    {
        EventInterface ei = new SpeedLeveaRoomEvent();
        events.Add(ei);

    }

    private EventInterface getRandomEvent(String banEventType) {

        EventInterface et = events[random.Next(events.Count)];
        if (et.getEventType() == banEventType)
        {
            return getRandomEvent(banEventType);
        }
        else {
            return et;
        }

           
    }

    private void setRoomEvents(RoomInterface room)
    {

        //只有30%的概率房间会生成事件
        if (1 == random.Next(0, 3)) {

        //判定房间是处于什么位置 楼上 地面 楼下， 不能出现 有冲突的事件， 比如楼下不能出现掉落事件
             if (room.getXYZ()[2] == RoomConstant.ROOM_TYPE_GROUND)
              {
                //对于地面事件 所有事件都可以发生
                 room.setRoomEvent(getRandomEvent(null));

                //  if () {
                //  }
             }
           else if (room.getXYZ()[2] == RoomConstant.ROOM_TYPE_UP)
            {
                //对于楼上事件 
           }
           else
           {
                //地下事件
            }
        }

    }


    public GameObject genRoom (int[] xyz, int[] door)
	{
		//房间Prefab所在文件夹路径
		string roomType = groundRoomType.Dequeue ();
		string url = "Prefabs/" + roomType;

		//仅用Resources.Load会永久修改原形Prefab。应该用Instatiate,操作修改原形的克隆体
		GameObject room = Instantiate (Resources.Load (url)) as GameObject;

		if (room == null) {
			Debug.Log ("cant find room Prefab !!!");
		} else {
			RoomInterface ri = room.GetComponent (System.Type.GetType (roomType)) as RoomInterface;
			ri.setXYZ (xyz);

            //随机生成事件

            setRoomEvents(ri);


            //根据这房间门的数据，生成对应的门
            if (door [0] == 1) {
				//门启用
				ri.northDoorEnable ();
				//门属于这个房间
				GameObject doorGo = ri.getNorthDoor ();
				doorGo.GetComponent<DoorInterface> ().setRoom (ri);
				//门外有相邻房间的坐标为
//				错误代码int[] nextRoomXYZ = xyz;
//				错误代码nextRoomXYZ [2] += 1原因：一维数组是引用类型,+1会导致xyz[]的修改;
//				体现为  房间的map坐标!=房间的getXYZ

				//修正为
				int[] nextRoomXYZ = new int[3];
				nextRoomXYZ [0] = xyz [0];
				nextRoomXYZ [1] = xyz [1];
				nextRoomXYZ [2] = xyz [2];
				nextRoomXYZ [1] += 1;

				doorGo.GetComponent<DoorInterface> ().setNextRoomXYZ (nextRoomXYZ);

			}
			if (door [1] == 1) {
				//门启用
				ri.southDoorEnable ();
				//门属于这个房间
				GameObject doorGo = ri.getSouthDoor ();
				doorGo.GetComponent<DoorInterface> ().setRoom (ri);
				//门外有相邻房间的坐标为
				int[] nextRoomXYZ = new int[3];
				nextRoomXYZ [0] = xyz [0];
				nextRoomXYZ [1] = xyz [1];
				nextRoomXYZ [2] = xyz [2];
				nextRoomXYZ [1] -= 1;

				doorGo.GetComponent<DoorInterface> ().setNextRoomXYZ (nextRoomXYZ);
			}
			if (door [2] == 1) {
				//门启用
				ri.westDoorEnable ();
				//门属于这个房间
				GameObject doorGo = ri.getWestDoor ();
				doorGo.GetComponent<DoorInterface> ().setRoom (ri);
				//门外有相邻房间的坐标为
				int[] nextRoomXYZ = new int[3];
				nextRoomXYZ [0] = xyz [0];
				nextRoomXYZ [1] = xyz [1];
				nextRoomXYZ [2] = xyz [2];
				nextRoomXYZ [0] -= 1;

				doorGo.GetComponent<DoorInterface> ().setNextRoomXYZ (nextRoomXYZ);
			}
			if (door [3] == 1) {
				//门启用
				ri.eastDoorEnable ();
				//门属于这个房间
				GameObject doorGo = ri.getEastDoor ();
				doorGo.GetComponent<DoorInterface> ().setRoom (ri);
				//门外有相邻房间的坐标为
				int[] nextRoomXYZ = new int[3];
				nextRoomXYZ [0] = xyz [0];
				nextRoomXYZ [1] = xyz [1];
				nextRoomXYZ [2] = xyz [2];
				nextRoomXYZ [0] += 1;

				doorGo.GetComponent<DoorInterface> ().setNextRoomXYZ (nextRoomXYZ);
			}
			roomList.Add (ri.getXYZ (), ri);
		}

		return room;
	}


	public RoomInterface findRoomByXYZ (int[] xyz)
	{

		foreach (int[] key in roomList.Keys) {
			if (key [0] == xyz [0] && key [1] == xyz [1] && key [2] == xyz [2]) {
				return roomList [key];
			}
		}
		return null;
	}
}
