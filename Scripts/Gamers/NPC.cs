using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface NPC : Character {

     void defaultAction();

     Bag getBag();

    bool checkItem(string itemCode);

    void sendMessageToPlayer(string[] message);

    bool isFollowGuangBoAction();

    void setFollowGuangBoAction(bool flag);

    void setGuangBoAction(GuangBoAction gb);

    List<string> getTargetChara();

    void checkTargetRoomLocked(string roomType);

    void setTargetRoomLocked(bool locked);

    bool isTargetRoomLocked();

    Queue<RoomInterface> getTargetRoomList();

    void CharaMoveByMouse(Vector3 position);

    void CharaMoveByKey(string forward);

}
