using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextReader : MonoBehaviour {

   

    public static string[] readerText(string path) {
        TextAsset textFile = Resources.Load(path) as TextAsset;
        return textFile.text.Split('\n');
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
