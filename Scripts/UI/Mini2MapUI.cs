using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Mini2MapUI : MonoBehaviour, IPointerClickHandler
{

    private RoundController roundController;

    private MiniMapUI miniMapUI;
    [Tooltip("小地图面板")] public GameObject MinMapPlane;

    private RoomContraller roomContraller;

    //小地图房间起始点
    private Vector3 showMPos = new Vector3(-24, -4, 0);
    private Vector3 showUPos = new Vector3(-24, 27, 0);
    private Vector3 showDPos = new Vector3(-24, -36, 0);
    private Vector3 showLPos = new Vector3(-56, -4, 0);
    private Vector3 showRPos = new Vector3(7, -4, 0);
    [Tooltip("小地图房间")] public GameObject minRoom;


    public void OnPointerClick(PointerEventData eventData)
    {
       Character chara =  roundController.getPlayerChara();
        if (chara.getCurrentRoom()[2] == RoomConstant.ROOM_Z_UP) {
            miniMapUI.clickUpMap();
        } else if (chara.getCurrentRoom()[2] == RoomConstant.ROOM_Z_GROUND) {
            miniMapUI.clickGroundMap();
        } else if (chara.getCurrentRoom()[2] == RoomConstant.ROOM_Z_DOWN) {
            miniMapUI.clickDownMap();
        }
    }

    // Use this for initialization
    private GameObject newDi1;
    private GameObject newDi2;
    private GameObject newDi3;
    private GameObject newDi4;
    private GameObject newDi5;

    void Start () {
      
        roundController = FindObjectOfType<RoundController>();
        roomContraller = FindObjectOfType<RoomContraller>();
        miniMapUI = FindObjectOfType<MiniMapUI>();

        newDi1 = Instantiate(minRoom) as GameObject;
        newDi1.GetComponent<RectTransform>().SetParent(MinMapPlane.transform);
        newDi1.GetComponent<RectTransform>().localPosition = showMPos;

        newDi2 = Instantiate(minRoom) as GameObject;
        newDi2.GetComponent<RectTransform>().SetParent(MinMapPlane.transform);
        // newDi2.GetComponent<RectTransform>().localPosition = show2Pos;
        newDi2.SetActive(false);
         newDi3 = Instantiate(minRoom) as GameObject;
        newDi3.GetComponent<RectTransform>().SetParent(MinMapPlane.transform);
        //newDi3.GetComponent<RectTransform>().localPosition = show1Pos;
        newDi3.SetActive(false);
        newDi4 = Instantiate(minRoom) as GameObject;
        newDi4.GetComponent<RectTransform>().SetParent(MinMapPlane.transform);
        // newDi4.GetComponent<RectTransform>().localPosition = show1Pos;
        newDi4.SetActive(false);
        newDi5 = Instantiate(minRoom) as GameObject;
        newDi5.GetComponent<RectTransform>().SetParent(MinMapPlane.transform);
        newDi5.SetActive(false);
        // newDi5.GetComponent<RectTransform>().localPosition = show1Pos;
        east = new int[] {0,0,0 };
        west = new int[] { 0, 0, 0 };
        north = new int[] { 0, 0, 0 };
        south = new int[] { 0, 0, 0 };
    }

    private int[] east;

    private int[] west;
    private int[] north ;
    private int[] south;
    private void checkDoorStatus(MinMapRoom mRoom, MinMapRoom mmr) {
      
        if (mRoom.getEastDoorStatus())
        {
            mmr.setEastDoorenable();            
            newDi5.GetComponent<RectTransform>().localPosition = showRPos;
           
           MinMapRoom eRoom = newDi5.GetComponent<MinMapRoom>();
           east[0] = mRoom.xyz[0];
            east[1] = mRoom.xyz[1];
            east[2] = mRoom.xyz[2];
            east[0] = east[0] + 1;
         
           MinMapRoom eNextRoom = roomContraller.findMiniRoomByXYZ(east);
           this.checkPStatus(eNextRoom, eRoom);
            if (eNextRoom.isVisited())
            {

                newDi5.SetActive(true);
            } else
            {
                newDi5.SetActive(false);
            }

        }
       else {
            
           mmr.setEastDoorDisable();
           newDi5.SetActive(false);
       }
       if (mRoom.getWestDoorStatus())
       {
           mmr.setWestDoorenable();
           
           
            newDi4.GetComponent<RectTransform>().localPosition = showLPos;
           
           MinMapRoom wRoom = newDi4.GetComponent<MinMapRoom>();
            west[0] = mRoom.xyz[0];
            west[1] = mRoom.xyz[1];
            west[2] = mRoom.xyz[2];
           
            west[0] = west[0] - 1;
           
           MinMapRoom wNextRoom = roomContraller.findMiniRoomByXYZ(west);
           this.checkPStatus(wNextRoom, wRoom);
            if (wNextRoom.isVisited())
            {
                newDi4.SetActive(true);
            }
            else
            {
                newDi4.SetActive(false);
            }
        }
        else
        {
            mmr.setWestDoorDisable();
            newDi4.SetActive(false);
        }
        if (mRoom.getSouthDoorStatus())
        {
            mmr.setSouthDoorenable();
           
            

            newDi3.GetComponent<RectTransform>().localPosition = showDPos;
            
           MinMapRoom sRoom = newDi3.GetComponent<MinMapRoom>();
            south[0] = mRoom.xyz[0];
            south[1] = mRoom.xyz[1];
            south[2] = mRoom.xyz[2];

            south[1] = mRoom.xyz[1]-1;
         
           MinMapRoom sNextRoom = roomContraller.findMiniRoomByXYZ(south);
           this.checkPStatus(sNextRoom, sRoom);
            if (sNextRoom.isVisited())
            {
                newDi3.SetActive(true);
            }
            else
            {
                newDi3.SetActive(false);
            }
        }
        else
        {
            mmr.setSouthDoorDisable();
            newDi3.SetActive(false);
        }
        if (mRoom.getNorthDoorStatus())
        {
            mmr.setNorthDoorenable();
           
            
            newDi2.GetComponent<RectTransform>().localPosition = showUPos;
            
                   MinMapRoom nRoom = newDi2.GetComponent<MinMapRoom>();
            north[0] = mRoom.xyz[0];
            north[1] = mRoom.xyz[1];
            north[2] = mRoom.xyz[2];

         
            north[1] = mRoom.xyz[1] + 1;
                  
                   MinMapRoom nNextRoom = roomContraller.findMiniRoomByXYZ(north);
                   this.checkPStatus(nNextRoom, nRoom);
            if (nNextRoom.isVisited())
            {
                newDi2.SetActive(true);
            }
            else
            {
                newDi2.SetActive(false);
            }
        }
        else
        {
            mmr.setNorthDoorDisable();
            newDi2.SetActive(false);
        }
        
    }

    private void checkPStatus(MinMapRoom mRoom, MinMapRoom mmr) {
        if (mRoom.getP1Status())
        {
            mmr.setP1enable(true);
        }
        else
        {
            mmr.setP1enable(false);
        }

        if (mRoom.getP2Status())
        {
            mmr.setP2enable(true);
        }
        else
        {
            mmr.setP2enable(false);
        }

        if (mRoom.getP3Status())
        {
            mmr.setP3enable(true);
        }
        else
        {
            mmr.setP3enable(false);
        }


        if (mRoom.getP4Status())
        {
            mmr.setP4enable(true);
        }
        else
        {
            mmr.setP4enable(false);
        }


        if (mRoom.getP5Status())
        {
            mmr.setP5enable(true);
        }
        else
        {
            mmr.setP5enable(false);
        }
    }

	// Update is called once per frame
	void FixedUpdate () {

        Character chara = roundController.getPlayerChara();
        
        MinMapRoom mRoom = roomContraller.findMiniRoomByXYZ(chara.getCurrentRoom());     ;
        MinMapRoom mmr = newDi1.GetComponent<MinMapRoom>();
        mmr.setP6enable(true);
        checkDoorStatus(mRoom, mmr);
        checkPStatus(mRoom, mmr);

        if (Input.GetKey(KeyCode.M))
        {
          
            if (chara.getCurrentRoom()[2] == RoomConstant.ROOM_Z_UP)
            {
                miniMapUI.clickUpMap();
            }
            else if (chara.getCurrentRoom()[2] == RoomConstant.ROOM_Z_GROUND)
            {
                miniMapUI.clickGroundMap();
            }
            else if (chara.getCurrentRoom()[2] == RoomConstant.ROOM_Z_DOWN)
            {
                miniMapUI.clickDownMap();
            }
        }
    }
}
