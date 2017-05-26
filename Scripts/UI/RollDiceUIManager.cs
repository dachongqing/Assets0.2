using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollDiceUIManager : MonoBehaviour {

    private Player ply;

    private DiceRollCtrl diceRoll;

    public Text text;
	public GameObject UIrollPlane;
	public GameObject rollCloseGO;

  // Use this for initialization
    void Start () {
        ply = FindObjectOfType<Player>();
        diceRoll = FindObjectOfType<DiceRollCtrl>();
		UIrollPlane.SetActive (false);
    }

    void Update()
    {
        text.text = "当前行动力:" + ply.getActionPoint();
    }

	/// <summary>
	/// 显示roll点界面，关闭按钮启用
	/// </summary>
	public void showRollPlaneCanClose()
    {

        if (ply.ActionPointrolled())
        {
            //弹出roll点界面
            UIrollPlane.SetActive(true);
		    rollCloseGO.SetActive (true);
        }
        else
        {
            Debug.Log("你已经丢过行动力骰子");
        }
    }

	/// <summary>
	/// 显示roll点界面，关闭按钮不启用
	/// </summary>
	public void showRollPlaneNoClose()
	{//弹出roll点界面
		UIrollPlane.SetActive (true);
		//不能关闭该界面
		rollCloseGO.SetActive (false);
	}

	/// <summary>
	/// 禁用roll点界面
	/// </summary>
	public void closeRollPlane()
	{
		UIrollPlane.SetActive (false);
		rollCloseGO.SetActive (false);
	}

	/// <summary>
	/// 为了增加行动力roll点
	/// </summary>
	public void rollForActionPoint()
	{
	
			//int speed = ply.getAbilityInfo()[1] + ply.getEffectBuff();
			int speed = ply.getAbilityInfo()[1];
			int res = diceRoll.calculateDice( speed, 0);
			ply.updateActionPoint(res);
			ply.setActionPointrolled(false);
			//show ui message
			//text.text = "行动力:" + res;
		
	}

	/// <summary>
	/// 为了事件判定roll点
	/// </summary>
	public void rollForJugement()
	{
		
	}
}
