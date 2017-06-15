using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinMapRoom : MonoBehaviour {

    public GameObject northDoor;
    public GameObject southDoor;
    public GameObject westDoor;
    public GameObject eastDoor;

    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject p4;
    public GameObject p5;
    public GameObject p6;

    public int[] xyz;

    public void setXYZ(int[] xyz) {
        this.xyz = xyz;
    }

   // private RoomContraller roomContraller;



    public void setNorthDoorenable() {
        // northDoor.GetComponent<SpriteRenderer>().enabled = true;
        northDoor.GetComponent<Image>().enabled = true;
    }

    public void setSouthDoorenable()
    {
        southDoor.GetComponent<Image>().enabled = true;
    }

    public void setWestDoorenable()
    {
        westDoor.GetComponent<Image>().enabled = true;
    }

    public void setEastDoorenable()
    {
        eastDoor.GetComponent<Image>().enabled = true;
    }

    public void setPenable(string name,bool showFlag)
    {
        Debug.Log("name is " + name  + ", showFlag " + showFlag);
        //   p1.GetComponent<SpriteRenderer>().enabled = true;
        if (name == SystemConstant.P1_NAME) { 
             p1.GetComponent<Image>().enabled = showFlag;

        } else if (name == SystemConstant.P2_NAME) {
            p2.GetComponent<Image>().enabled = showFlag;
        }
        else if (name == SystemConstant.P3_NAME)
        {
            p3.GetComponent<Image>().enabled = showFlag;
        }
        else if (name == SystemConstant.P4_NAME)
        {
            p4.GetComponent<Image>().enabled = showFlag;
        }
        else if (name == SystemConstant.P5_NAME)
        {
            p5.GetComponent<Image>().enabled = showFlag;
        }
        else if (name == SystemConstant.P6_NAME)
        {
            p6.GetComponent<Image>().enabled = showFlag;
        }
       
    }

   

    //List<string> ps = new List<string>();

    // Use this for initialization
    void Start () {

        //Debug.Log("mini init");
        // northDoor.GetComponent<SpriteRenderer>().enabled = true;
        //  southDoor.GetComponent<SpriteRenderer>().enabled = true;
        //  p1.GetComponent<SpriteRenderer>().enabled = true;
        //p2.GetComponent<SpriteRenderer>().gameObject.SetActive(false);
       // roomContraller = FindObjectOfType<RoomContraller>();
    
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        /**
         * 
        RoomInterface ri = roomContraller.findRoomByXYZ(this.xyz);
        List<Character> list = ri.getCharas();
        if (list != null && list.Count > 0) {
            foreach (string c in ps) {
                if (ri.getCharas()) {
                    this.setP1enable(true);
                } else if (c.getName() == SystemConstant.P2_NAME) {
                    this.setP2enable(true);
                }
                else if (c.getName() == SystemConstant.P6_NAME)
                {
                    this.setP6enable(true);
                }
            }
        } else if () {

        }
    **/
    }
}
