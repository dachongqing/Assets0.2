using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldMan : CommonUser {

    private FindSometingTask task;

    private string npcCode = "npc001";

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
            if (this.task.getTaskStatus() == TaskConstant.STATUS_INIT) {
                string[] beginContent = new string[] {"年轻人，我这里有一个宝贝，你想要吗？",
                    "这个宝贝的来历可大了，听我慢慢道来。。。。",
                    "咳咳咳，人老了，肺不行了，我需要补肺丸，年轻人，你能帮忙我买瓶补肺丸吗？"};
                duiHuaUImanager.showDuiHua(this.getLiHuiURL(),beginContent,0);
                showConfirm = true;
            } else if (this.task.getTaskStatus() == TaskConstant.STATUS_BEGIN) {

                if (this.taskMananger.checkTaskDone(this.task)) {
                    string[] beginContent = new string[] {"好人啊！， 。。嗯。。好的了",
                        "我其实是个牧师， 擅长祈祷，来， 我先给祈祷一下。。。"
                        };
                    duiHuaUImanager.showDuiHua(this.getLiHuiURL(), beginContent, 0);
                    this.task.getTaskAwards().executeAwards();
                    this.task.setTaskStatus(TaskConstant.STATUS_END);
                    this.taskMananger.removeTask(this.task);
                    this.taskMananger.UpdateHistoryTask(this.task);
                }
                else
                {
                    string[] beginContent = new string[] {"年轻人。。这个宝贝是这样来的。。。。",
                        "咳咳咳。。。。",
                        "咳咳咳。。。。血。。血都出来了。。。医生！"};
                    duiHuaUImanager.showDuiHua(this.getLiHuiURL(), beginContent, 0);
                }
            } else if (this.task.getTaskStatus() == TaskConstant.STATUS_END) {
                string[] beginContent = new string[] {"好多了。。我说道那里了？ ",
                    "哎。。这记忆力不行了啊，，，",
                    "你是谁？。。赶紧离我远点。。我有传染病！"};
                duiHuaUImanager.showDuiHua(this.getLiHuiURL(), beginContent, 0);
            }
            else if (this.task.getTaskStatus() == TaskConstant.STATUS_INDALID)
            {
                string[] beginContent = new string[] {"安静！。。安静！。。老人家要多休息。"};
                duiHuaUImanager.showDuiHua(this.getLiHuiURL(), beginContent, 0);
            }


        }
    }



    private void initTask() {
        TaskAward award = new TaskAward();
        award.setAttriAwards(1, 1, 1, 1);
        string taskDesc = "我有些咳嗽，能找些药给我吗？";
        string taskName = "我是雷锋";
        List<TaskItemInterface> taskItems = new List<TaskItemInterface>();
        FindSomethingTaskItem cti = new FindSomethingTaskItem("找到一瓶蓝色的药水。");
        cti.setTargetItem(ItemConstant.ITEM_CODE_POTION_10001);
        FindSomethingTaskItem cti2 = new FindSomethingTaskItem("找到一瓶红色的药水。");
        cti.setTargetItem(ItemConstant.ITEM_CODE_POTION_00001);
        taskItems.Add(cti);
        taskItems.Add(cti2);
        this.task = new FindSometingTask(TaskConstant.TASK_CODE_01, TaskConstant.TASK_TYPE_01, award,null, taskDesc, npcCode,null, taskItems,
            taskName,TaskConstant.STATUS_INIT);
    }

	// Use this for initialization
	void Start () {
        confirmManageUI = FindObjectOfType<ConfirmManageUI>();
        duiHuaUImanager = FindObjectOfType<DuiHuaUImanager>();
        roundController = FindObjectOfType<RoundController>();
        taskMananger = FindObjectOfType<TaskMananger>();
        initTask();

        if (!roundController.newOrLoad) {

            SaveData data = (SaveData)IOHelper.GetData(Application.persistentDataPath + "/Save/SaveData0.sav", typeof(SaveData));
            Debug.Log("load oldMan task status.." + data.HistoryTasks.Count); 
            foreach (TaskInfo ti in data.HistoryTasks)
            {
                Debug.Log("load oldMan task code is .." + ti.TaskCode);
                if (ti.TaskCode == this.task.getTaskCode()) {
                    this.task.setTaskStatus(ti.TaskStatus);
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (this.showConfirm == true && this.duiHuaUImanager.isDuiHuaEnd()) {
            this.confirmManageUI.showConfirm(this.getLiHuiURL(), "年轻人，你愿意帮我找到补肺丸吗？ 红瓶的那种！？");
            this.status = 1;
            this.showConfirm = false;
        }

        if (this.status == 1 && !this.confirmManageUI.isActiveAndEnabled) {
            if (this.confirmManageUI.getResult())
            {
                // do action
                Debug.Log("regisiter task is " + this.task.getTaskName());
                if (taskMananger.regisisterTask(this.task)) {
                    this.task.setTaskStatus(TaskConstant.STATUS_BEGIN);
                    this.task.setTaskOwner(roundController.getCurrentRoundChar().getName());
                    this.taskMananger.UpdateHistoryTask(this.task);
                }
                this.status = 0;
            }
        }
	}
}
