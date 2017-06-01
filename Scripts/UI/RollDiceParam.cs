using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDiceParam  {

    private int diceNum;

    public RollDiceParam(int diceNum) {
        this.diceNum = diceNum;
    }

    public int  getDiceNum() {
        return this.diceNum;
    }

    public void setDiceNum(int diceNum) {
        this.diceNum = diceNum;
    }
}
