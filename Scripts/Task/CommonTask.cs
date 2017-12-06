using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonTask : MonoBehaviour,TaskInterface {

    private string taskId;

    private string taskCode;

    private string taskType;

    private TaskAwardInterface award;

    private List<TaskConditionSPInterface> conditionSp;

    private string taskDesc;

    private string taskDistrubtor;

    private List<TaskItemInterface> taskItems;

    private string taskName;

    private string taskOwner;

    private string taskStatus;

    public CommonTask(string taskCode, string taskType, TaskAwardInterface award, List<TaskConditionSPInterface> conditionSp,
        string taskDesc, string taskDistrubtor,string taskOwner, List<TaskItemInterface> taskItems, string taskName, string taskStatus)
    {
        this.taskCode = taskCode;
        this.taskType = taskType;
        this.award = award;
        this.conditionSp = conditionSp;
        this.taskDesc = taskDesc;
        this.taskDistrubtor = taskDistrubtor;
        this.taskOwner = taskOwner;
        this.taskItems = taskItems;
        this.taskName = taskName;
        this.taskStatus = taskStatus;
    }


    public TaskAwardInterface getTaskAwards()
    {
        return award;
    }

    public string getTaskCode()
    {
        return taskCode;
    }

    public List<TaskConditionSPInterface> getTaskConditionSP()
    {
        return conditionSp;
    }

    public string getTaskDes()
    {
        return taskDesc;
    }

    public string getTaskDistributor()
    {
        return taskDistrubtor;
    }

    public string getTaskId()
    {
        return taskId;
    }

    public List<TaskItemInterface> getTaskItems()
    {
        return taskItems;
    }

    public string getTaskName()
    {
        return taskName;
    }

    public string getTaskOwner()
    {
        return taskOwner;
    }

    public string getTaskStatus()
    {
        return taskStatus;
    }

    public string getTaskType()
    {
        return taskType;
    }

    public void setTaskAward(TaskAwardInterface taskAward)
    {
        this.award = taskAward;
    }

    public void setTaskDes(string des)
    {
        this.taskDesc = des;
    }

    public void setTaskDistributor(string taskDistributor)
    {
        this.taskDistrubtor = taskDistributor;
    }

    public void setTaskItem(List<TaskItemInterface> taskItem)
    {
        this.taskItems = taskItem;
    }

    public void setTaskStatus(string status)
    {
        this.taskStatus = status;
    }

    public void setTaskOwner(string owner) {
        this.taskOwner = owner;
    }



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
