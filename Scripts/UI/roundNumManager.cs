using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class roundNumManager : MonoBehaviour {

    public Text roundCount;

    private RoundController roundController;

    // Use this for initialization
    void Start () {

        roundController = FindObjectOfType<RoundController>();
    }
	
	// Update is called once per frame
	void Update () {
        roundCount.text = "第" +roundController.getRoundCount() + "回合";

    }
}
