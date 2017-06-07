using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Box : MonoBehaviour {

	private Item item;

	private bool isEmpty;

	private bool listenRoll;

	private RoundController roundController;

	private MessageUI messageUI;

	private int phase;

	private int maxValue;

	private RollDiceUIManager uiManager;


	void OnMouseDown()

	{
		
		Debug.Log("click a barrel");
		if (this.isEmpty)
		{
			messageUI.ShowMessge("里面是空的", 1);
		}

		else {
			NPC chara =  (NPC)roundController.getCurrentRoundChar();
			
			if (chara.getActionPoint () >= 2) {
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
				else {
					messageUI.ShowMessge("因为你的知识低下的关系，没能看懂任何内容", 1);
				}
				chara.updateActionPoint (chara.getActionPoint () - 2);
			} else {
				messageUI.ShowMessge("行动力不足，不能进行调查.", 1);
			}


		}
	}

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
			if (this.isEmpty) {
				return null;
			} else {
				this.isEmpty = true;
				return this.item;
			
			}
		}
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
		item = new ItemTask(ItemConstant.ITEM_CODE_SPEC_00001
			,"带血的内容：你需要用活血的几十来开始剧情");

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
