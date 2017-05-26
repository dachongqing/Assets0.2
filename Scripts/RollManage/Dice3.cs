using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice3 : Dice {

    int _DiceRes;

    void Dice.Roll() {
        _DiceRes = Random.Range(1, 4);
    }
    int Dice.getDiceRes() {
        return _DiceRes;
    }
}
