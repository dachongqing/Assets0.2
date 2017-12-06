using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSignTask :CommonTask {

    public BlackSignTask(string taskCode, string taskType, TaskAwardInterface award, List<TaskConditionSPInterface> conditionSp,
        string taskDesc, string taskDistrubtor, string taskOwner, List<TaskItemInterface> taskItems, string taskName,
        string taskStatus) : base(taskCode, taskType, award, conditionSp, taskDesc, taskDistrubtor, taskOwner, taskItems, taskName, taskStatus)     
    {

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
