using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageResult
{

    private bool isDone;

    private string messageResult;

    public MessageResult() {
        isDone = false;
    }

    public void setDone(bool isDone) {
        this.isDone = isDone;
    }

    public bool getDone() {
        return this.isDone;
    }

    public void setResult(string messageResult) {
        this.messageResult = messageResult;
    }
       
    public string getResult()
    {
        return this.messageResult;
    }
}
