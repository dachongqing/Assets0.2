using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollDiceUIManager : MonoBehaviour {

    private Player ply;

    private DiceRollCtrl diceRoll;

    public Text text;

  // Use this for initialization
    void Start () {
        ply = FindObjectOfType<Player>();
        diceRoll = FindObjectOfType<DiceRollCtrl>();

    }

    void Update()
    {
        text.text = "当前行动力:" + ply.getActionPoint();
    }



    public void rollBtnDown()
    {
        if (ply.ActionPointrolled())
        {
            //int speed = ply.getAbilityInfo()[1] + ply.getEffectBuff();
            int speed = ply.getAbilityInfo()[1];
            int res = diceRoll.calculateDice(speed, speed, 0);
            ply.updateActionPoint(res);
            ply.setActionPointrolled(false);
            //show ui message
            //text.text = "行动力:" + res;
        }
        else 
            {
            Debug.Log("你已经丢过行动力骰子");
        }
    }
}
