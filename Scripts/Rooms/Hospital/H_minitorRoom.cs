using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_minitorRoom : CommonRoom

{
    public GameObject computer;

    public GameObject getComputer() {
        return computer;
    }

    // Use this for initialization
    void Start () {
        computer.GetComponent<Computer>().init(this.getXYZ());
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
