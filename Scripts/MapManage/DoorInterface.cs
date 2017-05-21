using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DoorInterface
{

	//显示门的标志
	bool getShowFlag ();

	void setShowFlag (bool showFlag);

	//门通往房间的坐标
	int[] getNextRoomXYZ ();

	void setNextRoomXYZ (int[] xyz);

	//获取当前房间对象
	RoomInterface getRoom ();

	void setRoom (RoomInterface room);

    //开门事件：扣除行动力 需要参数
    bool openDoor (Character chara);


}
