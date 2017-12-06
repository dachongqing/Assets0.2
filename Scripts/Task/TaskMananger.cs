using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskMananger : MonoBehaviour {


    private List<TaskInterface> regisitedTasks = new List<TaskInterface>();

    private DuiHuaUImanager duiHuaUImanager;

    private RoundController roundController;

    private TaskInterface storyTask;

    private List<TaskInfo> historyTaskList = new  List<TaskInfo>();

    public bool neworLoad;

    public void UpdateHistoryTask(TaskInterface task)
    {
        bool isExsit = false;

        foreach (TaskInfo ti in historyTaskList) {
            if (ti.TaskCode == task.getTaskCode()) {
                isExsit = true;
                ti.TaskStatus = task.getTaskStatus();
            }
        }

        if (!isExsit) {
            TaskInfo ti = new TaskInfo();
            ti.TaskStatus = task.getTaskStatus();
            ti.TaskCode = task.getTaskCode();
            this.historyTaskList.Add(ti);
        }
       
    }

    public List<TaskInfo> getHistoryTask() {
        return this.historyTaskList;
    }


    public void regisisterStoryTask(TaskInterface task)
    {
        this.storyTask = task;
    }

    public TaskInterface getStoryTask() {
        return this.storyTask;
    }

    public bool regisisterTask(TaskInterface task) {
        if (regisitedTasks.Count > 3) {
            duiHuaUImanager = FindObjectOfType<DuiHuaUImanager>();
            roundController = FindObjectOfType<RoundController>();
            duiHuaUImanager.showDuiHua(roundController.getPlayerChara().getLiHuiURL(), new string[] { "任务有点多了，先完成前面的吧。" }, 0);
            return false;
        }


        if (this.getTaskByTaskCode(task.getTaskCode()) != null || task.getTaskStatus() == TaskConstant.STATUS_INDALID)
        {
            Debug.Log("cant regisiter task is " + task.getTaskCode());
            return false;
        }
        else {
            Debug.Log("regisiter task is " + task.getTaskCode());
            regisitedTasks.Add(task);
            return true;
        }     
    }

    public bool checkTaskDone(TaskInterface task) {

        if (this.regisitedTasks.Contains(task)) {
           List<TaskItemInterface> items =  task.getTaskItems();
            foreach (TaskItemInterface i in items) {
                if (!i.isCompleted()) {
                    //Debug.Log("你任务没有做完！");
                    return false;
                }
            }
            return true;

        } else {

           // Debug.Log("你没有这个任务！");
            return false;
        }
    }

    public List<TaskInterface> getRegisitedTasks() {
        return regisitedTasks;
    }



    // Use this for initialization
    void Start () {

        if (!this.neworLoad) {
            string datapath = Application.persistentDataPath + "/Save/SaveData0.sav";
            SaveData data = (SaveData)IOHelper.GetData(datapath, typeof(SaveData));

            if (data.Tasks.Count > 0)
            {
                foreach (TaskInfo ti in data.Tasks)
                {
                    TaskAward award = new TaskAward();
                    award.setAttriAwards(ti.AwardAttr[0], ti.AwardAttr[1], ti.AwardAttr[2], ti.AwardAttr[3]);
                    List<Item> awardsItems = new List<Item>();
                    foreach (ItemInfo item in ti.Items)
                    {
                        if (item.Type == ItemConstant.ITEM_TYPE_POTION)
                        {
                            awardsItems.Add(new ItemPotion(item.Code, item.Name, item.Desc));
                        }
                    }
                    List<TaskItemInterface> taskItems = new List<TaskItemInterface>();
                    foreach (TaskItemInfo itemInfo in ti.TaskItems)
                    {
                        if (itemInfo.ItemCode == TaskConstant.TASK_ITEM_CODE_SAVEORKILL)
                        {
                            taskItems.Add(new KillOrSaveTaskItem(itemInfo.ItemDesc));
                        }
                        else if (itemInfo.ItemCode == TaskConstant.TASK_ITEM_CODE_KILL)
                        {
                            taskItems.Add(new KillSomethingTaskItem(itemInfo.ItemDesc));
                        }
                        else if (itemInfo.ItemCode == TaskConstant.TASK_ITEM_CODE_FIND)
                        {
                            taskItems.Add(new FindSomethingTaskItem(itemInfo.ItemDesc));
                        }
                    }


                    award.setItemAwards(awardsItems);
                    if (ti.TaskType == TaskConstant.TASK_TYPE_01)
                    {

                        FindSometingTask ft = new FindSometingTask(ti.TaskCode, ti.TaskType, award, null, ti.TaskDesc, ti.TaskDistrubtor,
                            ti.TaskOwner, taskItems, ti.TaskName, ti.TaskStatus);
                        this.regisisterTask(ft);
                    } else if (ti.TaskType == TaskConstant.TASK_TYPE_02) {
                        if (ti.TaskCode == TaskConstant.TASK_STORY_CODE_01) {
                            BlackSignTask ft = new BlackSignTask(ti.TaskCode, ti.TaskType, award, null, ti.TaskDesc, ti.TaskDistrubtor,
                           ti.TaskOwner, taskItems, ti.TaskName, ti.TaskStatus);
                            this.regisisterStoryTask(ft);
                        }
                    }


                }
            }
            else {
                Debug.Log("no task loaded");
            }
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        foreach (TaskInterface i in regisitedTasks)
        {
            foreach (TaskItemInterface t in i.getTaskItems())
            {

                t.checkItemComplelted();
            }
            
        }
    }

    public TaskInterface getTaskByTaskCode(string code)
    {

        foreach (TaskInterface i in regisitedTasks)
        {

            if (i.getTaskCode() == code) {

                return i;
            }
        }

        return null;
    }

    internal void removeTask(FindSometingTask task)
    {
        this.regisitedTasks.Remove(task);
    }
}
