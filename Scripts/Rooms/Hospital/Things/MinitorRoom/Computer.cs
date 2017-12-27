using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : CommonThing
{

    private Item item;

    private bool listenRoll;

    private RoundController roundController;

    private MessageUI messageUI;

    private int phase;

    private int maxValue;

    private RollDiceUIManager uiManager;

    public GameObject minOperationComputer;


    public override GameObject getOperationItem()
    {
        return minOperationComputer;
    }

    public override void doMiniOperation()
    {
        doClick();
    }

    public override void doClick()
    {
        Debug.Log("click a barrel");
        if (this.getIsEmpty())
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

   /** void OnMouseDown()
    {
        if (!SystemUtil.IsTouchedUI())
        {

            doClick();

        }
    } **/

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
        this.setIsEmpty(false);
        this.listenRoll = false;
        this.phase = 1;
        uiManager = FindObjectOfType<RollDiceUIManager>();
        item = new ItemTask(ItemConstant.ITEM_CODE_SPEC_Y0006
            , ItemDesConstant.ITEM_CODE_SPEC_Y0006_NAME, ItemDesConstant.ITEM_CODE_SPEC_Y0006_NAME);
        this.setThingCode(ThingConstant.COMPUTER_01_CODE);
        if (roundController.newOrLoad)
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
        roundController = FindObjectOfType<RoundController>();
        messageUI = FindObjectOfType<MessageUI>();
        this.setIsEmpty(false);
        this.listenRoll = false;
        this.phase = 1;
        uiManager = FindObjectOfType<RollDiceUIManager>();
        item = new ItemTask(ItemConstant.ITEM_CODE_SPEC_Y0006
            , ItemDesConstant.ITEM_CODE_SPEC_Y0006_NAME, ItemDesConstant.ITEM_CODE_SPEC_Y0006_NAME);
        this.setThingCode(ThingConstant.COMPUTER_01_CODE);
    }
    private int rollValue;

    // Update is called once per frame
    void Update()
    {

    }

}
