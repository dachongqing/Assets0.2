using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface TaskInterface {

    string getTaskId();

    string getTaskCode();

    string getTaskName();

    string getTaskType();

    string getTaskDistributor();

    string getTaskOwner();

    List<TaskItemInterface> getTaskItems();

    TaskAwardInterface getTaskAwards();

    string getTaskDes();

    List<TaskConditionSPInterface> getTaskConditionSP();

    string getTaskStatus();

    void setTaskDistributor(string taskDistributor);

    void setTaskItem(List<TaskItemInterface> taskItem);

    void setTaskAward(TaskAwardInterface taskAward);

    void setTaskDes(string des);

    void setTaskStatus(string status);

    void setTaskOwner(string owner);



}
