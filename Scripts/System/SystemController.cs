using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemController : MonoBehaviour {

    private RoundController roundController;
    private MapContraller mapContraller;
    private RoomContraller roomContraller;
    private initMap initMapObject;
    private LoadingManager loadingManager;
    private StoryController storyController;
    private ThingController thingController;
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
        //定义存档路径
        string dirpath = Application.persistentDataPath + "/Save";
        //创建存档文件夹
        IOHelper.CreateDirectory(dirpath);
        //定义存档文件路径
        filename = dirpath + "/SaveData0.sav";
        filename1 = dirpath + "/SaveData1.sav";
        filename2 = dirpath + "/SaveData2.sav";
        filename3 = dirpath + "/SaveData3.sav";

        List<Character> charas = roundController.getAllCharaFromMap();
        SaveData data = new SaveData();
        foreach(Character chara in charas)
        {
            saveCharInfo(data, chara);

        }
        foreach (Character chara in roundController.getAllChara())
        {
            data.CharaNames.Add(chara.getName());

        }

        data.StoryInfo.IsStoryStart = storyController.getIsStartStory();
        if(storyController.getIsStartStory())
        {
            data.StoryInfo.StoryCode = storyController.getStory().getStoryCode();
        }
        data.RoundCount = roundController.getRoundCount();
        // Debug.Log("save filename" + filename);

        foreach(ThingInfo ti in thingController.getEmptyThings())
        {        
            data.Things.Add(ti);
        }
        IOHelper.SetData(filename, data);
        data.CharaNames.Clear();
        //保存地图数据
        Dictionary<int[],RoomInterface> roomsInfo =   roomContraller.getAllRoom();          
        IOHelper.SetData(filename1, getStringMap(initMapObject.getMapUpInfo()));
      
        stringMap.Clear();       
        IOHelper.SetData(filename2, getStringMap(initMapObject.getMapGroundInfo()));
        stringMap.Clear();  
        IOHelper.SetData(filename3, getStringMap(initMapObject.getMapDownInfo()));
        stringMap.Clear();
       
    }

    private void saveCharInfo(SaveData savaData, Character chara)
    {
        P0 p = new P0();
        p.Xyz = chara.getCurrentRoom();
        p.AbilityInfo = chara.getAbilityInfo();
        p.MaxAbilityInfo = chara.getMaxAbilityInfo();
        p.PlayerName = chara.getName();
        p.LiHuiURL = chara.getLiHuiURL();
        p.RoundOver = chara.isRoundOver();
        p.ProfilePic = chara.getProfilePic();
        p.CrazyFlag = chara.isCrazy();
        p.ActionPoint = chara.getActionPoint();
        
        if (typeof(NPC).IsAssignableFrom(chara.GetType()))
        {
            NPC npc = (NPC)chara;
            foreach(Item i in npc.getBag().getAllItems())
            {
                ItemInfo ii = new ItemInfo();
                ii.Code = i.getCode();
                ii.Desc = i.getDesc();
                ii.Durability = i.getDurability();
                ii.Name = i.getName();
                ii.Type = i.getType();
                p.Bag.Add(ii);
            }
            foreach(RoomInterface ri in npc.getTargetRoomList())
            {
                p.TargetRoomlist.Add(ri.getRoomType());
            }
        }
        //p.BossFlag = chara.getb
        if (chara.getName() == SystemConstant.P1_NAME)
        {
            savaData.P1 = p;
        } else if(chara.getName() == SystemConstant.P2_NAME)
        {
            savaData.P2 = p;
        }
        else if (chara.getName() == SystemConstant.P3_NAME)
        {
            savaData.P3 = p;
        }
        else if (chara.getName() == SystemConstant.P4_NAME)
        {
            savaData.P4 = p;
        }
        else if (chara.getName() == SystemConstant.P5_NAME)
        {
            savaData.P5 = p;
        }
        else if (chara.getName() == SystemConstant.P6_NAME)
        {
            savaData.P6 = p;
        }
        else if (chara.getName() == SystemConstant.MONSTER1_NAME)
        {
            savaData.BenMonster = p;
        }
    }

    public void load()
    {
        // Dictionary<string, int[]> p6 = (Dictionary<string, int[]>)IOHelper.GetData(filename1, typeof(Dictionary<string, int[]>));

        // Debug.Log("p6 :" + p6.Count);
        // loadingManager.loadRecord();
        SceneManager.LoadScene("Loading");



    }

    // Use this for initialization
    void Start () {
        roundController = FindObjectOfType<RoundController>();
        roomContraller = FindObjectOfType<RoomContraller>();
        initMapObject = FindObjectOfType<initMap>();
        loadingManager = FindObjectOfType<LoadingManager>();
        storyController = FindObjectOfType<StoryController>();
        thingController = FindObjectOfType<ThingController>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
