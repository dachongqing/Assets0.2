﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : CommonThing
{
    public override void doClick()
    {
        
    }

    public override void init(int[] xyz)
    {
        this.setRoom(xyz);
        this.setThingCode(ThingConstant.BED_01_CODE);
        
    }

    // Use this for initialization
    void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
