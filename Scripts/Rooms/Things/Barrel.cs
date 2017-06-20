using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Barrel : MonoBehaviour, Thing
{

    private Item item;

    private bool isEmpty;

    private bool listenRoll;

    private RoundController roundController;

    private MessageUI messageUI;

    private int phase;

    private int maxValue;

    private RollDiceUIManager uiManager;

    public void doClick() {
        Debug.Log("click a barrel");
        if (this.isEmpty)
        {
            messageUI.ShowMessge("里面是空的", 1);
        }
        else
        {

            NPC chara = (NPC)roundController.getCurrentRoundChar();
            if (chara.getActionPoint() >= 2)
            {
                if (chara.getAbilityInfo()[2] >= 1)
                {
                    messageUI.ShowMessge("因为你的注意力高度集中，很容易就发现了药水道具", 1);
                    chara.getBag().insertItem(this.getItem());
                }
                else if (chara.getAbilityInfo()[2] >= 0)
                {
                    messageUI.ShowMessge("你注意桶底有点黑色的物品，你需要对力量进行判断 大于5 才能取出那个物品", 1);
                    this.maxValue = 5;
                    this.phase = 1;
                    listenRoll = true;
                }
                else
                {
                    messageUI.ShowMessge("因为你的注意力低下的关系，没能找到任何道具", 1);
                }
                chara.updateActionPoint(chara.getActionPoint() - 2);
            }
            else
            {
                messageUI.ShowMessge("行动力不足，不能进行调查.", 1);
            }
        }
    }

    void OnMouseDown()
    {
        if (!SystemUtil.IsTouchedUI())
        {

            doClick();

        }
    }

    private void openEvent(int rollValue) {
        if (this.maxValue <= rollValue)
        {
            NPC chara = (NPC)roundController.getCurrentRoundChar();
            chara.getBag().insertItem(this.getItem());
            messageUI.ShowMessge("你的力气刚好，拔出了那个物品", 1);
        }
        else {
            messageUI.ShowMessge("因为你的力气不够，没能拔出那个物品", 1);
        }
        this.listenRoll = false;
    }

    private Item getItem() {
        this.isEmpty = true;
        return this.item;
    }

	// Use this for initialization
	void Start () {
        roundController = FindObjectOfType<RoundController>();
        messageUI = FindObjectOfType<MessageUI>();
        this.isEmpty = false;
        this.listenRoll = false;
        this.phase = 1;
        uiManager = FindObjectOfType<RollDiceUIManager>();
        item = new ItemPotion(ItemConstant.ITEM_CODE_POTION_10001
            ,"速度回复药水","模糊的字迹写着是哈尔滨六厂生产，蓝屏的钙");

    }
    private int rollValue;
    
    // Update is called once per frame
    void Update () {
        if (listenRoll)
        {

            if (phase == 1 && messageUI.getResult().getDone())
            {
                phase =  2;
            }

            if (phase == 2 && !uiManager.getResult().getDone() && messageUI.isClosed())
            {
                NPC chara = (NPC)roundController.getCurrentRoundChar();
                RollDiceParam param = new RollDiceParam(chara.getAbilityInfo()[1]);
                uiManager.setRollDiceParam(param);
                uiManager.showRollDice();
            }
            else if (phase == 2 && uiManager.getResult().getDone())
            {
                rollValue = uiManager.getResult().getResult();
                phase = 3;

            } else if (phase == 3 &&  !uiManager.isClosedPlane()) {

                this.openEvent(rollValue);
            }       
           
        }
    }
}
