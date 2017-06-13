using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class initMap : MonoBehaviour
{

	[Tooltip ("请指定Prefab基本房间")]public GameObject unitMap;
	[Range(-1,1)]public int roomLevel=0;
	[Range (1, 15)]public int roomAmount = 5;
	[Tooltip ("水平调整偏移")][Range (0f, 30f)]public float horizonDis = 7.5f;
	[Tooltip ("竖直调整偏移")][Range (0f, 30f)]public float vertiDis = 7.5f;

	//房间生成的起点坐标
	private Transform mapSpawnPoint;
	//地图生成器，产生房间坐标
	private MapContraller mapManager;
	//房间生成器，生成门生成相邻房间
	private RoomContraller roomManager;

    //小地图
    [Tooltip("小地图面板")] public GameObject MinPlane;
    [Tooltip("小地图房间")] public GameObject minRoom;
    //小地图房间起始点
    [Tooltip("小地图房间起始点")] public Transform initMinRoom;

    [Tooltip("水平调整偏移")] private float minRoomhorizonDis = 100;
    [Tooltip("竖直调整偏移")] private float minRoomvertiDis = 100;

    public Vector3 showPos = new Vector3(4, 0, 0);

    private void genMinMap(Dictionary<int[], int[]> map)
    {
        MinPlane.SetActive(true);
       // MinPlane.transform.localPosition = showPos;
        foreach (int[] key in map.Keys)
        {
            Vector3 temPos = initMinRoom.localPosition;
            temPos.x = key[0] * minRoomhorizonDis;
            temPos.y = key[1] * minRoomvertiDis;
            //生成一个骰子
            GameObject newDi = Instantiate(minRoom) as GameObject;
            newDi.GetComponent<RectTransform>().SetParent(MinPlane.transform);
            newDi.GetComponent<RectTransform>().localPosition = temPos;
        }
    }

    // Use this for initialization
    void Start ()
	{
        Debug.Log("init map begin");
		mapManager = new MapContraller ();
		roomManager = GetComponent<RoomContraller> ();

		//在场景中搜索房间起点
		mapSpawnPoint = GameObject.Find ("MapSpawnPoint").GetComponent<Transform> ();
		//生成好的地图数据<房间坐标xy,门的信息>
		Dictionary<int[], int[]> map = mapManager.genMap (roomLevel,roomAmount);

		//根据地图数据，生成新房间
		foreach (int[] key in map.Keys) {
			//新房间在地图中的坐标
			int[] rXYZ = new int[]{key[0],key[1],key[2]};
			//产生新房间
			unitMap = roomManager.genRoom (rXYZ,map [key]);

			//预备给新房间的坐标
			Vector3 newMap = mapSpawnPoint.position;
			//根据房间的宽度，水平偏移
			newMap.x = key [0] * horizonDis;      
			//根据房间的高度，竖直偏移
			newMap.y = key [1] * vertiDis;
			//根据房间的楼层，设定z坐标值
			newMap.z = key[2];
			//设置新房间在屏幕上的坐标
			unitMap.transform.position = newMap;
		}

        genMinMap(map);
    }
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
