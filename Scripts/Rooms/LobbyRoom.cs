using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LobbyRoom : CommonRoom
{
    public GameObject Barrel;

    public GameObject getBarrel()
    {
        return Barrel;
    }

    void Start()
    {
        Debug.Log("房间内物品初始化");
        Barrel.GetComponent<Barrel>().init(getXYZ());
      
    }


}

