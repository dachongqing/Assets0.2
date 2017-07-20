using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsBoard : CommonThing
{

    private MessageUI messageUI;

    void OnMouseDown()

    {

        if (!SystemUtil.IsTouchedUI())
        {

            doClick();

        }


    }

    public override void doClick()
    {
        messageUI.showMessges(this.getClickMessage());

    }
    public override void init(int[] xyz)
    {
        
        messageUI = FindObjectOfType<MessageUI>();
    }

 }
