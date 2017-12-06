using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour {

    private bool isBattle;

    private Character fighter;

    private Character victim;

    public RollDiceUIManager uiManager;

    private BattleMenuUI battleMenuUI;

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
        battleMenuUI = FindObjectOfType<BattleMenuUI>();
    }

    public void showBattleUI(Character chara1, Character chara2, bool isF)
    {
        Debug.Log("同房间战斗");
        battleMenuUI.showBattleUI(chara1, chara2, isF);
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

					RollDiceParam param = new RollDiceParam(fighter.getAbilityInfo()[0] + this.fighter.getDiceNumberBuffer());
                    uiManager.setRollDiceParam(param);

                    if (!uiManager.getResult().getDone()) {
                         uiManager.showRollDice();

                    }else if (uiManager.getResult().getDone()) {
						attackValue = uiManager.getResult().getResult() + this.fighter.getDiceValueBuffer();
                        fighteEnd = true;
                        Debug.Log("玩家 打出了 " + attackValue + " 伤害");
                    }

                }
                else {

					int str = this.fighter.getAbilityInfo()[0] + this.fighter.getDiceNumberBuffer();
					attackValue = diceRoll.calculateDice(str)  + this.fighter.getDiceValueBuffer();
						Debug.Log("NPC 打出了 " + attackValue + " 伤害");
						fighteEnd = true;
					

                }
            }

            if ( fighteEnd && !victimEnd)
            {
                if (this.victim.isPlayer()) {
                   // this.fighter.setWaitPlayer(true);
					RollDiceParam param = new RollDiceParam(victim.getAbilityInfo()[0] + this.victim.getDiceNumberBuffer());
                    uiManager.setRollDiceParam(param);
                    if (!uiManager.getResult().getDone())
                    {
                        uiManager.showRollDice();

                    }
                    else if (uiManager.getResult().getDone())
                    {
						defendValue = uiManager.getResult().getResult()  + this.victim.getDiceValueBuffer();
                        victimEnd = true;
                        Debug.Log("玩家 打出了 " + defendValue + " 伤害");
                    }
                }
                else
                {
					int str = this.victim.getAbilityInfo () [0] + this.victim.getDiceNumberBuffer ();
					defendValue = diceRoll.calculateDice(str) + this.victim.getDiceValueBuffer();
                    
                    Debug.Log("NPC 打出了 "+ defendValue + " 伤害");

                    victimEnd = true;
                }
                    //弹出丢骰子ui
            }
            if (fighteEnd && victimEnd) {
				int trueDamge;
                if (attackValue > defendValue) {
					trueDamge = (attackValue - defendValue) * this.fighter.getDamgeBuffer();
					this.victim.getAbilityInfo()[0] = this.victim.getAbilityInfo()[0] - trueDamge;
					Debug.Log("打斗结算：被攻击者受到了 " + trueDamge  + " 点伤害， 还剩下 " + this.victim.getAbilityInfo()[0]);
                } else if (attackValue < defendValue) {
					trueDamge = (defendValue - attackValue) * this.victim.getDamgeBuffer();
					this.fighter.getAbilityInfo()[0] = this.fighter.getAbilityInfo()[0] - trueDamge;
					Debug.Log("打斗结算：攻击者受到了 " + trueDamge+ " 点伤害， 还剩下 " + this.fighter.getAbilityInfo()[0]);
                }
                this.fighter.setWaitPlayer(false);
                isBattle = false;
            }
        }
	}
}
