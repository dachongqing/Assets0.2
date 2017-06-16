using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour {


    //摄像机是从下往上看  z -2 看楼上 1， -3 能看 地面 -2  -5 能看楼下 -4
    //有一个问题 是 从上往下 看 是能把人 正确显示 但是往上看， 把人都全显示了

	[Range(0.5f,5f)]public float lerpSpeed=1.5f;
	[SerializeField]Vector3 targetPos=new Vector3(0f,0f,-10f);
	public float roomH = 13.7f;
	public float roomV=11f;

	// Use this for initialization
	void Start () {
		Debug.Log ("CamaeraCtrl.cs Start() 相机进入默认位置");
	}

	public void setTargetPos(int[] pos)
	{
		targetPos.x = pos [0]*roomH;
		targetPos.y = pos [1]*roomV;
		targetPos.z = pos [2]-10;

	}

    public void setTargetPos(int[] pos,int y)
    {
        targetPos.x = pos[0] * roomH;
        targetPos.y = y + pos[1] * roomV;
        targetPos.z = pos[2] - 10;

    }

    // Update is called once per frame
    void LateUpdate () {
		transform.position = Vector3.Lerp (transform.position,targetPos,lerpSpeed * Time.smoothDeltaTime);
	}
}
