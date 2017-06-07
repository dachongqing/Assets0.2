using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionPointUIManager : MonoBehaviour, IPointerClickHandler
{
    private Player ply;
    private MessageUI msgUI;
    private RollDiceUIManager rollUI;
    private bool isRoll;
    //UI右下角的行动力UI文本
    [Tooltip("场景-Canvas-rightUIBG-rollDice-diceNum")] public Text text;

    public void OnPointerClick(PointerEventData eventData)
    {
       
        if (ply.ActionPointrolled())
        {
			RollDiceParam para = new RollDiceParam(this.ply.getAbilityInfo()[1] + this.ply.getDiceNumberBuffer());
            rollUI.setRollDiceParam(para);
            rollUI.showRollDice();
            isRoll = true;
          
        }
        else
        {
            isRoll = false;
            msgUI.ShowMessge("你已经丢过行动力骰子了", 0);
        }
    }

    // Use this for initialization
    void Start () {
        ply = FindObjectOfType<Player>();
        msgUI = FindObjectOfType<MessageUI>(); 
        rollUI = FindObjectOfType<RollDiceUIManager>();
        isRoll = false;
        }
	
	// Update is called once per frame
	void Update () {
            if (isRoll) {
                if (rollUI.getResult().getDone())
                {
				ply.updateActionPoint(rollUI.getResult().getResult() + this.ply.getDiceValueBuffer());
                    ply.setActionPointrolled(false);
                //信息UI
				msgUI.ShowMessge("增加 " + (rollUI.getResult().getResult() + this.ply.getDiceValueBuffer()) + " 点行动力", 0);
                    isRoll = false;
                   // rollUI.closeRollPlane();
                }
                else {
                   // ply.setActionPointrolled(true);
                }
            }

        text.text = "当前行动力:" + ply.getActionPoint();
    }
}
