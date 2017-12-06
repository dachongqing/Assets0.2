using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskInfo  {

    private string taskId;

    private string taskCode;

    private string taskType;

    private int[] awardAttr;

    private List<ItemInfo> items;

    private string taskDesc;

    private string taskDistrubtor;

    private List<TaskItemInfo> taskItems;

    private string taskName;

    private string taskOwner;

    private string taskStatus;

    public string TaskId
    {
        get
        {
            return taskId;
        }

        set
        {
            taskId = value;
        }
    }

    public string TaskCode
    {
        get
        {
            return taskCode;
        }

        set
        {
            taskCode = value;
        }
    }

    public string TaskType
    {
        get
        {
            return taskType;
        }

        set
        {
            taskType = value;
        }
    }

   

    public string TaskDesc
    {
        get
        {
            return taskDesc;
        }

        set
        {
            taskDesc = value;
        }
    }

    public string TaskDistrubtor
    {
        get
        {
            return taskDistrubtor;
        }

        set
        {
            taskDistrubtor = value;
        }
    }

    public List<TaskItemInfo> TaskItems
    {
        get
        {
            return taskItems;
        }

        set
        {
            taskItems = value;
        }
    }

    public string TaskName
    {
        get
        {
            return taskName;
        }

        set
        {
            taskName = value;
        }
    }

    public string TaskOwner
    {
        get
        {
            return taskOwner;
        }

        set
        {
            taskOwner = value;
        }
    }

    public string TaskStatus
    {
        get
        {
            return taskStatus;
        }

        set
        {
            taskStatus = value;
        }
    }

   

    public int[] AwardAttr
    {
        get
        {
            return awardAttr;
        }

        set
        {
            awardAttr = value;
        }
    }

    public List<ItemInfo> Items
    {
        get
        {
            return items;
        }

        set
        {
            items = value;
        }
    }
}
