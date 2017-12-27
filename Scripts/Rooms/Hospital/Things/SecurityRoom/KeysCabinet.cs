using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysCabinet : CommonThing
{

    private Item item;

    private bool listenRoll;

    private RoundController roundController;

    private MessageUI messageUI;

    private int phase;

    private int maxValue;

    private RollDiceUIManager uiManager;

    public GameObject minOperationKeysCabinet;


    public override GameObject getOperationItem()
    {
        return minOperationKeysCabinet;
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
            messageUI.showMessage("柜子里面是空的。");
        }
        else
        {

            NPC chara = (NPC)roundController.getCurrentRoundChar();
            if (chara.getActionPoint() >= SystemConstant.InverstActionPoint)
            {
                if (chara.getAbilityInfo()[0] >= 7)
                {
                    messageUI.showMessage("你感觉很轻松的就打开的柜子。");
                    chara.getBag().insertItem(this.getItem());
                }
                else if (chara.getAbilityInfo()[2] >= 5)
                {
                    messageUI.showMessage("打开柜子似乎有点费力，你需要对力量进行检测 大于5 才能取出里面的物品。");
                    this.maxValue = 5;
                    this.phase = 1;
                    listenRoll = true;
                }
                else
                {
                    messageUI.showMessage("你试图打开柜子，但是发现门很紧，你无法打开。");
                }
                chara.updateActionPoint(chara.getActionPoint() - SystemConstant.InverstActionPoint);
            }
            else
            {
                messageUI.ShowMessge("行动力不足，不能进行调查。", 1);
            }
        }
    }

    /**void OnMouseDown()
    {
        if (!SystemUtil.IsTouchedUI())
        {

            doClick();

        }
    }**/

    private void openEvent(int rollValue)
    {
        if (this.maxValue <= rollValue)
        {
            NPC chara = (NPC)roundController.getCurrentRoundChar();
            chara.getBag().insertItem(this.getItem());
            messageUI.showMessage("你花了点力气，还是打开门取出了那个物品。");
        }
        else
        {
            messageUI.showMessage("你试图打开柜子，但是发现门很紧，你无法打开。");
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
        this.setIsEmpty(false);
        this.listenRoll = false;
        this.phase = 1;
        uiManager = FindObjectOfType<RollDiceUIManager>();
        item = new ItemPotion(ItemConstant.ITEM_CODE_SPEC_Y0002
            , ItemDesConstant.ITEM_CODE_SPEC_Y0002_NAME, ItemDesConstant.ITEM_CODE_SPEC_Y0002_DES);
        this.setThingCode(ThingConstant.KEYS_CABINET_01_CODE);
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

        }
    }
}
