using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_securityRoom : CommonRoom
{

    public GameObject keysCabinet;

    public GameObject getKeysCabinet() {
        return keysCabinet;
    }

    // Use this for initialization
    void Start () {
        keysCabinet.GetComponent<KeysCabinet>().init(this.getXYZ());
      
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
