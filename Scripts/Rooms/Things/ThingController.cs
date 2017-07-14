using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingController : MonoBehaviour {

    private List<ThingInfo> emptyThings = new List<ThingInfo>();

    public void emptyThing(int[] roomXYZ, string thingCode)
    {
        ThingInfo ti = new ThingInfo();
        ti.RoomXyz = roomXYZ;
        ti.ThingCode = thingCode;
        emptyThings.Add(ti);
    }

    public List<ThingInfo> getEmptyThings()
    {
        return this.emptyThings;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
