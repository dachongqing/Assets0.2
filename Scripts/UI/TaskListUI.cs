using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskListUI : MonoBehaviour {


    private Vector3 showPos = new Vector3(78, -90, 0);
    private Vector3 hidePos = new Vector3(-10, 0, 0);

    public GameObject TaskUI;

    public Transform TaskNamePosition;

    public Transform TaskItemPosition;

    public GameObject TaskNameButtonPrefab;

    public GameObject TaskItemPrefab;

    private TaskMananger taskMananger;

    private List<GameObject> buttionList = new List<GameObject>();

    private List<GameObject> taskItemList;

    private Dictionary<string , List<GameObject>> mapItems = new Dictionary<string , List<GameObject>>();

    private Text taskDesc;

    public void showTaskUI() {
        TaskUI.SetActive(true);
        TaskUI.transform.localPosition = showPos;
        this.initTaskInfo();
        genTaskNameButton();
    }

    public void closeTaskUI() {
        TaskUI.SetActive(false);
        TaskUI.transform.localPosition = hidePos;
    }

    private void initTaskInfo()
    {
       // this.TaskUI.
        foreach (GameObject gb in buttionList) {
            //gb.SetActive(false);
            Destroy(gb);
        }
        if (this.taskDesc != null) {
            this.taskDesc.text = "你还没有任务。";  
        }
        foreach (string key in mapItems.Keys)
        {
            foreach (GameObject gb in mapItems[key]) {
              Destroy(gb);
            }
  
        }
        buttionList.Clear();       
        mapItems.Clear();
    }

    public void clickTaskNameButton(string taskCode) {
       // Debug.Log(taskCode);

        loadTaskInfo(this.taskMananger.getTaskByTaskCode(taskCode));
    }


    private void genTaskNameButton() {

       // Debug.Log("task numis " + taskMananger.getRegisitedTasks().Count);

        if(taskMananger.getStoryTask() != null) {
          
            GameObject newDi = Instantiate(TaskNameButtonPrefab) as GameObject;
            newDi.GetComponent<RectTransform>().SetParent(TaskUI.transform);
            newDi.GetComponent<RectTransform>().localPosition = new Vector3(-100, 330, 0);
            Text text = newDi.GetComponentInChildren<Text>();
            // Debug.Log("i is " + text.text);
            text.text = taskMananger.getStoryTask().getTaskName();
            buttionList.Add(newDi);

            Button button = newDi.GetComponent<Button>();
            button.onClick.AddListener(
            delegate () {               
                loadTaskInfo(this.taskMananger.getStoryTask());
            });
        }

        Vector3 temPos = new Vector3(-100, 330, 0);
        foreach (TaskInterface i in  taskMananger.getRegisitedTasks()) {               
            temPos.y -= 100;
            GameObject newDi = Instantiate(TaskNameButtonPrefab) as GameObject;
            newDi.GetComponent<RectTransform>().SetParent(TaskUI.transform);
            newDi.GetComponent<RectTransform>().localPosition = temPos;
            Text text = newDi.GetComponentInChildren<Text>();
           // Debug.Log("i is " + text.text);
            text.text = i.getTaskName();
            buttionList.Add(newDi);

            Button button = newDi.GetComponent<Button>();
            button.onClick.AddListener(
            delegate () {
                this.clickTaskNameButton(i.getTaskCode());
            }
            );

        }
        if (taskMananger.getRegisitedTasks().Count > 0 ) {
            loadTaskInfo(taskMananger.getRegisitedTasks()[0]);
        }

    }

    private void loadTaskInfo(TaskInterface task) {

        this.taskDesc = TaskUI.GetComponentsInChildren<Text>()[0];
        taskDesc.text = task.getTaskDes();

        foreach (string key in mapItems.Keys)
        {
            foreach (GameObject gb in mapItems[key])
            {
                gb.SetActive(false);
            }

        }

        if (mapItems.ContainsKey(task.getTaskCode()) && mapItems[task.getTaskCode()].Count > 0 ) {

           
            foreach (GameObject newDi in mapItems[task.getTaskCode()]) {
                newDi.SetActive(true);
            }
        } else {
                     
            Vector3 temPos = new Vector3(30f, 0, 0);
            taskItemList = new List<GameObject>();
            foreach (TaskItemInterface item in task.getTaskItems())
            {
                temPos.y -= 100;
                GameObject newDi = Instantiate(TaskItemPrefab) as GameObject;
                newDi.GetComponent<RectTransform>().SetParent(TaskUI.transform);
                newDi.GetComponent<RectTransform>().localPosition = temPos;
                Text text = newDi.GetComponent<Text>();
                if (item.isCompleted())
                {
                    text.text = item.getItemDesc() + "(完成)";
                }
                else {
                    text.text = item.getItemDesc() + "(未完成)";
                }
                taskItemList.Add(newDi);


            }
                mapItems.Add(task.getTaskCode(), taskItemList);
        }

        
    }

	// Use this for initialization
	void Start () {
        taskMananger = FindObjectOfType<TaskMananger>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
