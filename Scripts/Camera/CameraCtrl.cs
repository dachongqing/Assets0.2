using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour {

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
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = Vector3.Lerp (transform.position,targetPos,lerpSpeed * Time.smoothDeltaTime);
	}
}
