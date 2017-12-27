using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniOperation : MonoBehaviour {

    public GameObject owner;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {

        if (!SystemUtil.IsTouchedUI())
        {


            owner.GetComponent<BaseGameOject>().doMiniOperation();
        }
        else
        {
            Debug.Log("click ui");
        }

    }
}
