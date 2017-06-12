﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DuiHuaUImanager : MonoBehaviour
{ 

	private Vector3 showPos = new Vector3(69, -193, 0);
	private Vector3 hidePos = new Vector3(69, 1198, 0);

	//对话界面
	public GameObject UIduihua;

	public Image lihui;

	public Text duihua;

	private string[] content;

    private DuihuaClickManager duihuaClickManager;

    public void showDuiHua(string url,string[] content) {
		UIduihua.SetActive (true);
		UIduihua.transform.localPosition = showPos;

		Sprite s = 	Resources.Load (url, typeof(Sprite)) as Sprite;
		this.content = content;
        Debug.Log("content " + content.Length);
		lihui.overrideSprite = s;
		//duihua.text = content[0];
        duihuaClickManager.startDuihua(0);
        duiHuaEndFlag = false;


    }

    public void showGuangBoDuiHua(string url, string[] content)
    {
        UIduihua.SetActive(true);
        UIduihua.transform.localPosition = showPos;

        Sprite s = Resources.Load(url, typeof(Sprite)) as Sprite;
        this.content = content;
        Debug.Log("content " + content.Length);
        lihui.overrideSprite = s;
        duihua.text = content[0];
        duihuaClickManager.startDuihua(1);
        duiHuaEndFlag = false;


    }

    private bool duiHuaEndFlag = true;

    public void setDuiHuaEndFalse()
    {
        duiHuaEndFlag = false;
    }

    public bool isDuiHuaEnd() {
        return duiHuaEndFlag;
    }

	// Use this for initialization
	void Start () {
        //lihui = UIduihua.GetComponent<Image>();
        duihuaClickManager = FindObjectOfType<DuihuaClickManager>();

    }
	
	// Update is called once per frame
	void Update () {
       // Debug.Log("this.content :" + this.name);
	}

    public void getNextContent(int clickCount) {
		if (clickCount >= this.content.Length) {
			UIduihua.SetActive (false);
			UIduihua.transform.localPosition = hidePos;
            duihua.text = "";
            duiHuaEndFlag = true;
          //  return null;
        } else {
            Debug.Log("clickCount   " + clickCount);
			duihua.text = content[clickCount];
			clickCount++;
    	}

    }

	
	

}
