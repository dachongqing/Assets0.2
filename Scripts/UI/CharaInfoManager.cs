using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaInfoManager : MonoBehaviour {



    public Slider NPCStrSlider;

    public Slider NPCSpeSlider;

    public Slider NPCIntSlider;

    public Slider NPCSanSlider;

    public Text NPCName;

    public Text NPCDesc;



    private BattleController battleController;

    private RoundController roundController;

    private DuiHuaUImanager duiHuaUImanager;

    private BagUIManager bagUIManager;

    private Character chara;

    private string[] content;

    private Vector3 showPos = new Vector3(4, 0, 0);
    private Vector3 hidePos = new Vector3(-10, 0, 0);

    //对话界面
    public GameObject UIInfoMenu;

    public void showCharaInfoMenu(Character chara, string[] content) {
        if (chara.isPlayer()) {
            duiHuaUImanager.showDuiHua(chara.getLiHuiURL(), content);
        } else {

            this.chara = chara;
            this.content = content;
            UIInfoMenu.SetActive(true);
            UIInfoMenu.transform.localPosition = showPos;

            NPCStrSlider.value = this.chara.getAbilityInfo()[0];

            NPCSpeSlider.value = this.chara.getAbilityInfo()[1];

            NPCIntSlider.value = this.chara.getAbilityInfo()[2];

            NPCSanSlider.value = this.chara.getAbilityInfo()[3];

            NPCName.text = this.chara.getName();

            NPCDesc.text = this.chara.getDesc();
        }
    }


    public void clickDuiHua() {
        UIInfoMenu.SetActive(false);
        UIInfoMenu.transform.localPosition = hidePos;
        duiHuaUImanager.showDuiHua(chara.getLiHuiURL(), content);
    }

    public void clickBattle() {
        UIInfoMenu.SetActive(false);
        UIInfoMenu.transform.localPosition = hidePos;
        battleController.fighte(roundController.getPlayerChara(), chara);
    }

    public void clickUseItem()
    {
        UIInfoMenu.SetActive(false);
        UIInfoMenu.transform.localPosition = hidePos;
        bagUIManager.showBagItemUI();
    }

    public void close()
    {
        UIInfoMenu.SetActive(false);
        UIInfoMenu.transform.localPosition = hidePos;
    }

    // Use this for initialization
    void Start () {
        duiHuaUImanager = FindObjectOfType<DuiHuaUImanager>();
        roundController = FindObjectOfType<RoundController>();
        battleController = FindObjectOfType<BattleController>();
        bagUIManager = FindObjectOfType<BagUIManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
