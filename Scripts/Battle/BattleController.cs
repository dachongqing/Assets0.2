using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour {

    private bool isBattle;

    private Character fighter;

    private Character victim;

    public RollDiceUIManager uiManager;

    private DiceRollCtrl diceRoll = new DiceRollCtrl();

    private bool fighteEnd;

    private bool victimEnd;


    public void fighte(Character fighter, Character victim) {
        this.fighter = fighter;
        this.victim = victim;
        this.fighteEnd = false;
        this.victimEnd = false;
        this.isBattle = true;
        if (this.victim.isPlayer())
        {
            this.fighter.setWaitPlayer(true);
        }
            //开始打斗
            Debug.Log("显示打斗ui，准备开打");
    }

	// Use this for initialization
	void Start () {
        uiManager = FindObjectOfType<RollDiceUIManager>();
    }

    int attackValue;

    int defendValue;

    // Update is called once per frame
    void Update () {
        if (isBattle) {
            if ( !fighteEnd)
            {
                if (this.fighter.isPlayer() ) {
                    //弹出丢骰子ui
                    RollDiceParam param = new RollDiceParam(fighter.getAbilityInfo()[0]);
                    uiManager.setRollDiceParam(param);

                    if (!uiManager.getResult().getDone()) {
                         uiManager.showRollDice();

                    }else if (uiManager.getResult().getDone()) {
                        attackValue = uiManager.getResult().getResult();
                        fighteEnd = true;
                        Debug.Log("玩家 打出了 " + attackValue + " 伤害");
                    }

                }
                else {
                    int str = this.fighter.getAbilityInfo()[0];
                    attackValue = diceRoll.calculateDice(str);
                    Debug.Log("NPC 打出了 " + attackValue + " 伤害");
                    fighteEnd = true;
                }
            }

            if ( fighteEnd && !victimEnd)
            {
                if (this.victim.isPlayer()) {
                   // this.fighter.setWaitPlayer(true);
                    RollDiceParam param = new RollDiceParam(victim.getAbilityInfo()[0]);
                    uiManager.setRollDiceParam(param);
                    if (!uiManager.getResult().getDone())
                    {
                        uiManager.showRollDice();

                    }
                    else if (uiManager.getResult().getDone())
                    {
                        defendValue = uiManager.getResult().getResult();
                        victimEnd = true;
                        Debug.Log("玩家 打出了 " + defendValue + " 伤害");
                    }
                }
                else
                {
                    int str = this.victim.getAbilityInfo()[0];
                    defendValue = diceRoll.calculateDice(str);
                    
                    Debug.Log("NPC 打出了 "+ defendValue + " 伤害");

                    victimEnd = true;
                }
                    //弹出丢骰子ui
            }
            if (fighteEnd && victimEnd) {
                if (attackValue > defendValue) {
                    this.victim.getAbilityInfo()[4] = this.victim.getAbilityInfo()[4] - (attackValue - defendValue);
                    Debug.Log("打斗结算：被攻击者受到了 " + (attackValue - defendValue) + " 点伤害， 还剩下 " + this.victim.getAbilityInfo()[4]);
                } else if (attackValue < defendValue) {
                    this.fighter.getAbilityInfo()[4] = this.fighter.getAbilityInfo()[4] - (defendValue - attackValue);
                    Debug.Log("打斗结算：攻击者受到了 " + (defendValue - attackValue) + " 点伤害， 还剩下 " + this.fighter.getAbilityInfo()[4]);
                }
                this.fighter.setWaitPlayer(false);
                isBattle = false;
            }
        }
	}
}
