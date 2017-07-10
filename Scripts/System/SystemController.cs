using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemController : MonoBehaviour {

    private RoundController roundController;
    private MapContraller mapContraller;
    private RoomContraller roomContraller;
    private initMap initMapObject;
    private LoadingManager loadingManager;
    private string filename;
    private string filename1;
    private string filename2;
    private string filename3;
    private Dictionary<string, int[]> stringMap = new Dictionary<string, int[]>();
    private Dictionary<string, int[]> getStringMap(Dictionary<int[], int[]> map)
    {
        foreach (int[] key in map.Keys)
        {
            stringMap.Add(key[0] + "," + key[1] + "," + key[2], map[key]);
        }
        return stringMap;
    }

    public void save()
    {
        Character chara = roundController.getPlayerChara();

        SaveData data = new SaveData();
        P6 p6 = new P6();
        p6.PlayerName = chara.getName();
        //定义存档路径
        string dirpath = Application.persistentDataPath + "/Save";
        //创建存档文件夹
        IOHelper.CreateDirectory(dirpath);
        //定义存档文件路径
        filename = dirpath + "/SaveData0.sav";
        filename1 = dirpath + "/SaveData1.sav";
        filename2 = dirpath + "/SaveData2.sav";
        filename3 = dirpath + "/SaveData3.sav";
        Debug.Log("save filename" + filename);
        //保存数据
        Dictionary<int[],RoomInterface> roomsInfo =   roomContraller.getAllRoom();
        Dictionary<int[], int[]> maps =  initMapObject.getMapUpInfo();
       


        data.P6 = p6;
        data.Map = maps;
        //  IOHelper.SetData(filename, data);
      
        IOHelper.SetData(filename1, getStringMap(maps));
        maps.Clear();
        stringMap.Clear();
        maps = initMapObject.getMapGroundInfo();
        IOHelper.SetData(filename2, getStringMap(maps));
        stringMap.Clear();
        maps.Clear();
        maps = initMapObject.getMapDownInfo();
        IOHelper.SetData(filename3, getStringMap(maps));

    }

    public void load()
    {
       // Dictionary<string, int[]> p6 = (Dictionary<string, int[]>)IOHelper.GetData(filename1, typeof(Dictionary<string, int[]>));

       // Debug.Log("p6 :" + p6.Count);
        loadingManager.loadRecord();


    }

    // Use this for initialization
    void Start () {
        roundController = FindObjectOfType<RoundController>();
        roomContraller = FindObjectOfType<RoomContraller>();
        initMapObject = FindObjectOfType<initMap>();
        loadingManager = FindObjectOfType<LoadingManager>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
