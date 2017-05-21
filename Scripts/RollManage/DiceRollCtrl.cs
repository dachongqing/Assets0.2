using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRollCtrl : MonoBehaviour {

//    [Range(1,10)]public int dice3Amount = 1;
//    [Range(1,10)]public int dice6Amount = 1;



    public int calculateDice(int totalDiceNum, int dic3, int dic6)
    {
        if ((dic3 + dic6) > totalDiceNum)
        {
			Debug.Log ("输入个数 和 实际个数 不一致");
            return 0;
        }


		int[] resD3Array = new int[dic3];
        int _RollD3resSUM = 0;
		int[] resD6Array = new int[dic6];
        int _RollD6resSUM = 0;
		for (int i=0; i < dic3; i++) {
            Dice tem = new Dice3();
            tem.Roll();
            resD3Array[i]=tem.getDiceRes();
            Debug.Log(tem.getDiceRes());
            _RollD3resSUM += tem.getDiceRes();
        }
		Debug.Log(dic3 + " 颗三面骰子的和 " + _RollD3resSUM);

		for (int i = 0; i < dic6; i++) {
            Dice tem = new Dice6();
            tem.Roll();
            resD6Array[i] = tem.getDiceRes();
            Debug.Log(tem.getDiceRes());
            _RollD6resSUM += tem.getDiceRes();

        }
		Debug.Log(dic6+ " 颗六面骰子的和 " + _RollD6resSUM);
        return _RollD3resSUM + _RollD6resSUM;

    }

	//非正式测试用，点击roll按钮，调用roll点功能
	

}
