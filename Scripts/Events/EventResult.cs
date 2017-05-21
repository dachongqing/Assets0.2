using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventResult {

    private bool status;

    private string resultCode;


    public bool getStatus() {
        return this.status;
    }

    public string getResultCode()
    {
        return this.resultCode;
    }

    public void setStatus(bool status)
    {
        this.status = status;
    }

    public void setResultCode(string resultCode)
    {
        this.resultCode = resultCode;
    }

}
