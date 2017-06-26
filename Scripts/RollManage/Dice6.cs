using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice6 : Dice {

    int _DiceRes;

    void Dice.Roll()
    {
        _DiceRes = Random.Range(0, 7);
    }
    int Dice.getDiceRes()
    {
        return _DiceRes;
    }
}
