using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskConstant  {

    public const string TASK_STORY_CODE_01 = "storyTask001"; //剧本任务，黑色征兆

    public const string TASK_CODE_01 = "t001"; //测试任务，找一瓶药水

    public const string TASK_CODE_02 = "t002"; //测试任务，找一个打火机

    public const string TASK_TYPE_01 = "find"; //寻找物品任务

    public const string TASK_TYPE_02 = "story"; //剧本主线任务

    public const string TASK_NPC_CODE_STORY = "system"; //npcCode

    public const string STATUS_INDALID = "-1"; //任务不能再获取
    public const string STATUS_INIT = "0"; //任务未开始
    public const string STATUS_BEGIN = "1"; //任务开始
    public const string STATUS_END = "2"; //任务结束


    public const string TASK_ITEM_CODE_SAVEORKILL = "saveOrKill"; //唤醒或者杀死条件

    public const string TASK_ITEM_CODE_KILL = "kill"; //杀死条件

    public const string TASK_ITEM_CODE_FIND = "find"; //寻找条件
}
