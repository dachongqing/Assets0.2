﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsBoard : CommonThing
{

    private MessageUI messageUI;

    public GameObject minOperationNewsBoard;


    public override GameObject getOperationItem()
    {
        return minOperationNewsBoard;
    }

    public override void doMiniOperation()
    {
        doClick();
    }

    /**
    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log(coll.gameObject.name);
        doClick();

    }
    void OnMouseDown()

    {

        if (!SystemUtil.IsTouchedUI())
        {

            doClick();

        }


    }**/

    public override void doClick()
    {
        messageUI.showMessges(this.getClickMessage());

    }
    public override void init(int[] xyz)
    {
        
        messageUI = FindObjectOfType<MessageUI>();
    }

 }
