using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class MessageUI : MonoBehaviour, IPointerClickHandler
{
	[Tooltip("显示时长，默认2")]public float ShowTime=4f;
	[Tooltip("显示时的坐标,默认(0,-140,0)")]public Vector3 showPos=new Vector3(69, -193, 0);
	[Tooltip("隐藏时的坐标(0,-500,0)")]public Vector3 hidPos=new Vector3(0,-500,0);
	[Tooltip("移动速度，默认1")]public float speed=1f;

	private bool isShow;
	private Text theText;

    private MessageResult messageResult = new MessageResult();


  

    // Use this for initialization
    void Start () {
		theText = GetComponentInChildren<Text> ();
        
    }

	/// <summary>
	/// 显示信息UI框,内容,延时几秒显示
	/// </summary>
	/// <param name="msg">Message.</param>
	/// <param name="time">Time.</param>
	public void ShowMessge(string msg,float time)
	{
       
        this.messageResult.setDone(false);  
		isShow = true;
		theText.text = msg;
      //  StartCoroutine (ShowDelay(msg,time));
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
        return messageResult;
    }

    void Update()
	{
		if (isShow) {
            this.transform.localPosition = Vector3.Lerp (showPos,this.transform.localPosition,speed*Time.deltaTime);
		} else {
           
			this.transform.localPosition = Vector3.Lerp (hidPos,this.transform.localPosition,speed*Time.deltaTime);
		}
	}

   public bool isClosed()
    {

        return !isShow;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
		Debug.Log ("click!");
            isShow = false;
            this.messageResult.setDone(true);
            this.messageResult.setResult("你选择了1");
        
    }
}
