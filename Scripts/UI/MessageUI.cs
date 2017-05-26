using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageUI : MonoBehaviour {
	[Tooltip("显示时长，默认2")]public float ShowTime=2f;
	[Tooltip("显示时的坐标,默认(0,-140,0)")]public Vector3 showPos=new Vector3(0,-140,0);
	[Tooltip("隐藏时的坐标(0,-500,0)")]public Vector3 hidPos=new Vector3(0,-500,0);
	[Tooltip("移动速度，默认1")]public float speed=1f;

	private bool isShow;
	private Text theText;

	// Use this for initialization
	void Start () {
		theText = GetComponentInChildren<Text> ();
	}

	public void ShowMessge(string msg)
	{
		isShow = true;

		theText.text = msg;

		StartCoroutine (DelayHide());
	}

	IEnumerator DelayHide()
	{
		yield return new WaitForSeconds (ShowTime);
		isShow = false;
	}

	void Update()
	{
		if (isShow) {
			this.transform.localPosition = Vector3.Lerp (showPos,this.transform.localPosition,speed*Time.deltaTime);
		} else {
			this.transform.localPosition = Vector3.Lerp (hidPos,this.transform.localPosition,speed*Time.deltaTime);
		}
	}
}
