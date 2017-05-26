using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRollCtrl : MonoBehaviour {
	int[] resD3Array;
	int[] resD6Array;

    public int calculateDice(int totalDiceNum, int dic3, int dic6)
    {

        if ((dic3 + dic6) > totalDiceNum)
        {
			Debug.Log ("输入个数 和 实际个数 不一致");
            return 0;
        }


		resD3Array = new int[dic3];
        int _RollD3resSUM = 0;
		resD6Array = new int[dic6];
        int _RollD6resSUM = 0;
		for (int i=0; i < dic3; i++) {
            Dice tem = new Dice3();
            tem.Roll();
            resD3Array[i]=tem.getDiceRes();
//            Debug.Log(tem.getDiceRes());
            _RollD3resSUM += tem.getDiceRes();
        }
//		Debug.Log(dic3 + " 颗三面骰子的和 " + _RollD3resSUM);

		for (int i = 0; i < dic6; i++) {
            Dice tem = new Dice6();
            tem.Roll();
            resD6Array[i] = tem.getDiceRes();
//            Debug.Log(tem.getDiceRes());
            _RollD6resSUM += tem.getDiceRes();

        }
//		Debug.Log(dic6+ " 颗六面骰子的和 " + _RollD6resSUM);
		int _sum=_RollD3resSUM+_RollD6resSUM;
		Debug.Log(dic3+" 颗三面骰子和 "+dic6+" 颗六面骰子的结果为 "+_sum);
        return _RollD3resSUM + _RollD6resSUM;

    }
		
    //我只想丢dic3个骰子， 不关心是几面的
    public int calculateDice( int dic3)
    {
        
        int[] resD3Array = new int[dic3];
        int _RollD3resSUM = 0;
       
        for (int i = 0; i < dic3; i++)
        {
            Dice tem = new Dice3();
            tem.Roll();
            resD3Array[i] = tem.getDiceRes();
//            Debug.Log(tem.getDiceRes());
            _RollD3resSUM += tem.getDiceRes();
        }
        Debug.Log(dic3 + " 颗三面骰子的和 " + _RollD3resSUM);

      
        return _RollD3resSUM ;

    }

	/// <summary>
	/// 返回三面骰子的结果数组
	/// </summary>
	/// <returns>The d3s reslut.</returns>
	public int[] getD3sResult()
	{
		return resD3Array;
	}

	/// <summary>
	/// 返回六面骰子的结果数组
	/// </summary>
	/// <returns>The d6s result.</returns>
	public int[] getD6sResult()
	{
		return resD6Array;
	}
}
