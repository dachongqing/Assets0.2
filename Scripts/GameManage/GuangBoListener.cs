using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuangBoListener : MonoBehaviour {

    List<Character> guangBoQuere = new List<Character>();
    private System.Random random = new System.Random();

    private DuiHuaUImanager duiHuaUImanager;

    public void cleanQuere() {
        guangBoQuere.Clear();
    }

    public void insert(Character chara) {
        guangBoQuere.Add(chara);
    }

    // Use this for initialization
    void Start () {
        duiHuaUImanager = FindObjectOfType<DuiHuaUImanager>();

    }

    IEnumerator showMessageToPlay(Character chara,int pro)
    {
        yield return new WaitForSeconds(pro);
        
        duiHuaUImanager.showGuangBoDuiHua(chara.getLiHuiURL(), chara.getMessage());
       
    }

    // Update is called once per frame
    void Update () {
        if (duiHuaUImanager.isDuiHuaEnd()) {
            if (guangBoQuere.Count > 0 ) {
                int i = random.Next(0, guangBoQuere.Count);
                Character chara = guangBoQuere[i];
                guangBoQuere.Remove(chara);
                StartCoroutine(showMessageToPlay(chara, random.Next(2, 5)));
            }
        }
    }
}
