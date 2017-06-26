using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDiceParam  {

    private int diceNum;

    private string diceType;

    private string defautType = "D2";

    public RollDiceParam(int diceNum) {
        this.diceNum = diceNum;
        this.diceType = defautType;
    }

    public string getDiceType()
    {
        return this.diceType;
    }

    public void setDiceType(string diceType)
    {
        this.diceType = diceType;
    }

    public int  getDiceNum() {
        return this.diceNum;
    }

    public void setDiceNum(int diceNum) {
        this.diceNum = diceNum;
    }
}
