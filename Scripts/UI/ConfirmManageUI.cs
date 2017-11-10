using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ConfirmManageUI : MonoBehaviour {

    private Vector3 showPos = new Vector3(69, -163, 0);
    private Vector3 hidePos = new Vector3(69, 1198, 0);

    //对话界面
    public GameObject uiConfirm;

    public Image lihui;

    public Text content;

    public Button YesButton;

    public Button NoButton;

    private bool result; 

    public void clickYes()
    {
        uiConfirm.SetActive(false);
        this.result = true;
        uiConfirm.transform.localPosition = hidePos;
    }

    public void clickNo()
    {
        uiConfirm.SetActive(false);
        this.result = false;
        uiConfirm.transform.localPosition = hidePos;
      
    }

    public bool getResult() {
        return this.result;
    }


    public void showConfirm(string url, string msg)
    {
        //Debug.Log("click 3");
        uiConfirm.SetActive(true);
        uiConfirm.transform.localPosition = showPos;
        Sprite s = Resources.Load(url, typeof(Sprite)) as Sprite;
      
        lihui.overrideSprite = s;
        content.text = msg;
      

    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
