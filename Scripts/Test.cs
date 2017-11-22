using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    public RenderTexture t;

    public int x;

    public int y;

    public int z;

    public int k;

    // Use this for initialization
    void Start () {
        //x = 0;
       // y = 0;
       // z = 100;
       // k = 100;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(x, y, z, k), t);
    }
}
