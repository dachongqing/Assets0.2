using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_dragRoom : CommonRoom
{

    public GameObject dragTable;

    public GameObject getDragTable() {
        return dragTable;
    }

    // Use this for initialization
    void Start () {
       // dragTable.GetComponent<DragTable>().setRoom(this.getXYZ());
        dragTable.GetComponent<DragTable>().init(this.getXYZ());
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
