using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Character  {

    //当前角色是否死亡
    bool isDead();

    //是否是玩家
    bool isPlayer();
       
    //是否在等待玩家操作
    bool isWaitPlayer();

    //回合开始， 玩家空实现
    void roundStart();

    //判定回合结束
    bool isRoundOver();

    //回合结束
    void endRound();

    //角色名称
    string getName();

    //角色所在房间
    int[] getCurrentRoom();

    void setCurrentRoom(int[] nextRoomXYZ);

    //获取行动力
    int getActionPoint();

    //更新行动力
    void updateActionPoint(int actionPoint);

    //获取面板信息[力量，速度，知识，神志] 可以扩充
    int[] getAbilityInfo();

    bool ActionPointrolled();

    void setActionPointrolled(bool actionPointrolled);
}
