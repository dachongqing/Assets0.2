using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionalMessageUI : MonoBehaviour {

    public Text OptionText1;

    public Text OptionText2;

    public Text OptionText3;

    public Text Des;

    public Button OptionBuuton1;

    public Button OptionBuuton2;

    public Button OptionBuuton3;

    [Tooltip("显示时长，默认2")] public float ShowTime = 4f;
    [Tooltip("显示时的坐标,默认(0,-140,0)")] private Vector3 showPos = new Vector3(73, 742, 0);
    [Tooltip("隐藏时的坐标(0,-500,0)")] private Vector3 hidPos = new Vector3(0, -1090, 0);
    [Tooltip("移动速度，默认1")] public float speed = 1f;

    private MessageResult messageResult = new MessageResult();

    private MouseMoveManger mouseMoveManger;

    private bool closed = true; 


    public void showOptionalMessage(string content,string[] options)
    {
        mouseMoveManger.updateLock(true);

        Des.text = content;

        OptionText1.text = options[0];

        OptionText2.text = options[1];

        OptionText3.text = options[2];

        this.transform.localPosition = Vector3.Lerp(showPos, this.transform.localPosition, speed * Time.deltaTime);

        this.closed = false;
    }

    // Use this for initialization
    void Start () {
        mouseMoveManger = FindObjectOfType<MouseMoveManger>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public MessageResult getResult()
    {
        return this.messageResult;
    }

    private void closeUI() {
        this.transform.localPosition = Vector3.Lerp(hidPos, this.transform.localPosition, speed * Time.deltaTime);
        mouseMoveManger.updateLock(false);
        this.closed = true;


    }

    public bool isClosed()
    {

        return this.closed;


    }

    public void clickButton1()
    {
        this.messageResult.setDone(true);
        this.messageResult.setResult("1");
        closeUI();

    }

    public void clickButton2()
    {
        this.messageResult.setDone(true);
        this.messageResult.setResult("2");
        closeUI();
    }

    public void clickButton3()
    {
        this.messageResult.setDone(true);
        this.messageResult.setResult("3");
        closeUI();
    }
}
