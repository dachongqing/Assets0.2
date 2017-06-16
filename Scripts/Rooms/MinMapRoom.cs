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

       
    
    }
	
	// Update is called once per frame
	void FixedUpdate () {

       
    }

    public bool getP1Status()
    {
        return p1.GetComponent<Image>().IsActive();
    }

    public bool getP2Status()
    {
        return p2.GetComponent<Image>().IsActive();
    }

    public bool getP3Status()
    {
        return p3.GetComponent<Image>().IsActive();
    }

    public bool getP4Status()
    {
        return p4.GetComponent<Image>().IsActive();
    }

    public bool getP5Status()
    {
        return p5.GetComponent<Image>().IsActive();
    }

    public bool getP6Status()
    {
        return p6.GetComponent<Image>().IsActive();
    }

    public bool getNorthDoorStatus()
    {
        // northDoor.GetComponent<SpriteRenderer>().enabled = true;
        return northDoor.GetComponent<Image>().IsActive();
    }

    public bool getSouthDoorStatus()
    {
        // northDoor.GetComponent<SpriteRenderer>().enabled = true;
        return southDoor.GetComponent<Image>().IsActive();
    }

    public bool getWestDoorStatus()
    {
        // northDoor.GetComponent<SpriteRenderer>().enabled = true;
        return westDoor.GetComponent<Image>().IsActive();
    }

    public bool getEastDoorStatus()
    {
        // northDoor.GetComponent<SpriteRenderer>().enabled = true;
        return eastDoor.GetComponent<Image>().IsActive();
    }


    public void setNorthDoorDisable()
    {
        // northDoor.GetComponent<SpriteRenderer>().enabled = true;
        northDoor.GetComponent<Image>().enabled = false;
    }

    public void setSouthDoorDisable()
    {
        southDoor.GetComponent<Image>().enabled = false;
    }

    public void setWestDoorDisable()
    {
        westDoor.GetComponent<Image>().enabled = false;
    }

    public void setEastDoorDisable()
    {
        eastDoor.GetComponent<Image>().enabled = false;
    }

    public void setP1enable( bool showFlag)
    {
            p1.GetComponent<Image>().enabled = showFlag;
    }

    public void setP2enable(bool showFlag)
    {
        p2.GetComponent<Image>().enabled = showFlag;
    }

    public void setP3enable(bool showFlag)
    {
        p3.GetComponent<Image>().enabled = showFlag;
    }

    public void setP4enable(bool showFlag)
    {
        p4.GetComponent<Image>().enabled = showFlag;
    }

    public void setP5enable(bool showFlag)
    {
        p5.GetComponent<Image>().enabled = showFlag;
    }

    public void setP6enable(bool showFlag)
    {
        p6.GetComponent<Image>().enabled = showFlag;
    }

}
