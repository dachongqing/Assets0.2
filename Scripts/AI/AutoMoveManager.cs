using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMoveManager  {

    public static bool move(Character chara, RoomContraller roomContraller, EventController eventController, DiceRollCtrl diceRoll, APathManager aPathManager, string targetRoomType)
    {
        RoomInterface targetRoom = roomContraller.findRoomByRoomType(targetRoomType);
        return doMove(chara,  roomContraller,  eventController,  diceRoll,  aPathManager, targetRoom,false);
    }

    public static bool move(Character chara, RoomContraller roomContraller, EventController eventController, DiceRollCtrl diceRoll, APathManager aPathManager, int[] targetRoomXYZ)
    {
        RoomInterface targetRoom = roomContraller.findRoomByXYZ(targetRoomXYZ);
        return doMove(chara, roomContraller, eventController, diceRoll, aPathManager, targetRoom, false);
    }

    private static bool move(Character chara, RoomContraller roomContraller, EventController eventController, DiceRollCtrl diceRoll, APathManager aPathManager, string targetRoomType, bool goUpOrDown)
    {
       // Debug.Log(targetRoomType + ", " + goUpOrDown);
        RoomInterface targetRoom = roomContraller.findRoomByRoomType(targetRoomType);
        return doMove(chara, roomContraller, eventController, diceRoll, aPathManager, targetRoom, goUpOrDown);
    }

    private static bool move(Character chara, RoomContraller roomContraller, EventController eventController, DiceRollCtrl diceRoll, APathManager aPathManager, int[] targetRoomXYZ, bool goUpOrDown)
    {
        RoomInterface targetRoom = roomContraller.findRoomByXYZ(targetRoomXYZ);
        return doMove(chara, roomContraller, eventController, diceRoll, aPathManager, targetRoom, goUpOrDown);
    }


    public static bool doMove(Character chara, RoomContraller roomContraller, EventController eventController, DiceRollCtrl diceRoll, APathManager aPathManager, RoomInterface targetRoom,bool goUpOrDown)
    {
        if (chara.ActionPointrolled() || chara.getActionPoint() >0)
        {
            
           
            Stack<Node> path = null;
            
            RoomInterface currentRoom = roomContraller.findRoomByXYZ(chara.getCurrentRoom());
            //如果当前房间不是目标房间
            //开始找路 
            if (chara.getCurrentRoom()[0] != targetRoom.getXYZ()[0] || chara.getCurrentRoom()[1] != targetRoom.getXYZ()[1] || chara.getCurrentRoom()[2] != targetRoom.getXYZ()[2])
            {
              //  Debug.Log("如果当前房间不是目标房间");
                //判定是否同层
                if (chara.getCurrentRoom()[2] != targetRoom.getXYZ()[2])
                {
                 //   Debug.Log("如果目标房间是楼下， 先定位到下楼梯口房间， 如果目标是楼上，先定位到上楼梯口房间");
                    // 如果目标房间是楼下， 先定位到下楼梯口房间， 如果目标是楼上，先定位到上楼梯口房间
                    if (targetRoom.getXYZ()[2] == RoomConstant.ROOM_Z_UP)
                    {
                    //    Debug.Log("目标是楼上，先定位到上楼梯口房间");
                        // targetRoom = roomContraller.findRoomByType(RoomConstant.);
                        if (chara.getCurrentRoom()[2] == RoomConstant.ROOM_Z_GROUND)
                        {
                        //    Debug.Log("当前房间 是地面， 只要到向上楼梯房间");
                            if (!AutoMoveManager.move(chara, roomContraller, eventController, diceRoll, aPathManager, RoomConstant.ROOM_TYPE_UPSTAIR,true))
                            {

                                return false;
                            }
                            else
                            {
                          //      Debug.Log("当前房间 是楼上， 寻找目标房间");
                                path = aPathManager.findPath(roomContraller.findRoomByXYZ(chara.getCurrentRoom()), targetRoom, roomContraller);
                            }


                        }
                        else
                        {

                            if (!AutoMoveManager.move(chara, roomContraller, eventController, diceRoll, aPathManager, RoomConstant.ROOM_TYPE_DOWNSTAIR_BACK,true))
                            {

                                return false;
                            }
                            else
                            {
                                if (!AutoMoveManager.move(chara, roomContraller, eventController, diceRoll, aPathManager, RoomConstant.ROOM_TYPE_UPSTAIR, true))
                                {

                                    return false;
                                }
                                else
                                {
                               //     Debug.Log("现在同层了。。可以找最终目标房间了 ：" + targetRoom);
                                    path = aPathManager.findPath(roomContraller.findRoomByXYZ(chara.getCurrentRoom()), targetRoom, roomContraller);
                                }
                            }


                        }
                    }
                    else if (targetRoom.getXYZ()[2] == RoomConstant.ROOM_Z_GROUND)
                    {
                        if (chara.getCurrentRoom()[2] == RoomConstant.ROOM_Z_UP)
                        {
                            if (!AutoMoveManager.move(chara, roomContraller, eventController, diceRoll, aPathManager, RoomConstant.ROOM_TYPE_UPSTAIR_BACK, true))
                            {

                                return false;
                            }
                            else
                            {
                             //   Debug.Log("现在同层了。。可以找最终目标房间了 ：" + targetRoom);
                                path = aPathManager.findPath(roomContraller.findRoomByXYZ(chara.getCurrentRoom()), targetRoom, roomContraller);
                            }


                        }
                        else
                        {

                            if (!AutoMoveManager.move(chara, roomContraller, eventController, diceRoll, aPathManager, RoomConstant.ROOM_TYPE_DOWNSTAIR_BACK, true))
                            {

                                return false;
                            }
                            else
                            {
                             //   Debug.Log("现在同层了。。可以找最终目标房间了 ：" + targetRoom);
                                path = aPathManager.findPath(roomContraller.findRoomByXYZ(chara.getCurrentRoom()), targetRoom, roomContraller);

                            }


                        }
                    }

                    else if (targetRoom.getXYZ()[2] == RoomConstant.ROOM_Z_DOWN) {

                       // Debug.Log("目标是楼下，先定位到下楼梯口房间");
                        if (chara.getCurrentRoom()[2] == RoomConstant.ROOM_Z_GROUND)
                        {
                         //   Debug.Log("当前房间 是地面， 只要到向下楼梯房间");
                            if (!AutoMoveManager.move(chara, roomContraller, eventController, diceRoll, aPathManager, RoomConstant.ROOM_TYPE_DOWNSTAIR, true))
                            {

                                return false;
                            }
                            else
                            {
                                //
                           //     Debug.Log("现在同层了。。可以找最终目标房间了 ：" + targetRoom);
                             
                                path = aPathManager.findPath(roomContraller.findRoomByXYZ(chara.getCurrentRoom()), targetRoom, roomContraller);
                            }


                        }   
                        else
                        {

                            if (!AutoMoveManager.move(chara, roomContraller, eventController, diceRoll, aPathManager, RoomConstant.ROOM_TYPE_UPSTAIR_BACK, true))
                            {

                                return false;
                            }
                            else
                            {
                                if (!AutoMoveManager.move(chara, roomContraller, eventController, diceRoll, aPathManager, RoomConstant.ROOM_TYPE_DOWNSTAIR, true))
                                {

                                    return false;
                                }
                                else
                                {
                                    Debug.Log("现在同层了。。可以找最终目标房间了 ：" + targetRoom);
                                    path = aPathManager.findPath(roomContraller.findRoomByXYZ(chara.getCurrentRoom()), targetRoom, roomContraller);
                                }
                            }


                        }
                    }
                }
                else
                {
                    if (chara.ActionPointrolled()) {
                      //  Debug.Log("如果目标房间同层，直接找路");
                        int speed = chara.getAbilityInfo()[1] + chara.getDiceNumberBuffer();
                        int res = diceRoll.calculateDice(speed) + chara.getDiceValueBuffer();
                        chara.updateActionPoint(res);
                        chara.setActionPointrolled(false);
                    }
                    path = aPathManager.findPath(currentRoom, targetRoom, roomContraller);
                }
                while (chara.getActionPoint() > 0 && path.Count > 0)
                {
                    Node nextRoom = path.Peek();
                    bool opened = false;
                    //判断向什么方向的房间
                    GameObject targetDoor = null ;
                    if (chara.getCurrentRoom()[0] == nextRoom.xy[0] && chara.getCurrentRoom()[1] - nextRoom.xy[1] < 0)
                    {
                        //up room
                        //调用AI 专用方法

                        targetDoor = currentRoom.getNorthDoor();
                        //开门成功

                    }
                    else if (chara.getCurrentRoom()[0] == nextRoom.xy[0] && chara.getCurrentRoom()[1] - nextRoom.xy[1] > 0)
                    {
                        //down room
                        targetDoor = currentRoom.getSouthDoor();
                    }
                    else if (chara.getCurrentRoom()[1] == nextRoom.xy[1] && chara.getCurrentRoom()[0] - nextRoom.xy[0] < 0)
                    {
                        //east room
                        targetDoor = currentRoom.getEastDoor();
                    }
                    else
                    {
                        //west room
                        targetDoor = currentRoom.getWestDoor();
                    }

                    if (roomContraller.findRoomByXYZ(nextRoom.xy).checkOpen(chara))
                    {
                   //     Debug.Log("没有锁，可以开门");
                        opened = targetDoor.GetComponent<WoodDoor>().openDoor(chara);
                        //开门成功
                    }
                    else
                    {
                   //     Debug.Log("有锁，不可以开门");
                        if (typeof(NPC).IsAssignableFrom(chara.GetType()))
                        {
                      //      Debug.Log("我是npc，我要去找钥匙开门");
                            NPC npc = (NPC)chara;
                            npc.checkTargetRoomLocked(roomContraller.findRoomByXYZ(nextRoom.xy).getRoomType());
                            return false;
                        }
                        else
                        {
                      //      Debug.Log("怪物无法发言，只能等门被打开。");
                        };
                    }


                    //如果进入房间是目标房间 暂时回合结束
                    if (opened)
                    {

                        bool result = eventController.excuteLeaveRoomEvent(currentRoom, chara);

                        //非正式测试用，只考虑行动力足够
                        
                        if (result == true)
                        {
                            //离开门成功
                                path.Pop();
                                currentRoom.removeChara(chara);
                                roomContraller.setCharaInMiniMap(chara.getCurrentRoom(),chara, false);
                                //当前人物坐标移动到下一个房间
                                chara.setCurrentRoom(nextRoom.xy);
                                roomContraller.findRoomByXYZ(nextRoom.xy).setChara(chara);
                                roomContraller.setCharaInMiniMap(nextRoom.xy,chara, true);
                               
                                //触发进门事件
                                //	eventController.excuteEnterRoomEvent (nextRoom, roundController.getCurrentRoundChar ());  暂时禁用 运行时有异常
                        }
                        else
                        {
                            //离开失败
                         //   Debug.Log("WoodDoor.cs OnMouseDown 离开房间失败");
                        }
                    }

                }

                //找到房间后， 如果还有体力值， 判定是否是上下楼的房间，如果是 直接上下楼
                    if (chara.getActionPoint() > 0)
                    {
                    if ( targetRoom.getRoomType() == RoomConstant.ROOM_TYPE_UPSTAIR_BACK
                        || targetRoom.getRoomType() == RoomConstant.ROOM_TYPE_DOWNSTAIR || targetRoom.getRoomType() == RoomConstant.ROOM_TYPE_DOWNSTAIR_BACK)
                    {
                         //   Debug.Log("找到目标房间了，但是行动力没有用完，直接上下楼");
                        RoomInterface stairRoom;
                        if (targetRoom.getRoomType() == RoomConstant.ROOM_TYPE_UPSTAIR) {
                             stairRoom = roomContraller.findRoomByRoomType(RoomConstant.ROOM_TYPE_UPSTAIR_BACK);
                           
                        } else if (targetRoom.getRoomType() == RoomConstant.ROOM_TYPE_UPSTAIR_BACK) {
                             stairRoom = roomContraller.findRoomByRoomType(RoomConstant.ROOM_TYPE_UPSTAIR);
                           
                        } else if (targetRoom.getRoomType() == RoomConstant.ROOM_TYPE_DOWNSTAIR) {
                             stairRoom = roomContraller.findRoomByRoomType(RoomConstant.ROOM_TYPE_DOWNSTAIR_BACK);
                        }
                        else 
                        {
                            stairRoom = roomContraller.findRoomByRoomType(RoomConstant.ROOM_TYPE_DOWNSTAIR);
                        }

                        targetRoom.removeChara(chara);
                        roomContraller.setCharaInMiniMap(chara.getCurrentRoom(),chara, false);
                        stairRoom.setChara(chara);
                        chara.setCurrentRoom(stairRoom.getXYZ());
                        chara.updateActionPoint(chara.getActionPoint() - SystemConstant.UPStairActionPoint);
                        roomContraller.setCharaInMiniMap(stairRoom.getXYZ(),chara, true);
                       

                        return true;
                    }
                    
                }
            }
            else
            {
                 if (chara.ActionPointrolled())
                {
                   // Debug.Log("如果目标房间同层，直接找路");
                    int speed = chara.getAbilityInfo()[1] + chara.getDiceNumberBuffer();
                    int res = diceRoll.calculateDice(speed) + chara.getDiceValueBuffer();
                    chara.updateActionPoint(res);
                    chara.setActionPointrolled(false);
                }

                if (goUpOrDown &&(targetRoom.getRoomType() == RoomConstant.ROOM_TYPE_UPSTAIR || targetRoom.getRoomType() == RoomConstant.ROOM_TYPE_UPSTAIR_BACK
                    || targetRoom.getRoomType() == RoomConstant.ROOM_TYPE_DOWNSTAIR || targetRoom.getRoomType() == RoomConstant.ROOM_TYPE_DOWNSTAIR_BACK))
                {
                  //  Debug.Log("当前房间是上或者下楼口");
                    //找到房间后， 如果还有体力值， 判定是否是上下楼的房间，如果是 直接上下楼
                    if (chara.getActionPoint() > 0)
                    {
                   //     Debug.Log("找到目标房间了，但是行动力没有用完，直接上下楼");
                        RoomInterface stairRoom;
                        if (targetRoom.getRoomType() == RoomConstant.ROOM_TYPE_UPSTAIR)
                        {
                            stairRoom = roomContraller.findRoomByRoomType(RoomConstant.ROOM_TYPE_UPSTAIR_BACK);

                        }
                        else if (targetRoom.getRoomType() == RoomConstant.ROOM_TYPE_UPSTAIR_BACK)
                        {
                            stairRoom = roomContraller.findRoomByRoomType(RoomConstant.ROOM_TYPE_UPSTAIR);

                        }
                        else if (targetRoom.getRoomType() == RoomConstant.ROOM_TYPE_DOWNSTAIR)
                        {
                            stairRoom = roomContraller.findRoomByRoomType(RoomConstant.ROOM_TYPE_DOWNSTAIR_BACK);
                        }
                        else
                        {
                            stairRoom = roomContraller.findRoomByRoomType(RoomConstant.ROOM_TYPE_DOWNSTAIR);
                        }

                        targetRoom.removeChara(chara);
                        roomContraller.setCharaInMiniMap(chara.getCurrentRoom(),chara, false);
                        stairRoom.setChara(chara);
                        chara.setCurrentRoom(stairRoom.getXYZ());
                        chara.updateActionPoint(chara.getActionPoint() - SystemConstant.UPStairActionPoint);
                        roomContraller.setCharaInMiniMap(stairRoom.getXYZ(),chara, true);
                        
                        return true;
                    }
                    else {
                     //   Debug.Log("没有体力行动了");
                        return false;
                    }

                }
               
               // Debug.Log("和目标房间 一起");
                return true;

            }
        }
        else
        {
            Debug.Log("你已经丢过行动力骰子");
            return false;
        }
        return false;
    }
    
}
