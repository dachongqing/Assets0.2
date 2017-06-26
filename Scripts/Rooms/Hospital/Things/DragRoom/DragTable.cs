using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragTable : MonoBehaviour {

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
            messageUI.showMessage("桌子上没有值得注意的物品。");
        }
        else
        {

            NPC chara = (NPC)roundController.getCurrentRoundChar();
            if (chara.getActionPoint() >= SystemConstant.InverstActionPoint)
            {
                if (chara.getAbilityInfo()[2] >= 4)
                {
                    messageUI.showMessage("你注意到一瓶药水上面贴着蜘蛛的图标。");
                    chara.getBag().insertItem(this.getItem());
                }
              
                else
                {
                    messageUI.showMessage("桌子上一堆药品但是没有一个是你认识的。");
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
        item = new ItemPotion(ItemConstant.ITEM_CODE_SPEC_Y0005
            , ItemDesConstant.ITEM_CODE_SPEC_Y0005_NAME, ItemDesConstant.ITEM_CODE_SPEC_Y0005_NAME);

    }
    private int rollValue;

    // Update is called once per frame
    void Update()
    {
       
    }

   
}
