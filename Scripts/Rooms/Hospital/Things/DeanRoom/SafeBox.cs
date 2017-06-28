using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeBox : MonoBehaviour {

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
            messageUI.showMessage("保险箱里面没有值得注意的物品。");
        }
        else
        {

            NPC chara = (NPC)roundController.getCurrentRoundChar();
            if (chara.getActionPoint() >= SystemConstant.InverstActionPoint)
            {
                if (chara.getAbilityInfo()[2] >= 7)
                {
                    messageUI.showMessage("你明锐的观察到密码键里有几个已经掉漆的数字，尝试了一会就打开了。");
                    chara.getBag().insertItem(this.getItem());
                }
                else 
                {
                    messageUI.showMessage("你发现保险箱是有密码的，你想用概率学进行密码穷举法破解，需要知识进行检测大于6才能打开保险箱。");
                    this.maxValue = 6;
                    this.phase = 1;
                    listenRoll = true;
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

    private void openEvent(int rollValue)
    {
        if (this.maxValue <= rollValue)
        {
            NPC chara = (NPC)roundController.getCurrentRoundChar();
            chara.getBag().insertItem(this.getItem());
            messageUI.showMessage("你花了点时间，试出了密码123456打开了保险箱。");
        }
        else
        {
            messageUI.showMessage("你试了几次密码都不对，保险箱自动加锁了，可能需要等会再试下。");           
        }
        this.listenRoll = false;
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
        item = new ItemPotion(ItemConstant.ITEM_CODE_SPEC_Y0007
            , ItemDesConstant.ITEM_CODE_SPEC_Y0007_NAME, ItemDesConstant.ITEM_CODE_SPEC_Y0007_DES);

    }
    private int rollValue;

    // Update is called once per frame
    void Update()
    {
        if (listenRoll)
        {

            if (phase == 1 && messageUI.getResult().getDone())
            {
                phase = 2;
            }

            if (phase == 2 && !uiManager.getResult().getDone() && messageUI.isClosed())
            {
                NPC chara = (NPC)roundController.getCurrentRoundChar();
                RollDiceParam param = new RollDiceParam(chara.getAbilityInfo()[0]);
                uiManager.setRollDiceParam(param);
                uiManager.showRollDice();
            }
            else if (phase == 2 && uiManager.getResult().getDone())
            {
                rollValue = uiManager.getResult().getResult();
                phase = 3;

            }
            else if (phase == 3 && !uiManager.isClosedPlane())
            {

                this.openEvent(rollValue);
            }

            if (phase == 4 && !uiManager.getResult().getDone() && messageUI.isClosed())
            {
                NPC chara = (NPC)roundController.getCurrentRoundChar();
                RollDiceParam param = new RollDiceParam(1);
                uiManager.setRollDiceParam(param);
                uiManager.showRollDice();
            }
            else if (phase == 4 && uiManager.getResult().getDone())
            {
                rollValue = uiManager.getResult().getResult();
                phase = 5;

            }
            else if (phase == 5 && !uiManager.isClosedPlane())
            {

                this.addtionalEvent(rollValue);
            }

        }
    }

    private void addtionalEvent(int rollValue)
    {
        NPC chara = (NPC)roundController.getCurrentRoundChar();
        chara.getAbilityInfo()[3] = chara.getAbilityInfo()[3] - rollValue;
    }
}
