using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BookTable : CommonThing
{

    private Item item;

    private bool listenRoll;

    private RoundController roundController;

    private MessageUI messageUI;

    private int phase;

    private int maxValue;

    private RollDiceUIManager uiManager;

    public GameObject minOperationBookTable;


    public override GameObject getOperationItem()
    {
        return minOperationBookTable;
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
            messageUI.showMessage("桌子上没有值得注意的东西。");
        }
        else
        {

            NPC chara = (NPC)roundController.getCurrentRoundChar();
            if (chara.getActionPoint() >= SystemConstant.InverstActionPoint)
            {
                if (chara.getAbilityInfo()[3] >= 5)
                {
                    messageUI.showMessage("桌子上一叠病例记录引起了你的注意。");
                    chara.getBag().insertItem(this.getItem());
                }
               
                else
                {
                    messageUI.showMessage("你觉得桌子上没有太重要的物品。");
                }
                chara.updateActionPoint(chara.getActionPoint() - SystemConstant.InverstActionPoint);
            }
            else
            {
                messageUI.showMessage("行动力不足，不能进行调查。");
            }
        }
    }

    /**void OnMouseDown()
    {
        if (!SystemUtil.IsTouchedUI())
        {

            doClick();

        }
    }
    **/
   

    private Item getItem()
    {
        setIsEmpty(true);
        return this.item;
    }

    public Item getItem(Character chara)
    {
        if (chara.isPlayer())
        {
            return null;
        }
        else
        {
            if (getIsEmpty())
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
    public override void init(int[] xyz)
    {
        roundController = FindObjectOfType<RoundController>();
        messageUI = FindObjectOfType<MessageUI>();
        this.setRoom(xyz);
        this.setIsEmpty(false);
        this.listenRoll = false;
        this.phase = 1;
        uiManager = FindObjectOfType<RollDiceUIManager>();
        item = new ItemTask(ItemConstant.ITEM_CODE_SPEC_Y0001
            , ItemDesConstant.ITEM_CODE_SPEC_Y0001_NAME, ItemDesConstant.ITEM_CODE_SPEC_Y0001_DES);
        this.setThingCode(ThingConstant.BOOK_TABLE_01_CODE);
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
    }
    private int rollValue;

    // Update is called once per frame
    void Update()
    {
        
    }
}
