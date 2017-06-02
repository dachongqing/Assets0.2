using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagUIManager : MonoBehaviour {

    /**
     * 正常流程是
     * 1点击后显示包裹ui， 里面有所有玩家收集的物品，
     * 最好有tab页面 按照物品的类型进行归类 ，可以切换
     * 
     * 2 点击一个物品，出现物品的介绍 和可选功能： 使用，丢弃
     * 
     * 3点击使用后， 调用itemcontroller 的使用方法
     * 
     * 4 点击丢弃后，调用itemcontroller的 丢弃方法
     * */
    // Use this for initialization

    private ItemController itemController;

    private NPC chara;

    private RoundController roundController;

    public void showBag() {

    }

    public void tempUse() {
        chara = (NPC)roundController.getCurrentRoundChar();
        if (chara.getBag().getItemTotalCount() > 0)
        {
            itemController.useItem(chara.getBag().getRandomPotionItem(), chara);

        }
        else {
            Debug.Log("你没有可以用的物品");
        }
    }
	void Start () {
        itemController = FindObjectOfType<ItemController>();
        roundController = FindObjectOfType<RoundController>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
