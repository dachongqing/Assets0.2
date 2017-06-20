using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemClickEvent : MonoBehaviour, IPointerClickHandler
{

    private int i;

    private BagUIManager bagUIManager;

    public void setI(int i ) {
        this.i = i;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // eventData.selectedObject.GetComponent<>();

        Debug.Log("你点击了第" + i + "个物品");
        bagUIManager.setSelectItem(i);

    }

    // Use this for initialization
    void Start () {
        bagUIManager = FindObjectOfType<BagUIManager>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
