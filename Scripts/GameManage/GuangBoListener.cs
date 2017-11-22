using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuangBoListener : MonoBehaviour {

  
    private System.Random random = new System.Random();
    Dictionary<Character, Queue<GuangBoMessage>> quere = new Dictionary<Character, Queue<GuangBoMessage>>();
    private DuiHuaUImanager duiHuaUImanager;
    private ConfirmManageUI confirmUI;
    private RoundController roundController;

    private GuangBoController guangBoController;
    private bool showConfirm = false;
    private string eventGuangBoCode = "";
    private bool needShowConfirm = false;
    private bool hasMsg;

    List<Character> keyList = new List<Character>();

    public bool CheckMsg() {
        return hasMsg;
    }

    public void cleanQuere() {
        quere.Clear();
        keyList.Clear();
    }

    public void insert(Character chara, GuangBoMessage msg) {
        if (quere.ContainsKey(chara))
        {
            quere[chara].Enqueue(msg);
        }
        else {
            Queue<GuangBoMessage> content = new Queue<GuangBoMessage>();
            content.Enqueue(msg);
            quere.Add(chara, content);
            keyList.Add(chara);
        }
      //  keyList = FunctionUnity<Character>.orderList(keyList);
    }

    // Use this for initialization
    void Start () {
        duiHuaUImanager = FindObjectOfType<DuiHuaUImanager>();
        roundController = FindObjectOfType<RoundController>();
        guangBoController = FindObjectOfType<GuangBoController>();
        confirmUI = FindObjectOfType<ConfirmManageUI>();
        //recevor = GetComponent<Animator>();
    }

    IEnumerator showMessageToPlay(Character chara,string[] msg, int pro)
    {
        yield return new WaitForSeconds(pro);
        
        duiHuaUImanager.showGuangBoDuiHua(chara.getLiHuiURL(), msg);
       
    }

    IEnumerator showMessageToSelf(string msg, int pro)
    {
        yield return new WaitForSeconds(pro);

        confirmUI.showConfirm(roundController.getPlayerChara().getLiHuiURL(), "我是不是要赶过去看一下？");

    }

    public void showMessageToPlay(Character chara, string[] msg)
    {
       
        duiHuaUImanager.showGuangBoDuiHua(chara.getLiHuiURL(), msg);

    }

    public void showGuangBoMessage() {

        if (duiHuaUImanager.isDuiHuaEnd()) {

           

            if (quere.Count > 0)
            {
                // quere.g
                Debug.Log("guangbo quere.Count " + quere.Count);
                Character key = keyList[0];

                if (quere[key].Count > 0)
                {
                    GuangBoMessage ms = quere[key].Dequeue();
                    Debug.Log(key.getName() + " guangbo msg " + ms.Massage[0]);
                    duiHuaUImanager.setDuiHuaEndFalse();
                     StartCoroutine(showMessageToPlay(key, ms.Massage, random.Next(1, 2)));
                    //showMessageToPlay(key, ms.Massage);
                    //  Debug.Log("guangbo quere[key].Count " + quere[key].Count);

                    if (guangBoController.getEventGuangBoMap().ContainsKey(ms.Code)) {
                        needShowConfirm = true;
                        this.eventGuangBoCode = ms.Code;
                       
                        Debug.Log(duiHuaUImanager.isDuiHuaEnd());
                       // confirmUI.showConfirm(roundController.getPlayerChara().getLiHuiURL(), "我是不是要赶过去看一下？");
                    }
                }
                else
                {
                    keyList.Remove(key);
                    quere.Remove(key);
                }

                // quere.Clear();
            }
            else {
                Debug.Log("没有广播信息了");
            }
        }
    }

    // Update is called once per frame
    void Update () {
        if (duiHuaUImanager.isDuiHuaEnd() && needShowConfirm) {
            StartCoroutine(showMessageToSelf("", random.Next(1, 2)));
            needShowConfirm = false;
            showConfirm = true;
        }

        if (showConfirm && duiHuaUImanager.isDuiHuaEnd()) {
            if (!confirmUI.isActiveAndEnabled) {
                showConfirm = false;
                Debug.Log(confirmUI.getResult());
                if (confirmUI.getResult()) {
                    // do action
                    guangBoController.doNow(this.eventGuangBoCode);
                }
                guangBoController.getEventGuangBoMap().Remove(this.eventGuangBoCode);
                needShowConfirm = false;
            }
        }

        if (quere.Count > 0) {
            hasMsg = true;
        } else {
            hasMsg = false;
        }

    }
}
