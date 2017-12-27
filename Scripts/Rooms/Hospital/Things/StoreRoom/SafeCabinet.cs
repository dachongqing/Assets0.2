using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SafeCabinet : CommonThing
{

    private Item item;

    private bool listenRoll;

    private RoundController roundController;

    private MessageUI messageUI;

    private int phase;

    private int maxValue;

    private RollDiceUIManager uiManager;

    public GameObject minOperationKeysSafeCabinet;


    public override GameObject getOperationItem()
    {
        return minOperationKeysSafeCabinet;
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
            messageUI.showMessage("安全柜子是空的。");
        }
        else
        {

            NPC chara = (NPC)roundController.getCurrentRoundChar();
            if (chara.getActionPoint() >= SystemConstant.InverstActionPoint)
            {
                if (chara.getAbilityInfo()[3] >= 8)
                {
                    messageUI.showMessage("你平静地看着瓶子里还未孵化出来的蛛卵，小心翼翼地把它拿了出来。");
                    chara.getBag().insertItem(this.getItem());
                }
                else if (chara.getAbilityInfo()[3] >= 6)
                {
                    messageUI.showMessage("瓶子里未孵化出的蛛卵让你感到恐惧，你需要下很大的决心才能把它取出来。");
                    this.maxValue = 7;
                    this.phase = 1;
                    listenRoll = true;
                }
                else if (chara.getAbilityInfo()[3] >= 4)
                {
                    messageUI.showMessage("你似乎看见瓶子的蛛卵像人的心脏一样还在微微跳动，吓得你赶紧关上的柜门。");
                    this.maxValue = 7;
                    this.phase = 1;
                    listenRoll = true;
                }
                else
                {
                    messageUI.showMessage("你看见未孵化的蛛卵像你的兄弟一样，你毫不犹豫地拿出了瓶子。");
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
    }**/

    private void openEvent(int rollValue)
    {
        if (this.maxValue <= rollValue)
        {
            NPC chara = (NPC)roundController.getCurrentRoundChar();
            chara.getBag().insertItem(this.getItem());
            messageUI.showMessage("你战胜了恐惧，把瓶子安全地拿了出来。");
        }
        else
        {
            messageUI.showMessage("你好不容易拿出来了瓶子， 你突然感觉到瓶子里的蛛卵似乎还活着，而且时不时跳动，这吓你赶紧扔出了瓶子，等你回个神来，蛛卵已经不见了。");
            this.getItem();
            //this.phase = 4;
            //listenRoll = true;
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
        item = new ItemPotion(ItemConstant.ITEM_CODE_SPEC_Y0004
            , ItemDesConstant.ITEM_CODE_SPEC_Y0004_NAME, ItemDesConstant.ITEM_CODE_SPEC_Y0004_DES);
        this.setThingCode(ThingConstant.SAFE_CABINET_01_CODE);
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
