using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonTaskItem : MonoBehaviour, TaskItemInterface
{
    private string itemDesc;

    private bool completed;

    private RoundController roundController;

    public CommonTaskItem(string itemDesc) {
        this.itemDesc = itemDesc;
    }

    public virtual void checkItemComplelted() {
       
    }

    public string getItemDesc()
    {
        return itemDesc;
    }

    public bool isCompleted()
    {
        return completed;
    }

    public void setCompleted(bool c) {
        this.completed = c;
    }

    public virtual string getItemCode() {
        return null;
    }

    public NPC getPlayer() {
        this.roundController = FindObjectOfType<RoundController>();
        return (NPC)this.roundController.getPlayerChara();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
    }
}
