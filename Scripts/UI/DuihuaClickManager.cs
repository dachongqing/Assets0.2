using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DuihuaClickManager : MonoBehaviour, IPointerDownHandler
{

    public DuiHuaUImanager duiHuaUImanager;
    private int clickCount =0;
    // Use this for initialization
    void Start () {
        //  clickCount = 1;
        duiHuaUImanager = FindObjectOfType<DuiHuaUImanager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

  //  int clickCount;
    public void OnPointerDown(PointerEventData eventData)
    {

        duiHuaUImanager.getNextContent(clickCount);
        clickCount++;
    }

    public void startDuihua(int i ) {
        clickCount = i;
    }

    public void clear() {
        this.clickCount = 0;
    }

}
