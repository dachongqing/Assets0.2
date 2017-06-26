using System.Collections;
using System.Collections.Generic;


public  class  RoomConstant
{

    public const string ROOM_TYPE_COMMON = "CommonRoom"; //大厅

    public const string ROOM_TYPE_LOBBY = "LobbyRoom"; //大厅

    public const string ROOM_TYPE_BOOK = "BookRoom"; //书房

    public const string ROOM_TYPE_UPSTAIR = "UpStairEnterRoom"; //楼上入口房间

    public const string ROOM_TYPE_UPSTAIR_BACK = "UpStairOuterRoom"; //楼上返回房间

    public const string ROOM_TYPE_DOWNSTAIR = "DownStairEnterRoom"; //地下入口房间

    public const string ROOM_TYPE_DOWNSTAIR_BACK = "DownStairOuterRoom"; //地下返回房间

    public const int ROOM_Z_GROUND = 0;   //地面

    public const int ROOM_Z_UP = 1; // 楼上

    public const int ROOM_Z_DOWN = -1; //地下

    public const int ROOM_Y_GROUND = 0;   //地面

    public const int ROOM_Y_UP = 440; // 楼上

    public const int ROOM_Y_DOWN = -440; //地下


    public const string ROOM_TYPE_HOSPITAIL_DEAN = "H_deanRoom"; //医院院长室
    public const string ROOM_TYPE_HOSPITAIL_MINITOR = "H_minitorRoom"; //医院监控室
    public const string ROOM_TYPE_HOSPITAIL_MORGUE = "H_morgueRoom"; //医院停尸室
    public const string ROOM_TYPE_HOSPITAIL_SECURITY = "H_securityRoom"; //医院保安室
    public const string ROOM_TYPE_HOSPITAIL_STORE = "H_storeRoom"; //医院储存室
    public const string ROOM_TYPE_HOSPITAIL_SURGERY = "H_surgeryRoom"; //医院外科室
    public const string ROOM_TYPE_HOSPITAIL_TRI_OPERATION = "H_tridOperationRoom"; //医院第3手术室

}
