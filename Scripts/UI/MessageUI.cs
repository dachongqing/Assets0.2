using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class MessageUI : MonoBehaviour, IPointerDownHandler
{
	[Tooltip("显示时长，默认2")]public float ShowTime=4f;
	[Tooltip("显示时的坐标,默认(0,-140,0)")]private Vector3 showPos=new Vector3(73, 742, 0);
	[Tooltip("隐藏时的坐标(0,-500,0)")] private Vector3 hidPos=new Vector3(0, -1090, 0);
	[Tooltip("移动速度，默认1")]public float speed=1f;

	private bool isShow;
	private Text theText;

    public OptionalMessageUI messageUIOptions;


    private MessageResult messageResult = new MessageResult();

    private string[] content;

    private int clickCount;
    private MouseMoveManger mouseMoveManger;

    private bool isOptionalUIShow = false;

    // Use this for initialization
    void Start () {
		theText = GetComponentInChildren<Text> ();
        mouseMoveManger = FindObjectOfType<MouseMoveManger>();
    }

    public void showMessges(string msg, string[] options)
    {
        this.isOptionalUIShow = true;      
        messageUIOptions.showOptionalMessage(msg, options);
    }

    public void showMessage(string msg) {
        this.ShowMessge(msg, ShowTime);
    }

	/// <summary>
	/// 显示信息UI框,内容,延时几秒显示
	/// </summary>
	/// <param name="msg">Message.</param>
	/// <param name="time">Time.</param>
	public void ShowMessge(string msg,float time)
	{
       
        
        //theText.text = msg;
        //this.transform.localPosition = Vector3.Lerp(showPos, this.transform.localPosition, speed * Time.deltaTime);
        //  StartCoroutine (ShowDelay(msg,time));
        this.showMessges(new string[] { msg });
    }
		
	IEnumerator ShowDelay(string msg,float time)
	{
		yield return new WaitForSeconds (time);
		//StartCoroutine (DelayHide());
	}

	IEnumerator DelayHide()
	{
		yield return new WaitForSeconds (ShowTime);
		isShow = false;
     }

    public MessageResult getResult() {
        if (this.isOptionalUIShow)
        {
          
            return messageUIOptions.getResult();
        }
        else
        {
          
            return messageResult;

        }
    }

    void Update()
	{
		if (isShow) {
            
            this.transform.localPosition = Vector3.Lerp (showPos,this.transform.localPosition,speed*Time.deltaTime);
		} else {
           
            this.transform.localPosition = Vector3.Lerp (hidPos,this.transform.localPosition,speed*Time.deltaTime);
		}
	}

    public void showMessges(string[] message)
    {
        mouseMoveManger.updateLock(true);
        this.content = message;
        theText.text = this.content[0];
        this.clickCount = 1;
        this.messageResult.setDone(false);
        isShow = true;
        this.isOptionalUIShow = false;
    }

    public bool isClosed()
    {
        if (this.isOptionalUIShow)
        {
            return messageUIOptions.isClosed();
        }
        else {
            return !isShow;

        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
       
            if (this.content.Length <= this.clickCount)
            {
                mouseMoveManger.updateLock(false);
               
                isShow = false;
                this.messageResult.setDone(true);
                this.messageResult.setResult("你选择了1");
            }
            else
            {
                Debug.Log(this.content.Length + " OnPointerDown click! " + clickCount);
                theText.text = this.content[this.clickCount];
                this.clickCount++;
            }
       
        
        
    }

  

}
