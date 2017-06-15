using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaInfoManager : MonoBehaviour {

    private DuiHuaUImanager duiHuaUImanager;

    private Character chara;

    private string[] content;

    private Vector3 showPos = new Vector3(69, -193, 0);
    private Vector3 hidePos = new Vector3(69, 1198, 0);

    //对话界面
    public GameObject UIInfoMenu;

    public void showCharaInfoMenu(Character chara, string[] content) {
        this.chara = chara;
        this.content = content;
        UIInfoMenu.SetActive(true);
        UIInfoMenu.transform.localPosition = showPos;
    }


    public void clickDuiHua() {
        UIInfoMenu.SetActive(false);
        UIInfoMenu.transform.localPosition = hidePos;
        duiHuaUImanager.showDuiHua(chara.getLiHuiURL(), content);
    }

    public void clickBattle() {
        UIInfoMenu.SetActive(false);
        UIInfoMenu.transform.localPosition = hidePos;
    }

	// Use this for initialization
	void Start () {
        duiHuaUImanager = FindObjectOfType<DuiHuaUImanager>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
