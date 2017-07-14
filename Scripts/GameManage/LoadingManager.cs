using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        loadRecord();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void loadRecord()
    {
        StartCoroutine(loadScene());
    }

    IEnumerator loadScene()
    {
        int displayProgress = 0;
        int toProgress = 0;
        //AsyncOperation op = Application.LoadLevelAsync(Global.GetInstance().loadName);
        AsyncOperation op = SceneManager.LoadSceneAsync("Load");
        op.allowSceneActivation = false;
        while (op.progress < 0.9f)
        {
            toProgress = (int)op.progress * 100;
            while (displayProgress < toProgress)
            {
                ++displayProgress;
            //    SetLoadingPercentage(displayProgress);
                yield return new WaitForEndOfFrame();
            }
        }
        toProgress = 100;
        while (displayProgress < toProgress)
        {
            ++displayProgress;
         //   SetLoadingPercentage(displayProgress);
            yield return new WaitForEndOfFrame();
        }
        op.allowSceneActivation = true;
    }
  ///  public void SetLoadingPercentage(int DisplayProgress)
  //  {
  //      m_Slider.value = DisplayProgress * 0.01f;
  //      m_Text.text = DisplayProgress.ToString() + "%";
  //  }
}
