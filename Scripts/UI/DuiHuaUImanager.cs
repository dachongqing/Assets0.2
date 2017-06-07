using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DuiHuaUImanager : MonoBehaviour,IPointerClickHandler {

	private Vector3 showPos = new Vector3(69, -193, 0);
	private Vector3 hidePos = new Vector3(69, 1198, 0);

	//对话界面
	public GameObject UIduihua;

	public Image lihui;

	public Text duihua;

	private string[] content;

	public void showDuiHua(string url,string[] content) {
		UIduihua.SetActive (true);
		UIduihua.transform.localPosition = showPos;

		Sprite s = 	Resources.Load (url, typeof(Sprite)) as Sprite;
		this.content = content;
		lihui.overrideSprite = s;
		duihua.text = content[0];
		clickCount = 1;
	}

	// Use this for initialization
	void Start () {
		//lihui = UIduihua.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	int clickCount;
	public void OnPointerClick(PointerEventData eventData)
	{
		Debug.Log ("you click me");
		if (clickCount >= this.content.Length) {
			UIduihua.SetActive (false);
			UIduihua.transform.localPosition = hidePos;
		} else {
			duihua.text = content[clickCount];
			clickCount++;
		}


	}

}
