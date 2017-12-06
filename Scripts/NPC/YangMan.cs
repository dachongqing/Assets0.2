using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YangMan : CommonUser
{

    private FindSometingTask task;

    private string npcCode = "npc002";

    private CharaInfoManager charaInfoManager;

    private ConfirmManageUI confirmManageUI;

    private DuiHuaUImanager duiHuaUImanager;

    private RoundController roundController;

    private TaskMananger taskMananger;

    private bool showConfirm = false;

    private int status = 0;

    public new bool isPlayer()
    {
        return false;
    }

    public new string getLiHuiURL()
    {
        return "lihui/ren_wu_2";
    }

    public new string getDeitalPic()
    {
        return "detail/kate_detail01";
    }

    public new string getProfilePic()
    {
        return "detail/9";
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log(coll.gameObject.name);
        if (coll.gameObject.name == "Player")
        {
            if (this.task.getTaskStatus() == TaskConstant.STATUS_INIT)
            {
                string[] beginContent = new string[] {"朋友，我这里有一根烟，你想要吗？",
                    "这烟劲可大了，吸一口爽几天。。。。",
                    "看见旁边的老人了吗？他就是吸成这样的"};
                duiHuaUImanager.showDuiHua(this.getLiHuiURL(), beginContent, 0);
                showConfirm = true;
            }
            else if (this.task.getTaskStatus() == TaskConstant.STATUS_BEGIN)
            {

                if (this.taskMananger.checkTaskDone(this.task))
                {
                    string[] beginContent = new string[] {"嗯，不错！， 。。嗯。。好的了",
                        "我其实是个小偷， 帮你的东西拿来。。。"
                        };
                    duiHuaUImanager.showDuiHua(this.getLiHuiURL(), beginContent, 0);
                    this.task.getTaskAwards().executeAwards();
                    this.task.setTaskStatus(TaskConstant.STATUS_END);
                    this.taskMananger.removeTask(this.task);
                    this.taskMananger.UpdateHistoryTask(this.task);
                }
                else
                {
                    string[] beginContent = new string[] {"朋友。。这烟要钱的。。。。。。",
                        "咳咳咳。。。。"
                        };
                    duiHuaUImanager.showDuiHua(this.getLiHuiURL(), beginContent, 0);
                }
            }
            else if (this.task.getTaskStatus() == TaskConstant.STATUS_END)
            {
                string[] beginContent = new string[] {"你还想要？ ",
                    "没有了 没有了，，，",
                    "看看地上还有没有烟屁股吧。。"};
                duiHuaUImanager.showDuiHua(this.getLiHuiURL(), beginContent, 0);
            }
            else if (this.task.getTaskStatus() == TaskConstant.STATUS_INDALID)
            {
                string[] beginContent = new string[] { "滚开。" };
                duiHuaUImanager.showDuiHua(this.getLiHuiURL(), beginContent, 0);
            }


        }
    }



    private void initTask()
    {
        TaskAward award = new TaskAward();
        award.setAttriAwards(1, 1, 1, 1);
        string taskDesc = "我有些烟，能找些打火的吗？";
        string taskName = "借个火";
        List<TaskItemInterface> taskItems = new List<TaskItemInterface>();
        FindSomethingTaskItem cti = new FindSomethingTaskItem("找到一个火柴。");
        cti.setTargetItem(ItemConstant.ITEM_CODE_POTION_10001);
        FindSomethingTaskItem cti2 = new FindSomethingTaskItem("找到一个打火机。");
        cti.setTargetItem(ItemConstant.ITEM_CODE_POTION_00001);
        taskItems.Add(cti);
        taskItems.Add(cti2);
        this.task = new FindSometingTask(TaskConstant.TASK_CODE_02, TaskConstant.TASK_TYPE_01, award, null, taskDesc, npcCode, null, taskItems,
            taskName, TaskConstant.STATUS_INIT);
    }

    // Use this for initialization
    void Start()
    {
        confirmManageUI = FindObjectOfType<ConfirmManageUI>();
        duiHuaUImanager = FindObjectOfType<DuiHuaUImanager>();
        roundController = FindObjectOfType<RoundController>();
        taskMananger = FindObjectOfType<TaskMananger>();
        initTask();

        if (!roundController.newOrLoad)
        {
            SaveData data = (SaveData)IOHelper.GetData(Application.persistentDataPath + "/Save/SaveData0.sav", typeof(SaveData));
            foreach (TaskInfo ti in data.HistoryTasks)
            {
                if (ti.TaskCode == this.task.getTaskCode())
                {
                    this.task.setTaskStatus(ti.TaskStatus);
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (this.showConfirm == true && this.duiHuaUImanager.isDuiHuaEnd())
        {
            this.confirmManageUI.showConfirm(this.getLiHuiURL(), "朋友，打火机？ 火柴都行！？");
            this.status = 1;
            this.showConfirm = false;
        }

        if (this.status == 1 && !this.confirmManageUI.isActiveAndEnabled)
        {
            if (this.confirmManageUI.getResult())
            {
                // do action
                if (taskMananger.regisisterTask(this.task))
                {
                    this.task.setTaskStatus(TaskConstant.STATUS_BEGIN);
                    this.task.setTaskOwner(roundController.getCurrentRoundChar().getName());
                    this.taskMananger.UpdateHistoryTask(this.task);
                }
                this.status = 0;
            }
        }
    }
}