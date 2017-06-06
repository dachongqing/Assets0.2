using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RollDiceUIManager : MonoBehaviour ,IPointerClickHandler
{

    //UI右下角的行动力UI文本
    [Tooltip("场景-Canvas-rightUIBG-rollDice-diceNum")] public Text text;
    //roll点界面
    [Tooltip("场景-Canvas-UIRollPlane")] public GameObject UIrollPlane;
    //roll点界面上的关闭按钮GO
    [Tooltip("场景-Canvas-UIRollPlane-BtnClose")] public GameObject rollCloseGO;
    //roll点界面上的roll点按钮GO
    [Tooltip("场景_Canvas_UIRollPlane-BtnRoll")] public GameObject rollStartGO;
    //消息界面
    [Tooltip("场景-Canvas-MasssgeUI")] public GameObject msgGO;
    //3面骰子起始点
    [Tooltip("场景-Canvas-UIRollPlane-D3_Point")] public Transform D3_point;
    //6面骰子起始点
    [Tooltip("场景-Canvas-UIRollPlane-D6_Point")] public Transform D6_point;
    [Tooltip("两个骰子的间距")] public float DiceWise = 30f;
    [Tooltip("文件夹-Resources-Dice3")] public GameObject Dice3Prefab;
    [Tooltip("文件夹-Resources-Dice6")] public GameObject Dice6Prefab;
    //	[Tooltip ("延时显示结果，请大于动画的1.5秒,默认2")]public float ShowResultDelay = 2f;
    //	[Tooltip ("结果之后延时roll点界面自动关闭,默认3")]public float AutoCloseDelay = 3f;
    [Tooltip("文件夹-Textures-Picture-6个骰子图片")] public Sprite[] DiceSprites;

    public Vector3 showPos = new Vector3(0, 0, 0);
    public Vector3 hidePos = new Vector3(0, 800, 0);

   // private Player ply;
    private DiceRollCtrl diceRoll;
    private MessageUI msgUI;
    private GameObject[] D3Array;
    //3面控制数组，用结果替换骰子图片
    private GameObject[] D6Array;
    //6面控制数组，用结果替换骰子图片

    private RollDiceResult rollDiceRs = new RollDiceResult();

    private RollDiceParam para;

    public void setRollDiceParam(RollDiceParam para) {
        this.para = para;
    }

    // Use this for initialization
    void Start()
    {
        //ply = FindObjectOfType<Player>();
        diceRoll = FindObjectOfType<DiceRollCtrl>();
        //		UIrollPlane.SetActive (false);
        msgUI = msgGO.GetComponent<MessageUI>();
    }

    void Update()
    {
       // text.text = "当前行动力:" + ply.getActionPoint();
    }

    /// <summary>
    /// 显示roll点界面，关闭按钮启用
    /// </summary>
    public void showRollDice()
    {
       
      
        //弹出roll点界面
        UIrollPlane.SetActive (true);
        UIrollPlane.transform.localPosition = showPos;
        //关闭按钮启用
        rollCloseGO.SetActive(true);
        //roll点按钮启用
        rollStartGO.SetActive(true);
       
    }

  
    /// <summary>
    /// 关闭roll点界面
    /// </summary>
    
    public void closeRollPlane()
    {
        if (!rollStartGO.activeSelf) {
            foreach (GameObject go in D3Array)
            {
                Destroy(go);
            }

            foreach (GameObject go in D6Array)
            {
                Destroy(go);
            }
        }
       
        //roll点界面禁用
        UIrollPlane.SetActive (false);
        UIrollPlane.transform.localPosition = hidePos;
        //关闭按钮禁用
        rollCloseGO.SetActive(false);
        //roll点按钮禁用
        rollStartGO.SetActive(false);

    }
    
    /// <summary>
    /// 为了增加行动力roll点
    /// </summary>
    public void rollForActionPoint()
    {
       
            //不能再roll
          //  ply.setActionPointrolled(false);

            rollStartGO.SetActive(false);

            //马山根据属性值计算roll点的得到了结果
//            int speed = ply.getAbilityInfo()[1] + ply.getEffectBuff();
            //int speed = ply.getAbilityInfo()[1];
            int res = diceRoll.calculateDice(this.para.getDiceNum(), 0);

            //根据属性值，播放几颗骰子的动画
            displayDices(this.para.getDiceNum(), 0);
            //稍后替换图片
            StartCoroutine(ChangeDicePicture(2f));
            //稍后出现信息提示
         //   StartCoroutine(DelayResult(res, 2.5f));
            this.rollDiceRs.setResult(res);
            this.rollDiceRs.setDone(true);
        //稍后自动关闭UI
        // StartCoroutine(DelayColseUI(5.5f));

    }
		
    /*
    IEnumerator DelayResult(int res, float ti)
    {
        yield return new WaitForSeconds(ti);
        //更新玩家的数据
        ply.updateActionPoint(res);
        //信息UI
        msgUI.ShowMessge("增加 " + res + " 点行动力", 0);
    }
    */

    /*
    IEnumerator DelayColseUI(float ti)
    {
        yield return new WaitForSeconds(ti);
        //销毁骰子
        foreach (GameObject go in D3Array)
        {
            Destroy(go);
        }

        foreach (GameObject go in D6Array)
        {
            Destroy(go);
        }

        closeRollPlane();
    }
    */
    public RollDiceResult getResult() {
       
        return this.rollDiceRs;
    }

    /// <summary>
    /// 显示不同个数的骰子roll点动画
    /// </summary>
    /// <param name="num3">Num3.</param>
    /// <param name="num6">Num6.</param>
    void displayDices(int num3, int num6)
    {
        //声明控制数组
        D3Array = new GameObject[num3];
        D6Array = new GameObject[num6];

        for (int i = 0; i < num3; i++)
        {
            //不同的颗数也会居中对齐
            Vector3 temPos = D3_point.localPosition;
            temPos.x += i * DiceWise;
            temPos.x -= (num3 / 2) * DiceWise;
            //生成一个骰子
            GameObject newDi = Instantiate(Dice3Prefab) as GameObject;
            newDi.GetComponent<RectTransform>().SetParent(UIrollPlane.transform);
            newDi.GetComponent<RectTransform>().localPosition = temPos;

            //放入控制数组中，以后要以结果替换图片
            D3Array[i] = newDi;
        }

        for (int i = 0; i < num6; i++)
        {
            //不同的颗数也会居中对齐
            Vector3 temPos = D6_point.localPosition;
            temPos.x += i * DiceWise;
            temPos.x -= (num6 / 2) * DiceWise;
            //生成一个骰子
            GameObject newDi = Instantiate(Dice6Prefab) as GameObject;
            newDi.GetComponent<RectTransform>().parent = UIrollPlane.transform;
            newDi.GetComponent<RectTransform>().localPosition = temPos;

            //放入控制数组中，以后要以结果替换图片
            D6Array[i] = newDi;
        }
    }

    IEnumerator ChangeDicePicture(float ti)
    {
        yield return new WaitForSeconds(ti);
        //		Debug.Log ("替换3面骰子图片");
        //取得3面结果数组
        int[] d3ResArray = diceRoll.getD3sResult();
        //根据结果替换显示的图片
        for (int i = 0; i < d3ResArray.LongLength; i++)
        {
            //播放动画，需要Animator组件，但同时也锁住了图片，导致不能替换
            D3Array[i].GetComponent<Animator>().enabled = false;

            switch (d3ResArray[i])
            {
                case 1:
                    D3Array[i].GetComponent<Image>().sprite = DiceSprites[0];
                    break;
                case 2:
                    D3Array[i].GetComponent<Image>().sprite = DiceSprites[1];
                    break;
                case 3:
                    D3Array[i].GetComponent<Image>().sprite = DiceSprites[2];
                    break;
                case 4:
                    D3Array[i].GetComponent<Image>().sprite = DiceSprites[3];
                    break;
                case 5:
                    D3Array[i].GetComponent<Image>().sprite = DiceSprites[4];
                    break;
                case 6:
                    D3Array[i].GetComponent<Image>().sprite = DiceSprites[5];
                    break;

            }
        }
			
        //取得3面结果数组
        int[] d6ResArray = diceRoll.getD6sResult();
        //根据结果替换显示的图片
        for (int i = 0; i < d6ResArray.LongLength; i++)
        {
            //播放动画，需要Animator组件，但同时也锁住了图片，导致不能替换
            D6Array[i].GetComponent<Animator>().enabled = false;

            switch (d6ResArray[i])
            {
                case 1:
                    D6Array[i].GetComponent<Image>().sprite = DiceSprites[0];
                    break;
                case 2:
                    D6Array[i].GetComponent<Image>().sprite = DiceSprites[1];
                    break;
                case 3:
                    D6Array[i].GetComponent<Image>().sprite = DiceSprites[2];
                    break;
                case 4:
                    D6Array[i].GetComponent<Image>().sprite = DiceSprites[3];
                    break;
                case 5:
                    D6Array[i].GetComponent<Image>().sprite = DiceSprites[4];
                    break;
                case 6:
                    D6Array[i].GetComponent<Image>().sprite = DiceSprites[5];
                    break;

            }
        }
    }

    public bool isClosedPlane() {
        return UIrollPlane.activeSelf;
    }

    public void OnPointerClick(PointerEventData eventData)
    {

       
        closeRollPlane();

    }
}   