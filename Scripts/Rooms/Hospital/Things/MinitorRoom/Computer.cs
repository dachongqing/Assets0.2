using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour {

    private Item item;

    private bool isEmpty;

    private bool listenRoll;

    private RoundController roundController;

    private MessageUI messageUI;

    private int phase;

    private int maxValue;

    private RollDiceUIManager uiManager;

    public void doClick()
    {
        Debug.Log("click a barrel");
        if (this.isEmpty)
        {
            messageUI.showMessage("电脑里已经没有特别的图片。");
        }
        else
        {

            NPC chara = (NPC)roundController.getCurrentRoundChar();
            if (chara.getActionPoint() >= SystemConstant.InverstActionPoint)
            {
                if (chara.getAbilityInfo()[3] >= 4)
                {
                    messageUI.showMessage("一张监控图片引起了你的注意。");
                    chara.getBag().insertItem(this.getItem());
                }

                else
                {
                    messageUI.showMessage("恐惧让你无法注意到电脑里的照片。");
                    chara.getBag().insertItem(this.getItem());
                }
                chara.updateActionPoint(chara.getActionPoint() - SystemConstant.InverstActionPoint);
            }
            else
            {
                messageUI.ShowMessge("行动力不足，不能进行调查。", 1);
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

    public Item getItem(Character chara)
    {
        if (chara.isPlayer())
        {
            return null;
        }
        else
        {
            if (this.isEmpty)
            {
                return null;
            }
            else
            {
                this.isEmpty = true;
                return this.item;

            }
        }
    }

    private Item getItem()
    {
        this.isEmpty = true;
        return this.item;
    }

    // Use this for initialization
    void Start()
    {
        roundController = FindObjectOfType<RoundController>();
        messageUI = FindObjectOfType<MessageUI>();
        this.isEmpty = false;
        this.listenRoll = false;
        this.phase = 1;
        uiManager = FindObjectOfType<RollDiceUIManager>();
        item = new ItemPotion(ItemConstant.ITEM_CODE_SPEC_Y0006
            , ItemDesConstant.ITEM_CODE_SPEC_Y0006_NAME, ItemDesConstant.ITEM_CODE_SPEC_Y0006_NAME);

    }
    private int rollValue;

    // Update is called once per frame
    void Update()
    {

    }

}
