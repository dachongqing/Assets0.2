using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagUIManager : MonoBehaviour {

  

    private Vector3 showPos = new Vector3(4, 0, 0);
    private Vector3 hidePos = new Vector3(-10, 0, 0);

    public GameObject ItemParentPosition1;
    public Transform ItemPosition1;
    public GameObject ItemParentPosition2;
    public Transform ItemPosition2;
    public GameObject ItemParentPosition3;
    public Transform ItemPosition3;
    public Text itemDesc;

    private List<Transform> positionList = new List<Transform>();
    private List<GameObject> parentPositionList = new List<GameObject>();
    private List<GameObject> profabsList = new List<GameObject>();

    private ItemController itemController;

    private Item selectItem;

    //对话界面
    public GameObject BagItemMenuUI;

    private RoundController roundController;


    public void showBagItemUI()
    {
        BagItemMenuUI.SetActive(true);
        BagItemMenuUI.transform.localPosition = showPos;

        NPC player = (NPC)roundController.getPlayerChara();
        Bag bag = player.getBag();
        for (int i = 0; i < bag.getItemTotalCount(); i++)
        {
            Item item = bag.getAllItems()[i];
            string url = "Prefabs/Items/" + item.getCode();

            Debug.Log("item url " + url);
            GameObject itemPrefab = Instantiate(Resources.Load(url)) as GameObject;
            Vector3 temPos = positionList[i].localPosition;
            Debug.Log("parentPositionList[i] " + parentPositionList[i]);
            itemPrefab.GetComponent<RectTransform>().SetParent(parentPositionList[i].transform);
            itemPrefab.GetComponent<RectTransform>().localPosition = temPos;
            ItemClickEvent itemClickEvent = itemPrefab.GetComponent<ItemClickEvent>();
            itemClickEvent.setI(i);
            profabsList.Add(itemPrefab);
         
        }
            itemDesc.text = "";


    }

    public void setSelectItem(int position) {
        NPC player = (NPC)roundController.getPlayerChara();
        Bag bag = player.getBag();
        selectItem = bag.getAllItems()[position];
    }

    public void closeBagItemUI()
    {
        BagItemMenuUI.SetActive(false);
        BagItemMenuUI.transform.localPosition = hidePos;
        selectItem = null;
    }

    public void useItem() {     
        itemController.useItem(selectItem,(NPC)roundController.getPlayerChara());
        this.clear();
    }

    public void dropItem() {        
        itemController.destroyItemTool(selectItem, (NPC)roundController.getPlayerChara());
        clear();
    }


    private void clear() {
        foreach (GameObject itemPrefab in profabsList)
        {
            itemPrefab.SetActive(false);
        }
        profabsList.Clear();       
        closeBagItemUI();
    }

    void Start () {
        roundController = FindObjectOfType<RoundController>();
        itemController = FindObjectOfType<ItemController>();
        positionList.Add(ItemPosition1);
        parentPositionList.Add(ItemParentPosition1);
        positionList.Add(ItemPosition2);
        parentPositionList.Add(ItemParentPosition2);
        positionList.Add(ItemPosition3);
        parentPositionList.Add(ItemParentPosition3);

    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (selectItem != null ) {
            itemDesc.text = selectItem.getDesc();
        }
		
	}
}
