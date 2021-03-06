﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapUI : MonoBehaviour {

    public Vector3 showPos = new Vector3(4, 0, 0);
    public Vector3 hideUpPos = new Vector3(2891, 1455, 0);
    public Vector3 hideGroPos = new Vector3(2891, 0, 0);
    public Vector3 hideDowPos = new Vector3(2891, -1455, 0);


    public GameObject MiniMapUpPlane;
     public GameObject MiniMapGroPlane;
     public GameObject MiniMapDowPlane;
    private MouseMoveManger mouseMoveManger;




    public void closeMinMap() {
       // MiniMapUpPlane.SetActive(false);
       // MiniMapGroPlane.SetActive(false);
      //  MiniMapDowPlane.SetActive(false);
        MiniMapUpPlane.transform.localPosition = hideUpPos;
        MiniMapGroPlane.transform.localPosition = hideGroPos;
        MiniMapDowPlane.transform.localPosition = hideDowPos;
        mouseMoveManger.updateLock(false);
    }


    public void clickUpMap() {
        mouseMoveManger.updateLock(true);
        // MiniMapUpPlane.SetActive(true);
        MiniMapUpPlane.transform.localPosition = showPos;

      //  MiniMapGroPlane.SetActive(false);
        MiniMapGroPlane.transform.localPosition = hideGroPos;

      //  MiniMapDowPlane.SetActive(false);
        MiniMapDowPlane.transform.localPosition = hideDowPos;

    }

    public void clickGroundMap()
    {
        mouseMoveManger.updateLock(true);
        //  MiniMapUpPlane.SetActive(false);
        MiniMapUpPlane.transform.localPosition = hideUpPos;

     //   MiniMapGroPlane.SetActive(true);
        MiniMapGroPlane.transform.localPosition = showPos;

      //  MiniMapDowPlane.SetActive(false);
        MiniMapDowPlane.transform.localPosition = hideDowPos;
    }

    public void clickDownMap()
    {
        mouseMoveManger.updateLock(true);
        //  MiniMapUpPlane.SetActive(false);
        MiniMapUpPlane.transform.localPosition = hideUpPos;

        //MiniMapGroPlane.SetActive(false);
        MiniMapGroPlane.transform.localPosition = hideGroPos;

        //MiniMapDowPlane.SetActive(true);
        MiniMapDowPlane.transform.localPosition = showPos;
    }

    // Use this for initialization
    void Start () {
        mouseMoveManger = FindObjectOfType<MouseMoveManger>();


    }
	
	// Update is called once per frame
	void FixedUpdate () {
      

    }
}
