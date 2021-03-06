﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Character: BaseGameOject
{

    //当前角色是否死亡
    bool isDead();

    //是否是玩家
    bool isPlayer();

    //是否在等待玩家操作
    bool isWaitPlayer();

    void setWaitPlayer(bool waitFlag);

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

    void setCurrentRoom(RoomInterface nextRoom, string doorFrom);

    //获取行动力
    int getActionPoint();

    //更新行动力
    void updateActionPoint(int actionPoint);

    //获取面板信息[力量，速度，知识，神志，生命值] 可以扩充
    int[] getAbilityInfo();

    int[] getMaxAbilityInfo(); //获取上限信息

    bool ActionPointrolled();

    void setActionPointrolled(bool actionPointrolled);

    void setScriptAction(StoryScript ss);

    bool isScriptWin();

    StoryScript getScriptAciont();

    bool isBoss();

    void setBoss(bool bossFlag);

	void setDiceNumberBuffer(int number);

	int getDiceNumberBuffer();

	void setDiceValueBuffer(int value);

	int getDiceValueBuffer();

	void setDamgeBuffer (int damge);

	int getDamgeBuffer ();

    string getLiHuiURL();

    void setDesc(string desc);

    string getDesc();

    bool isCrazy();

    string getDeitalPic();

    string getProfilePic();

    Vector3 getCharaTransformPosition();

    void setCharaInHiddenRoom(RoomInterface room);

}
