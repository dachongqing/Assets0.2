using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatingTable : CommonThing
{

    private Item item;

    private bool listenRoll;

    private RoundController roundController;

    private MessageUI messageUI;

    private int phase;

    private int maxValue;

    private RollDiceUIManager uiManager;

    public override void doClick()
    {
        Debug.Log("click a barrel");
        if (this.getIsEmpty())
        {
            messageUI.showMessage("尸体已经被处理干净了。");
        }
        else
        {

            NPC chara = (NPC)roundController.getCurrentRoundChar();
            if (chara.getActionPoint() >= SystemConstant.InverstActionPoint)
            {
                if (chara.getAbilityInfo()[3] >= 7)
                {
                    messageUI.showMessage("你很镇静地看着尸体手臂上残留的脓包，然后用旁边的手术刀把残留物装进了瓶子里。");
                    chara.getBag().insertItem(this.getItem());
                }
                else if (chara.getAbilityInfo()[3] >= 4)
                {
                    messageUI.showMessage("你感觉到一阵恶心，但是你强忍住用旁边的手术刀试图把残留物收集起来，你需要对理智进行检测 大于6 才能收集残留物。");
                    this.maxValue = 6;
                    this.phase = 1;
                    listenRoll = true;
                }
                else
                {
                    messageUI.showMessage("尸体手臂上的脓包残留物让你感觉到一种亲切感。你小心翼翼地用手把它们收集起来");
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

    private void openEvent(int rollValue)
    {
        if (this.maxValue <= rollValue)
        {
            NPC chara = (NPC)roundController.getCurrentRoundChar();
            chara.getBag().insertItem(this.getItem());
            messageUI.showMessage("你花了点时间，强忍住呕吐感把残留物取下来装进了瓶子。");
        }
        else
        {
            messageUI.showMessage("你无法忍住残留物带来的恶臭感，一阵呕吐让你的刀子切烂了残留物，恶心的画面带来的冲击力对你的理智造成了伤害。");
            this.getItem();
            this.phase = 4;
            listenRoll = true;
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
            if (this.getIsEmpty())
            {
                return null;
            }
            else
            {
                setIsEmpty(true);
                return this.item;

            }
        }
    }

    private Item getItem()
    {
        setIsEmpty(true);
        return this.item;
    }

    public override void init(int[] xyz)
    {
        roundController = FindObjectOfType<RoundController>();
        messageUI = FindObjectOfType<MessageUI>();
        this.setRoom(xyz);
        this.listenRoll = false;
        this.phase = 1;
        uiManager = FindObjectOfType<RollDiceUIManager>();
        this.setThingCode(ThingConstant.OPERATING_TABLE_01_CODE);
        item = new ItemPotion(ItemConstant.ITEM_CODE_SPEC_Y0003
            , ItemDesConstant.ITEM_CODE_SPEC_Y0003_NAME, ItemDesConstant.ITEM_CODE_SPEC_Y0003_DES);
        if(roundController.newOrLoad)
        {
            this.setIsEmpty(false);
        }
        else
        {
            this.loadInfo();
        }
    }

        // Use this for initialization
        void Start()
    {
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
