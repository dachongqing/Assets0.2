using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class initMap : MonoBehaviour
{

	[Tooltip ("请指定Prefab基本房间")]public GameObject unitMap;
	private int roomLevel=0;
    private int roomAmount = 20;
    [Tooltip("水平调整偏移")] [Range(0f, 30f)] public float horizonDis = 13.7f;
    [Tooltip ("竖直调整偏移")][Range (0f, 30f)]public float vertiDis = 11f;

	//房间生成的起点坐标
	private Transform mapSpawnPoint;

    private Transform mapSpawnUpPoint;

    private Transform mapSpawnDownPoint;
    //地图生成器，产生房间坐标
    private MapContraller mapManager;
	//房间生成器，生成门生成相邻房间
	private RoomContraller roomManager;

    //小地图
    [Tooltip("小地图面板")] public GameObject MinUpPlane;
    [Tooltip("小地图面板")] public GameObject MinGroPlane;
    [Tooltip("小地图面板")] public GameObject MinDownPlane;
    [Tooltip("小地图房间")] public GameObject minRoom;
    //小地图房间起始点
    [Tooltip("小地图房间起始点")] public Transform initMinRoom;

    [Tooltip("水平调整偏移")] private float minRoomhorizonDis = 18;
    [Tooltip("竖直调整偏移")] private float minRoomvertiDis = 20;

    public Vector3 showPos = new Vector3(4, 0, 0);

    private void genMinMap(Dictionary<int[], int[]> map,int z)
    {
       // MinPlane.transform.localPosition = showPos;
        foreach (int[] key in map.Keys)
        {
            Vector3 temPos = initMinRoom.localPosition;
            temPos.x = key[0] * minRoomhorizonDis;
            temPos.y = key[1] * minRoomvertiDis;
       
            GameObject newDi = Instantiate(minRoom) as GameObject;
            MinMapRoom mmr = newDi.GetComponent<MinMapRoom>();
            mmr.setXYZ(key);
            if (map[key][0] == 1) {
                 mmr.setNorthDoorenable();
            }
            if (map[key][1] == 1)
            {
                mmr.setSouthDoorenable();
            }
            if (map[key][2] == 1)
            {
                mmr.setWestDoorenable();
            }
            if (map[key][3] == 1)
            {
                mmr.setEastDoorenable();
            }
            if (z == RoomConstant.ROOM_Z_UP) {
                MinUpPlane.SetActive(true);
                newDi.GetComponent<RectTransform>().SetParent(MinUpPlane.transform);
            } else if (z == RoomConstant.ROOM_Z_GROUND) {
                MinGroPlane.SetActive(true);
                newDi.GetComponent<RectTransform>().SetParent(MinGroPlane.transform);
            } else if (z == RoomConstant.ROOM_Z_DOWN) {
                MinDownPlane.SetActive(true);
                newDi.GetComponent<RectTransform>().SetParent(MinDownPlane.transform);

            }
            newDi.GetComponent<RectTransform>().localPosition = temPos;
            roomManager.addMiniRoomList(key, mmr);

        }
    }

    private void genUpMap() {
        Debug.Log("init map begin");
        mapManager = new MapContraller();
        roomManager = GetComponent<RoomContraller>();

        //在场景中搜索房间起点
        mapSpawnUpPoint = GameObject.Find("MapSpawnUpPoint").GetComponent<Transform>();
        //生成好的地图数据<房间坐标xy,门的信息>
        Dictionary<int[], int[]> map = mapManager.genMap(RoomConstant.ROOM_Z_UP, roomAmount);
        Debug.Log("map count: " + map.Count);
        //根据地图数据，生成新房间
        foreach (int[] key in map.Keys)
        {
            //新房间在地图中的坐标
            int[] rXYZ = new int[] { key[0], key[1], key[2] };
            //产生新房间
            unitMap = roomManager.genRoom(rXYZ, map[key]);

            //预备给新房间的坐标
            Vector3 newMap = mapSpawnUpPoint.position;
            //根据房间的宽度，水平偏移
            newMap.x = key[0] * horizonDis;
            //根据房间的高度，竖直偏移
            newMap.y = RoomConstant.ROOM_Y_UP + key[1] * vertiDis;
            //根据房间的楼层，设定z坐标值
            newMap.z = key[2];
            //设置新房间在屏幕上的坐标
            unitMap.transform.position = newMap;
        }

        genMinMap(map, RoomConstant.ROOM_Z_UP);
    }
    private void genGroundMap() {
        Debug.Log("init map begin");
        mapManager = new MapContraller();
        roomManager = GetComponent<RoomContraller>();

        //在场景中搜索房间起点
        mapSpawnPoint = GameObject.Find("MapSpawnPoint").GetComponent<Transform>();
        //生成好的地图数据<房间坐标xy,门的信息>
        Dictionary<int[], int[]> map = mapManager.genMap(RoomConstant.ROOM_Z_GROUND, roomAmount);
        Debug.Log("map count: " + map.Count);
        //根据地图数据，生成新房间
        foreach (int[] key in map.Keys)
        {
            //新房间在地图中的坐标
            int[] rXYZ = new int[] { key[0], key[1], key[2] };
            //产生新房间
            unitMap = roomManager.genRoom(rXYZ, map[key]);

            //预备给新房间的坐标
            Vector3 newMap = mapSpawnPoint.position;
            //根据房间的宽度，水平偏移
            newMap.x = key[0] * horizonDis;
            //根据房间的高度，竖直偏移
            newMap.y = key[1] * vertiDis;
            //根据房间的楼层，设定z坐标值
            newMap.z = key[2];
            //设置新房间在屏幕上的坐标
            unitMap.transform.position = newMap;
        }

        genMinMap(map, RoomConstant.ROOM_Z_GROUND);
    }

    private void genDownMap() {
        Debug.Log("init map begin");
        mapManager = new MapContraller();
        roomManager = GetComponent<RoomContraller>();

        //在场景中搜索房间起点
        mapSpawnDownPoint = GameObject.Find("MapSpawnDownPoint").GetComponent<Transform>();
        //生成好的地图数据<房间坐标xy,门的信息>
        Dictionary<int[], int[]> map = mapManager.genMap(RoomConstant.ROOM_Z_DOWN, roomAmount);
        Debug.Log("map count: " + map.Count);
        //根据地图数据，生成新房间
        foreach (int[] key in map.Keys)
        {
            //新房间在地图中的坐标
            int[] rXYZ = new int[] { key[0], key[1], key[2] };
            //产生新房间
            unitMap = roomManager.genRoom(rXYZ, map[key]);

            //预备给新房间的坐标
            Vector3 newMap = mapSpawnDownPoint.position;
            //根据房间的宽度，水平偏移
            newMap.x = key[0] * horizonDis;
            //根据房间的高度，竖直偏移
            newMap.y = RoomConstant.ROOM_Y_DOWN + key[1] * vertiDis;
            //根据房间的楼层，设定z坐标值
            newMap.z = key[2];
            //设置新房间在屏幕上的坐标
            unitMap.transform.position = newMap;
        }

        genMinMap(map, RoomConstant.ROOM_Z_DOWN);
    }

    // Use this for initialization
    void Start ()
	{

        this.genUpMap();

        this.genGroundMap();

        this.genDownMap();
    }
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
