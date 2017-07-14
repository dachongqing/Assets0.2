using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_deanRoom : CommonRoom
{
    public GameObject safeBox;

    public GameObject getSafeBox()
    {
        return safeBox;

    }
    // Use this for initialization
    void Start () {
        safeBox.GetComponent<SafeBox>().init(this.getXYZ());
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
