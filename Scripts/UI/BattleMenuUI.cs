using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMenuUI : MonoBehaviour {

    public Text enemyPointText;

    public Image enemyPic;

    public Image enemyProfile;

    public Text enemyNameText;

    public Slider enemySanInfo;

    public Slider enemyIntInfo;

    public Slider enemySpeInfo;

    public Slider enemyStrInfo;

    public Slider charaSanInfo;

    public Slider charaIntInfo;

    public Slider charaSpeInfo;

    public Slider charaStrInfo;

    public Image charaProfile;

    public Text charaNameText;

    public Text remainTime_Text;

    public Text charaPoint_Text;

    public GameObject battleMenuUI;
    public GameObject battleButton;
    public GameObject useButton;
    public GameObject BagItemMenuUI;

    private Vector3 showPos = new Vector3(4, 0, 0);
    private Vector3 hidePos = new Vector3(-1515, 10, 0);

    private bool isBattle;

    private Character fighter;

    private Character victim;

    public RollDiceUIManager uiManager;

    private DiceRollCtrl diceRoll = new DiceRollCtrl();

    private bool fighteEnd;
    private bool victimEnd;

    private BagUIManager bagUIManager;
    private MessageUI messageUI;
    private Character enemy;
    private Character chara;
    private int diceValue;
    private string resultMessage;
    public bool isFighter;
    

    public void showBag() {
       // battleMenuUI.SetActive(false);
        battleMenuUI.transform.localPosition = hidePos;
       
        bagUIManager.showBagItemUI(chara, enemy,this);
        
    }


    private void closeUI()
    {
        battleMenuUI.SetActive(false);
        battleMenuUI.transform.localPosition = hidePos;
    }

    public void rollDice()
    {
        RollDiceParam param = new RollDiceParam(fighter.getAbilityInfo()[0] + this.fighter.getDiceNumberBuffer());
        param.setDiceType("D2");
        // uiManager.setRollDiceParam(param);
        int result = uiManager.showRollDiceImmediately(param);       
        this.diceValue = result;
        this.battleButton.SetActive(false);
        this.useButton.SetActive(false);
        StartCoroutine(WaitAIAction(3.5f));
       
    }
    IEnumerator WaitAIAction(float ti)
    {
        yield return new WaitForSeconds(ti);
        if (this.isFighter)
        {
            this.fighteEnd = true;
        }
        else {
            this.victimEnd = true;
        }
    }

    IEnumerator WaitResultAction(float ti)
    {
        yield return new WaitForSeconds(ti);
        this.closeUI();
        messageUI.showMessage(this.resultMessage);
        
    }

    IEnumerator AutoAction(float ti)
    {
        yield return new WaitForSeconds(ti);
        if (BagItemMenuUI.activeSelf)
        {
            BagItemMenuUI.SetActive(false);
            battleMenuUI.transform.localPosition = showPos;
        }
        if (this.battleButton.activeSelf)
        {
            rollDice();
        }
    }

    public void showBattleUI(Character chara, Character enemy, bool isFighter) {
        this.enemy = enemy;
        this.chara = chara;
        this.isFighter = isFighter;
        this.second = 10;
        if (isFighter)
        {
            fighte(chara, enemy);
        } else
        {
            fighte(enemy,chara);
        }
        StartCoroutine(AutoAction(10f));


        battleMenuUI.SetActive(true);
        battleMenuUI.transform.localPosition = showPos;
        this.battleButton.SetActive(true);
        this.useButton.SetActive(true);
        Sprite enemyPicSprite = Resources.Load(enemy.getDeitalPic(), typeof(Sprite)) as Sprite;      
        enemyPic.overrideSprite = enemyPicSprite;
        Sprite enemyProfileSprite = Resources.Load(enemy.getProfilePic(), typeof(Sprite)) as Sprite;
        enemyProfile.overrideSprite = enemyProfileSprite;
        enemyNameText.text = enemy.getName();
        enemyStrInfo.value = enemy.getAbilityInfo()[0];
        enemySpeInfo.value = enemy.getAbilityInfo()[1];
        enemyIntInfo.value = enemy.getAbilityInfo()[2];
        enemySanInfo.value = enemy.getAbilityInfo()[3];


        Sprite charaProfileSprite = Resources.Load(chara.getProfilePic(), typeof(Sprite)) as Sprite;
        charaProfile.overrideSprite = charaProfileSprite;
        charaStrInfo.value = chara.getAbilityInfo()[0];
        charaSpeInfo.value = chara.getAbilityInfo()[1];
        charaIntInfo.value = chara.getAbilityInfo()[2];
        charaSanInfo.value = chara.getAbilityInfo()[3];
        charaNameText.text = chara.getName();
    }


  

   


    public void fighte(Character fighter, Character victim)
    {
        this.fighter = fighter;
        this.victim = victim;
        this.fighteEnd = false;
        this.victimEnd = false;
        this.isBattle = true;
        if (this.victim.isPlayer())
        {
            this.fighter.setWaitPlayer(true);
        }        
    }

    // Use this for initialization
    void Start()
    {
        uiManager = FindObjectOfType<RollDiceUIManager>();
        bagUIManager = FindObjectOfType<BagUIManager>();
        messageUI = FindObjectOfType<MessageUI>();
       
    }

    int attackValue;

    int defendValue;

    // Update is called once per frame
    private float totalTime = 0;
    private int second;

    void Update()
    {
        if (isBattle)
        {
            if (!fighteEnd)
            {
                if (this.fighter.isPlayer())
                {                    
                    attackValue = this.diceValue + this.fighter.getDiceValueBuffer();                 
                    charaPoint_Text.text = "我方伤害: " + attackValue;
                }
                else
                {
                    int str = this.fighter.getAbilityInfo()[0] + this.fighter.getDiceNumberBuffer();
                    attackValue = diceRoll.calculateDice(str) + this.fighter.getDiceValueBuffer();
                    Debug.Log("NPC 打出了 " + attackValue + " 伤害");
                    enemyPointText.text = "敌方伤害: " + attackValue;
                    fighteEnd = true;
                }
            }

            if (fighteEnd && !victimEnd)
            {
                if (this.victim.isPlayer())
                {                   
                    defendValue = this.diceValue + this.victim.getDiceValueBuffer();
                    charaPoint_Text.text = "我方伤害: " + defendValue;
                }
                else
                {
                    int str = this.victim.getAbilityInfo()[0] + this.victim.getDiceNumberBuffer();
                    defendValue = diceRoll.calculateDice(str) + this.victim.getDiceValueBuffer();
                    enemyPointText.text = "敌方伤害: " + defendValue;
                    Debug.Log("NPC 打出了 " + defendValue + " 伤害");
                    victimEnd = true;
                }
                
            }
            if (fighteEnd && victimEnd)
            {
                int trueDamge;
                if (attackValue > defendValue)
                {
                    trueDamge = (attackValue - defendValue) * this.fighter.getDamgeBuffer();
                    this.victim.getAbilityInfo()[0] = this.victim.getAbilityInfo()[0] - trueDamge;
                    Debug.Log("打斗结算：被攻击者受到了 " + trueDamge + " 点伤害， 还剩下 " + this.victim.getAbilityInfo()[0]);
                    if (this.isFighter)
                    {
                        resultMessage = "敌方受到了" + trueDamge + " 点伤害";
                    }
                }
                else if (attackValue < defendValue)
                {
                    trueDamge = (defendValue - attackValue) * this.victim.getDamgeBuffer();
                    this.fighter.getAbilityInfo()[0] = this.fighter.getAbilityInfo()[0] - trueDamge;
                    Debug.Log("打斗结算：攻击者受到了 " + trueDamge + " 点伤害， 还剩下 " + this.fighter.getAbilityInfo()[0]);
                    if (this.isFighter)
                    {
                        resultMessage = "我方受到了" + trueDamge + " 点伤害";
                    }
                } else
                {
                    if (this.isFighter)
                    {
                        resultMessage = "敌方受到了0点伤害";
                    } else
                    {
                        resultMessage = "我方受到了0点伤害";
                    }
                   
                }
                this.fighter.setWaitPlayer(false);
                isBattle = false;
                //this.closeUI();
                StartCoroutine(WaitResultAction(3.5f));
            }
        }
        totalTime += Time.deltaTime;
        if(second >0 && this.battleButton.activeSelf)
        {
            if (totalTime >= 1)
            {
                second--;
                remainTime_Text.text = "回合剩余时间：" + second + "秒";
                totalTime = 0;
            }
        }else
        {
            remainTime_Text.text = "";
        }
    }
}
