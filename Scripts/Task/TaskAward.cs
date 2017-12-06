using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskAward : MonoBehaviour,TaskAwardInterface
{

    private List<Item> items;

    private int str;    
    private int spe;
    private int san;
    private int inte;
    private RoundController roundController;

    public void executeAwards()
    {
        this.roundController = FindObjectOfType<RoundController>();
        NPC player = (NPC)this.roundController.getPlayerChara();
        if (this.items != null) {
            foreach (Item item in this.items)
            {
                player.getBag().insertItem(item);
            }
        }
        player.getAbilityInfo()[0] = player.getAbilityInfo()[0] + str;
        player.getAbilityInfo()[1] = player.getAbilityInfo()[0] + spe;
        player.getAbilityInfo()[2] = player.getAbilityInfo()[0] + inte;
        player.getAbilityInfo()[3] = player.getAbilityInfo()[0] + san;
        player.getMaxAbilityInfo()[0] = player.getMaxAbilityInfo()[0] + str;
        player.getMaxAbilityInfo()[1] = player.getMaxAbilityInfo()[0] + spe;
        player.getMaxAbilityInfo()[2] = player.getMaxAbilityInfo()[0] + inte;
        player.getMaxAbilityInfo()[3] = player.getMaxAbilityInfo()[0] + san;
    }

    public void setAttriAwards(int str, int spe, int san, int inte)
    {
        this.str = str;
        this.spe = spe;
        this.san = san;
        this.inte = inte;

    }

    public int[] getTaskAwardAttrInfo() {
        return new int[] { this.str, this.spe, this.san, this.inte };
    }

    public List<Item> getTaskAwardItemInfo() {
        return this.items;
    }

    public void setItemAwards(List<Item> items)
    {
        this.items = items;
    }

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
