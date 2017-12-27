using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Box : CommonThing
{

	private Item item;

	

	private bool listenRoll;

	private RoundController roundController;

	private MessageUI messageUI;

	private int phase;

	private int maxValue;

	private RollDiceUIManager uiManager;

    public GameObject minOperationBox;


    public override GameObject getOperationItem()
    {
        return minOperationBox;
    }

    public override void doMiniOperation()
    {
        doClick();
    }

    public override void doClick()
    {
        Debug.Log("click a barrel");
        if (getIsEmpty())
        {
            messageUI.ShowMessge("里面是空的", 1);
        }

        else
        {
            NPC chara = (NPC)roundController.getCurrentRoundChar();

            if (chara.getActionPoint() >= 2)
            {
                if (chara.getAbilityInfo()[2] >= 7)
                {
                    messageUI.ShowMessge("因为你的知识丰富，很容易就发现了带血的书", 1);
                    chara.getBag().insertItem(this.getItem());
                }
                else if (chara.getAbilityInfo()[2] >= 3)
                {
                    messageUI.ShowMessge("你注意到了带血的黑书，你需要对知识进行判断 大于5 才能看懂书的内容", 1);
                    this.maxValue = 7;
                    this.phase = 1;
                    listenRoll = true;
                }
                else
                {
                    messageUI.ShowMessge("因为你的知识低下的关系，没能看懂任何内容", 1);
                }
                chara.updateActionPoint(chara.getActionPoint() - 2);
            }
            else
            {
                messageUI.ShowMessge("行动力不足，不能进行调查.", 1);
            }


        }
    }


    /**
    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log(coll.gameObject.name);
        doClick();

    }
    void OnMouseDown()

    {

        if (!SystemUtil.IsTouchedUI())
        {

            doClick();

        }


    }**/

    private void openEvent(int rollValue) {
		if (this.maxValue <= rollValue)
		{
			NPC chara = (NPC)roundController.getCurrentRoundChar();
			chara.getBag().insertItem(this.getItem());
			messageUI.ShowMessge("你的知识刚好，刚好读懂", 1);
		}
		else {
			messageUI.ShowMessge("因为你的知识不够，没能读懂那个物品", 1);
		}
		this.listenRoll = false;
	}

	public Item getItem(Character chara) {
		if (chara.isPlayer ()) {
			return null;
		} else {
			if (getIsEmpty()) {
				return null;
			} else {
			   setIsEmpty(true);
               return this.item;
			
			}
		}
	}

	private Item getItem() {
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
        item = new ItemTask(ItemConstant.ITEM_CODE_SPEC_00001
            , "黑色的书", "带血的内容：你需要用活血的几十来开始剧情");
        this.setThingCode(ThingConstant.BOX_01_CODE);
        if (roundController.newOrLoad)
        {
            setIsEmpty(false);
        }
        else
        {
            this.loadInfo();
        }
    }

        // Use this for initialization
        void Start () {
		

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
				RollDiceParam param = new RollDiceParam(chara.getAbilityInfo()[2]);
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
