﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CameraCtrl : MonoBehaviour {


    //摄像机是从下往上看  z -2 看楼上 1， -3 能看 地面 -2  -5 能看楼下 -4
    //有一个问题 是 从上往下 看 是能把人 正确显示 但是往上看， 把人都全显示了

	[Range(0.5f,5f)]public float lerpSpeed=1.5f;
	[SerializeField]Vector3 targetPos=new Vector3(0f,0f,-10f);
	public float roomH = 13.7f;
	public float roomV=11f;

    public Image loadingUpPage;

	// Use this for initialization
	void Start () {
		//Debug.Log ("CamaeraCtrl.cs Start() 相机进入默认位置");
	}

	public void setTargetPos(int[] pos)
	{
       
       
        if (pos[2] == RoomConstant.ROOM_Z_GROUND) {
            setTargetPos(pos, RoomConstant.ROOM_Y_GROUND,false);
        } else if (pos[2] == RoomConstant.ROOM_Z_UP) {
            setTargetPos(pos, RoomConstant.ROOM_Y_UP, false);
        } else {
            setTargetPos(pos, RoomConstant.ROOM_Y_DOWN, false);
        }
        Debug.Log("setTargetPos pos call ");
    }

    public void setHiddenTargetPos(int[] pos, int x)
    {
        
        loadingUpPage.GetComponent<Image>().enabled = true;
        StartCoroutine(EndLoadingPageUI(3.5f));
        
        targetPos.x = x + pos[0] * roomH;
        targetPos.y = pos[1] * roomV;
        targetPos.z = pos[2] - 10;
        //切换场景会导致程序初始化，目前不能用
        //SceneManager.LoadScene("Loading");
        Debug.Log("setHiddenTargetPos call ");
    }


    public void setTargetPos(int[] pos,int y, bool showLoading)
    {
        if (showLoading) {
         loadingUpPage.GetComponent<Image>().enabled = true;
         StartCoroutine(EndLoadingPageUI(3.5f));
        }
        targetPos.x = pos[0] * roomH;
        targetPos.y = y + pos[1] * roomV;
        targetPos.z = pos[2] - 10;
        //切换场景会导致程序初始化，目前不能用
        //SceneManager.LoadScene("Loading");
        Debug.Log("setTargetPos showLoading call ");
    }

    IEnumerator EndLoadingPageUI(float ti)
    {
        yield return new WaitForSeconds(ti);
        //销毁骰子
         loadingUpPage.GetComponent<Image>().enabled = false;
        Debug.Log("EndLoadingPageUI call ");
    }

    // Update is called once per frame
    void LateUpdate () {
       // Debug.Log("LateUpdate call ");
		transform.position = Vector3.Lerp (transform.position,targetPos,lerpSpeed * Time.smoothDeltaTime);
       
    }
}
