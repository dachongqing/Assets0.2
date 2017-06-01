using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDiceResult  {

    private bool isDone;

    private int result;

    public RollDiceResult()
    {
        isDone = false;
    }

    public void setDone(bool isDone)
    {
        this.isDone = isDone;
    }

    public bool getDone()
    {
        return this.isDone;
    }

    public void setResult(int result)
    {
        this.result = result;
    }

    public int getResult()
    {
        setDone(false);
        return this.result;
    }
}
