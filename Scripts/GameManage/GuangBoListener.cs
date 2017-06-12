using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuangBoListener : MonoBehaviour {

  
    private System.Random random = new System.Random();
    Dictionary<Character, Queue<string[]>> quere = new Dictionary<Character, Queue<string[]>>();
    private DuiHuaUImanager duiHuaUImanager;

    List<Character> keyList = new List<Character>();

    public void cleanQuere() {
        quere.Clear();
        keyList.Clear();
    }

    public void insert(Character chara, string[] msg) {
        if (quere.ContainsKey(chara))
        {
            quere[chara].Enqueue(msg);
        }
        else {
            Queue<string[]> content = new Queue<string[]>();
            content.Enqueue(msg);
            quere.Add(chara, content);
            keyList.Add(chara);
        }
        keyList = FunctionUnity<Character>.orderList(keyList);
    }

    // Use this for initialization
    void Start () {
        duiHuaUImanager = FindObjectOfType<DuiHuaUImanager>();

    }

    IEnumerator showMessageToPlay(Character chara,string[] msg, int pro)
    {
        yield return new WaitForSeconds(pro);
        
        duiHuaUImanager.showGuangBoDuiHua(chara.getLiHuiURL(), msg);
       
    }

    // Update is called once per frame
    void Update () {
        if (duiHuaUImanager.isDuiHuaEnd()) {
          
            if (quere.Count > 0 ) {
                // quere.g
                Debug.Log("guangbo quere.Count " + quere.Count);
                Character key = keyList[0];

                if (quere[key].Count > 0)
                {
                    string[] ms = quere[key].Dequeue();
                    Debug.Log("guangbo msg " + ms[0]);
                    duiHuaUImanager.setDuiHuaEndFalse();
                    StartCoroutine(showMessageToPlay(key, ms, random.Next(2, 5)));
                    Debug.Log("guangbo quere[key].Count " + quere[key].Count);
                }
                else {
                    keyList.Remove(key);
                    quere.Remove(key);
                }
                 
               // quere.Clear();
            }
        }
    }
}
