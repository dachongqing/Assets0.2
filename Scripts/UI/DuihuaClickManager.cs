using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DuihuaClickManager : MonoBehaviour, IPointerClickHandler
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
    public void OnPointerClick(PointerEventData eventData)
    {

        duiHuaUImanager.getNextContent(clickCount);
        clickCount++;
    }

    public void startDuihua() {
        clickCount = 0;
    }

}
